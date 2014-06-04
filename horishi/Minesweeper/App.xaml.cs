using Minesweeper.Model;
using Minesweeper.View;
using Minesweeper.ViewModel;
using System.Windows;

namespace Minesweeper
{
    /// <summary>
    /// App.xaml の相互作用ロジック
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            var mainWindow = new MainWindowView();
            mainWindow.Show();
        }
    }
}
