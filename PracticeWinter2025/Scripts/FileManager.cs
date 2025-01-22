using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Linq;

namespace PracticeWinter2025.Scripts
{
    public class FileManager
    {
        string folderName = "PracticeWinter2025/Clients";
        string projectDirectory = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;
        public string targetFolder;
        public FileManager()
        {
            // Формируем полный путь к целевой папке
            targetFolder = Path.Combine(projectDirectory, folderName);

            // Создаем целевую папку, если её нет
            if (!Directory.Exists(targetFolder))
            {
                Directory.CreateDirectory(targetFolder);
            }
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

                try
                {
                    // Копируем файл в целевую папку
                    File.Copy(sourcePath, destinationPath, overwrite: true);
                    
                    // Сообщаем, что нужно обновить проект
                    Console.WriteLine("Файл успешно скопирован. Обновите проект в Visual Studio!");

                    // Возвращаем относительный путь
                    return $"/Clients/{fileName}";
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка при копировании файла: {ex.Message}");
                }
            }

            return null;
        }
        public void AddResourceToCsproj(string filePath)
        {
            string projectDirectory = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;
            string projectFilePath = Path.Combine(projectDirectory, $"{Path.GetFileName(projectDirectory)}.csproj");

            if (!File.Exists(projectFilePath))
            {
                Console.WriteLine("Файл проекта (.csproj) не найден.");
                return;
            }

            try
            {
                XDocument projectFile = XDocument.Load(projectFilePath);

                // Находим или создаем ItemGroup
                XElement itemGroup = projectFile.Root.Elements("ItemGroup")
                    .FirstOrDefault(e => e.Elements("Resource").Any())
                    ?? new XElement("ItemGroup");

                if (itemGroup.Parent == null)
                {
                    projectFile.Root.Add(itemGroup); // Добавляем ItemGroup в корень, если его не было
                }

                string relativePath = filePath.Replace("\\", "/"); // Убираем лишние обратные слэши

                // Проверяем, существует ли уже ресурс
                bool fileExists = itemGroup.Elements("Resource").Any(e =>
                    e.Attribute("Include")?.Value == relativePath);

                if (!fileExists)
                {
                    XElement resourceElement = new XElement("Resource", new XAttribute("Include", relativePath));
                    itemGroup.Add(resourceElement);

                    projectFile.Save(projectFilePath);
                    Console.WriteLine($"Файл добавлен в проект как ресурс: {relativePath}");
                }
                else
                {
                    Console.WriteLine($"Файл уже существует в проекте как ресурс: {relativePath}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при добавлении файла в проект: {ex.Message}");
            }
        }
    }
}
