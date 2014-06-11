using BlackJack.Model;
using BlackJack.Properties;
using Framework.Command;
using Framework.ViewModel;

namespace BlackJack.ViewModel
{
    /// <summary>
    /// 掛け金入力ダイアログViewModel
    /// </summary>
    public sealed class BetCoinsInputDialogViewModel : ViewModelBase
    {
        #region フィールド
        /// <summary>
        /// 掛け金
        /// </summary>
        private string betCoins = "1";

        /// <summary>
        /// ウィンドウを閉じるか
        /// </summary>
        private bool closeWindow = false;

        /// <summary>
        /// OKコマンド
        /// </summary>
        private DelegateCommand clickOkCommand;

        /// <summary>
        /// 閉じるコマンド
        /// </summary>
        private DelegateCommand closeCommand; 
        #endregion

        #region プロパティ
        /// <summary>
        /// ウィンドウタイトル
        /// </summary>
        public string WindowTitle
        {
            get { return Resources.BetCoinsInputDialogView_WindowTitle; }
        }

        /// <summary>
        /// マージン
        /// </summary>
        public double Margin
        {
            get { return SizeDefinition.Margin; }
        }

        /// <summary>
        /// メインメッセージ
        /// </summary>
        public string MainMessage
        {
            get { return Resources.BetCoinsInputDialogView_MainMessage; }
        }

        /// <summary>
        /// 掛け金
        /// </summary>
        public string BetCoins
        {
            get
            {
                return this.betCoins;
            }
            set
            {
                this.betCoins = value;
                base.NotifyChanged("BetCoins");
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
        
        #endregion

        #region OKコマンド

        /// <summary>
        /// OKコマンド
        /// </summary>
        public DelegateCommand ClickOkCommand
        {
            get
            {
                if (this.clickOkCommand == null)
                {
                    this.clickOkCommand = new DelegateCommand(this.ExecuteClickOk, this.CanExecuteClickOk);
                }
                return this.clickOkCommand;
            }
        }

        /// <summary>
        /// OKボタンを実行します
        /// </summary>
        private void ExecuteClickOk()
        {
            var val = int.Parse(this.BetCoins);
            // GameLogic.CreateInstance(val);
        }

        /// <summary>
        /// OKボタンを実行できるかどうか
        /// </summary>
        /// <returns>OKボタンを実行できるかどうか</returns>
        private bool CanExecuteClickOk()
        {
            int val;
            return int.TryParse(this.BetCoins, out val);
        }
        #endregion

        #region 閉じるコマンド
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
        #endregion
    }
}
