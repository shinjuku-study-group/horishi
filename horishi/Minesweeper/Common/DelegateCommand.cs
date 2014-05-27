using System;
using System.Windows.Input;

namespace Minesweeper.Common
{
    /// <summary>
    /// 引数なしのコマンド
    /// </summary>
    public sealed class DelegateCommand : ICommand
    {
        /// <summary>
        /// 実行するアクション
        /// </summary>
        private Action execute;

        /// <summary>
        /// 実行可否
        /// </summary>
        private Func<bool> canExecute;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="execute">実行するアクション</param>
        /// <param name="canExecute">実行可否</param>
        public DelegateCommand(Action execute, Func<bool> canExecute)
        {
            if (execute == null)
            {
                throw new ArgumentNullException("execute is null");
            }
            if (canExecute == null)
            {
                throw new ArgumentNullException("canExecute is null");
            }
            this.execute = execute;
            this.canExecute = canExecute;
        }

        /// <summary>
        /// 登録したアクションを実行します
        /// </summary>
        /// <param name="parameter">アクションで使用する引数</param>
        public void Execute(object parameter)
        {
            this.execute();
        }

        /// <summary>
        /// 登録したアクションを実行できるかの判定を行います
        /// </summary>
        /// <param name="parameter">判定を行う引数</param>
        /// <returns>判定結果</returns>
        public bool CanExecute(object parameter)
        {
            return this.canExecute();
        }

        /// <summary>
        /// アクションが実行できるか変更があった時のイベントハンドラ
        /// </summary>
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
    }
}
