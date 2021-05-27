using System;
using System.Diagnostics;
using System.ComponentModel;
using System.Windows.Threading;

namespace ExamTimer
{
    internal class Countdown
    {
        private TimeSpan limit;
        private Stopwatch stopwatch;
        private DispatcherTimer timer;

        public event EventHandler CountdownEnded;
        public event EventHandler CountdownUpdated;

        public TimeSpan RemainingTime => limit - stopwatch.Elapsed;

        public bool Running => stopwatch.IsRunning;

        public Countdown(TimeSpan time)
        {
            limit = time;

            stopwatch = new Stopwatch();

            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(100);
            timer.Tick += TimerOnTick;
            
            StartCountdown();
        }

        public void StopCountdown()
        {
            stopwatch.Stop();
            timer.Stop();
        }

        public void StartCountdown()
        {
            stopwatch.Start();
            timer.Start();
        }

        private void TimerOnTick(object sender, EventArgs e)
        {
            OnCountdownUpdated(e);

            if (RemainingTime <= TimeSpan.FromTicks(0))
            {
                StopCountdown();
                OnCountdownEnded(e);
            }
        }

        protected virtual void OnCountdownEnded(EventArgs e)
        {
            var handler = CountdownEnded;

            handler?.Invoke(this, e);
        }

        protected virtual void OnCountdownUpdated(EventArgs e)
        {
            var handler = CountdownUpdated;

            handler?.Invoke(this, e);
        }
    }
}