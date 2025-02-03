using Microsoft.Win32;
using PracticeWinter2025.db;
using PracticeWinter2025.Scripts;
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

namespace PracticeWinter2025.Pages
{
    /// <summary>
    /// Логика взаимодействия для AddClient.xaml
    /// </summary>
    public partial class AddClient : Page
    {
        private Client _client = new Client();
        private string selectedImagePath;

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
                ImagePhotoBT.Source = new BitmapImage(new Uri(Path.Combine(fileManager.targetFolder, Path.GetFileName(relativePath))));
                selectedImagePath = relativePath;

                //string projectFolderPath = @"C:\Path\To\Your\Project"; // Путь к папке проекта
                //string filePath = @"Clients\photoOne.png";             // Относительный путь к файлу
                //string projectFileName = "PracticeWinter2025.csproj";  // Имя csproj файла

                ////fileManager.AddResourceToCsproj(filePath, projectFolderPath, projectFileName);

                //// Пример: вывод пути в консоль или сохранение в БД
                //Console.WriteLine($"Image path to save: {relativePath}");
            }
            else
            {
                MessageBox.Show("Изображение не выбрано.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
