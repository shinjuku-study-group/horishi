using Framework.ViewModel;
using FreeCell.Common;

namespace FreeCell.ViewModel
{
    /// <summary>
    /// フリーセルのメイン画面ViewModel
    /// </summary>
    public sealed partial class MainWindowViewModel : ViewModelBase
    {
        #region フィールド
        /// <summary>
        /// ホームセル：cell.Item2
        /// </summary>
        private CardViewModel cloverHomeCell;

        /// <summary>
        /// ホームセル：cell.Item2
        /// </summary>
        private CardViewModel daiaHomeCell;

        /// <summary>
        /// ホームセル：cell.Item2
        /// </summary>
        private CardViewModel spadeHomeCell;

        /// <summary>
        /// ホームセル：cell.Item2
        /// </summary>
        private CardViewModel heartHomeCell;

        /// <summary>
        /// フリーセル1
        /// </summary>
        private CardViewModel freeCell1;

        /// <summary>
        /// フリーセル2
        /// </summary>
        private CardViewModel freeCell2;

        /// <summary>
        /// フリーセル3
        /// </summary>
        private CardViewModel freeCell3;

        /// <summary>
        /// フリーセル4
        /// </summary>
        private CardViewModel freeCell4;

        #endregion

        #region プロパティ
        /// <summary>
        /// ホームセル：Clover
        /// </summary>
        public CardViewModel CloverHomeCell
        {
            get
            {
                return this.cloverHomeCell;
            }
            set
            {
                this.cloverHomeCell = value;
                base.NotifyChanged("CloverHomeCell");
            }
        }

        /// <summary>
        /// ホームセル：Daia
        /// </summary>
        public CardViewModel DaiaHomeCell
        {
            get
            {
                return this.daiaHomeCell;
            }
            set
            {
                this.daiaHomeCell = value;
                base.NotifyChanged("DaiaHomeCell");
            }
        }

        /// <summary>
        /// ホームセル：Spade
        /// </summary>
        public CardViewModel SpadeHomeCell
        {
            get
            {
                return this.spadeHomeCell;
            }
            set
            {
                this.spadeHomeCell = value;
                base.NotifyChanged("SpadeHomeCell");
            }
        }

        /// <summary>
        /// ホームセル：Heart
        /// </summary>
        public CardViewModel HeartHomeCell
        {
            get
            {
                return this.heartHomeCell;
            }
            set
            {
                this.heartHomeCell = value;
                base.NotifyChanged("HeartHomeCell");
            }
        }

        /// <summary>
        /// フリーセル1
        /// </summary>
        public CardViewModel FreeCell1
        {
            get
            {
                return this.freeCell1;
            }
            set
            {
                this.freeCell1 = value;
                base.NotifyChanged("FreeCell1");
            }
        }

        /// <summary>
        /// フリーセル2
        /// </summary>
        public CardViewModel FreeCell2
        {
            get
            {
                return this.freeCell2;
            }
            set
            {
                this.freeCell2 = value;
                base.NotifyChanged("FreeCell2");
            }
        }

        /// <summary>
        /// フリーセル3
        /// </summary>
        public CardViewModel FreeCell3
        {
            get
            {
                return this.freeCell3;
            }
            set
            {
                this.freeCell3 = value;
                base.NotifyChanged("FreeCell3");
            }
        }

        /// <summary>
        /// フリーセル4
        /// </summary>
        public CardViewModel FreeCell4
        {
            get
            {
                return this.freeCell4;
            }
            set
            {
                this.freeCell4 = value;
                base.NotifyChanged("FreeCell4");
            }
        }

        /// <summary>
        /// CardHeight
        /// </summary>
        public double CardHeight
        {
            get { return SizeDefinition.CardHeight; }
        }

        /// <summary>
        /// CardWidth
        /// </summary>
        public double CardWidth
        {
            get { return SizeDefinition.CardWidth; }
        }

        /// <summary>
        /// CellSpaceMargin
        /// </summary>
        public double CellSpaceMargin
        {
            get { return SizeDefinition.CellSpaceMargin; }
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