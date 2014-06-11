
namespace BlackJack.Model
{
    /// <summary>
    /// 数値定義クラス
    /// </summary>
    internal sealed class NumberDefinition
    {
        /// <summary>
        /// 倍率
        /// </summary>
        public static readonly double BetRate = 2.0d;

        /// <summary>
        /// ブラックジャック倍率
        /// </summary>
        public static readonly double BlackJackRate = 1.5d;

        /// <summary>
        /// ダブル倍率
        /// </summary>
        public static readonly double DoubleRate = 2.0d;

        /// <summary>
        /// ブラックジャックのスコア
        /// </summary>
        public static readonly int BlackJackScore = 21;
    }
}
