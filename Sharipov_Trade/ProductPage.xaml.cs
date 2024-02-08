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
            var currentProduct = SharipovEntities.GetContext().Product.ToList();
            ProdList.ItemsSource = currentProduct;
        }
    }
}
