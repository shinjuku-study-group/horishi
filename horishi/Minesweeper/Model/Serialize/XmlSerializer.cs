using Minesweeper.Model.GameResult;
using System;
using System.Configuration;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper.Model.Serialize
{
    /// <summary>
    /// XMLでデータを永続化します
    /// </summary>
    public sealed class XmlSerializer : ISerializer
    {
        private string path;

        public XmlSerializer()
        {
            string currentpath = Assembly.GetEntryAssembly().Location;
            string fileName = ConfigurationManager.AppSettings["GameResultConfig"];
            this.path = Path.Combine(Directory.GetParent(currentpath).FullName, fileName);
        }

        /// <summary>
        /// データを読み込みます
        /// </summary>
        /// <param name="path">読み込み先</param>
        /// <returns>デシリアライズしたデータ</returns>
        public TotalGameResult Read()
        {
            if(!File.Exists(path))
            {
                return new TotalGameResult();
            }
            var formatter = new BinaryFormatter();
            using (var fs = File.OpenRead(path))
            {
                return (TotalGameResult)formatter.Deserialize(fs);
            }
        }
        
        /// <summary>
        /// データをシリアライズします
        /// </summary>
        /// <param name="path">出力先</param>
        /// <param name="data">出力するデータ</param>
        public void Write(TotalGameResult data)
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
