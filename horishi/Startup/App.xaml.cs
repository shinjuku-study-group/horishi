using Startup.View;
using System.Windows;

namespace Startup
{
    /// <summary>
    /// App.xaml の相互作用ロジック
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            var mainWindow = new StartupWindowView();
            mainWindow.Show();
        }
    }
}
