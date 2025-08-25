using System.Text.Json;
using ResunetBl.General;
using ResunetDAL.Models;

namespace ResunetBl.Auth;

public class DbSession(
    ResunetDAL.Interfaces.IDbSessionDAL session,
    IWebCookie _webCookie) : IDbSession
{
    private SessionModel? sessionModel;
    private Dictionary<string, object> SessionData = new();

    private void CreateSessionCookie(Guid sessionid)
    {
        _webCookie.Delete(AuthConstants.SessionCookieName);
        _webCookie.AddSecure(AuthConstants.SessionCookieName, sessionid.ToString());
    }

    private async Task<SessionModel> CreateSession()
    {
        var data = new SessionModel()
        {
            DbSessionId = Guid.NewGuid(),
            Created = DateTime.Now,
            LastAccessed = DateTime.Now
        };

        await session.Create(data);
        return data;
    }

    public async Task<SessionModel> GetSession()
    {
        if (sessionModel is not null)
            return sessionModel;

        Guid sessionId;
        var sessionString = _webCookie.Get(AuthConstants.SessionCookieName);

        if (sessionString is not null)
            sessionId = Guid.Parse(sessionString);
        else
            sessionId = Guid.NewGuid();

        var data = await session.Get(sessionId);
        if (data is null)
        {
            data = await CreateSession();
            CreateSessionCookie(data.DbSessionId);
        }

        sessionModel = data;
        if (data.SessionData is not null)
            SessionData =
                JsonSerializer.Deserialize<Dictionary<string, object>>(data.SessionData)
                ?? new Dictionary<string, object>();

        await session.Extend(data.DbSessionId);
        return data;
    }

    public async Task UpdateSessionData()
    {
        if (sessionModel is not null)
            await session.Update(sessionModel.DbSessionId, JsonSerializer.Serialize(SessionData));
        else
            throw new Exception("Сессия не загружена");
    }

    public void AddValue(string key, object value)
    {
        if (SessionData.ContainsKey(key))
            SessionData[key] = value;
        else
            SessionData.Add(key, value);
    }

    public void RemoveValue(string key)
    {
        if (SessionData.ContainsKey(key))
            SessionData.Remove(key);
    }

    public async Task SetUserId(int userId)
    {
        var data = await GetSession();
        data.UserId = userId;
        data.DbSessionId = Guid.NewGuid();
        CreateSessionCookie(data.DbSessionId);
        data.SessionData = JsonSerializer.Serialize(SessionData);
        await session.Create(data);
    }

    public async Task<int?> GetUserId()
    {
        var data = await GetSession();
        return data.UserId;
    }

    public async Task<bool> IsLoggedIn()
    {
        var data = await GetSession();
        return data.UserId is not null;
    }

    public async Task Lock()
    {
        var data = await GetSession();
        await session.Lock(data.DbSessionId);
    }

    // хелпер для тестов, проверка в бд
    public void ResetSessionCache()
    {
        sessionModel = null;
    }

    public object GetValueDef(string key, object defaultValue)
    {
        if (SessionData.ContainsKey(key))
            return SessionData[key];
        return defaultValue;
    }
}