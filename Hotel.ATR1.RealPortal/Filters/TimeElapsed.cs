using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics;

namespace Hotel.ATR1.RealPortal.Filters
{
    public class TimeElapsed : Attribute, IActionFilter
    {
        private Stopwatch timer;

        public void OnActionExecuted(ActionExecutedContext context)
        {
            timer.Stop();
            string result = "Elapsed time: " + $"{timer.Elapsed.TotalMilliseconds} ms";
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            timer = Stopwatch.StartNew();
            
        }
    }
}
