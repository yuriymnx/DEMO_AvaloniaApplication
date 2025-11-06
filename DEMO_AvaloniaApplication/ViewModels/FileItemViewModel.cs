using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Windows.Input;

namespace DEMO_AvaloniaApplication.ViewModels;

public class FileItemViewModel : ObservableObject
{
    private string _name;
    private string _path;
    private string _size;
    private string? _error;
    private DateTime _date;

    public string FileName
    {
        get => _name;
        set => SetProperty(ref _name, value);
    }

    public string FilePath
    {
        get => _path;
        set => SetProperty(ref _path, value);
    }

    public string FileSize
    {
        get => _size;
        set => SetProperty(ref _size, value);
    }
    public string? ErrorMessage
    {
        get => _error;
        set
        {
            SetProperty(ref _error, value);
            OnPropertyChanged(nameof(HasError));
        }
    }

    public DateTime Date
    {
        get => _date;
        set => SetProperty(ref _date, value);
    }

    public bool HasError
    {
        get => string.IsNullOrEmpty(ErrorMessage);
    }

    public ICommand DeleteCommand { get; }

    public FileItemViewModel(string name, string path, string size, DateTime date, Action<FileItemViewModel> onDelete, string? error = null)
    {
        _name = name;
        _path = path;
        _size = size;
        _error = error;
        _date = date;
        DeleteCommand = DeleteCommand = new RelayCommand(() => onDelete(this)); ;
    }
}
