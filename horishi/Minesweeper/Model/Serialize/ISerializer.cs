using Minesweeper.Model.GameResult;

namespace Minesweeper.Model.Serialize
{
    /// <summary>
    /// データの永続化IF
    /// </summary>
    public interface ISerializer
    {
        /// <summary>
        /// データを読み込みます
        /// </summary>
        /// <returns>デシリアライズしたデータ</returns>
        TotalGameResult Read();
        
        /// <summary>
        /// データをシリアライズします
        /// </summary>
        /// <param name="data">出力するデータ</param>
        void Write(TotalGameResult data);
    }
}
