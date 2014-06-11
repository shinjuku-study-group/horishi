using Framework.ViewModel;
using System.Windows.Media;

namespace BlackJack.ViewModel
{
    /// <summary>
    /// カードのViewModel
    /// </summary>
    public sealed class CardViewModel : ViewModelBase
    {
        /// <summary>
        /// 画像
        /// </summary>
        private ImageSource image;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public CardViewModel(ImageSource source)
        {
            
            this.image = source;
        }

        /// <summary>
        /// 画像
        /// </summary>
        public ImageSource Image
        {
            get
            { 
                return this.image;
            }
            private set
            {
                this.image = value;
                base.NotifyChanged("Image");
            }
        }

        /// <summary>
        /// CardWidth
        /// </summary>
        public double Width
        {
            get
            {
                return SizeDefinition.CardWidth;
            }
        }

        /// <summary>
        /// CardHeight
        /// </summary>
        public double Height
        {
            get
            {
                return SizeDefinition.CardHeight;
            }
        }
    }
}
