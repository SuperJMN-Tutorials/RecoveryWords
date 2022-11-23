using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using RecoverWords.ViewModels;
using RecoverWords.Views;

namespace RecoverWords
{
    public partial class App : Application
    {
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public override void OnFrameworkInitializationCompleted()
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                desktop.MainWindow = new MainWindow
                {
                    DataContext = new RecoveryWordsViewModel(new[]
                    {
                        "leader",
                        "society",
                        "eavesdrop",
                        "charge",
                        "tax",
                        "pavement",
                        "colon",
                        "multiply",
                        "net",
                        "fork",
                        "feminine",
                        "dominant"
                    })
                };
            }

            base.OnFrameworkInitializationCompleted();
        }
    }
}
