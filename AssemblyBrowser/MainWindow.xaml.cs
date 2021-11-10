using System.Windows;
using Microsoft.Win32;

namespace AssemblyBrowser
{
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new ApplicationViewModel();
        }
    }
}