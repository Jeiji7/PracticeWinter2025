using Microsoft.Win32;
using System;
using System.IO;
using System.Windows;

namespace PracticeWinter2025.Scripts
{
    public class FileManager
    {
        string folderName = "PracticeWinter2025/Clients";
        string projectDirectory = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;
        string binDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Clients"); // Путь до bin/Debug/Clients
        public string targetFolder;

        public FileManager()
        {
            targetFolder = Path.Combine(projectDirectory, folderName);

            // Создание папок, если их нет
            if (!Directory.Exists(targetFolder))
                Directory.CreateDirectory(targetFolder);

            if (!Directory.Exists(binDirectory))
                Directory.CreateDirectory(binDirectory);
        }

        public string SelectAndCopyImage()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Image Files|*.png;*.jpeg;*.jpg"
            };

            if (openFileDialog.ShowDialog().GetValueOrDefault())
            {
                string sourcePath = openFileDialog.FileName;
                string fileName = Path.GetFileName(sourcePath);
                string destinationPath = Path.Combine(targetFolder, fileName);
                string binPath = Path.Combine(binDirectory, fileName);

                try
                {
                    // Копирование в папку проекта
                    File.Copy(sourcePath, destinationPath, overwrite: true);
                    // Копирование в bin/Debug/Clients
                    File.Copy(sourcePath, binPath, overwrite: true);

                    Console.WriteLine("Файл успешно скопирован в обе папки!");

                    return $"/Clients/{fileName}";
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка при копировании файла: {ex.Message}");
                }
            }

            return null;
        }
    }
}
