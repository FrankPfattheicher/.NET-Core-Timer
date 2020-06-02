using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Timers;
using Xunit;

namespace TimerTest
{
    public class SystemTimerTimerTests : IDisposable
    {
        private readonly Stopwatch _stopwatch;
        private readonly System.Timers.Timer _timer;
        private int _handlingTimeMs = 1;
        private readonly List<long> _callTimes;
        
        public SystemTimerTimerTests()
        {
            _stopwatch = new Stopwatch();
            _callTimes = new List<long>();
            _timer = new System.Timers.Timer(100);
            _timer.Elapsed += TimerOnElapsed;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }

        private void TimerOnElapsed(object sender, ElapsedEventArgs e)
        {
            _callTimes.Add(_stopwatch.ElapsedMilliseconds);
            Task.Delay(TimeSpan.FromMilliseconds(_handlingTimeMs)).Wait();
        }

        [Fact]
        public void ShouldNotCallTimerCallbackPriorToStart()
        {
            Assert.True(_callTimes.Count == 0);
        }

        [Fact]
        public void ShouldCallTimerCallbackAfterStart()
        {
            _callTimes.Clear();   
            _timer.Start();

            Task.Delay(TimeSpan.FromSeconds(1)).Wait();
            
            Assert.True(_callTimes.Count > 0);
        }

        [Fact]
        public void IfHandlingIsLongerThanDurationCallbackIsCalledInParallel()
        {
            _handlingTimeMs = 750;
            _callTimes.Clear();
            _stopwatch.Start();
            _timer.Start();

            Task.Delay(TimeSpan.FromSeconds(1)).Wait();
            
            Assert.True(_callTimes.Count > 1, $"Call count = {_callTimes.Count}");
        }
        
        
    }
}