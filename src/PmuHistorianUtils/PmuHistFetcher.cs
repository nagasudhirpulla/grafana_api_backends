using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace PmuHistorianUtils
{
    public class PmuHistFetcher
    {
        private static readonly DateTime Epoch = new(1970, 1, 1, 0, 0, 0, 0);
        private readonly ILogger<PmuHistFetcher> _logger;
        private readonly bool _useRandom;

        public PmuHistFetcher(ILogger<PmuHistFetcher> logger, IConfiguration configuration)
        {
            _logger = logger;
            _useRandom = configuration.GetValue<bool>("UseRandom");
        }

        private static List<List<double>> FetchRandomHistData(DateTime startTime, DateTime endTime, int samplingPeriod, bool isFetchFuture)
        {
            List<List<double>> reslt = new();
            DateTime curTime = startTime;
            int resFreq = (samplingPeriod > 0) ? samplingPeriod : 60;
            Random rand = new();
            DateTime targetEndTime = endTime;
            if (!isFetchFuture && targetEndTime > DateTime.UtcNow)
            {
                targetEndTime = DateTime.UtcNow;
            }
            while (curTime <= targetEndTime)
            {
                reslt.Add(new List<double>() { rand.Next(50, 100), curTime.Subtract(Epoch).TotalMilliseconds });
                curTime += TimeSpan.FromSeconds(resFreq);
            }
            return reslt;
        }

        public List<List<double>> FetchHistData(string pnt, DateTime startTime, DateTime endTime, string type, int samplingPeriod, TimeSpan fetchShift, bool isFetchFuture)
        {
            return FetchRandomHistData(startTime, endTime, samplingPeriod, isFetchFuture);
        }
    }
}
