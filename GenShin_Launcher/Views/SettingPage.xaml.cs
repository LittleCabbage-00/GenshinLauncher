using Genshin_Launcher.ViewModels;
using MahApps.Metro.Controls.Dialogs;
using System.Windows.Controls;

namespace Genshin_Launcher.Views
{
    /// <summary>
    /// SettingPage.xaml 的交互逻辑
    /// </summary>
    public partial class SettingPage : UserControl
    {
        public SettingPage()
        {
            DataContext = new SettingsPageViewModel(DialogCoordinator.Instance);
            InitializeComponent();
        }
    }
}
