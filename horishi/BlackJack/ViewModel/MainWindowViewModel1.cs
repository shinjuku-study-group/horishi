using BlackJack.Properties;
using Common.Trump;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace BlackJack.ViewModel
{
    /// <summary>
    /// メイン画面のViewModel
    /// </summary>
    public sealed partial class MainWindowViewModel
    {
        #region フィールド
        /// <summary>
        /// playerCards
        /// </summary>
        private IList<CardViewModel> playerCards = new ObservableCollection<CardViewModel>();

        /// <summary>
        /// dealerCards
        /// </summary>
        private IList<CardViewModel> dealerCards = new ObservableCollection<CardViewModel>();

        /// <summary>
        /// System.Int32
        /// </summary>
        private System.Int32 dealerScoreValue;
        /// <summary>
        /// System.Int32
        /// </summary>
        private System.Int32 playerScoreValue;
        /// <summary>
        /// System.Int32
        /// </summary>
        private System.Int32 currentCoinsValue;
        /// <summary>
        /// System.Int32
        /// </summary>
        private System.Int32 betCoinsValue;
        /// <summary>
        /// System.Int32
        /// </summary>
        private System.Int32 getCoinsWhenYouWinValue;
        /// <summary>
        /// System.String
        /// </summary>
        private System.String dealerMessage;
        /// <summary>
        /// System.String
        /// </summary>
        private System.String playerMessage;
        #endregion

        #region プロパティ
        /// <summary>
        /// PlayerCards
        /// </summary>
        public IList<CardViewModel> PlayerCards
        {
            get
            {
                return this.playerCards;
            }
        }

        /// <summary>
        /// DealerCards
        /// </summary>
        public IList<CardViewModel> DealerCards
        {
            get
            {
                return this.dealerCards;
            }
        }

        /// <summary>
        /// DealerScoreValue
        /// </summary>
        public System.Int32 DealerScoreValue
        {
            get
            {
                return this.dealerScoreValue;
            }
            set
            {
                this.dealerScoreValue = value;
                base.NotifyChanged("DealerScoreValue");
            }
        }

        /// <summary>
        /// PlayerScoreValue
        /// </summary>
        public System.Int32 PlayerScoreValue
        {
            get
            {
                return this.playerScoreValue;
            }
            set
            {
                this.playerScoreValue = value;
                base.NotifyChanged("PlayerScoreValue");
            }
        }

        /// <summary>
        /// CurrentCoinsValue
        /// </summary>
        public System.Int32 CurrentCoinsValue
        {
            get
            {
                return this.currentCoinsValue;
            }
            set
            {
                this.currentCoinsValue = value;
                base.NotifyChanged("CurrentCoinsValue");
            }
        }

        /// <summary>
        /// BetCoinsValue
        /// </summary>
        public System.Int32 BetCoinsValue
        {
            get
            {
                return this.betCoinsValue;
            }
            set
            {
                this.betCoinsValue = value;
                base.NotifyChanged("BetCoinsValue");
            }
        }

        /// <summary>
        /// GetCoinsWhenYouWinValue
        /// </summary>
        public System.Int32 GetCoinsWhenYouWinValue
        {
            get
            {
                return this.getCoinsWhenYouWinValue;
            }
            set
            {
                this.getCoinsWhenYouWinValue = value;
                base.NotifyChanged("GetCoinsWhenYouWinValue");
            }
        }

        /// <summary>
        /// DealerMessage
        /// </summary>
        public System.String DealerMessage
        {
            get
            {
                return this.dealerMessage;
            }
            set
            {
                this.dealerMessage = value;
                base.NotifyChanged("DealerMessage");
            }
        }

        /// <summary>
        /// PlayerMessage
        /// </summary>
        public System.String PlayerMessage
        {
            get
            {
                return this.playerMessage;
            }
            set
            {
                this.playerMessage = value;
                base.NotifyChanged("PlayerMessage");
            }
        }

        /// <summary>
        /// (Margin, Margin)
        /// </summary>
        public double Margin
        {
            get
            {
                return SizeDefinition.Margin;
            }
        }

        /// <summary>
        /// (GridHeight, CardHeight)
        /// </summary>
        public double GridHeight
        {
            get
            {
                return SizeDefinition.CardHeight;
            }
        }

        /// <summary>
        /// WindowTitle
        /// </summary>
        public System.String WindowTitle
        {
            get
            {
                return Resources.WindowTitle;
            }
        }

        /// <summary>
        /// DealerName
        /// </summary>
        public System.String DealerName
        {
            get
            {
                return Resources.DealerName;
            }
        }

        /// <summary>
        /// PlayerName
        /// </summary>
        public System.String PlayerName
        {
            get
            {
                return Resources.PlayerName;
            }
        }

        /// <summary>
        /// CurrentCoins
        /// </summary>
        public System.String CurrentCoins
        {
            get
            {
                return Resources.CurrentCoins;
            }
        }

        /// <summary>
        /// BetCoins
        /// </summary>
        public System.String BetCoins
        {
            get
            {
                return Resources.BetCoins;
            }
        }

        /// <summary>
        /// GetCoinsWhenYouWin
        /// </summary>
        public System.String GetCoinsWhenYouWin
        {
            get
            {
                return Resources.GetCoinsWhenYouWin;
            }
        }

        /// <summary>
        /// ButtonContent_Hit
        /// </summary>
        public System.String ButtonContent_Hit
        {
            get
            {
                return Resources.ButtonContent_Hit;
            }
        }

        /// <summary>
        /// ButtonContent_Stand
        /// </summary>
        public System.String ButtonContent_Stand
        {
            get
            {
                return Resources.ButtonContent_Stand;
            }
        }

        /// <summary>
        /// ButtonContent_Double
        /// </summary>
        public System.String ButtonContent_Double
        {
            get
            {
                return Resources.ButtonContent_Double;
            }
        }

        /// <summary>
        /// ButtonContent_Split
        /// </summary>
        public System.String ButtonContent_Split
        {
            get
            {
                return Resources.ButtonContent_Split;
            }
        }

        /// <summary>
        /// ButtonContent_Insurance
        /// </summary>
        public System.String ButtonContent_Insurance
        {
            get
            {
                return Resources.ButtonContent_Insurance;
            }
        }

        #endregion

        /// <summary>
        /// カードのキャッシュを作成します
        /// </summary>
        private void CreateCardCache()
        {
            this.cardDic[Card.Get(Mark.Clover, Number.One)] = new CardViewModel(this.ReadBitmapImage("Resources\\c01.png"));
            this.cardDic[Card.Get(Mark.Daia, Number.One)] = new CardViewModel(this.ReadBitmapImage("Resources\\d01.png"));
            this.cardDic[Card.Get(Mark.Spade, Number.One)] = new CardViewModel(this.ReadBitmapImage("Resources\\s01.png"));
            this.cardDic[Card.Get(Mark.Heart, Number.One)] = new CardViewModel(this.ReadBitmapImage("Resources\\h01.png"));
            this.cardDic[Card.Get(Mark.Clover, Number.Two)] = new CardViewModel(this.ReadBitmapImage("Resources\\c02.png"));
            this.cardDic[Card.Get(Mark.Daia, Number.Two)] = new CardViewModel(this.ReadBitmapImage("Resources\\d02.png"));
            this.cardDic[Card.Get(Mark.Spade, Number.Two)] = new CardViewModel(this.ReadBitmapImage("Resources\\s02.png"));
            this.cardDic[Card.Get(Mark.Heart, Number.Two)] = new CardViewModel(this.ReadBitmapImage("Resources\\h02.png"));
            this.cardDic[Card.Get(Mark.Clover, Number.Three)] = new CardViewModel(this.ReadBitmapImage("Resources\\c03.png"));
            this.cardDic[Card.Get(Mark.Daia, Number.Three)] = new CardViewModel(this.ReadBitmapImage("Resources\\d03.png"));
            this.cardDic[Card.Get(Mark.Spade, Number.Three)] = new CardViewModel(this.ReadBitmapImage("Resources\\s03.png"));
            this.cardDic[Card.Get(Mark.Heart, Number.Three)] = new CardViewModel(this.ReadBitmapImage("Resources\\h03.png"));
            this.cardDic[Card.Get(Mark.Clover, Number.Four)] = new CardViewModel(this.ReadBitmapImage("Resources\\c04.png"));
            this.cardDic[Card.Get(Mark.Daia, Number.Four)] = new CardViewModel(this.ReadBitmapImage("Resources\\d04.png"));
            this.cardDic[Card.Get(Mark.Spade, Number.Four)] = new CardViewModel(this.ReadBitmapImage("Resources\\s04.png"));
            this.cardDic[Card.Get(Mark.Heart, Number.Four)] = new CardViewModel(this.ReadBitmapImage("Resources\\h04.png"));
            this.cardDic[Card.Get(Mark.Clover, Number.Five)] = new CardViewModel(this.ReadBitmapImage("Resources\\c05.png"));
            this.cardDic[Card.Get(Mark.Daia, Number.Five)] = new CardViewModel(this.ReadBitmapImage("Resources\\d05.png"));
            this.cardDic[Card.Get(Mark.Spade, Number.Five)] = new CardViewModel(this.ReadBitmapImage("Resources\\s05.png"));
            this.cardDic[Card.Get(Mark.Heart, Number.Five)] = new CardViewModel(this.ReadBitmapImage("Resources\\h05.png"));
            this.cardDic[Card.Get(Mark.Clover, Number.Six)] = new CardViewModel(this.ReadBitmapImage("Resources\\c06.png"));
            this.cardDic[Card.Get(Mark.Daia, Number.Six)] = new CardViewModel(this.ReadBitmapImage("Resources\\d06.png"));
            this.cardDic[Card.Get(Mark.Spade, Number.Six)] = new CardViewModel(this.ReadBitmapImage("Resources\\s06.png"));
            this.cardDic[Card.Get(Mark.Heart, Number.Six)] = new CardViewModel(this.ReadBitmapImage("Resources\\h06.png"));
            this.cardDic[Card.Get(Mark.Clover, Number.Seven)] = new CardViewModel(this.ReadBitmapImage("Resources\\c07.png"));
            this.cardDic[Card.Get(Mark.Daia, Number.Seven)] = new CardViewModel(this.ReadBitmapImage("Resources\\d07.png"));
            this.cardDic[Card.Get(Mark.Spade, Number.Seven)] = new CardViewModel(this.ReadBitmapImage("Resources\\s07.png"));
            this.cardDic[Card.Get(Mark.Heart, Number.Seven)] = new CardViewModel(this.ReadBitmapImage("Resources\\h07.png"));
            this.cardDic[Card.Get(Mark.Clover, Number.Eight)] = new CardViewModel(this.ReadBitmapImage("Resources\\c08.png"));
            this.cardDic[Card.Get(Mark.Daia, Number.Eight)] = new CardViewModel(this.ReadBitmapImage("Resources\\d08.png"));
            this.cardDic[Card.Get(Mark.Spade, Number.Eight)] = new CardViewModel(this.ReadBitmapImage("Resources\\s08.png"));
            this.cardDic[Card.Get(Mark.Heart, Number.Eight)] = new CardViewModel(this.ReadBitmapImage("Resources\\h08.png"));
            this.cardDic[Card.Get(Mark.Clover, Number.Nine)] = new CardViewModel(this.ReadBitmapImage("Resources\\c09.png"));
            this.cardDic[Card.Get(Mark.Daia, Number.Nine)] = new CardViewModel(this.ReadBitmapImage("Resources\\d09.png"));
            this.cardDic[Card.Get(Mark.Spade, Number.Nine)] = new CardViewModel(this.ReadBitmapImage("Resources\\s09.png"));
            this.cardDic[Card.Get(Mark.Heart, Number.Nine)] = new CardViewModel(this.ReadBitmapImage("Resources\\h09.png"));
            this.cardDic[Card.Get(Mark.Clover, Number.Ten)] = new CardViewModel(this.ReadBitmapImage("Resources\\c10.png"));
            this.cardDic[Card.Get(Mark.Daia, Number.Ten)] = new CardViewModel(this.ReadBitmapImage("Resources\\d10.png"));
            this.cardDic[Card.Get(Mark.Spade, Number.Ten)] = new CardViewModel(this.ReadBitmapImage("Resources\\s10.png"));
            this.cardDic[Card.Get(Mark.Heart, Number.Ten)] = new CardViewModel(this.ReadBitmapImage("Resources\\h10.png"));
            this.cardDic[Card.Get(Mark.Clover, Number.Eleven)] = new CardViewModel(this.ReadBitmapImage("Resources\\c11.png"));
            this.cardDic[Card.Get(Mark.Daia, Number.Eleven)] = new CardViewModel(this.ReadBitmapImage("Resources\\d11.png"));
            this.cardDic[Card.Get(Mark.Spade, Number.Eleven)] = new CardViewModel(this.ReadBitmapImage("Resources\\s11.png"));
            this.cardDic[Card.Get(Mark.Heart, Number.Eleven)] = new CardViewModel(this.ReadBitmapImage("Resources\\h11.png"));
            this.cardDic[Card.Get(Mark.Clover, Number.Twelve)] = new CardViewModel(this.ReadBitmapImage("Resources\\c12.png"));
            this.cardDic[Card.Get(Mark.Daia, Number.Twelve)] = new CardViewModel(this.ReadBitmapImage("Resources\\d12.png"));
            this.cardDic[Card.Get(Mark.Spade, Number.Twelve)] = new CardViewModel(this.ReadBitmapImage("Resources\\s12.png"));
            this.cardDic[Card.Get(Mark.Heart, Number.Twelve)] = new CardViewModel(this.ReadBitmapImage("Resources\\h12.png"));
            this.cardDic[Card.Get(Mark.Clover, Number.Thirteen)] = new CardViewModel(this.ReadBitmapImage("Resources\\c13.png"));
            this.cardDic[Card.Get(Mark.Daia, Number.Thirteen)] = new CardViewModel(this.ReadBitmapImage("Resources\\d13.png"));
            this.cardDic[Card.Get(Mark.Spade, Number.Thirteen)] = new CardViewModel(this.ReadBitmapImage("Resources\\s13.png"));
            this.cardDic[Card.Get(Mark.Heart, Number.Thirteen)] = new CardViewModel(this.ReadBitmapImage("Resources\\h13.png"));
        }
    }
}