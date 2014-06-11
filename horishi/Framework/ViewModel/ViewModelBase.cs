using System.ComponentModel;

namespace Framework.ViewModel
{
    /// <summary>
    /// ViewModelの基底クラス
    /// </summary>
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        /// <summary>
        /// プロパティ変更の通知イベント
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// プロパティ変更の通知します
        /// </summary>
        /// <param name="propertyName">変更したプロパティ名</param>
        public void NotifyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
