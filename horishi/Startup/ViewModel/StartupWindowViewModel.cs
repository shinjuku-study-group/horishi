using Framework.Command;
using Framework.ViewModel;
using Startup.Properties;

namespace Startup.ViewModel
{
    /// <summary>
    /// スタートアップウィンドウのViewModel
    /// </summary>
    public sealed class StartupWindowViewModel : ViewModelBase
    {
        /// <summary>
        /// マインスイーパークリック時のコマンド
        /// </summary>
        private DelegateCommand clickMinesweeperButton;

        /// <summary>
        /// フリーセルクリック時のコマンド
        /// </summary>
        private DelegateCommand clickFreeCellButton;

        /// <summary>
        /// ブラックジャッククリック時のコマンド
        /// </summary>
        private DelegateCommand clickBlackJackButton;

        /// <summary>
        /// ウィンドウタイトル
        /// </summary>
        public string WindowTitle
        {
            get { return Resources.WindowTitle; }
        }

        /// <summary>
        /// マージン
        /// </summary>
        public double Margin
        {
            get { return 10; }
        }

        /// <summary>
        /// マインスイーパークリック時のコマンド
        /// </summary>
        /// <returns></returns>
        public DelegateCommand ClickMinesweeperButton
        {
            get
            {
                if (this.clickMinesweeperButton == null)
                {
                    this.clickMinesweeperButton = new DelegateCommand(this.ExecuteClickMinesweeper, () => true);
                }
                return this.clickMinesweeperButton;
            }
        }


        /// <summary>
        /// マインスイーパーを起動します
        /// </summary>
        private void ExecuteClickMinesweeper()
        {
            var minesweeper = new Minesweeper.View.MainWindowView();
            minesweeper.ShowDialog();
        }

        /// <summary>
        /// フリーセルクリック時のコマンド
        /// </summary>
        /// <returns></returns>
        public DelegateCommand ClickFreeCellButton
        {
            get
            {
                if (this.clickFreeCellButton == null)
                {
                    this.clickFreeCellButton = new DelegateCommand(this.ExecuteFreeCell, () => false);
                }
                return this.clickFreeCellButton;
            }
        }


        /// <summary>
        /// フリーセルを起動します
        /// </summary>
        private void ExecuteFreeCell()
        {
            var freecell = new FreeCell.View.MainWindowView();
            freecell.ShowDialog();
        }

        /// <summary>
        /// ブラックジャッククリック時のコマンド
        /// </summary>
        /// <returns></returns>
        public DelegateCommand ClickBlackJackButton
        {
            get
            {
                if (this.clickBlackJackButton == null)
                {
                    this.clickBlackJackButton = new DelegateCommand(this.ExecuteBlackJack, () => true);
                }
                return this.clickBlackJackButton;
            }
        }


        /// <summary>
        /// フリーセルを起動します
        /// </summary>
        private void ExecuteBlackJack()
        {
            var blackjack = new BlackJack.View.MainWindowView();
            blackjack.ShowDialog();
        }
    }
}
