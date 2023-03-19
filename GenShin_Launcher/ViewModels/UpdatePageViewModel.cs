using MahApps.Metro.Controls.Dialogs;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Windows.Input;
using Genshin_Launcher.Models;
using Genshin_Launcher.Service.IService;
using Genshin_Launcher.Service;
using Genshin_Launcher.Helper;

namespace Genshin_Launcher.ViewModels
{
    /// <summary>
    /// 更新页面的ViewModel 
    /// 集成了更新页面的UI更新绑定
    /// </summary>
    public class UpdatePageViewModel : ObservableObject
    {
        public IDialogCoordinator dialogCoordinator;
        public UpdatePageViewModel(IDialogCoordinator instance)
        {
            DFC = new();
            dialogCoordinator = instance;
            UpadteService = new UpdateService();
            UpdateRunCommand = new RelayCommand(RunUpdate);
            ViewControlVisibility = "Hidden";
        }

        private IUpdateService UpadteService { get; set; }


        private DownloadHelper _DFC;
        public DownloadHelper DFC { get => _DFC; set => SetProperty(ref _DFC, value); }


        public LanguageModel languages { get => App.Current.Language; }
        public string Notify
        {
            get => App.Current.UpdateObject.Content;
        }
        public string Title
        {
            get => App.Current.UpdateObject.Title;
        }

        private bool _ButtonIsEnabled = true;
        public bool ButtonIsEnabled
        {
            get => _ButtonIsEnabled;
            set => SetProperty(ref _ButtonIsEnabled, value);
        }

        private string _ViewControlVisibility;
        public string ViewControlVisibility
        {
            get => _ViewControlVisibility;
            set => SetProperty(ref _ViewControlVisibility, value);
        }

        private bool _UseGlobalUrlCheck;
        public bool UseGlobalUrlCheck
        {
            get => _UseGlobalUrlCheck;
            set => SetProperty(ref _UseGlobalUrlCheck, value);
        }

        public ICommand UpdateRunCommand { get; set; }
        private void RunUpdate() { UpadteService.UpdateRun(this); }
    }
}
