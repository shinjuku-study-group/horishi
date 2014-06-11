
namespace Framework.Common.Serialize
{
    /// <summary>
    /// データの永続化IF
    /// </summary>
    public interface ISerializer<T>
    {
        /// <summary>
        /// データを読み込みます
        /// </summary>
        /// <returns>デシリアライズしたデータ</returns>
        T Read();
        
        /// <summary>
        /// データをシリアライズします
        /// </summary>
        /// <param name="data">出力するデータ</param>
        void Write(T data);
    }
}
