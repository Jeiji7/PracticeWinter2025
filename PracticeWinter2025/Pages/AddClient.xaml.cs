using Microsoft.Win32;
using PracticeWinter2025.db;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.ConstrainedExecution;
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
    /// Логика взаимодействия для AddClient.xaml
    /// </summary>
    public partial class AddClient : Page
    {
        private Client _client = new Client();
        private string selectedImagePath;
        public string folderName = "";
        public AddClient()
        {
            InitializeComponent();
            var gender = App.db.Gender.ToList();
            GenderCB.ItemsSource = gender.ToList();
            GenderCB.DisplayMemberPath = "Name";
        }

        private void Button_Click_Exit(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Pages.NavigationPage());
        }

        private void Button_Click_SaveData(object sender, RoutedEventArgs e)
        {
            if (GenderCB.SelectedIndex == -1)
            {
                MessageBox.Show("Пожалуйста, выберите пол клиента!");
                return; // Прерываем выполнение метода, если клиент не выбран
            }
            try
            {
                _client.GenderCode = GenderCB.SelectedIndex + 1;
                _client.FirstName = FirstNameTB.Text;
                _client.LastName = LastNameTB.Text;
                _client.Patronymic = PatronymicTB.Text;
                _client.Email = EmailTB.Text;
                _client.Phone = PhoneTB.Text;
                _client.Birthday = BirthDayDP.SelectedDate.Value;
                _client.RegistrationDate = DateTime.Now;
                _client.ActiceClient = true;
                _client.PhotoPath = selectedImagePath;
                App.db.Client.Add(_client);
                App.db.SaveChanges();
                MessageBox.Show("Данные успешно добавлены");
                NavigationService.Navigate(new Pages.NavigationPage());
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        
        }

        private void ImagePhotoBT_Click(object sender, RoutedEventArgs e)
        {
            //var imagesBD = App.db.ServicePhoto.FirstOrDefault(x => x.ID == ser.ServicePhotoID).PhotoPath.ToString();
            //string folderName = "StudPracticeAutumn2024/Resource";
            //string projectDirectory = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;
            //string fullPath = System.IO.Path.Combine(projectDirectory, folderName, imagesBD);

            ////Заменяем обратные слеши на прямые слеши
            //ImageService.Source = new BitmapImage(new Uri(fullPath, UriKind.Absolute));
            OpenFileDialog openFileDialog = new OpenFileDialog()
            {
                Filter = "*.png|*.png|*.jpeg|*.jpeg|*.jpg|*.jpg"
            };
            if (openFileDialog.ShowDialog().GetValueOrDefault())
            {

                selectedImagePath = openFileDialog.FileName;

                // Ищем индекс папки "Услуги школы" и обрезаем строку
                int index = selectedImagePath.IndexOf(folderName);

                if (index != -1)
                {
                    string result = selectedImagePath.Substring(index);
                    selectedImagePath = result;
                }
                ImagePhotoBT.Source = new BitmapImage(new Uri(openFileDialog.FileName));
            }
        }
    }
}
