using System;
using System.IO;

class Utility
{
    static void Main(string[] args)
    {
        // Проверяем, что аргументов достаточно
        if (args.Length != 3)
        {
            Console.WriteLine("Использование: utility.exe <путь> <расширение> <текст>");
            return;
        }

        // Получаем путь, расширение файлов и текст для поиска из аргументов
        string path = args[0];
        string fileExtension = args[1];
        string searchText = args[2];

        // Проверяем, существует ли указанная директория
        if (!Directory.Exists(path))
        {
            Console.WriteLine($"Директория '{path}' не найдена.");
            return;
        }

        // Запускаем рекурсивный поиск файлов
        SearchFiles(path, fileExtension, searchText);
    }

    // Метод для поиска файлов
    static void SearchFiles(string path, string fileExtension, string searchText)
    {
        try
        {
            // Получаем все файлы с указанным расширением в текущей директории
            foreach (string file in Directory.GetFiles(path, $"*.{fileExtension}"))
            {
                // Проверяем содержимое файла на наличие искомого текста
                if (FileContainsText(file, searchText))
                {
                    Console.WriteLine($"Файл: {file}");
                }
            }

            // Рекурсивно проходим по всем подкаталогам
            foreach (string directory in Directory.GetDirectories(path))
            {
                SearchFiles(directory, fileExtension, searchText);
            }
        }
        catch (UnauthorizedAccessException ex)
        {
            Console.WriteLine($"Нет доступа к директории: {path}. Сообщение: {ex.Message}");
        }
    }

    // Метод для проверки, содержит ли файл искомый текст
    static bool FileContainsText(string filePath, string searchText)
    {
        try
        {
            // Читаем содержимое файла
            string content = File.ReadAllText(filePath);
            return content.Contains(searchText);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при чтении файла: {filePath}. Сообщение: {ex.Message}");
            return false;
        }
    }
}
//.\utility.exe C:\Games txt hello
