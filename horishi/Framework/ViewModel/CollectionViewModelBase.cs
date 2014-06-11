using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.ViewModel
{
    /// <summary>
    /// ViewModelの基底クラス：コレクション変更通知版
    /// </summary>
    public abstract class CollectionViewModelBase : ViewModelBase, INotifyCollectionChanged
    {
        /// <summary>
        /// コレクションプロパティの変更通知イベント
        /// </summary>
        public event NotifyCollectionChangedEventHandler CollectionChanged;

        /// <summary>
        /// プロパティ変更の通知します
        /// </summary>
        /// <param name="action">変更操作</param>
        /// <param name="changedItem">変更される項目</param>
        public void NotifyCollectionChanged(NotifyCollectionChangedAction action, object changedItem)
        {
            if (this.CollectionChanged != null)
            {
                this.CollectionChanged(this, new NotifyCollectionChangedEventArgs(action, changedItem));
            }
        }

        /// <summary>
        /// プロパティ変更の通知します
        /// </summary>
        /// <param name="action">変更操作</param>
        /// <param name="changedItems">変更される項目</param>
        public void NotifyCollectionChanged(NotifyCollectionChangedAction action, IList changedItems)
        {
            if (this.CollectionChanged != null)
            {
                this.CollectionChanged(this, new NotifyCollectionChangedEventArgs(action, changedItems));
            }
        }
    }
}
