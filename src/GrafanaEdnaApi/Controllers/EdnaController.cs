using EdnaUtils;
using GrafanaCore.Models.Query;
using GrafanaCore.Models.Response;
using GrafanaCore.Models.Tags;
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
            // initialise response
            List<TargetResponse> dataResponse = new();

            // iterate through each series for data fetch
            foreach (QueryTarget trgt in query.Targets)
            {
                string samplingType = trgt.Target;

                // derive extra data object
                var dataObj = trgt.ExtraData;

                // derive series name
                string pntName = dataObj.PntName ?? dataObj.Pnt ?? trgt.Target;

                // derive fetch shift
                TimeSpan fetchShift = new(days: dataObj.FetchShiftDays, hours: dataObj.FetchShiftHrs, minutes: dataObj.FetchShiftMins, seconds: dataObj.FetchShiftSecs);

                // fetch data
                var measData = _ednaFetcher.FetchHistData(dataObj.Pnt, query.Range.From, query.Range.To, samplingType, dataObj.SamplingPeriod, fetchShift, dataObj.FetchFuture, dataObj.OnlyStatus);

                // add data to response
                dataResponse.Add(new TargetResponse(pntName, measData));
            }

            // create the json response string
            var jsonResult = JsonConvert.SerializeObject(dataResponse);

            // return the response with 200 status header
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
