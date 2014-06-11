using System.IO;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;

namespace Framework.Common.Serialize
{
    /// <summary>
    /// XMLでデータを永続化します
    /// </summary>
    public sealed class XmlSerializer<T> : ISerializer<T>
        where T : new ()
    {
        /// <summary>
        /// 出力先パス
        /// </summary>
        private string path;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public XmlSerializer()
        {
            string currentpath = Assembly.GetEntryAssembly().Location;
            string fileName = typeof(T).ToString() + ".config";
            this.path = Path.Combine(Directory.GetParent(currentpath).FullName, fileName);
        }

        /// <summary>
        /// データを読み込みます
        /// </summary>
        /// <param name="path">読み込み先</param>
        /// <returns>デシリアライズしたデータ</returns>
        public T Read()
        {
            if(!File.Exists(path))
            {
                return new T();
            }
            var formatter = new BinaryFormatter();
            using (var fs = File.OpenRead(path))
            {
                return (T)formatter.Deserialize(fs);
            }
        }
        
        /// <summary>
        /// データをシリアライズします
        /// </summary>
        /// <param name="path">出力先</param>
        /// <param name="data">出力するデータ</param>
        public void Write(T data)
        {
            var formatter = new BinaryFormatter();
            using (var stream = new MemoryStream())
            {
                formatter.Serialize(stream, data);
                stream.Seek(0, SeekOrigin.Begin);
                using(var fs = File.Create(path, stream.Capacity))
                {
                    stream.WriteTo(fs);
                }
            }
        }
    }
}
