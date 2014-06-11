using BlackJack.Model;
using BlackJack.Properties;
using BlackJack.View;
using Common.Trump;
using Framework.Command;
using Framework.ViewModel;
using System.Collections.Generic;
using System.IO;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace BlackJack.ViewModel
{
    /// <summary>
    /// メイン画面のViewModel
    /// </summary>
    public sealed partial class MainWindowViewModel : ViewModelBase
    {
        #region フィールド
        /// <summary>
        /// スタートボタンクリック時のコマンド
        /// </summary>
        private DelegateCommand clickStartButton;

        /// <summary>
        /// ヒットボタンクリック時のコマンド
        /// </summary>
        private DelegateCommand clickHitButton;

        /// <summary>
        /// スタンドボタンクリック時のコマンド
        /// </summary>
        private DelegateCommand clickStandButton;

        /// <summary>
        /// ダブルボタンクリック時のコマンド
        /// </summary>
        private DelegateCommand clickDoubleButton;

        /// <summary>
        /// スプリットボタンクリック時のコマンド
        /// </summary>
        private DelegateCommand clickSplitButton;

        /// <summary>
        /// インシュランスボタンクリック時のコマンド
        /// </summary>
        private DelegateCommand clickInsuranceButton;

        /// <summary>
        /// カードキャッシュ
        /// </summary>
        private IDictionary<Card, CardViewModel> cardDic = new Dictionary<Card, CardViewModel>();

        /// <summary>
        /// 裏面のカード
        /// </summary>
        private CardViewModel reverseCardViewModel;

        /// <summary>
        /// 裏面のカード
        /// </summary>
        private Card reverseCard;

        /// <summary>
        /// ゲームが開始しているか
        /// </summary>
        private bool isStarted = false;

        /// <summary>
        /// ゲームオーバーか
        /// </summary>
        private bool isGameOver = false;

        #endregion

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public MainWindowViewModel()
        {
            this.DealerMessage = string.Empty;
            this.PlayerMessage = string.Empty;
            this.CurrentCoinsValue = 20;
        }

        #region スタートコマンド
        /// <summary>
        /// スタートボタンクリック時のコマンド
        /// </summary>
        public DelegateCommand ClickStartButton
        {
            get
            {
                if (this.clickStartButton == null)
                {
                    this.clickStartButton = new DelegateCommand(this.ExexuteClickStart, () => !this.isGameOver);
                }
                return this.clickStartButton;
            }
        }

        /// <summary>
        /// スタートボタンの実行内容
        /// </summary>
        private void ExexuteClickStart()
        {
            if(!this.Initialize())
            {
                return;
            }

            if (this.cardDic.Count == 0)
            {
                this.CreateCardCache();
                this.reverseCardViewModel = new CardViewModel(this.ReadBitmapImage("Resources\\z01.png"));
            }

            this.PlayerCards.Add(this.cardDic[GameLogic.Instance.Hit()]);
            this.PlayerCards.Add(this.cardDic[GameLogic.Instance.Hit()]);
            this.PlayerScoreValue = GameLogic.Instance.PlayerScore;

            this.DealerCards.Add(this.cardDic[GameLogic.Instance.DealerHit()]);
            this.DealerScoreValue = GameLogic.Instance.DealerScore;
            this.DealerCards.Add(this.reverseCardViewModel);

            this.reverseCard = GameLogic.Instance.DealerHit();
            if (this.CanExexuteClickInsurance())
            {
                this.PlayerMessage = Resources.Message_Insurance;
            }
            if (GameLogic.Instance.PlayerIsBlackJack)
            {
                this.PlayerMessage = Resources.Message_BlackJack;
            }
        } 

        #endregion

        #region ヒットコマンド
        /// <summary>
        /// ヒットボタンクリック時のコマンド
        /// </summary>
        public DelegateCommand ClickHitButton
        {
            get
            {
                if (this.clickHitButton == null)
                {
                    this.clickHitButton = new DelegateCommand(this.ExexuteClickHit, () => this.isStarted);
                }
                return this.clickHitButton;
            }
        }

        /// <summary>
        /// ヒットボタン実行内容
        /// </summary>
        private void ExexuteClickHit()
        {
            var card = GameLogic.Instance.Hit();
            this.PlayerCards.Add(this.cardDic[card]);
            this.PlayerScoreValue = GameLogic.Instance.PlayerScore;
            if(GameLogic.Instance.PlayerIsBurst)
            {
                this.PlayerMessage = Resources.Message_Burst;
                this.ExexuteClickStand();
            }
        }

        #endregion

        #region スタンドコマンド
        /// <summary>
        /// スタンドコマンドクリック時のコマンド
        /// </summary>
        public DelegateCommand ClickStandButton
        {
            get
            {
                if (this.clickStandButton == null)
                {
                    this.clickStandButton = new DelegateCommand(this.ExexuteClickStand, () => this.isStarted);
                }
                return this.clickStandButton;
            }
        }

        /// <summary>
        /// スタンドコマンド実行内容
        /// </summary>
        private void ExexuteClickStand()
        {
            GameLogic.Instance.Stand();
            this.PlayerScoreValue = GameLogic.Instance.PlayerScore;
            this.GetCoinsWhenYouWinValue = GameLogic.Instance.GetCoinWhenYouWin;

            // ディーラーの処理
            var cards = GameLogic.Instance.DealerThinking();
            foreach (var c in cards) { this.DealerCards.Add(this.cardDic[c]); }
            this.DealerScoreValue = GameLogic.Instance.DealerScore;
            if (GameLogic.Instance.DealerIsBlackJack)
            {
                this.DealerMessage = Resources.Message_BlackJack;
            }

            // 勝敗判定
            var result = GameLogic.Instance.GetResultCoin();
            this.DealerCards[1] = this.cardDic[this.reverseCard];
            if(result == 0)
            {
                this.PlayerMessage = "負け";
                this.DealerMessage = "勝ち";
                if (this.CurrentCoinsValue < 0)
                {
                    this.GameOver();
                }
            }
            else if (result == GameLogic.Instance.GetCoinWhenYouWin)
            {
                this.PlayerMessage = "勝ち";
                this.DealerMessage = "負け";
            }
            else
            {
                this.PlayerMessage = "引き分け";
                this.DealerMessage = "引き分け";
            }
            this.CurrentCoinsValue += result;
            if (GameLogic.Instance.IsInsuranced && GameLogic.Instance.DealerIsBlackJack)
            {
                this.CurrentCoinsValue += this.BetCoinsValue;
                this.PlayerMessage += " 、 " + Resources.Message_InsurancePayed;
            }
            this.isStarted = false;
        }
        #endregion

        #region ダブルコマンド
        /// <summary>
        /// ダブルボタンクリック時のコマンド
        /// </summary>
        public DelegateCommand ClickDoubleButton
        {
            get
            {
                if (this.clickDoubleButton == null)
                {
                    this.clickDoubleButton = new DelegateCommand(this.ExexuteClickDouble, this.CanExexuteClickDouble);
                }
                return this.clickDoubleButton;
            }
        }

        /// <summary>
        /// ダブルボタン実行内容
        /// </summary>
        private void ExexuteClickDouble()
        {
            this.CurrentCoinsValue -= this.BetCoinsValue;
            this.BetCoinsValue = this.BetCoinsValue * 2;

            var card = GameLogic.Instance.Double();
            this.PlayerCards.Add(this.cardDic[card]);
            this.PlayerScoreValue = GameLogic.Instance.PlayerScore;
            if (GameLogic.Instance.PlayerIsBurst)
            {
                this.PlayerMessage = Resources.Message_Burst;
            }
            this.ExexuteClickStand();
        }

        /// <summary>
        /// ダブルが実行可能か
        /// </summary>
        /// <returns>ダブルが実行可能か</returns>
        private bool CanExexuteClickDouble()
        {
            return this.isStarted && this.PlayerCards.Count == 2;
        }

        #endregion

        #region スプリットコマンド
        /// <summary>
        /// スプリットボタンクリック時のコマンド
        /// </summary>
        public DelegateCommand ClickSplitButton
        {
            get
            {
                if (this.clickSplitButton == null)
                {
                    this.clickSplitButton = new DelegateCommand(this.ExexuteClickSplit, () => false);
                }
                return this.clickSplitButton;
            }
        }

        /// <summary>
        /// スプリットボタン実行内容
        /// </summary>
        private void ExexuteClickSplit()
        {
        }

        #endregion

        #region インシュランスコマンド
        /// <summary>
        /// インシュランスボタンクリック時のコマンド
        /// </summary>
        public DelegateCommand ClickInsuranceButton
        {
            get
            {
                if (this.clickInsuranceButton == null)
                {
                    this.clickInsuranceButton = new DelegateCommand(this.ExexuteClickInsurance, this.CanExexuteClickInsurance);
                }
                return this.clickInsuranceButton;
            }
        }

        /// <summary>
        /// インシュランスボタン実行内容
        /// </summary>
        private void ExexuteClickInsurance()
        {
            GameLogic.Instance.IsInsuranced = true;
            this.CurrentCoinsValue -= this.BetCoinsValue / 2;
        }

        /// <summary>
        /// インシュランスが実行可能か
        /// </summary>
        /// <returns>インシュランス実行可能か</returns>
        private bool CanExexuteClickInsurance()
        {
            return this.isStarted
                && GameLogic.Instance.CanInsurance;
        }

        #endregion

        /// <summary>
        /// 画面を初期化します
        /// </summary>
        private bool Initialize()
        {
            /*var betDialog = new BetCoinsInputDialogView();
            betDialog.ShowDialog();
            if(GameLogic.Instance.BetCoins == 0)
            {
                return false;
            }*/
            this.BetCoinsValue = this.CurrentCoinsValue / 3 + 1;
            GameLogic.CreateInstance(this.CurrentCoinsValue, this.BetCoinsValue);
            this.CurrentCoinsValue -= this.BetCoinsValue;
            this.GetCoinsWhenYouWinValue = GameLogic.Instance.GetCoinWhenYouWin;

            this.PlayerCards.Clear();
            this.DealerCards.Clear();

            this.PlayerScoreValue = 0;
            this.DealerScoreValue = 0;

            this.PlayerMessage = string.Empty;
            this.DealerMessage = string.Empty;

            this.isStarted = true;
            return true;
        }

        /// <summary>
        /// ゲームを終了します
        /// </summary>
        private void GameOver()
        {
            this.DealerMessage = Resources.Message_Gameover_Dealer;
            this.PlayerMessage = Resources.Message_Gameover_Player;
            this.isStarted = false;
            this.isGameOver = true;
        }

        /// <summary>
        /// カードのイメージを読み込みます
        /// </summary>
        /// <param name="path">画像パス</param>
        /// <returns>カードのイメージ</returns>
        private ImageSource ReadBitmapImage(string path)
        {
            using (Stream stream = new FileStream(
               path,
               FileMode.Open,
               FileAccess.Read, FileShare.ReadWrite | FileShare.Delete))
            {
                PngBitmapDecoder decoder = new PngBitmapDecoder(
                    stream,
                    BitmapCreateOptions.PreservePixelFormat,
                    BitmapCacheOption.Default);
                BitmapSource bmp = new WriteableBitmap(decoder.Frames[0]);
                bmp.Freeze();
                return bmp;
            }
        }
    }
}
