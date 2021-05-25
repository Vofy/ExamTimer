using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ExamTimer
{
    /// <summary>
    /// Interakční logika pro MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static System.Timers.Timer timer;
        private Part[] examParts;
        private int currentTimer;

        public MainWindow()
        {
            InitializeComponent();

            examParts = new Part[] {
                new Part("Introduction", 60),
                new Part("Part 1", 240),
                new Part("Part 2", 240, new Part[] {
                    new Part("Subpart 1", 120),
                    new Part("Subpart 2", 120)}),
                new Part("Part 3", 360, new Part[] {
                    new Part("Subpart 1", 180),
                    new Part("Subpart 2", 180)})
            };

            currentTimer = 60;

            // Nastavení intervalu na 1 sekundu
            timer = new System.Timers.Timer(1000);

            // Přiřazení události (která se provede vždy na konci periody
            timer.Elapsed += Timer_Elapsed;

            // Start the timer
            timer.Enabled = true;
        }

        private void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            TimeSpan t = TimeSpan.FromSeconds(currentTimer);
            MessageBox.Show(t.Seconds.ToString());
            //Title.Content = string.Format("{0:D2}:{1:D2}", t.Minutes.ToString(), t.Seconds.ToString());
            currentTimer--;
        }
    }
}
