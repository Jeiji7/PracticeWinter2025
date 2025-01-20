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
    /// Логика взаимодействия для RecordService.xaml
    /// </summary>
    public partial class RecordService : Page
    {
        private ClientService _clientService = new ClientService();
        public RecordService()
        {
            InitializeComponent();
            var client = App.db.Client.ToList();
            ClientCB.ItemsSource = client.ToList();
            ClientCB.DisplayMemberPath = "FirstName";
            var service = App.db.Service.ToList();
            ServiceCB.ItemsSource = service.ToList();
            ServiceCB.DisplayMemberPath = "Title";

        }

        private void Button_Click_Exit(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Pages.NavigationPage());
        }

        private void Button_Click_Save(object sender, RoutedEventArgs e)
        {
            if (ClientCB.SelectedIndex == -1 || ServiceCB.SelectedIndex == -1)
            {
                MessageBox.Show("Пожалуйста, выберите клиента или услугу.");
                return; // Прерываем выполнение метода, если клиент не выбран
            }
            try
            {
                _clientService.ClientID = (ClientCB.SelectedIndex + 1);
                _clientService.ServiceID = ServiceCB.SelectedIndex + 1;
                _clientService.StartTime = DateRecord.SelectedDate.Value;
                App.db.ClientService.Add(_clientService);
                App.db.SaveChanges();
                MessageBox.Show("Данные успешно добавлены");
                NavigationService.Navigate(new Pages.NavigationPage());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
