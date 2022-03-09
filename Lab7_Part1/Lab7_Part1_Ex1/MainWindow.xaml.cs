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
            //Query the db for all customers
            var query = from customer in db.Customers
                where customer.Orders.Count >= 20
                select new
                {
                    Name = customer.CompanyName,
                    OrderCount = customer.Orders.Count
                };

            //Set the result set as the data source for the datagrid
            Ex4lbDisplay.ItemsSource = query.ToList();
        }

        private void Ex5Button_Click(object sender, RoutedEventArgs e)
        {
            //Query db for customers and return a new object with company, city, region and ordercount
            var query = from customer in db.Customers
                where customer.Orders.Count < 3
                select new
                {
                    Company = customer.CompanyName,
                    City = customer.City,
                    Region = customer.Region,
                    OrderCount = customer.Orders.Count
                };

            //Set result set as data source for the the ex 5 data grid
            Ex5lbDisplay.ItemsSource = query.ToList();
        }

        private void Ex6Button_Click(object sender, RoutedEventArgs e)
        {
            //Query the database for company names
            var query = from customer in db.Customers
                orderby customer.CompanyName
                select customer.CompanyName;

            //Assign the resultset as data source for the listbox
            Ex6lbDisplay.ItemsSource = query.ToList();
        }

        private void Ex6lbDisplay_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //Geta  reference to the selected item in the listbox
            string company = (string) Ex6lbDisplay.SelectedItem;
            //If the selected Item exists
            if (company != null)
            {
                //Query the db for the total order cost for selected company
                var query = (from detail in db.Order_Details
                    where detail.Order.Customer.CompanyName == company
                    select detail.UnitPrice * detail.Quantity).Sum();

                //Assign the result as a formatted string to the textblock
                Ex6TblkDetails.Text = string.Format("Total for supplier {0}\n\n{1:c}", company, query);
            }
        }
    }
}
