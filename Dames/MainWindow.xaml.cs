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

namespace Dames
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            Damier = new Damier();
            InitializeComponent();
            Dispatcher.BeginInvoke(new Action(() =>
            {
                UserControlDamesVisuel.Damier = Damier;
            }), null);
        }

        public Damier Damier { get; set; }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            GridDames.ColumnDefinitions[1].Width = new GridLength(this.Height);
        }

        private void ButtonInit_Click(object sender, RoutedEventArgs e)
        {
            UserControlDamesVisuel.Init();
        }
    }
}
