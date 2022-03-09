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

        private void Ex2Button_Click(object sender, RoutedEventArgs e)
        {
            var query = from p in db.Products
                orderby p.Category.CategoryName, p.ProductName
                select new
                {
                    Category = p.Category.CategoryName,
                    Product = p.ProductName
                };

            var results = query.ToList();

            Ex2lbDisplay.ItemsSource = results;

            Ex2TblkCount.Text = results.Count.ToString();
        }

        private void Ex3Button_Click(object sender, RoutedEventArgs e)
        {
            //return the total number of orders for product 7
            var queryl = 
                from detail in db.Order_Details
                where detail.ProductID == 7
                select detail;
            //return the total value of orders for product 7
            var query2 = from detail in db.Order_Details
                where detail.ProductID == 7
                select detail.UnitPrice * detail.Quantity;
            int numberOforders = queryl.ToList().Count;
            decimal totalValue = query2.Sum();
            decimal averageValue = query2.Average();
            Ex3TblkDetails.Text = string.Format("Total number of orders {0}\nValue of Orders {1:C}\nAverage Order Value {2:C}", numberOforders, totalValue, averageValue);
        }

        private void Ex4Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
