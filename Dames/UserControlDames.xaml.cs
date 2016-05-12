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
    /// Interaction logic for UserControlDames.xaml
    /// </summary>
    public partial class UserControlDames : UserControl
    {
        public UserControlDames()
        {
            InitializeComponent();
            for (int i = 0; i < 10; i++)
                for (int j = 0; j < 10; j++)
                {
                    Rectangle r = new Rectangle();
                    r.Name = "Rectangle" + i.ToString() + j.ToString();
                    r.Fill = (i % 2 == 0 && j % 2 == 0) || (i % 2 == 1 && j % 2 == 1) ? Brushes.Black : Brushes.White;
                    GridDames.Children.Add(r);
                    Grid.SetRow(r, i);
                    Grid.SetColumn(r, j);
                    Panel.SetZIndex(r, 1);


                    //Ellipse l = new Ellipse();
                    //l.Width = 30;
                    //l.Height = 30;
                    //l.VerticalAlignment = VerticalAlignment.Center;
                    //l.HorizontalAlignment = HorizontalAlignment.Center;
                    //l.Fill = (i % 2 == 0 && j % 2 == 0) || (i % 2 == 1 && j % 2 == 1) ? Brushes.White : Brushes.Black;
                    //GridDames.Children.Add(l);
                    //Grid.SetRow(l, i);
                    //Grid.SetColumn(l, j);
                    //Panel.SetZIndex(l, 2);
                }
        }


    }
}
