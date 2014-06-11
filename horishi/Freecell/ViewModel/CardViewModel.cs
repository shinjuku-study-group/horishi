using Framework.ViewModel;
using FreeCell.Properties;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace FreeCell.ViewModel
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
    }
}
