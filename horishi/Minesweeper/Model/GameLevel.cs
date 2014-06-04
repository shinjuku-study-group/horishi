
namespace Minesweeper.Model
{
    /// <summary>
    /// 難易度の基底クラス
    /// </summary>
    public abstract class GameLevel
    {
        /// <summary>
        /// 列数
        /// </summary>
        public abstract int Column
        {
            get;
        }

        /// <summary>
        /// 行数
        /// </summary>
        public abstract int Row
        {
            get;
        }

        /// <summary>
        /// 地雷数
        /// </summary>
        public abstract int MineNum
        {
            get;
        }
    }
}
