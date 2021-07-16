using System;
using System.Collections.Generic;
using InStep.eDNA.EzDNAApiNet;

namespace EdnaUtils
{
    public class EdnaFetcher
    {
        public static List<List<double>> FetchRandomHistData(DateTime startTime, DateTime endTime, int freqSec)
        {
            List<List<double>> reslt = new();
            DateTime curTime = startTime;
            int resFreq = (freqSec > 0) ? freqSec : 60;
            Random rand = new();
            DateTime epoch = new(1970, 1, 1, 0, 0, 0, 0);
            while (curTime <= endTime)
            {
                curTime += TimeSpan.FromSeconds(resFreq);
                reslt.Add(new List<double>() { rand.Next(50, 100), curTime.Subtract(epoch).TotalMilliseconds });
            }
            return reslt;
        }
    }
}
