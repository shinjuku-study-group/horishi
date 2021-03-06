﻿
namespace Minesweeper.Model
{
    /// <summary>
    /// 難易度：難しい
    /// </summary>
    public sealed class HardLevel : GameLevel
    {
        /// <summary>
        /// 列数
        /// </summary>
        public override int Column
        {
            get { return 15; }
        }

        /// <summary>
        /// 行数
        /// </summary>
        public override int Row
        {
            get { return 15; }
        }

        /// <summary>
        /// 地雷数
        /// </summary>
        public override int MineNum
        {
            get { return 50; }
        }
    }
}
