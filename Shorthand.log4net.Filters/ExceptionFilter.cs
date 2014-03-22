using System;
using log4net.Core;
using log4net.Filter;

namespace Shorthand.log4net.Filters {
    public class ExceptionFilter : FilterSkeleton {
        public string ExceptionTypeName { get; set; }
        public bool AcceptOnMatch { get; set; }
        public override FilterDecision Decide(LoggingEvent ev) {
            if (ev.ExceptionObject == null)
                return FilterDecision.Neutral;

            Type filterType;
            try {
                filterType = Type.GetType(ExceptionTypeName, true, true);
            }
            catch (Exception) {
                return FilterDecision.Neutral;
            }

            if(filterType == null)
                return FilterDecision.Neutral;

            var actualType = ev.ExceptionObject.GetType();
            var isMatch = (actualType == filterType || actualType.IsSubclassOf(filterType));

            if (!isMatch)
                return FilterDecision.Neutral;

            return AcceptOnMatch ? FilterDecision.Accept : FilterDecision.Deny;
        }
    }
}
