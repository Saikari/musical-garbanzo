using Avalonia.Controls;
using Avalonia.ReactiveUI;
using AvaloniaApplication8.ViewModels;
using ReactiveUI;
using System.Threading.Tasks;

namespace AvaloniaApplication8.Views
{
    public partial class MainWindow : ReactiveWindow<MainWindowViewModel>
    {
        public MainWindow()
        {
            InitializeComponent();
        }
    }
}