using Avalonia.Controls;
using DEMO_AvaloniaApplication.ViewModels;

namespace DEMO_AvaloniaApplication;

public partial class FileListView : UserControl
{
    public FileListView()
    {
        InitializeComponent();
        DataContext = new FileListViewModel();
    }
}