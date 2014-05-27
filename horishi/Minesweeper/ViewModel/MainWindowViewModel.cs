using Minesweeper.Common;
using Minesweeper.Model;
using Minesweeper.Properties;
using System;
using System.Collections.Generic;

namespace Minesweeper.ViewModel
{
    /// <summary>
    /// メイン画面のViewModelクラス
    /// </summary>
    public sealed partial class MainWindowViewModel : ViewModelBase
    {
        #region フィールド
        /// <summary>
        /// マインスイーパーのロジック処理
        /// </summary>
        private GameLogic logic;

        /// <summary>
        /// 左クリックコマンド
        /// </summary>
        private DelegateCommand<CellButton> clickLeftCellCommand;

        /// <summary>
        /// 右クリックコマンド
        /// </summary>
        private DelegateCommand<CellButton> clickRightCellCommand;

        /// <summary>
        /// スタートクリックコマンド
        /// </summary>
        private DelegateCommand clickStartButtonCommand;

        /// <summary>
        /// 残り爆弾数
        /// </summary>
        private int remainMineCount;

        #endregion

        #region プロパティ

        /// <summary>
        /// グリッドの幅
        /// </summary>
        public int GridWidth 
        { 
            get { return int.Parse(Resources.CellWidth) * 5; }
        }

        /// <summary>
        /// グリッドの高さ
        /// </summary>
        public int GridHeight 
        { 
            get { return int.Parse(Resources.CellHeight) * 5; }
        }

        /// <summary>
        /// 残り爆弾数
        /// </summary>
        public int RemainMineCount
        {
            get
            {
                return this.remainMineCount;
            }
            private set 
            {
                this.remainMineCount = value;
                base.NotifyChanged("RemainMineCount");
            }
        }

        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public MainWindowViewModel()
        {
            this.RemainMineCount = 5;
            this.logic = new GameLogic(5, 5, 5);
        }

        #endregion

        #region コマンド
        /// <summary>
        /// 左クリックコマンド
        /// </summary>
        public DelegateCommand<CellButton> ClickLeftCellCommand
        {
            get
            {
                if(this.clickLeftCellCommand == null)
                {
                    this.clickLeftCellCommand = new DelegateCommand<CellButton>(ExecuteLeftCellClick, CanExecuteCellClick);
                }
                return this.clickLeftCellCommand;
            }
        }

        /// <summary>
        /// 右クリックコマンド
        /// </summary>
        public DelegateCommand<CellButton> ClickRightCellCommand
        {
            get
            {
                if (this.clickRightCellCommand == null)
                {
                    this.clickRightCellCommand = new DelegateCommand<CellButton>(ExecuteRightCellClick, CanExecuteCellClick);
                }
                return this.clickRightCellCommand;
            }
        }

        /// <summary>
        /// スタートクリックコマンド
        /// </summary>
        public DelegateCommand ClickStartButtonCommand
        {
            get
            {
                if (this.clickStartButtonCommand == null)
                {
                    this.clickStartButtonCommand = new DelegateCommand(ExecuteStartButtonClick, () => true);
                }
                return this.clickStartButtonCommand;
            }
        }

        /// <summary>
        /// 左クリック実行時のコマンド処理
        /// </summary>
        /// <param name="parameter">クリック対象のボタンコントロール</param>
        private void ExecuteLeftCellClick(CellButton parameter)
        {
            int col = parameter.Column;
            int row = parameter.Row;
            if (this.logic.GetCell(col, row).IsMarked)
            {
                return;
            }
            IEnumerable<Tuple<int, Point>> aroundMineInfo = this.logic.Open(col, row);
            if (this.logic.IsGameOver)
            {
                this.OpenMineCell();
                this.ClearTextLableContext = Resources.GameOverTextLabelContext;
                this.cellGridItemsSource[Point.GetPoint(col, row)].Display = Resources.MineClickedMark;
                return;
            }

            foreach (Tuple<int, Point> t in aroundMineInfo)
            {
                this.cellGridItemsSource[t.Item2].Display = t.Item1.ToString();
            }
            if (this.logic.IsCleared)
            {
                this.OpenMineCell();
                this.ClearTextLableContext = Resources.ClearTextLableContext;
                this.RemainMineCount = 0;
                this.ClearTime = this.logic.GetClearTime().ToString(Resources.ClearTimeFormat);
                return;
            }
        }

        /// <summary>
        /// 右クリック実行時のコマンド処理
        /// </summary>
        /// <param name="parameter">クリック対象のボタンコントロール</param>
        private void ExecuteRightCellClick(CellButton parameter)
        {
            Cell target = this.logic.GetCell(parameter.Column, parameter.Row);
            CellState state = target.ToNextState();
            if (state == CellState.Default)
            {
                this.CellGridItemsSource[Point.GetPoint(parameter.Column, parameter.Row)].Display = string.Empty;
            }
            else if (state == CellState.MineMarked)
            {
                this.CellGridItemsSource[Point.GetPoint(parameter.Column, parameter.Row)].Display = Resources.MineRightClickMark;
                this.RemainMineCount--;
            }
            else if (state == CellState.QuestionMarked)
            {
                this.CellGridItemsSource[Point.GetPoint(parameter.Column, parameter.Row)].Display = Resources.QuestionMarked;
                this.RemainMineCount++;
            }
        }

        /// <summary>
        /// クリックが実行可能か
        /// </summary>
        /// <param name="parameter">クリック対象のボタンコントロール</param>
        /// <returns>実行可否</returns>
        private bool CanExecuteCellClick(CellButton parameter)
        {
            Cell target = this.logic.GetCell(parameter.Column, parameter.Row);
            return !this.logic.IsGameOver
                && !this.logic.IsCleared
                && !target.IsOpened;
        }

        /// <summary>
        /// 地雷セルを表示します
        /// </summary>
        private void OpenMineCell()
        {
            foreach (Point p in this.logic.GetMineCellPoints())
            {
                this.cellGridItemsSource[p].Display = Resources.MineCellMark;
            }
        }

        /// <summary>
        /// スタートボタンをクリックしたときのコマンド処理
        /// </summary>
        private void ExecuteStartButtonClick()
        {
            foreach(var source in this.CellGridItemsSource)
            {
                source.Value.Display = string.Empty;
            }
            this.logic.Start();
            this.ClearTextLableContext = string.Empty;
            this.ClearTime = string.Empty;
            this.RemainMineCount = 5;
        }

        #endregion
    }
}
