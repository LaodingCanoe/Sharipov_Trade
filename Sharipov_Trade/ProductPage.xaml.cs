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

namespace Sharipov_Trade
{
    /// <summary>
    /// Логика взаимодействия для ProductPage.xaml
    /// </summary>
    public partial class ProductPage : Page
    {
        public ProductPage(int userID)
        {
            InitializeComponent();            
            var currentUser = SharipovEntities.GetContext().User.ToList();
            if(userID >= 0)
            {
                userRoleTB.Text = currentUser[userID].UserSurname.ToString() + " " + currentUser[userID].UserName.ToString() + " " + currentUser[userID].UserPatronymic.ToString();
            }
            else
            {
                userRoleTB.Text = "Гостевой режим";
            }
            ProdList.ItemsSource = SharipovEntities.GetContext().Product.ToList();
            SortCB.SelectedIndex = 0;
            FiterCB.SelectedIndex = 0;
        }

        public void Update()
        {
            var currentProduct = SharipovEntities.GetContext().Product.ToList();   
            if(FiterCB.SelectedIndex == 1)
            {
                currentProduct.Where(p => p.ProductDiscountAmount < 10).ToList();
            }
            if(FiterCB.SelectedIndex == 2)
            {
                currentProduct.Where(p => p.ProductDiscountAmount > 10 && p.ProductDiscountAmount < 15).ToList();
            }
            if (FiterCB.SelectedIndex == 3)
            {
                currentProduct.Where(p => p.ProductDiscountAmount > 15).ToList();
            }
            if(SortCB.SelectedIndex == 1) 
            {
                currentProduct = currentProduct.OrderBy(p => p.ProductCost).ToList();
            }
            if (SortCB.SelectedIndex == 2)
            {
                currentProduct = currentProduct.OrderByDescending(p => p.ProductCost).ToList();
            }
            ProdList.ItemsSource = currentProduct; 
        }

        private void SortCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Update();
        }

        private void FiterCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Update();
        }
    }
}
