namespace VacancyScheduler.FunctionalTests;

public class VacancySchedulerScenarios : 
    VacancySchedulerScenarioBase
{
    [Fact]
    public async Task Post_vacancy_and_response_ok_status_code()
    {
        using var server = CreateServer();
        var content = new StringContent(BuildVacancyScheduler(), UTF8Encoding.UTF8, "application/json");
        var uri = "/api/v1/vacancy/";
        var response = await server.CreateClient().PostAsync(uri, content);
        response.EnsureSuccessStatusCode();
    }

    [Fact]
    public async Task Get_vacancy_and_response_ok_status_code()
    {
        using var server = CreateServer();
        var response = await server.CreateClient()
            .GetAsync(Get.GetVacancyScheduler(1));
        response.EnsureSuccessStatusCode();
    }

    [Fact]
    public async Task Send_Checkout_vacancy_and_response_ok_status_code()
    {
        using var server = CreateServer();
        var contentVacancyScheduler = new StringContent(BuildVacancyScheduler(), UTF8Encoding.UTF8, "application/json");

        await server.CreateClient()
            .PostAsync(Post.VacancyScheduler, contentVacancyScheduler);

        var contentCheckout = new StringContent(BuildCheckout(), UTF8Encoding.UTF8, "application/json")
        {
             Headers = { { "x-requestid", Guid.NewGuid().ToString() } }
        };

        var response = await server.CreateClient()
            .PostAsync(Post.CheckoutOrder, contentCheckout);

        response.EnsureSuccessStatusCode();
    }

    string BuildVacancyScheduler()
    {
        var order = new CustomerVacancyScheduler(AutoAuthorizeMiddleware.IDENTITY_ID);

        order.Items.Add(new VacancySchedulerItem
        {
            ProductId = 1,
            ProductName = ".NET Bot Black Hoodie",
            UnitPrice = 10,
            Quantity = 1
        });

        return JsonSerializer.Serialize(order);
    }

    string BuildCheckout()
    {
        var checkoutVacancyScheduler = new
        {
            City = "city",
            Street = "street",
            State = "state",
            Country = "coutry",
            ZipCode = "zipcode",
            CardNumber = "1234567890123456",
            CardHolderName = "CardHolderName",
            CardExpiration = DateTime.UtcNow.AddDays(1),
            CardSecurityNumber = "123",
            CardTypeId = 1,
            Buyer = "Buyer",
            RequestId = Guid.NewGuid()
        };

        return JsonSerializer.Serialize(checkoutVacancyScheduler);
    }
}
