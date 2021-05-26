using System;
using System.Collections.Generic;
using System.Windows;

namespace ExamTimer
{
    public partial class MainWindow : Window
    {
        private List<Part> examParts;
        private int nextPart = -1;
        private Countdown countdown;

        public MainWindow()
        {
            InitializeComponent();

            /*examParts = new List<Part>(new Part[] {
                new Part("Introduction", 5),
                new Part("Part 1", 5),
                new Part("Part 2", "1. Podčást", 5),
                new Part("Part 2", "2. Podčást", 5),
                new Part("Part 3", "1. Podčást", 5),
                new Part("Part 3", "2. Podčást", 5)
            });*/

            examParts = new List<Part>(new Part[] {
                new Part("Introduction", 60),
                new Part("Part 1", 240),
                new Part("Part 2", "1. Podčást", 120),
                new Part("Part 2", "2. Podčást", 120),
                new Part("Part 3", "1. Podčást", 180),
                new Part("Part 3", "2. Podčást", 180),
                new Part("Konec")
            });
        }

        private void NextPart()
        {
            nextPart++;

            if (nextPart < examParts.Count)
            {
                countdown = new Countdown(TimeSpan.FromSeconds(examParts[nextPart].Time));

                // Aktualizace dat každou sekundu
                countdown.CountdownUpdated += (sender, args) =>
                {
                    countdownLabel.Content = countdown.RemainingTime.ToString(@"mm\:ss");
                    radialProgressBar.Value = countdown.RemainingTime.TotalSeconds;
                };

                // Po dokončení odpočtu se přejde na další část
                countdown.CountdownEnded += (sender, args) => NextPart();

                // Nastavení titulků a progressbaru
                titleLabel.Content = examParts[nextPart].Title;
                subtitleLabel.Content = (examParts[nextPart].Subtitle != null) ? examParts[nextPart].Subtitle : "-";
                countdownLabel.Content = countdown.RemainingTime.ToString(@"mm\:ss");

                radialProgressBar.Maximum = examParts[nextPart].Time;
                radialProgressBar.Value = examParts[nextPart].Time;
            }
        }

        private void nextPartButton_Click(object sender, RoutedEventArgs e)
        {
            countdown.StopCountdown();
            NextPart();
        }

        private void startExamButton_Click(object sender, RoutedEventArgs e)
        {
            NextPart();
            startExamButton.Visibility = Visibility.Collapsed;

            pauseButton.Visibility = Visibility.Visible;
            nextPartButton.Visibility = Visibility.Visible;
        }

        private void pauseButton_Click(object sender, RoutedEventArgs e)
        {
            if (countdown.Running)
            {
                countdown.StopCountdown();
                pauseButton.Content = "Resume";
            }
            else
            {
                countdown.StartCountdown();
                pauseButton.Content = "Pause";
            }
        }
    }
}