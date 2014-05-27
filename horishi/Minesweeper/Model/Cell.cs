
namespace Minesweeper.Model
{
    /// <summary>
    /// セルの基底クラス
    /// </summary>
    public class Cell
    {
        /// <summary>
        /// 現在の状態を保持
        /// </summary>
        private CellState state = CellState.Default;

        #region プロパティ
        /// <summary>
        /// 地雷セルかどうか
        /// </summary>
        public bool IsMineCell
        {
            get { return this.GetType() == typeof(MineCell); }
        }

        /// <summary>
        /// マーク済みかどうか
        /// </summary>
        public bool IsMarked
        {
            get { return this.state == CellState.MineMarked || this.state == CellState.QuestionMarked; }
        }

        /// <summary>
        /// 既に開いているかどうか
        /// </summary>
        public bool IsOpened
        {
            get { return this.state == CellState.Opened; }
        } 
        #endregion

        #region メソッド
        /// <summary>
        /// 次の状態に遷移します
        /// </summary>
        /// <returns>遷移した次の状態</returns>
        public CellState ToNextState()
        {
            if(this.state == CellState.Default)
            {
                this.state = CellState.MineMarked;
            }
            else if(this.state == CellState.MineMarked)
            {
                this.state = CellState.QuestionMarked;
            }
            else if(this.state == CellState.QuestionMarked)
            {
                this.state = CellState.Default;
            }
            return this.state;
        }

        /// <summary>
        /// 開きます
        /// </summary>
        public void Open()
        {
            this.state = CellState.Opened;
        }

        #endregion
    }
}
