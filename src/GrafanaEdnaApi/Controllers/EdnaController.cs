using EdnaUtils;
using GrafanaEdnaApi.Models.Query;
using GrafanaEdnaApi.Models.Response;
using GrafanaEdnaApi.Models.Tags;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrafanaEdnaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EdnaController : ControllerBase
    {
        private readonly EdnaFetcher _ednaFetcher;
        public EdnaController(EdnaFetcher ednaFetcher)
        {
            _ednaFetcher = ednaFetcher;
        }

        [HttpGet]
        public IActionResult Get() => Ok();

        [HttpPost("query")]
        public IActionResult Query([FromBody] QueryRequest query)
        {
            List<TargetResponse> dataResponse = new();
            foreach (QueryTarget trgt in query.Targets)
            {
                string samplingType = trgt.Target;
                JObject dataObj = (JObject)trgt.ExtraData;
                int samplingPeriod = (int)(dataObj["period"] ?? 60);
                string pnt = (string)dataObj["pnt"];
                var measData = _ednaFetcher.FetchHistData(pnt, query.Range.From, query.Range.To, samplingType, samplingPeriod);
                dataResponse.Add(new TargetResponse(trgt.Target, measData));
            }
            var jsonResult = JsonConvert.SerializeObject(dataResponse);
            return Ok(jsonResult);
        }

        [HttpPost("search")]
        public IActionResult Search()
        {
            return Ok(new List<string>() { "raw", "snap", "average", "max", "min" });
        }

        //[HttpPost("tag-keys")]
        //public IActionResult TagKeys()
        //{
        //    var jsonResult = JsonConvert.SerializeObject(new List<TagKeyResponse>() { new TagKeyResponse("string", "sampling") });
        //    return Ok(jsonResult);
        //}

        //[HttpPost("tag-values")]
        //public IActionResult TagValues([FromBody] TagValueRequest query)
        //{
        //    List<TagValueResponse> tagVals = new();
        //    if (query.Key == "sampling")
        //    {
        //        tagVals = new List<TagValueResponse>() {
        //            new TagValueResponse("raw"),
        //            new TagValueResponse("snap"),
        //            new TagValueResponse("average"),
        //            new TagValueResponse("max"),
        //            new TagValueResponse("min"),
        //            new TagValueResponse("interpolated")
        //        };
        //    }
        //    var jsonResult = JsonConvert.SerializeObject(tagVals);
        //    return Ok(jsonResult);
        //}

    }
}
