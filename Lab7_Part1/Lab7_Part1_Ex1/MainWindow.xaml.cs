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

namespace Lab7_Part1_Ex1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private NORTHWNDEntities db = new NORTHWNDEntities();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Ex1Button_Click(object sender, RoutedEventArgs e)
        {
            var query = from c in db.Categories
                join p in db.Products on c.CategoryName equals p.Category.CategoryName
                orderby c.CategoryName
                select new 
                {
                    Category = c.CategoryName,
                    Product = p.ProductName
                };

            Ex1lbDisplay.ItemsSource = query.ToList();
            Ex1TblkCount.Text = query.ToList().Count.ToString();

        }
    }
}
