using System.Text.Json;
using ResunetBl.General;
using ResunetDAL.Models;
using ResunetDAL.Interfaces;

namespace ResunetBl.Auth
{
    public class DbSession(
        IDbSessionDAL _sessionDAL,
        IWebCookie _webCookie)
        : IDbSession
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

            await _sessionDAL.Create(data);
            return data;
        }

        public async Task<SessionModel> GetSession()
        {
            if (sessionModel != null)
                return sessionModel;

            Guid sessionId;
            var sessionString = _webCookie.Get(AuthConstants.SessionCookieName);
            if (sessionString != null)
                sessionId = Guid.Parse(sessionString);
            else
                sessionId = Guid.NewGuid();

            var data = await _sessionDAL.Get(sessionId);
            if (data == null)
            {
                data = await CreateSession();
                CreateSessionCookie(data.DbSessionId);
            }

            sessionModel = data;
            if (data.SessionData != null)
                SessionData = JsonSerializer.Deserialize<Dictionary<string, object>>(data.SessionData) ??
                              new Dictionary<string, object>();

            await _sessionDAL.Extend(data.DbSessionId);
            return data;
        }

        public async Task UpdateSessionData()
        {
            if (sessionModel != null)
                await _sessionDAL.Update(sessionModel.DbSessionId, JsonSerializer.Serialize(SessionData));
            else throw new Exception("Сессия не загружена");
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
            await _sessionDAL.Create(data);
        }

        public async Task<int?> GetUserId()
        {
            var data = await GetSession();
            return data.UserId;
        }

        public async Task<bool> IsLoggedIn()
        {
            var data = await GetSession();
            return data.UserId != null;
        }

        public async Task Lock()
        {
            var data = await GetSession();
            await _sessionDAL.Lock(data.DbSessionId);
        }

        // хелпер, только для тестов, чтобы пошел и проверил в бд
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
}