using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TestLoadAnima
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        BlurEffect myEffect=new BlurEffect();
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            myEffect.Radius = 10;
            Effect = myEffect;
            using (LoadingWindow lw = new LoadingWindow(Simulator))
            {
                lw.Owner = this;
                lw.ShowDialog();
            }

            myEffect.Radius = 0;
            Effect = myEffect;
        }

        void Simulator()
        {
            for (int i = 0; i < 500; i++)
            {
                Thread.Sleep(5);
            }
        }
    }
}
