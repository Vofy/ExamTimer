using System;
using System.Windows.Threading;

namespace ExamTimer
{
    class Countdown
    {
        private TimeSpan remainingTime;
        private static DispatcherTimer timer;
        private bool running;

        public TimeSpan RemainingTime { get => remainingTime; }
        public bool Running { get => running; }

        public Countdown(TimeSpan time)
        {
            this.remainingTime = time;

            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;

            StartCountdown();
        }

        public void StopCountdown()
        {
            timer.Stop();
            running = false;
        }

        public void StartCountdown()
        {
            timer.Start();
            running = true;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            remainingTime -= TimeSpan.FromSeconds(1);

            if (remainingTime <= TimeSpan.FromSeconds(0))
            {
                StopCountdown();
                OnCountdownEnded(e);
            }

            OnCountdownUpdated(e);
        }

        protected virtual void OnCountdownEnded(EventArgs e)
        {
            EventHandler handler = CountdownEnded;

            if (handler != null)
            {
                handler(this, e);
            }
        }

        public event EventHandler CountdownEnded;

        protected virtual void OnCountdownUpdated(EventArgs e)
        {
            EventHandler handler = CountdownUpdated;

            if (handler != null)
            {
                handler(this, e);
            }
        }

        public event EventHandler CountdownUpdated;
    }
}
