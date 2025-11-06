using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Windows.Input;

namespace DEMO_AvaloniaApplication.ViewModels;

public class FileItemViewModel : ObservableObject
{
    private string _fileName;
    private string _filePath;
    private string _fileSize;
    private DateTime _date;

    public string FileName
    {
        get => _fileName;
        set => SetProperty(ref _fileName, value);
    }

    public string FilePath
    {
        get => _filePath;
        set => SetProperty(ref _filePath, value);
    }

    public string FileSize
    {
        get => _fileSize;
        set => SetProperty(ref _fileSize, value);
    }

    public DateTime Date
    {
        get => _date;
        set => SetProperty(ref _date, value);
    }

    public ICommand DeleteCommand { get; }

    public FileItemViewModel(Action<FileItemViewModel> onDelete)
    {
        DeleteCommand = new RelayCommand(() => onDelete(this));
    }
}
