

namespace VacancyScheduler.FunctionalTests
{
    public class RedisVacancySchedulerRepositoryTests
        : VacancySchedulerScenarioBase
    {

        [Fact]
        public async Task UpdateVacancyScheduler_return_and_add_vacancy()
        {
            var server = CreateServer();
            var redis = server.Services.GetRequiredService<ConnectionMultiplexer>();

            //var redisVacancySchedulerRepository = BuildVacancySchedulerRepository(redis);

            //var vacancy = await redisVacancySchedulerRepository.UpdateVacancySchedulerAsync(new CustomerVacancyScheduler("customerId")
            //{
            //    BuyerId = "buyerId",
            //    Items = BuildVacancySchedulerItems()
            //});

            //Assert.NotNull(vacancy);
            //Assert.Single(vacancy.Items);
        }

        //RedisVacancySchedulerRepository BuildVacancySchedulerRepository(ConnectionMultiplexer connMux)
        //{
        //    var loggerFactory = new LoggerFactory();
        //    return new RedisVacancySchedulerRepository(loggerFactory.CreateLogger<RedisVacancySchedulerRepository>(), connMux);
        //}

        //List<VacancySchedulerItem> BuildVacancySchedulerItems()
        //{
        //    return new List<VacancySchedulerItem>()
        //    {
        //        new VacancySchedulerItem()
        //        {
        //            Id = "vacancyId",
        //            PictureUrl = "pictureurl",
        //            ProductId = 1,
        //            ProductName = "productName",
        //            Quantity = 1,
        //            UnitPrice = 1
        //        }
        //    };
        //}
    }
}
