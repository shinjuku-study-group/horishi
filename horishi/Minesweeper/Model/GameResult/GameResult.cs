using System;

namespace Minesweeper.Model.GameResult
{
    /// <summary>
    /// 難易度ごとの戦績
    /// </summary>
    [Serializable]
    public sealed class GameResult
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="level">難易度</param>
        public GameResult(string level)
        {
            this.Level = level;
        }

        /// <summary>
        /// 難易度
        /// </summary>
        public string Level { get; private set; }

        /// <summary>
        /// 勝ち数
        /// </summary>
        public int WinCount { get; set; }

        /// <summary>
        /// 負け数
        /// </summary>
        public int LooseCount { get; set; }
    }
}
