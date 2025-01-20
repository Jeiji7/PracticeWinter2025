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

namespace PracticeWinter2025.Pages
{
    /// <summary>
    /// Логика взаимодействия для NavigationPage.xaml
    /// </summary>
    public partial class NavigationPage : Page
    {
        public NavigationPage()
        {
            InitializeComponent();
        }


        private void Button_Click_Exit(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Pages.Authorization());
        }

        private void Button_Click_ClientList(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Pages.ClientList());
        }

        private void Button_Click_RecordView(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Pages.ServiceList());
        }

        private void Button_Click_Record(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Pages.RecordService());
        }

        private void Button_Click_AddClient(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Pages.AddClient());
        }
    }
}
