using System.Collections.Generic;

namespace Minesweeper.Common
{
    /// <summary>
    /// グリッドの座標クラス
    /// </summary>
    public sealed partial class Point
    {
        /// <summary>
        /// 座標記憶
        /// </summary>
        private static IDictionary<int, Point> pointDic = new Dictionary<int, Point>();

        /// <summary>
        /// 座標を取得します
        /// </summary>
        /// <returns></returns>
        public static Point GetPoint(int col, int row)
        {
            int key = row * 100 + col;
            if(!pointDic.ContainsKey(key))
            {
                pointDic[key] = new Point(col, row);
            }
            return pointDic[key];
        }

        /// <summary>
        /// 内部用コンストラクタ
        /// </summary>
        /// <param name="col">X座標</param>
        /// <param name="row">Y座標</param>
        private Point(int col, int row)
        {
            this.Column = col;
            this.Row = row;
        }

        /// <summary>
        /// X座標
        /// </summary>
        public int Column
        {
            get;
            private set;
        }

        /// <summary>
        /// Y座標
        /// </summary>
        public int Row
        {
            get;
            private set;
        }

        /// <summary>
        /// ハッシュ値を取得する
        /// </summary>
        /// <returns>ハッシュ値</returns>
        public override int GetHashCode()
        {
            // 縦横ともに100を超えないことを前提の実装。偏りが出るのには目をつむる
            return this.Row * 100 + this.Column;
        }

        /// <summary>
        /// 同値かどうか比較する
        /// </summary>
        /// <param name="obj">比較対象</param>
        /// <returns>比較結果</returns>
        public override bool Equals(object obj)
        {
            if(obj == null)
            {
                return false;
            }
            if(object.ReferenceEquals(obj, this))
            {
                return true;
            }
            if(this.GetType() != obj.GetType())
            {
                return false;
            }
            Point comp = obj as Point;
            return this.Column == comp.Column && this.Row == comp.Row;
        }
    }
}
