using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace WaterMeCollectorFunction
{
    public static class GetMetrics
    {
        [FunctionName("GetMetrics")]
        public static IActionResult Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "HostName/{hostname}")] HttpRequest req,
            [CosmosDB(
                databaseName: "waterme",
                collectionName: "metrics",
                ConnectionStringSetting = "ConnectionStringSetting",
                SqlQuery ="SELECT * FROM c WHERE c.HostName={hostName}")] IEnumerable<Metric> host,
            ILogger log,
            string hostName
            )
        {
            log.LogInformation($"Searching for {hostName}");

            if (host == null)
            {
                return new NotFoundResult();
            }
            return new OkObjectResult(host);
        }
    }
}
