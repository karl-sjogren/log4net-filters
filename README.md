log4net-filters
===============

Custom filters for log4net (well, one at least so far)

ExceptionFilter
---------------
```xml
<appender>
   ....
   <filter type="Shorthand.log4net.Filters.ExceptionFilter">
      <exceptionTypeName value="Shorthand.log4net.Filters.Tests.UnimportantException, Shorthand.log4net.Filters.Tests, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null" />
      <acceptOnMatch value="false" />
   </filter>
</appender>
```