using Framework.Command;
using Framework.Common.Serialize;
using Framework.ViewModel;
using Minesweeper.Model.GameResult;
using Minesweeper.Properties;
using System.Collections.Generic;

namespace Minesweeper.ViewModel
{
    /// <summary>
    /// 戦歴画面のViewModel
    /// </summary>
    public sealed class GameResultsWindowViewModel : ViewModelBase
    {
        #region フィールド
        /// <summary>
        /// 戦歴viewmodel
        /// </summary>
        private List<GameResultViewModel> gameResults = new List<GameResultViewModel>();

        /// <summary>
        /// 選択中のタブ
        /// </summary>
        private GameResultViewModel selectedTab;

        /// <summary>
        /// ウィンドウを閉じるか
        /// </summary>
        private bool closeWindow = false;

        /// <summary>
        /// 閉じるコマンド
        /// </summary>
        private DelegateCommand closeCommand;

        /// <summary>
        /// リセットコマンド
        /// </summary>
        private DelegateCommand resetCommand;

        private TotalGameResult resultModel;

        #endregion
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public GameResultsWindowViewModel()
        {
            this.gameResults.Add(new GameResultViewModel(Resources.MenubarItemGameLevelEasy));
            this.gameResults.Add(new GameResultViewModel(Resources.MenubarItemGameLevelNormal));
            this.gameResults.Add(new GameResultViewModel(Resources.MenubarItemGameLevelHard));
        }

        #region プロパティ

        /// <summary>
        /// ウィンドウ名
        /// </summary>
        public string WindowTitle
        {
            get { return Resources.GameResultWindowTitle; }
        }

        /// <summary>
        /// 難易度ごとの戦歴戦歴
        /// </summary>
        public IEnumerable<GameResultViewModel> GameResultsItemsSource
        {
            get { return this.gameResults; }
        }

        /// <summary>
        /// リセットボタンの表示文字列
        /// </summary>
        public string ResultButtonContent
        {
            get
            {
                return Resources.ResetButtonContext;
            }
        }

        /// <summary>
        /// 選択中のタブ
        /// </summary>
        public GameResultViewModel SelectedTab
        {
            get
            {
                return this.selectedTab;
            }
            set
            {
                this.selectedTab = value;
                this.ReadDataFromXml();
                if (selectedTab.Level == Resources.MenubarItemGameLevelEasy)
                {
                    this.selectedTab.WinCount = this.resultModel.Easy.WinCount.ToString();
                    this.selectedTab.LooseCount = this.resultModel.Easy.LooseCount.ToString();
                }
                if (selectedTab.Level == Resources.MenubarItemGameLevelNormal)
                {
                    this.selectedTab.WinCount = this.resultModel.Normal.WinCount.ToString();
                    this.selectedTab.LooseCount = this.resultModel.Normal.LooseCount.ToString();
                }
                if (selectedTab.Level == Resources.MenubarItemGameLevelHard)
                {
                    this.selectedTab.WinCount = this.resultModel.Hard.WinCount.ToString();
                    this.selectedTab.LooseCount = this.resultModel.Hard.LooseCount.ToString();
                }
                base.NotifyChanged("SelectedTab");
            }
        }

        /// <summary>
        /// 閉じるボタンの表示文字列
        /// </summary>
        public string CloseButtonContent
        {
            get
            {
                return Resources.CloseButtonContext;
            }
        }

        /// <summary>
        /// ウィンドウを閉じるか
        /// </summary>
        public bool CloseWindow
        {
            get
            {
                return this.closeWindow;
            }
            set
            {
                this.closeWindow = value;
                base.NotifyChanged("CloseWindow");
            }
        }

        /// <summary>
        /// マージン
        /// </summary>
        public double Margin
        {
            get { return ControlSizeDefinition.ContainerMargin * 2; }
        }
        #endregion

        /// <summary>
        /// リセットコマンド
        /// </summary>
        public DelegateCommand ResetCommand
        {
            get
            {
                if (this.resetCommand == null)
                {
                    this.resetCommand = new DelegateCommand(ExexuteReset, () => true);
                }
                return this.resetCommand;
            }
        }

        /// <summary>
        /// 戦歴をリセットします
        /// </summary>
        private void ExexuteReset()
        {
            var serializer = new XmlSerializer<TotalGameResult>();
            var result = serializer.Read();
            if (this.SelectedTab.Level == Resources.MenubarItemGameLevelEasy)
            {
                result.Easy.WinCount = 0;
                result.Easy.LooseCount = 0;
            }
            if (this.SelectedTab.Level == Resources.MenubarItemGameLevelNormal)
            {
                result.Normal.WinCount = 0;
                result.Normal.LooseCount = 0;
            }
            if (this.SelectedTab.Level == Resources.MenubarItemGameLevelHard)
            {
                result.Hard.WinCount = 0;
                result.Hard.LooseCount = 0;
            }
            serializer.Write(result);
            this.resultModel = result;
            this.SelectedTab = this.selectedTab;
        }

        /// <summary>
        /// 閉じるコマンド
        /// </summary>
        public DelegateCommand Close
        {
            get
            {
                if (this.closeCommand == null)
                {
                    this.closeCommand = new DelegateCommand(ExecuteClose, () => true);
                }
                return this.closeCommand;
            }
        }

        /// <summary>
        /// 閉じます
        /// </summary>
        private void ExecuteClose()
        {
            this.CloseWindow = true;
        }

        private void ReadDataFromXml()
        {
            if (this.resultModel == null)
            {
                this.resultModel = new XmlSerializer<TotalGameResult>().Read();
            }
        }
    }
}
