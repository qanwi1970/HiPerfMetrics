HiPerfMetrics
=============

Time individual sections of code and get detailed metrics back on performance.

After the default performance counters have told you that a method takes *way* longer than you'd like, you need to dig into that method and find out what part
of it is taking so long. But this is the real world, and the method in question is very important and does a lot of very important things. This is where
__HiPerfMetrics__ comes in. Place simple Start and Stop calls around sections of your code and get detailed metrics on how long each section takes.

Installation
------------
The easiest way to get HiPerfMetrics in your project is to install the Nuget package:

`
install-package HiPerfMetrics
`

If you like to make things harder for yourself (or if you're not allowed to use Nuget), then you could download the source and build it.

##Simple Usage

###Instrument Your Code

First, create an instance of the HiPerfMetrics class

```C#
var hiPerfMetrics = new HiPerfMetrics("My Important Method");
```

Next, place Start and Stop calls around your sections of code:

__Before__

```C#
public CompleteOrder MyImportantMethod(int custId, OrderData orderData)
{
    MyInitMethod();

    var customerData = _customerDataService.GetDataForCustomer(custId);

    var newThing = SomeLogic(customerData);
    var anotherThing = SomeMoreLogic(newThing, orderData);
    var processedOrder = TheseReallyGoTogether(newThing, anotherThing);

    var returnValue = MapOrder(processedOrder);

    return returnValue;
}
```

__After__

```C#
public CompleteOrder MyImportantMethod(int custId, OrderData orderData)
{
    var hiPerfMetrics = new HiPerfMetrics("My Important Method");

    hiPerfMetrics.Start("Initialization");
    MyInitMethod();
    hiPerfMetrics.Stop();

    hiPerfMetrics.Start("Get Customer");
    var customerData = _customerDataService.GetDataForCustomer(custId);
    hiPerfMetrics.Stop();

    hiPerfMetrics.Start("Processing Logic");
    var newThing = SomeLogic(customerData);
    var anotherThing = SomeMoreLogic(newThing, orderData);
    var processedOrder = TheseReallyGoTogether(newThing, anotherThing);
    hiPerfMetrics.Stop();

    var returnValue = MapOrder(processedOrder);

    return returnValue;
}
```

### Get a Report
Choose a report that fits your needs and output it somewhere. The easiest way is to log the default report, then pull it up in the log afterwards. To do that,
just add a line like this before returning from the method:

`_logger.Debug(hiPerfMetrics.ReportAsDefault());`

When you check your log, you'll have an entry like this:

```
HiPerfMetric 'NormalTwoTaskReport' running time - .484 seconds
-----------------------------------------
   ms      %    Task name
-----------------------------------------
435.050   90 %  task 1        
49.056   10 %  task 2        
```
