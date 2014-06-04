
namespace Minesweeper.Model
{
    /// <summary>
    /// 難易度：普通
    /// </summary>
    public sealed class NormalLevel : GameLevel
    {
        /// <summary>
        /// 列数
        /// </summary>
        public override int Column
        {
            get { return 10; }
        }

        /// <summary>
        /// 行数
        /// </summary>
        public override int Row
        {
            get { return 10; }
        }

        /// <summary>
        /// 地雷数
        /// </summary>
        public override int MineNum
        {
            get { return 20; }
        }
    }
}
