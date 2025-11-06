using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEMO_AvaloniaApplication.ViewModels;

public class FileListViewModel
{
    public ObservableCollection<FileItemViewModel> Files { get; } = new();

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
            Files.Add(new FileItemViewModel(Remove)
            {
                FileName = file.Name,
                FilePath = file.DirectoryName ?? "",
                FileSize = FormatSize(file.Length),
                Date = file.LastWriteTime
            });
        }
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
