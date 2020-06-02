using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Xunit;

namespace TimerTest
{
    public class SystemThreadingTimerTests : IDisposable
    {
        private readonly Stopwatch _stopwatch;
        private System.Threading.Timer _timer;
        private int _handlingTimeMs = 1;
        private readonly List<long> _callTimes;

        public SystemThreadingTimerTests()
        {
            _stopwatch = new Stopwatch();
            _callTimes = new List<long>();
        }
        
        public void Dispose()
        {
            _timer?.Dispose();
        }

        private void TimerCallback(object state)
        {
            _callTimes.Add(_stopwatch.ElapsedMilliseconds);
            Task.Delay(TimeSpan.FromMilliseconds(_handlingTimeMs)).Wait();
        }

        [Fact]
        public void ShouldNotCallTimerCallbackPriorToCreation()
        {
            Assert.True(_callTimes.Count == 0);
        }

        [Fact]
        public void ShouldCallTimerCallbackAfterCreation()
        {
            _callTimes.Clear();
            _timer = new System.Threading.Timer(TimerCallback, this, 100, 100);

            Task.Delay(TimeSpan.FromSeconds(1)).Wait();
            
            Assert.True(_callTimes.Count > 0);
        }

        [Fact]
        public void IfHandlingIsLongerThanDurationCallbackIsCalledInParallel()
        {
            _handlingTimeMs = 750;
            _callTimes.Clear();
            _stopwatch.Start();
            _timer = new System.Threading.Timer(TimerCallback, this, 100, 100);

            Task.Delay(TimeSpan.FromSeconds(1)).Wait();
            
            Assert.True(_callTimes.Count > 1, $"Call count = {_callTimes.Count}");
        }


    }
}
