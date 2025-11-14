using Polly;
using Polly.CircuitBreaker;

//var circuitBreaker = Policy.Handle<HttpRequestException>()
//    .CircuitBreakerAsync(2, TimeSpan.FromSeconds(30));

var retryPolicy = Policy.Handle<HttpRequestException>()
    .WaitAndRetryAsync(
    retryCount: 3,
    sleepDurationProvider: retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)), 
    onRetry: (exception, timeSpan, retryCount, context) =>
    {
        Console.WriteLine($"Tentativa {retryCount} falhou. Esperando {timeSpan.TotalSeconds} segundos antes da próxima tentativa.");
    });

var httpClient = new HttpClient();

for (int i = 0; i < 5; i++)
{
    try
    {
        //await circuitBreaker.ExecuteAsync(async () =>
        //{
        //    var response = await httpClient.GetAsync("https://localhost:7272/WeatherForecast");
        //    response.EnsureSuccessStatusCode();
        //    var content = await response.Content.ReadAsStringAsync();
        //    Console.WriteLine(content);
        //});

        await retryPolicy.ExecuteAsync(async () =>
        {
            var response = await httpClient.GetAsync("https://localhost:7272/WeatherForecast");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            Console.WriteLine(content);
        });
    }
    //catch (BrokenCircuitException)
    //{
    //    Console.WriteLine("Circuito está aberto: falha detectada");
    //}
    catch (HttpRequestException ex)
    {
        Console.WriteLine($"Falha de requisição: {ex.Message}");
    }

    //await Task.Delay(5000); // Wait before the next request
}