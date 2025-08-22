using System.Transactions;
using ResunetBl.Exeption;
using Resutest.Helpers;

namespace Resutest
{
    public class RegisterTests : BaseTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        // асинхронные тесты должны что-то возвращать, иначе не будет дожидаться
        public async Task BaseRegistrationTest()
        {
            using (TransactionScope scope = Helper.CreateTransactionScope())
            {
                // Guid - для генерации новых пользователей
                string email = Guid.NewGuid().ToString() + "@test.com";

                // validate : should not be in the DB
                authBL.ValidateEmail(email).GetAwaiter().GetResult();

                // create user
                int userId = await authBL.CreateUser(
                    new ResunetDal.Models.UserModel()
                    {
                        Email = email,
                        Password = "qwer1234"
                    });

                Assert.Greater(userId, 0);

                var userByEmailDalResult = await authDAL.GetUser(email);
                Assert.That(email, Is.EqualTo(userByEmailDalResult.Email));

                var userDalResult = await authDAL.GetUser(userId);
                Assert.That(email, Is.EqualTo(userDalResult.Email));

                Assert.IsNotNull(userDalResult.Salt);

                // validate : should be in the DB
                Assert.Throws<DublicateEmailExeption>(delegate { authBL.ValidateEmail(email).GetAwaiter().GetResult(); });

                string endPassword = encrypt.HashPassword("qwer1234", userByEmailDalResult.Salt);
                Assert.That(endPassword, Is.EqualTo(userByEmailDalResult.Password));
            }
        }
    }
}