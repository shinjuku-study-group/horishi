using Framework.Command;
using Framework.ViewModel;
using FreeCell.Common;
using FreeCell.Properties;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace FreeCell.ViewModel
{
    /// <summary>
    /// フリーセルのメイン画面ViewModel
    /// </summary>
    public sealed partial class MainWindowViewModel : ViewModelBase
    {
        #region フィールド

        private Stack<CardViewModel> line1 = new Stack<CardViewModel>();

        private DelegateCommand clickStartButton;

        private DelegateCommand leftClickCard;

        /// <summary>
        /// カードキャッシュ
        /// </summary>
        private IDictionary<Card, CardViewModel> cardDic = new Dictionary<Card, CardViewModel>();

        #endregion

        public MainWindowViewModel()
        {
        }

        /// <summary>
        /// ウィンドウタイトル
        /// </summary>
        public string WindowTitle
        {
            get { return Resources.WindowTitle; }
        }

        #region プロパティ

        public Stack<CardViewModel> Line1
        {
            get 
            { 
                return this.line1; 
            }
            set
            {
                this.line1 = value;
                base.NotifyChanged("Line1");
            }
        }

        #endregion

        #region コマンド

        /// <summary>
        /// スタートクリック時のコマンド
        /// </summary>
        /// <returns></returns>
        public DelegateCommand ClickStartButton
        {
            get
            {
                if (this.clickStartButton == null)
                {
                    this.clickStartButton = new DelegateCommand(this.ExecuteClickStart, () => true);
                }
                return this.clickStartButton;
            }
        }

        /// <summary>
        /// スタートします
        /// </summary>
        private void ExecuteClickStart()
        {
            if(this.cardDic.Count == 0)
            {
                this.CreateCardCache();
            }
            this.CloverHomeCell = this.cardDic[Card.Get(Mark.Clover, Number.One)];
            this.DaiaHomeCell = this.cardDic[Card.Get(Mark.Daia, Number.One)];
            this.SpadeHomeCell = this.cardDic[Card.Get(Mark.Spade, Number.One)];
            this.HeartHomeCell = this.cardDic[Card.Get(Mark.Heart, Number.One)];
            this.Line1.Push(this.cardDic[Card.Get(Mark.Clover, Number.Two)]);
            this.Line1.Push(this.cardDic[Card.Get(Mark.Clover, Number.Three)]);
            this.Line1.Push(this.cardDic[Card.Get(Mark.Clover, Number.Four)]);
            this.Line1 = this.line1;
        }
        

        private DelegateCommand LeftClickCard()
        {
            if(this.leftClickCard == null)
            {
                this.leftClickCard = new DelegateCommand(this.ExecuteLeftClickCard, this.CanLeftClickCard);
            }
            return this.leftClickCard;
        }

        private void ExecuteLeftClickCard()
        {

        }

        private bool CanLeftClickCard()
        {
            return true;
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
        #endregion
    }
}
