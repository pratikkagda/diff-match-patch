using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using System;
using DiffMatchPatch;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace DiffTextFunctionApp
{
    public static class Function1
    {
        [FunctionName("CalculateDiffText")]
        public static async Task<HttpResponseMessage> Run([HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)]HttpRequestMessage req, TraceWriter log)
        {
            log.Info("C# HTTP trigger function processed a request.");

            DiffTextRequest request = await req.Content.ReadAsAsync<DiffTextRequest>();

            diff_match_patch dmp = new diff_match_patch();

            // Execute one reverse diff as a warmup.
            var result = dmp.diff_prettyHtml(dmp.diff_main(request.SourceText, request.DestinationText));
            result =result.Replace("ins","span") .Replace("del", "span") .Replace("=\"background:#ffe6e6;\"", "='color: Blue;'").Replace("=\"background:#e6ffe6;\"", "='color: Red;'");

            //result = result.Replace("del", "span").Replace("background:#ffe6e6;", "background:#ffe6e6;text-decoration:line-through");
            GC.Collect();
            GC.WaitForPendingFinalizers();

            return string.IsNullOrEmpty(result)
                ? req.CreateResponse(HttpStatusCode.BadRequest, "Please check request body")
                : req.CreateResponse(HttpStatusCode.OK, result);
        }
    }
}
