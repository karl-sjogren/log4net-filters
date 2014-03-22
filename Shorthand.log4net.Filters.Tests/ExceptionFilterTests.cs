using System;
using System.Text;
using log4net;
using log4net.Config;
using log4net.Core;
using NUnit.Framework;

namespace Shorthand.log4net.Filters.Tests {
    public class ExceptionFilterTests {
        private ILog _log;
        private StringBuilder _stringBuilder;
        [SetUp]
        public void SetUp() {
            _log = LogManager.GetLogger(typeof(ExceptionFilterTests));
            _stringBuilder = new StringBuilder();

            var appender = new StringBuilderAppender(_stringBuilder);
            appender.Threshold = Level.All;
            appender.AddFilter(new ExceptionFilter { ExceptionTypeName = typeof(UnimportantException).AssemblyQualifiedName });
            appender.AddFilter(new ExceptionFilter { ExceptionTypeName = typeof(ImportantException).AssemblyQualifiedName, AcceptOnMatch = true });
            appender.AddFilter(new ExceptionFilter { ExceptionTypeName = typeof(SpecialException).AssemblyQualifiedName });
            BasicConfigurator.Configure(appender);
        }

        [Test]
        public void ExceptionIsRemoved() {
            _log.Error("An unimportant exception occured!", new UnimportantException());

            var loggedText = _stringBuilder.ToString();
            Assert.That(!loggedText.Contains("An unimportant exception occured!"));
        }

        [Test]
        public void ExceptionIsAdded() {
            _log.Error("An important exception occured!", new ImportantException());

            var loggedText = _stringBuilder.ToString();
            Assert.That(loggedText.Contains("An important exception occured!"));
        }

        [Test]
        public void InheritedExceptionIsFiltered() {
            _log.Error("A super special exception occured!", new SuperSpecialException());

            var loggedText = _stringBuilder.ToString();
            Assert.That(!loggedText.Contains("A super special exception occured!"));
        }
    }

    public class ImportantException : Exception { }
    public class UnimportantException : Exception { }

    public class SpecialException : Exception { }
    public class SuperSpecialException : SpecialException { }
}
