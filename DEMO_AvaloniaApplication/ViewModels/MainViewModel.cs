using System;

namespace DEMO_AvaloniaApplication.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    public FileListViewModel FileList { get; }

    public MainViewModel()
    {
        FileList = new FileListViewModel();

        // Пример: загрузим папку "Documents"
        var folder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        FileList.LoadFromDirectory(folder);
    }
}
