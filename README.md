Loggery
=======

Unity logging framework



Demo:
1. Open PulseScene Unity scene in the Loggery/ExampleProject folder. 
2. Press play. 
3. Open Console tab
4. Menu -> Window -> Loggery
5. Play with setting to see the different results

Usage: 

Add this line to your class to create a logger:

private static readonly LoggeryLogger Logger = LoggeryManager.GetCurrentClassLogger();

Use it to send log message to the Unity console like this:

Logger.Info("Starting TCP client");

Logger.Debug("Sending " + mt + " TCP message");

catch (Exception e)
{
         Logger.Error(e.Message);
}


Popup menu:
Menu -> LoggeryLogger -> Drop Down log level

Here you can change the Log Level, filter on class method name and/or the log message via regular expressions. You can also change to colors of the different log levels.



When your app is running outside Unity LoggeryLogger defaults to 'Info'

Tips:
Mostly use Debug whilst developing your app, Use Trace in thight loops which get called often e.g Update() and would flood you console. Info is usefull to get feedback from people who are using your app and sending crash reports and log files. 

For questions & support email: loggery@robotmotel.com
