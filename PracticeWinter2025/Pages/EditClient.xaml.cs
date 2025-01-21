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
    /// Логика взаимодействия для EditClient.xaml
    /// </summary>
    public partial class EditClient : Page
    {
        private Client _client;
        public EditClient(Client client)
        {
            InitializeComponent();
            _client = client;
            var gender = App.db.Gender.ToList();
            GenderCB.ItemsSource = gender;
            GenderCB.DisplayMemberPath = "Name";
            FirstNameTB.Text = client.FirstName;
            LastNameTB.Text = client.LastName;
            PatronymicTB.Text = client.Patronymic;
            EmailTB.Text = client.Email;
            PhoneTB.Text = client.Phone;
            BirthDayDP.Text = client.Birthday.ToString();
            GenderCB.SelectedIndex = client.GenderCode - 1;
        }

        private void Button_Click_SaveData(object sender, RoutedEventArgs e)
        {

        }

        private void ImagePhotoBT_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_Exit(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Pages.ClientList());
        }
    }
}
