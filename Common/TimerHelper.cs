using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public static class TimerHelper
    {
        public static void StopWatch(Action method)
        {
            var stopWatch = new Stopwatch();

            stopWatch.Start();

            method();

            stopWatch.Stop();

            var ts = stopWatch.Elapsed;
            
            var elapsedTime = $"{ts.Hours:00}:{ts.Minutes:00}:{ts.Seconds:00}.{ts.Milliseconds / 10:00}";

            Console.WriteLine("RunTime " + elapsedTime);
        }
    }
}
