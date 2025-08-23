using ResunetBl.General;
using ResunetDAL.Models;
using ResunetDAL.Interfaces;
using ResunetBl.Exeption;

namespace ResunetBl.Auth;

public class Auth(
    ResunetDAL.Interfaces.IAuth auth,
    IEncrypt _encrypt,
    IWebCookie _webCookie,
    IDbSession _dbSession,
    IUserToken userToken) : IAuth
{
    // IDbSession dbSession - получает BL уровня сессию, а Auth должен работь пожизненно.
    // Если один из параметров должен умирать каждый запрос, то и IAuthDAL должен умирать

    public async Task Login(int id)
        => await _dbSession.SetUserId(id);

    public async Task<int> CreateUser(UserModel user)
    {
        user.Salt = Guid.NewGuid().ToString();
        user.Password = _encrypt.HashPassword(user.Password, user.Salt);

        int id = await auth.CreateUser(user);
        await Login(id);
        return id;
    }

    public async Task<int> Authenticate(string email, string password, bool rememberMe)
    {
        var user = await auth.GetUser(email);

        if (user.UserId is not null && user.Password == _encrypt.HashPassword(password, user.Salt))
        {
            await Login(user.UserId ?? 0);

            if (rememberMe)
            {
                Guid tokenId = await userToken.Create(user.UserId ?? 0);
                _webCookie.AddSecure(
                    AuthConstants.RememberMeCookieName,
                    tokenId.ToString(),
                    AuthConstants.RememberMeDays);
            }

            return user.UserId ?? 0;
        }

        throw new AuthorizationException();
    }

    public async Task ValidateEmail(string email)
    {
        var user = await auth.GetUser(email);
        if (user.UserId is not null)
            throw new DublicateEmailExeption();
    }

    public async Task Register(UserModel user)
    {
        // защита от спама, при этом пользователь может зайти с разных устройств
        using (var scope = Helpers.CreateTransactionScope())
        {
            await _dbSession.Lock();
            await ValidateEmail(user.Email);
            await CreateUser(user);
            scope.Complete();
        }
    }
}