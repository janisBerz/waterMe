using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using WaterMeCollectorFunction;

namespace WaterMe.CollectorFunction
{
    public static class Collector
    {
        // function to send data to the db
        [FunctionName("Collector")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            [CosmosDB(
                databaseName: "waterme",
                collectionName: "metrics",
                ConnectionStringSetting = "ConnectionStringSetting")] IAsyncCollector<object> metrics,
            ILogger log)
        {
            try
            {
                // Logs a message when the function is called
                log.LogInformation("Function processed a request.");

                // reads the incoming requests body
                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();

                // Deserialize the the body to Metric object
                Metric input = JsonConvert.DeserializeObject<Metric>(requestBody);

                // Checks if the host name is present
                if (input.Host != null)
                {
                    Metric output = new Metric
                    {
                        Host = input.Host,
                        TempCelsius = input.TempCelsius,
                        DateTime = DateTime.Now
                    };

                    // Sends the data to cosmos db
                    await metrics.AddAsync(output);
                    return new OkObjectResult(output);
                }
                else
                {
                    log.LogError($"Host name not specified");
                    return new StatusCodeResult(StatusCodes.Status204NoContent);
                }
            }
            catch (System.Exception ex)
            {
                log.LogError($"Couldn't insert item. Exception thrown: {ex.Message}");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
