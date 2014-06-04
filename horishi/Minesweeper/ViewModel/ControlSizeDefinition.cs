using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper.ViewModel
{
    /// <summary>
    /// サイズの定義クラス
    /// </summary>
    internal sealed class ControlSizeDefinition
    {
        /// <summary>
        /// セル幅
        /// </summary>
        public static readonly double CellWidth = 20d;

        /// <summary>
        /// セル高さ
        /// </summary>
        public static readonly double CellHeight = 20d;

        /// <summary>
        /// マージン幅
        /// </summary>
        public static readonly double ContainerMargin = 10d;
    }
}
