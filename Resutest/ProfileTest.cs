using System.Transactions;
using Resutest.Helpers;

namespace Resutest
{
    public class ProfileTest : Helpers.BaseTest
    {
        [Test]
        public async Task AddTest()
        {
            using (TransactionScope scope = Helper.CreateTransactionScope())
            {
                await profile.AddOrUpdate(
                    new Resunet.DAL.Models.ProfileModel()
                    {
                        UserId = 10,
                        FirstName = "Иван",
                        LastName = "Иванов",
                        ProfileName = "Тест"
                    });

                var results = await profile.Get(19);
                Assert.That(results.Count(), Is.EqualTo(1));

                var result = results.First();
                Assert.That(result.FirstName, Is.EqualTo("Иван"));
                Assert.That(result.LastName, Is.EqualTo("Иванов"));
                Assert.That(result.ProfileName, Is.EqualTo("Тест"));
                Assert.That(result.UserId, Is.EqualTo(19));
            }
        }

        [Test]
        public async Task UpdateTest()
        {
            using (TransactionScope scope = Helper.CreateTransactionScope())
            {
                var profileModel = new Resunet.DAL.Models.ProfileModel()
                {
                    UserId = 19,
                    FirstName = "Иван",
                    LastName = "Иванов",
                    ProfileName = "Тест"
                };

                await profile.AddOrUpdate(profileModel);
                
                profileModel.FirstName = "Иван1";

                await profile.AddOrUpdate(profileModel);

                var results = await profile.Get(19);
                Assert.That(results.Count(), Is.EqualTo(1));

                var result = results.First();
                Assert.That(result.FirstName, Is.EqualTo("Иван1"));
                Assert.That(result.UserId, Is.EqualTo(19));
            }
        }
    }
}