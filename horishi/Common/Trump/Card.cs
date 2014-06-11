using System.Collections.Generic;

namespace Common.Trump
{
    /// <summary>
    /// カードモデル
    /// </summary>
    public class Card
    {
        /// <summary>
        /// キャッシュ
        /// </summary>
        private static IDictionary<int, Card> dic = new Dictionary<int, Card>();

        /// <summary>
        /// キャッシュから取得します
        /// </summary>
        /// <param name="mark">マーク</param>
        /// <param name="num">数値</param>
        /// <returns>カード</returns>
        public static Card Get(Mark mark, Number num)
        {
            int key = (int)mark * 100 + (int)num;
            if(!Card.dic.ContainsKey(key))
            {
                Card card = new Card(mark, num);
                Card.dic[key] = card;
            }
            return Card.dic[key];
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="mark">マーク</param>
        /// <param name="num">数値</param>
        private Card(Mark mark, Number num)
        {
            this.Mark = mark;
            this.Number = num;
        }

        /// <summary>
        /// マーク
        /// </summary>
        public Mark Mark
        {
            get;
            private set;
        }

        /// <summary>
        /// 数値
        /// </summary>
        public Number Number
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
            // 偏りが出るのにはry
            return (int)this.Mark * 100 + (int)this.Number;
        }

        /// <summary>
        /// 同値かどうか比較する
        /// </summary>
        /// <param name="obj">比較対象</param>
        /// <returns>比較結果</returns>
        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            if (object.ReferenceEquals(obj, this))
            {
                return true;
            }
            if (this.GetType() != obj.GetType())
            {
                return false;
            }
            Card comp = (Card)obj;
            return this.Mark == comp.Mark && this.Number == comp.Number;
        }
    }
}
