﻿using Resunet.DAL.Models;
using Resunet.DAL;
using System.ComponentModel.DataAnnotations;
using Resunet.BL;
using Resunet.BL.General;

namespace Resunet.BL.Auth
{
    // Bl уровень
    public class Auth : IAuth
    {
        private readonly IAuthDAL authDal;
        private readonly IEncrypt encrypt;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IDbSession dbSession;

        public Auth(IAuthDAL authDal,
            IEncrypt encrypt,
            IHttpContextAccessor httpContextAccessor,
            IDbSession dbSession)
        /* IDbSession dbSession - получает BL уровня сессию, а AuthBL должен работь 
         * пожизненно сохранили объект, который должен умиреть после каждого 
         * запроса в объекте, который должен жить вечно - нельзя так :)
         * если один из параметров должен умирать каждый запрос, то и IAuthDAL должен умирать
         */
        {
            this.authDal = authDal;
            this.encrypt = encrypt;
            this.httpContextAccessor = httpContextAccessor;
            this.dbSession = dbSession;
        }

        public async Task<int> CreateUser(UserModel user)
        {
            user.Salt = Guid.NewGuid().ToString();
            user.Password = encrypt.HashPassword(user.Password, user.Salt);

            int id = await authDal.CreateUser(user);
            await Login(id);
            return id;
        }

        public async Task Login(int id)
        {
            await dbSession.SetUserId(id);
        }

        public async Task<int> Authenticate(string email, string password, bool rememberMe)
        {
            var user = await authDal.GetUser(email);

            if (user.UserId != null && user.Password == encrypt.HashPassword(password, user.Salt))
            {
                await Login(user.UserId ?? 0);
                return user.UserId ?? 0;
            }
            throw new AuthorizationException();
        }

        public async Task ValidateEmail(string email)
        {
            var user = await authDal.GetUser(email);
            if (user.UserId != null)
                throw new DublicateEmailExeption();
        }

        public async Task Register(UserModel user)
        {
            // защита от спама
            // при этом пользователь может зайти с телефона / компа
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