using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DEMO_AvaloniaApplication.ViewModels;

public class FileListViewModel
{
    public ObservableCollection<FileItemViewModel> Files { get; }

    public FileListViewModel()
    {
        Files = new();
#if DEBUG
        Files.Add(new FileItemViewModel("Example1.txt", "C:\\Documents", "15 КБ", DateTime.Now.AddDays(-1), Remove));
        Files.Add(new FileItemViewModel("Example2.jpg", "C:\\Pictures", "2.3 МБ", DateTime.Now.AddDays(-5), Remove, "Файл поврежден"));
        Files.Add(new FileItemViewModel("Example3.jpg", "C:\\Pictures\\Pictures\\Pictures\\Pictures\\Pictures\\Pictures\\Pictures\\Pictures", "23.3 МБ", DateTime.Now.AddDays(-5), Remove, "Файл поврежден"));
#endif
    }

    public void Remove(FileItemViewModel item)
    {
        Files.Remove(item);
    }

    public void LoadFromDirectory(string path)
    {
        Files.Clear();

        if (!Directory.Exists(path))
            return;

        var files = Directory.GetFiles(path)
            .Select(f => new FileInfo(f))
            .OrderByDescending(f => f.LastWriteTime);

        foreach (var file in files)
        {
            var fileItem = new FileItemViewModel(file.Name, file.DirectoryName ?? "", FormatSize(file.Length), file.LastWriteTime, Remove);
            Files.Add(fileItem);
        }

        Task.Run(() =>
        {
            Task.Delay(5000).Wait(); // Небольшая задержка для имитации асинхронной работы
            foreach (var file in files)
            {
                Task.Delay(500).Wait(); // Имитируем задержку на обработку каждого файла
                if (file.Length % 2 == 0)
                {
                    var item = Files.FirstOrDefault(f => f.FileName == file.Name);
                    if (item != null)
                    {
                        item.ErrorMessage = "Файл поврежден";
                    }
                }
            }
        });
    }

    private static string FormatSize(long bytes)
    {
        string[] sizes = { "Б", "КБ", "МБ", "ГБ" };
        double len = bytes;
        int order = 0;
        while (len >= 1024 && order < sizes.Length - 1)
        {
            order++;
            len /= 1024;
        }
        return $"{len:0.##} {sizes[order]}";
    }
}
