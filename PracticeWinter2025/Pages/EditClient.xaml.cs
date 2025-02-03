using PracticeWinter2025.db;
using PracticeWinter2025.Scripts;
using System;
using System.Collections.Generic;
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

namespace PracticeWinter2025.Pages
{
    /// <summary>
    /// Логика взаимодействия для EditClient.xaml
    /// </summary>
    public partial class EditClient : Page
    {
        private string selectedImagePath;
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

            string imagesBD = client.PhotoPath.ToString();
            string result = imagesBD.Remove(0, 1);

            MessageBox.Show(result);
            string folderName = "PracticeWinter2025";
            string projectDirectory = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;
            string fullPath = System.IO.Path.Combine(projectDirectory, folderName, result);

            //ImagePhotoBT.Source = new BitmapImage(new Uri(Path.Combine(fullPath, Path.GetFileName(imagesBD))));
            ImagePhotoBT.Source = new BitmapImage(new Uri(fullPath, UriKind.Absolute));
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
                _client.RegistrationDate = _client.RegistrationDate;
                _client.ActiceClient = true;
                _client.PhotoPath = selectedImagePath;
                App.db.SaveChanges();
                MessageBox.Show("Данные успешно изменены");
                NavigationService.Navigate(new Pages.NavigationPage());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

        private void ImagePhotoBT_Click(object sender, RoutedEventArgs e)
        {
            FileManager fileManager = new FileManager();
            string relativePath = fileManager.SelectAndCopyImage();
            if (!string.IsNullOrEmpty(relativePath))
            {
                // Пример: отображение изображения в интерфейсе
                ImagePhotoBT.Source = new BitmapImage(new Uri(System.IO.Path.Combine(fileManager.targetFolder, Path.GetFileName(relativePath))));
                selectedImagePath = relativePath;
                fileManager.AddResourceToCsproj(relativePath);
                // Пример: вывод пути в консоль или сохранение в БД
                Console.WriteLine($"Image path to save: {relativePath}");
            }
            else
            {
                MessageBox.Show("Изображение не выбрано.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void Button_Click_Exit(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Pages.ClientList());
        }
    }
}
