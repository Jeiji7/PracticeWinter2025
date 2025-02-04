using PracticeWinter2025.db;
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
    /// Логика взаимодействия для ServiceListPage.xaml
    /// </summary>
    public partial class ServiceListPage : Page
    {
        private Service service;
        public ServiceListPage()
        {
            InitializeComponent();
            ListServiceLV.ItemsSource = App.db.Service.Where(x => x.ActiveService == true).ToList();
        }
        private void Button_Click_Add(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Pages.AddService());
        }

        private void Button_Click_Edit(object sender, RoutedEventArgs e)
        {
            if (ListServiceLV.SelectedIndex != -1)
            {
                var selecPart = (Service)ListServiceLV.SelectedItem;
                EditService editPage = new EditService(selecPart);
                NavigationService.Navigate(editPage);
            }
            else
            {
                MessageBox.Show("Вы не выбрали сотрудника для изменения!");
            }
        }

        private void Button_Click_Delete(object sender, RoutedEventArgs e)
        {
            if (ListServiceLV.SelectedIndex != -1)
            {
                service = (Service)ListServiceLV.SelectedItem;
                service.ActiveService = false;
                App.db.SaveChanges();
                MessageBox.Show($"Клиент успешно удалён");
                NavigationService.Navigate(new Pages.ServiceListPage());
            }
            else
            {
                MessageBox.Show("Вы не выбрали сотрудника для удаления!");

            }
        }

        private void Button_Click_Exit(object sender, RoutedEventArgs e)
        {

            NavigationService.Navigate(new Pages.NavigationPage());
        }
    }
}
