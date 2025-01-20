using PracticeWinter2025.db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
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

namespace PracticeWinter2025.Pages
{
    /// <summary>
    /// Логика взаимодействия для Authorization.xaml
    /// </summary>
    public partial class Authorization : Page
    {
        public Authorization()
        {
            InitializeComponent();
        }

        private void Button_Click_Enter(object sender, RoutedEventArgs e)
        {
            string name = LoginTB.Text.Trim();
            string firstName = PassWordPS.Password.Trim();

            if (LoginTB.Text == "0000" && PassWordPS.Password == "0000")
                NavigationService.Navigate(new Pages.NavigationPage());
            else
            {
                MessageBox.Show("Мы не нашли ваши данные в системе, попробуйте зайти снова");
                LoginTB.Text = "";
                PassWordPS.Password = "";
            }
        }
    }
}
