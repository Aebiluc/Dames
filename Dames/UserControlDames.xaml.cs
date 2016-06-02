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
        public Damier Damier { get; set; }
        public Case SelectCase { get; set; }

        public UserControlDames()
        {
            InitializeComponent();
        }

        public void Init()
        {
            Damier.InitPiece();

            //Creation De toutes les cases
            for (int i = 0; i < 10; i++)
                for (int j = 0; j < 10; j++)
                {
                    Rectangle r = new Rectangle();
                    r.Name = "Rectangle" + i.ToString() + j.ToString();
                    r.Fill = (i % 2 == 0 && j % 2 == 0) || (i % 2 == 1 && j % 2 == 1) ? Brushes.White : Brushes.Gray;
                    r.DataContext = Damier.getCase(i, j);
                    GridDames.Children.Add(r);
                    Grid.SetRow(r, i);
                    Grid.SetColumn(r, j);
                    Panel.SetZIndex(r, 1);
                    r.MouseLeftButtonDown += R_MouseLeftButtonDownRectangle;

                    Case c;
                    c = Damier.getCase(i, j);
                    if (c.Ocuppe)
                    {
                        Ellipse l = new Ellipse();
                        l.Width = 30;
                        l.Height = 30;
                        l.VerticalAlignment = VerticalAlignment.Center;
                        l.HorizontalAlignment = HorizontalAlignment.Center;
                        l.Fill = c.Piece.Couleur == couleur.BLANC ? Brushes.Gold : Brushes.Black;
                        l.Stroke = Brushes.Red;
                        l.StrokeThickness = 0;
                        l.DataContext = Damier.getCase(i, j);
                        GridDames.Children.Add(l);
                        Grid.SetRow(l, i);
                        Grid.SetColumn(l, j);
                        Panel.SetZIndex(l, 2);
                        l.MouseLeftButtonDown += R_MouseLeftButtonDownEllipse;
                    }
                }
            SelectCase = new Case(-1, -1, couleur.BLANC);
        }

        public void Actualiser()
        {
            GridDames.Children.Clear();
            for (int i = 0; i < 10; i++)
                for (int j = 0; j < 10; j++)
                {
                    Rectangle r = new Rectangle();
                    r.Name = "Rectangle" + i.ToString() + j.ToString();
                    r.Fill = (i % 2 == 0 && j % 2 == 0) || (i % 2 == 1 && j % 2 == 1) ? Brushes.White : Brushes.Gray;
                    r.DataContext = Damier.getCase(i, j);
                    GridDames.Children.Add(r);
                    Grid.SetRow(r, i);
                    Grid.SetColumn(r, j);
                    Panel.SetZIndex(r, 1);
                    r.MouseLeftButtonDown += R_MouseLeftButtonDownRectangle;

                    Case c;
                    c = Damier.getCase(i, j);
                    if (c.Ocuppe)
                    {
                        Ellipse l = new Ellipse();
                        l.Width = 30;
                        l.Height = 30;
                        l.VerticalAlignment = VerticalAlignment.Center;
                        l.HorizontalAlignment = HorizontalAlignment.Center;
                        l.Fill = c.Piece.Couleur == couleur.BLANC ? Brushes.Gold : Brushes.Black;
                        l.Stroke = Brushes.Blue;
                        if(c.Piece is Pion)
                            l.StrokeThickness = 0;
                        else
                            l.StrokeThickness = 3;
                        l.DataContext = Damier.getCase(i, j);
                        GridDames.Children.Add(l);
                        Grid.SetRow(l, i);
                        Grid.SetColumn(l, j);
                        Panel.SetZIndex(l, 2);
                        l.MouseLeftButtonDown += R_MouseLeftButtonDownEllipse;
                    }
                }
            SelectCase = new Case(-1, -1, couleur.BLANC);
        }

        public void ResetChoixPce()
        {
            SelectCase = new Case(-1, -1, couleur.BLANC);
        }

        private void R_MouseLeftButtonDownRectangle(object sender, MouseButtonEventArgs e)
        {
            ClickGrid((sender as Rectangle).DataContext as Case);

            //throw new NotImplementedException();
        }

        private void R_MouseLeftButtonDownEllipse(object sender, MouseButtonEventArgs e)
        {
            //(sender as Ellipse).StrokeThickness = (sender as Ellipse).StrokeThickness > 0 ? 0 : 2;
            ClickGrid((sender as Ellipse).DataContext as Case);

            //throw new NotImplementedException();
        }

        private void SelectPce()
        {

        }

        private void ClickGrid(Case c)
        {
            if (SelectCase.Ocuppe) // si pièce sur la case sélectionner avant on bouge
            {
                try
                {
                    Damier.BougerPiece(SelectCase, c);
                    Actualiser();
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                    ResetChoixPce();
                }

            }
            else // aturement on assigne SelectCase
                SelectCase = c;

            //MessageBox.Show(c.Ligne.ToString() + " " + c.Colonne.ToString());
        }
    }
}
