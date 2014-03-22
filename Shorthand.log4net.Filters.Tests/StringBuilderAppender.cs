using System.Text;
using log4net.Appender;
using log4net.Core;

namespace Shorthand.log4net.Filters.Tests {
    public class StringBuilderAppender : AppenderSkeleton {
        private readonly StringBuilder _stringBuilder;
        public StringBuilderAppender(StringBuilder stringBuilder) {
            _stringBuilder = stringBuilder;
        }

        protected override void Append(LoggingEvent loggingEvent) {
            _stringBuilder.AppendLine(loggingEvent.RenderedMessage);
        }
    }
}
