using PracticeWinter2025.db;
using System;
using System.Collections.Generic;
using System.Data.Common;
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
    /// Логика взаимодействия для ClientList.xaml
    /// </summary>
    public partial class ClientList : Page
    {
        private Client client;
        public ClientList()
        {
            InitializeComponent();
            ListClientLV.ItemsSource = App.db.Client.Where(x => x.ActiceClient == true).ToList();
        }

        private void Button_Click_Exit(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Pages.NavigationPage());
        }


        private void Button_Click_Delete(object sender, RoutedEventArgs e)
        {
            if (ListClientLV.SelectedIndex != -1)
            {
                client = (Client)ListClientLV.SelectedItem;
                client.ActiceClient = false;
                App.db.SaveChanges();
                MessageBox.Show($"Клиент успешно удалён");
                NavigationService.Navigate(new Pages.ClientList());
            }
            else
            {
                MessageBox.Show("Вы не выбрали сотрудника для удаления!");

            }
        }

        private void Button_Click_Edit(object sender, RoutedEventArgs e)
        {
            if (ListClientLV.SelectedIndex != -1)
            {
                var selecPart = (Client)ListClientLV.SelectedItem;
                EditClient editPage = new EditClient(selecPart);
                NavigationService.Navigate(editPage);
            }
            else
            {
                MessageBox.Show("Вы не выбрали сотрудника для изменения!");
            }
        }
    }
}
