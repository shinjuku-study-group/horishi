using Framework.ViewModel;

namespace Minesweeper.ViewModel
{
    /// <summary>
    /// 難易度メニューバーの項目
    /// </summary>
    public sealed class GameLevelMenuItemViewModel : ViewModelBase
    {
        /// <summary>
        /// チェックされているか
        /// </summary>
        private bool isChecked = false;

        /// <summary>
        /// 表示されるテキスト
        /// </summary>
        private string text = string.Empty;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="text">表示されるテキスト</param>
        public GameLevelMenuItemViewModel(string text)
        {
            this.Text = text;
        }

        /// <summary>
        /// チェックされているか
        /// </summary>
        public bool IsChecked
        {
            get { return this.isChecked; }
            set
            {
                this.isChecked = value;
                base.NotifyChanged("IsChecked");
            }
        }

        /// <summary>
        /// 表示されるテキスト
        /// </summary>
        public string Text
        {
            get { return this.text; }
            private set
            {
                this.text = value;
                base.NotifyChanged("Text");
            }
        }
    }
}
