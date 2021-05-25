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

        public MainWindow()
        {
            InitializeComponent();

            examParts = new Part[] {
                new Part("Introduction", 1000),
                new Part("Part 1", 4000),
                new Part("Part 2", 4000, new Part[] {
                    new Part("Subpart 1", 2000),
                    new Part("Subpart 2", 2000)}),
                new Part("Part 3", 6000, new Part[] {
                    new Part("Subpart 1", 3000),
                    new Part("Subpart 2", 3000)}),
            };

            // Nastavení intervalu na 1 sekundu
            timer = new System.Timers.Timer(1000);

            // Přiřazení události (která se provede vždy na konci periody
            timer.Elapsed += Timer_Elapsed;

            // Start the timer
            timer.Enabled = true;
        }

        private void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            
            throw new NotImplementedException();
        }
    }
}
