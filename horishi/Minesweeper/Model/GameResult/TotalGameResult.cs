using System;

namespace Minesweeper.Model.GameResult
{
    /// <summary>
    /// 戦績モデル
    /// </summary>
    [Serializable]
    public sealed class TotalGameResult
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public TotalGameResult()
        {
            this.Easy = new GameResult("Easy");
            this.Normal = new GameResult("Normal");
            this.Hard = new GameResult("Hard");
        }

        /// <summary>
        /// かんたん
        /// </summary>
        public GameResult Easy { get; private set; }

        /// <summary>
        /// ふつう
        /// </summary>
        public GameResult Normal { get; private set; }

        /// <summary>
        /// むずかしい
        /// </summary>
        public GameResult Hard { get; private set; }
    }
}
