using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Threading;

namespace ExamTimer
{
    public partial class MainWindow : Window
    {
        private Part[] examParts;
        private int currentPartIndex;
        private double timeTotal;
        private Countdown countdown;

        private Brush blue = new SolidColorBrush(Color.FromRgb(6, 55, 122));

        public MainWindow()
        {
            InitializeComponent();

            examParts = new Part[] {
                new Part("Exam Timer", "Get ready"),
                new Part("INTRODUCTION", 60),
                new Part("PART ONE", 240),
                new Part("PART TWO", "TASK ONE", 120),
                new Part("PART TWO", "TASK TWO", 120),
                new Part("PART THREE", "TASK ONE", 180),
                new Part("PART THREE", "TASK TWO", 180),
                new Part("THE END")
            };

            timeTotal = 0;
            foreach (Part part in examParts)
            {
                timeTotal += part.Time;
            }

            BeginNewExam();
        }

        private void BeginNewExam()
        {
            currentPartIndex = 0;
            LoadPart(currentPartIndex);
        }

        private void LoadPart(int partIndex)
        {
            ResetToDefault();

            if (partIndex < examParts.Length)
            {
                Part currentPart = examParts[partIndex];

                SetPartName(currentPart.PartName);
                SetSubpartName(currentPart.SubpartName);
                SetTimer(partIndex, currentPart.Time);

                ConfigureButtons(partIndex);
            }
            else
            {
                BeginNewExam();
            }
        }

        #region Metody pro aktualizaci dat a ovládacích prvků formuláře
        /// <summary>
        /// Pokud je nastaven čas odpočtu vytvoří se nová instance odpočtu,
        /// jestliže není nastaven (respektive má výchozí hodnotu), tak se nastaví výchozí hodnoty.
        /// </summary>
        /// <param name="partIndex">Index části</param>
        /// <param name="time">Zbývající čas</param>
        private void SetTimer(int partIndex, double time)
        {
            if (partIndex == 0)
            {
                countdownLabel.Content = TimeSpan.FromSeconds(timeTotal).ToString(@"mm\:ss");
            }

            if (time != default)
            {
                radialProgressBar.Maximum = time;
                radialProgressBar.Value = time;

                countdown = new Countdown(TimeSpan.FromSeconds(time));

                // Aktualizace časomíry. V případě, že do konce části zbývá méně než 1 sekunda, RadialProgressBar zčervená.
                countdown.CountdownUpdated += (sender, args) =>
                {
                    countdownLabel.Content = countdown.RemainingTime.ToString(@"mm\:ss");
                    radialProgressBar.Value = countdown.RemainingTime.TotalSeconds;

                    if (countdown.RemainingTime.TotalSeconds <= 1)
                    {
                        radialProgressBar.Foreground = Brushes.Red;
                        radialProgressBar.OuterBackgroundBrush = Brushes.LightSalmon;
                    }
                };

                // Po dokončení odpočtu se přejde na další část
                countdown.CountdownEnded += (sender, args) => LoadPart(++currentPartIndex);

                countdownLabel.Content = countdown.RemainingTime.ToString(@"mm\:ss");
            }
        }

        /// <summary>
        /// Nastavení nadpisu jestliže není null a nemá výchozí hodnotu, tedy prázdný řetězec
        /// </summary>
        /// <param name="partName"></param>
        private void SetPartName(String partName)
        {
            if (partName != null && partName != default)
                titleLabel.Content = partName;
            else
                titleLabel.Content = "";
        }

        /// <summary>
        /// Nastavení podnadpisu jestliže není null a nemá výchozí hodnotu, tedy prázdný řetězec
        /// </summary>
        /// <param name="subpartName"></param>
        private void SetSubpartName(String subpartName)
        {
            if (subpartName != null && subpartName != default)
                subtitleLabel.Content = subpartName;
            else
                subtitleLabel.Content = "";
        }

        /// <summary>
        /// Zviditelnění relevantních tlačítek pro první, poslední i ostatní části
        /// </summary>
        /// <param name="partIndex">číslo čáti</param>
        private void ConfigureButtons(int partIndex)
        {
            if (partIndex == 0)
            {
                startExamButton.Visibility = Visibility.Visible;
                endExamButton.Visibility = Visibility.Collapsed;
                pauseButton.Visibility = Visibility.Collapsed;
                nextPartButton.Visibility = Visibility.Collapsed;
            }
            else if (partIndex == examParts.Length - 1)
            {
                startExamButton.Visibility = Visibility.Collapsed;
                endExamButton.Visibility = Visibility.Visible;
                pauseButton.Visibility = Visibility.Collapsed;
                nextPartButton.Visibility = Visibility.Collapsed;
            }
            else
            {
                startExamButton.Visibility = Visibility.Collapsed;
                endExamButton.Visibility = Visibility.Collapsed;
                pauseButton.Visibility = Visibility.Visible;
                nextPartButton.Visibility = Visibility.Visible;
            }
        }

        private void ResetToDefault()
        {
            radialProgressBar.Foreground = blue;
            radialProgressBar.OuterBackgroundBrush = Brushes.LightGray;
            countdownLabel.Content = "00:00";
            radialProgressBar.Value = radialProgressBar.Maximum;
            pauseButton.Content = "Pause";
        }
        #endregion

        #region Obslužné metody událostí
        private void nextPartButton_Click(object sender, RoutedEventArgs e)
        {
            if (countdown != null)
            {
                countdown.StopCountdown();
            }

            LoadPart(++currentPartIndex);
        }

        private void startExamButton_Click(object sender, RoutedEventArgs e)
        {
            LoadPart(++currentPartIndex);
        }

        private void pauseButton_Click(object sender, RoutedEventArgs e)
        {
            if (countdown != null)
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

        private void endExamButton_Click(object sender, RoutedEventArgs e)
        {
            BeginNewExam();
        }
        #endregion
    }
}