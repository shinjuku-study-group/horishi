using Minesweeper.Common;
using Minesweeper.Model;
using Minesweeper.Properties;
using Minesweeper.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;

namespace Minesweeper.ViewModel
{
    /// <summary>
    /// メニューバーviewmodel
    /// </summary>
    public sealed class MenubarViewModel : ViewModelBase
    {
        #region フィールド
        /// <summary>
        /// 難易度クリックコマンド
        /// </summary>
        private DelegateCommand<MenuItem> gameLevelClickCommand;

        /// <summary>
        /// その他クリックコマンド
        /// </summary>
        private DelegateCommand<MenuItem> optionClickCommand;

        /// <summary>
        /// セルグリッドの初期化アクション
        /// </summary>
        private Action gridInitializeAction;

        /// <summary>
        /// ウィンドウを閉じるか
        /// </summary>
        private Action<bool> closeWindow;

        /// <summary>
        /// 難易度メニュー以下の子要素
        /// </summary>
        private List<GameLevelMenuItemViewModel> gameLevelItemSources = new List<GameLevelMenuItemViewModel>();

        /// <summary>
        /// その他メニュー以下の子要素
        /// </summary>
        private List<string> optionItemSources = new List<string>();
        #endregion

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public MenubarViewModel(Action gridInitializeAction, Action<bool> closeWindow)
        {
            this.gridInitializeAction = gridInitializeAction;
            this.closeWindow = closeWindow;
            var easy = new GameLevelMenuItemViewModel(Resources.MenubarItemGameLevelEasy);
            easy.IsChecked = true;
            this.gameLevelItemSources.Add(easy);
            this.gameLevelItemSources.Add(new GameLevelMenuItemViewModel(Resources.MenubarItemGameLevelNormal));
            this.gameLevelItemSources.Add(new GameLevelMenuItemViewModel(Resources.MenubarItemGameLevelHard));

            this.optionItemSources.Add(Resources.MenubarItemGameResults);
            this.optionItemSources.Add(Resources.MenubarItemCloseWindow);
        }

        #region プロパティ
        /// <summary>
        /// 難易度メニューテキスト
        /// </summary>
        public string GameLevelMenuHeader
        {
            get { return Resources.MenubarHeaderGameLevel; }
        }

        /// <summary>
        /// その他メニューテキスト
        /// </summary>
        public string OptionMenuHeader
        {
            get { return Resources.MenubarHeaderOption; }
        }

        /// <summary>
        /// 難易度メニュー以下の子要素
        /// </summary>
        public IEnumerable<GameLevelMenuItemViewModel> GameLevelItemSources
        {
            get { return gameLevelItemSources; }
        }

        /// <summary>
        /// その他メニュー以下の子要素
        /// </summary>
        public IEnumerable<string> OptionItemSources
        {
            get { return optionItemSources; }
        }
        #endregion

        #region 難易度選択コマンド
        /// <summary>
        /// 左クリックコマンド
        /// </summary>
        public DelegateCommand<MenuItem> GameLevelClickCommand
        {
            get
            {
                if (this.gameLevelClickCommand == null)
                {
                    this.gameLevelClickCommand = new DelegateCommand<MenuItem>(ExecuteGameLevelClick, CanExecuteGamaLevelClick);
                }
                return this.gameLevelClickCommand;
            }
        }

        /// <summary>
        /// 左クリック実行時の処理
        /// </summary>
        /// <param name="item">対象のメニュー</param>
        private void ExecuteGameLevelClick(MenuItem item)
        {
            var preChecked = this.GameLevelItemSources.Where(i => i.IsChecked).First();
            preChecked.IsChecked = false;
            string header = item.Header.ToString();
            if (header == Resources.MenubarItemGameLevelEasy)
            {
                this.ChangeGameLevelMenuItem(Resources.MenubarItemGameLevelEasy, new EasyLevel());
            }
            if (header == Resources.MenubarItemGameLevelNormal)
            {
                this.ChangeGameLevelMenuItem(Resources.MenubarItemGameLevelNormal, new NormalLevel());
            }
            if (header == Resources.MenubarItemGameLevelHard)
            {
                this.ChangeGameLevelMenuItem(Resources.MenubarItemGameLevelHard, new HardLevel());
            }
        }

        /// <summary>
        /// メニュークリックの変更を設定する
        /// </summary>
        /// <param name="header">変更する難易度</param>
        /// <param name="level">ゲーム難易度</param>
        private void ChangeGameLevelMenuItem(string header, GameLevel level)
        {
            var currentChecked = this.GameLevelItemSources.Where(i => i.Text == header).First();
            currentChecked.IsChecked = true;
            GameLogic.CreateNextInstance(level);
            this.gridInitializeAction.Invoke();
        }

        /// <summary>
        /// メニューがクリックできるか
        /// </summary>
        /// <param name="item">対象のメニュー</param>
        /// <returns>メニューがクリックできるか</returns>
        private bool CanExecuteGamaLevelClick(MenuItem item)
        {
            return !item.IsChecked;
        } 
        #endregion

        #region その他コマンド

        /// <summary>
        /// 左クリックコマンド
        /// </summary>
        public DelegateCommand<MenuItem> OptionClickCommand
        {
            get
            {
                if (this.optionClickCommand == null)
                {
                    this.optionClickCommand = new DelegateCommand<MenuItem>(ExecuteOptionClick, item => true);
                }
                return this.optionClickCommand;
            }
        }

        /// <summary>
        /// 左クリック実行時の処理
        /// </summary>
        /// <param name="item">対象のメニュー</param>
        private void ExecuteOptionClick(MenuItem item)
        {
            string header = item.Header.ToString();
            if(header == Resources.MenubarItemCloseWindow)
            {
                this.closeWindow(true);
            }
            if(header == Resources.MenubarItemGameResults)
            {
                var resultView = new GameResultsWindowView();
                resultView.ShowDialog();
            }
        }
        #endregion
    }
}
