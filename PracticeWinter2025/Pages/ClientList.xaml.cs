using PracticeWinter2025.db;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.IO;
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
            //ListClientLV.ItemsSource = App.db.Client.Where(x => x.ActiceClient == true).ToList();
            var clients = App.db.Client.Where(x => x.ActiceClient == true).ToList();
            // Проверяем пути к файлам
            foreach (var client in clients)
            {
                if (!string.IsNullOrEmpty(client.PhotoPath))
                {
                    string absolutePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, client.PhotoPath.TrimStart('/'));
                    if (!File.Exists(absolutePath))
                    {
                        client.PhotoPath = "/Clients/default.jpg"; // Заглушка, если фото не найдено
                    }
                }
            }

            ListClientLV.ItemsSource = clients;

            var gender = App.db.Gender.ToList();
            gender.Insert(0, new Gender { Code = 0, Name = "Все" });
            GenderCB.ItemsSource = gender.ToList();
            GenderCB.DisplayMemberPath = "Name";
            GenderCB.SelectedIndex = 0;
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

        private void GenderCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (GenderCB.SelectedIndex)
            {
                case 0:
                    ListClientLV.ItemsSource = App.db.Client.ToList();
                    break;
                case 1:
                    ListClientLV.ItemsSource = App.db.Client.Where(x => x.Gender.Code == 1).ToList();
                    break;
                case 2:
                    ListClientLV.ItemsSource = App.db.Client.Where(x => x.Gender.Code == 2).ToList();
                    break;
            }
        }

        public void SortGender()
        {
            switch (GenderCB.SelectedIndex)
            {
                case 0:
                    ListClientLV.ItemsSource = App.db.Client.Where(x=> x.LastName.StartsWith(SearchNameTB.Text)).ToList();
                    break;
                case 1:
                    ListClientLV.ItemsSource = App.db.Client.Where(x => x.Gender.Code == 1 && x.LastName.StartsWith(SearchNameTB.Text)).ToList();
                    break;
                case 2:
                    ListClientLV.ItemsSource = App.db.Client.Where(x => x.Gender.Code == 2  && x.LastName.StartsWith(SearchNameTB.Text)).ToList();
                    break;
            }
        }
        private void TextBox_TextChanged_SearchName(object sender, TextChangedEventArgs e)
        {
            ListClientLV.ItemsSource = App.db.Client.Where(i => i.LastName.StartsWith(SearchNameTB.Text) && i.GenderCode == GenderCB.SelectedIndex).ToList();
        }
    }

}
