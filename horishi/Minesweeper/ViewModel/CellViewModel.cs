using Minesweeper.Common;

namespace Minesweeper.ViewModel
{
    /// <summary>
    /// セルのViewModel
    /// </summary>
    public sealed class CellViewModel : ViewModelBase
    {
        /// <summary>
        /// 表示状態
        /// </summary>
        private string display = string.Empty;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="display"></param>
        public CellViewModel(string display)
        {
            this.display = display;
        }

        /// <summary>
        /// 表示状態
        /// </summary>
        public string Display
        {
            get 
            {
                return this.display;
            }
            set
            {
                this.display = value;
                base.NotifyChanged("Display");
            }
        }
    }
}
