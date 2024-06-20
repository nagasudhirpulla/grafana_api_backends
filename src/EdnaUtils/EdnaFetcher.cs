﻿using System;
using System.Collections.Generic;
using InStep.eDNA.EzDNAApiNet;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace EdnaUtils
{
    public class EdnaFetcher
    {
        private static readonly DateTime Epoch = new(1970, 1, 1, 0, 0, 0, 0);
        private readonly ILogger<EdnaFetcher> _logger;
        private readonly bool _useRandom;

        public EdnaFetcher(ILogger<EdnaFetcher> logger, IConfiguration configuration)
        {
            _logger = logger;
            _useRandom = configuration.GetValue<bool>("UseRandom");
        }

        private static List<List<double>> FetchRandomHistData(DateTime startTime, DateTime endTime, int samplingPeriod, bool isFetchFuture, bool onlyStatus)
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
                double val = onlyStatus ? rand.Next(0, 2) : rand.Next(50, 100);
                reslt.Add([val, curTime.Subtract(Epoch).TotalMilliseconds]);
                curTime += TimeSpan.FromSeconds(resFreq);
            }
            return reslt;
        }

        public List<List<double>> FetchHistData(string pnt, DateTime startTime, DateTime endTime, string type, int samplingPeriod, TimeSpan fetchShift, bool isFetchFuture, bool onlyStatus)
        {
            if (_useRandom)
            {
                return FetchRandomHistData(startTime, endTime, samplingPeriod, isFetchFuture, onlyStatus);
            }

            List<List<double>> reslt = new();
            if (pnt == null)
            {
                return reslt;
            }
            int resFreq = (samplingPeriod > 0) ? samplingPeriod : 60;
            // start and end times are in universal time, hence convert to local time if required
            DateTime localStartTime = (startTime + fetchShift).ToLocalTime();
            DateTime localEndTime = (endTime + fetchShift).ToLocalTime();
            if (!isFetchFuture)
            {
                localEndTime = DateTime.Now;
            }
            try
            {
                uint s = 0;
                double dval = 0;
                DateTime timestamp = DateTime.Now;
                string status = "";
                ushort statusCode = 0;
                TimeSpan period = TimeSpan.FromSeconds(resFreq);
                int nret = 0;
                if (type == "raw")
                { nret = History.DnaGetHistRaw(pnt, localStartTime, localEndTime, out s); }
                else if (type == "snap")
                { nret = History.DnaGetHistSnap(pnt, localStartTime, localEndTime, period, out s); }
                else if (type == "average")
                { nret = History.DnaGetHistAvg(pnt, localStartTime, localEndTime, period, out s); }
                else if (type == "min")
                { nret = History.DnaGetHistMin(pnt, localStartTime, localEndTime, period, out s); }
                else if (type == "max")
                { nret = History.DnaGetHistMax(pnt, localStartTime, localEndTime, period, out s); }

                while (nret == 0)
                {
                    if (onlyStatus)
                    {
                        nret = History.DnaGetNextHistFull(s, out dval, out timestamp, out statusCode, out status);
                        if (status != null)
                        {
                            DateTime gmtTs = (timestamp - fetchShift).ToUniversalTime();
                            if (gmtTs > Epoch)
                            {
                                reslt.Add(new List<double> { statusCode, gmtTs.Subtract(Epoch).TotalMilliseconds });
                            }
                        }
                    }
                    else
                    {
                        nret = History.DnaGetNextHist(s, out dval, out timestamp, out status);
                        if (status != null)
                        {
                            DateTime gmtTs = (timestamp - fetchShift).ToUniversalTime();
                            if (gmtTs > Epoch)
                            {
                                reslt.Add(new List<double> { dval, gmtTs.Subtract(Epoch).TotalMilliseconds });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Error while fetching history results " + ex.Message);
            }
            return reslt;
        }
    }
}
