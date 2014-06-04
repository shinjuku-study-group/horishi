
namespace Minesweeper.Model
{
    /// <summary>
    /// 難易度：易しい
    /// </summary>
    public sealed class EasyLevel : GameLevel
    {
        /// <summary>
        /// 列数
        /// </summary>
        public override int Column
        {
            get { return 5; }
        }

        /// <summary>
        /// 行数
        /// </summary>
        public override int Row
        {
            get { return 5; }
        }

        /// <summary>
        /// 地雷数
        /// </summary>
        public override int MineNum
        {
            get { return 5; }
        }
    }
}
