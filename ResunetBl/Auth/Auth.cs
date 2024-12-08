using ResunetBl.BL;
using ResunetBl.General;
using ResunetDal.Models;
using ResunetDal.Interfaces;

namespace ResunetBl.Auth
{
    public class Auth : IAuth
    {
        private readonly IAuthDAL authDAL;
        private readonly IEncrypt encrypt;
        private readonly IDbSession dbSession;
        private readonly IUserTokenDAL userTokenDAL;
        private readonly IWebCookie webCookie;

        // IDbSession dbSession - получает BL уровня сессию, а Auth должен работь пожизненно.
        // Сохранили объект, который должен умереть после каждого 
        // запроса в объекте, который должен жить вечно - низя так.
        // Если один из параметров должен умирать каждый запрос, то и IAuthDAL должен умирать
        public Auth(IAuthDAL authDAL,
            IEncrypt encrypt,
            IWebCookie webCookie,
            IDbSession dbSession,
            IUserTokenDAL userTokenDAL
            )
        {
            this.authDAL = authDAL;
            this.encrypt = encrypt;
            this.webCookie = webCookie;
            this.dbSession = dbSession;
            this.userTokenDAL = userTokenDAL;
        }

        public async Task Login(int id)
        {
            await dbSession.SetUserId(id);
        }

        public async Task<int> CreateUser(UserModel user)
        {
            user.Salt = Guid.NewGuid().ToString();
            user.Password = encrypt.HashPassword(user.Password, user.Salt);

            int id = await authDAL.CreateUser(user);
            await Login(id);
            return id;
        }

        public async Task<int> Authenticate(string email, string password, bool rememberMe)
        {
            var user = await authDAL.GetUser(email);

            if (user.UserId != null && user.Password == encrypt.HashPassword(password, user.Salt))
            {
                await Login(user.UserId ?? 0);

                if (rememberMe)
                {
                    // создаем токен и отправляем его в куку
                    Guid tokenId = await userTokenDAL.Create(user.UserId ?? 0);
                    webCookie.AddSecure(AuthConstants.RememberMeCookieName, tokenId.ToString(), AuthConstants.RememberMeDays);
                }

                return user.UserId ?? 0;
            }
            throw new AuthorizationException();
        }

        public async Task ValidateEmail(string email)
        {
            var user = await authDAL.GetUser(email);
            if (user.UserId != null)
                throw new DublicateEmailExeption();
        }

        public async Task Register(UserModel user)
        {
            // защита от спама, при этом пользователь может зайти с разных устройств
            using (var scope = Helpers.CreateTransactionScope())
            {
                await dbSession.Lock();
                await ValidateEmail(user.Email);
                await CreateUser(user);
                scope.Complete();
            }
        }
    }
}