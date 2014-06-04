using Minesweeper.Model.Serialize;
using Minesweeper.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper.Model.GameResult
{
    public sealed class GameResultCreator
    {
        public static TotalGameResult Create()
        {
            ISerializer serializer = new XmlSerializer();
            return serializer.Read();
        }
    }
}
