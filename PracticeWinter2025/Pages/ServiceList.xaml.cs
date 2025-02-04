using PracticeWinter2025.db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
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
    /// Логика взаимодействия для ServiceList.xaml
    /// </summary>
    public partial class ServiceList : Page
    {
        
        public ServiceList()
        {
            InitializeComponent();
            //ListRecordServiceLV.ItemsSource = App.db.ClientService.ToList();
            var result = from clientService in App.db.ClientService
                         join client in App.db.Client
                         on clientService.ClientID equals client.ID
                         where client.ActiceClient == true // Условие для проверки клиента
                         select new
                         {
                             clientService.ID,
                             client.FirstName, // Имя клиента (можно добавить любые данные из обеих таблиц)
                             client.Phone, // Имя клиента (можно добавить любые данные из обеих таблиц)
                             clientService.Service.Title,
                             clientService.StartTime
                         };

            ListRecordServiceLV.ItemsSource = result.ToList(); // Устанавливаем источник данных для ListView

        }
        private void Button_Click_Exit(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Pages.NavigationPage());
        }

       
    }
}
