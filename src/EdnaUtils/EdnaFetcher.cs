using System;
using System.Collections.Generic;
using InStep.eDNA.EzDNAApiNet;

namespace EdnaUtils
{
    public class EdnaFetcher
    {
        public static RTValue FetchRealTimeData(string pnt)
        {
            RTValue rtVal;
            try
            {
                int nret = RealTime.DNAGetRTAll(pnt, out double dval, out DateTime timestamp, out string status, out string desc, out string units);//get RT value
                if (nret == 0)
                {
                    rtVal = new RTValue { Dval = dval, Timestamp = timestamp, Status = status, Units = units };
                    return rtVal;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error while fetching realtime result " + ex.Message);
                return null;
            }
            return null;
        }

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
