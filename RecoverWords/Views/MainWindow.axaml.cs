using System;
using Avalonia.Controls;

namespace RecoverWords.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Closed += (sender, args) => ((IDisposable) DataContext).Dispose();
        }
    }
}
