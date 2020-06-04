
# The Timer Storys

There are Timers in the .NET Framework!
* System.Threading.Timer 
* System.Timers.Timer
* System.Windows.Forms.Timer (not covered here)
* System.Web.UI.Timer (not covered here)

[Microsoft Documentation:](https://docs.microsoft.com/en-us/dotnet/api/system.threading.timer?view=netcore-3.1)

.NET includes four classes named Timer, each of which offers different functionality:

* System.Timers.Timer, which fires an event and executes the code in one or more event sinks at regular intervals. The class is intended for use as a server-based or service component in a multithreaded environment; it has no user interface and is not visible at runtime.
* System.Threading.Timer, which executes a single callback method on a thread pool thread at regular intervals. The callback method is defined when the timer is instantiated and cannot be changed. Like the System.Timers.Timer class, this class is intended for use as a server-based or service component in a multithreaded environment; it has no user interface and is not visible at runtime.
* System.Windows.Forms.Timer (.NET Framework only), a Windows Forms component that fires an event and executes the code in one or more event sinks at regular intervals. The component has no user interface and is designed for use in a single-threaded environment; it executes on the UI thread.
* System.Web.UI.Timer (.NET Framework only), an ASP.NET component that performs asynchronous or synchronous web page postbacks at a regular interval.


read also: [Comparing the Timer Classes in the .NET Framework Class Library](https://web.archive.org/web/20150329101415/https://msdn.microsoft.com/en-us/magazine/cc164015.aspx)


## Questions
* Thread-safety
* What if the handler took longer than timer period?
* What happens in case of an Exception in handler?
* Any more? Please add an issue!

