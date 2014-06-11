using Common.Trump;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BlackJack.Model
{
    /// <summary>
    /// ブラックジャックのロジック
    /// </summary>
    internal sealed class GameLogic
    {
        /// <summary>
        /// シングルトンインスタンス
        /// </summary>
        private static GameLogic instance = new GameLogic(20, 0);

        /// <summary>
        /// シングルトンインスタンス
        /// </summary>
        public static GameLogic Instance
        {
            get { return GameLogic.instance; }
        }

        /// <summary>
        /// シングルトンインスタンスを作成する
        /// </summary>
        public static GameLogic CreateInstance(int remain, int betCoin)
        {
            GameLogic.instance = new GameLogic(remain, betCoin);
            return GameLogic.instance;
        }

        #region フィールド
        
        /// <summary>
        /// プレイヤーの残り金額
        /// </summary>
        private int remainCoin;

        /// <summary>
        /// 勝利時獲得金
        /// </summary>
        private int getCoinWhenYouWin;

        /// <summary>
        /// 元々の掛け金
        /// </summary>
        private int betCoin;

        /// <summary>
        /// 使用カード
        /// </summary>
        private HashSet<Card> usingCards = new HashSet<Card>();

        /// <summary>
        /// プレイヤーの使用カード
        /// </summary>
        private List<Card> playerCards = new List<Card>();

        /// <summary>
        /// ディーラーの使用カード
        /// </summary>
        private List<Card> dealerCards = new List<Card>();

        /// <summary>
        /// 乱数製造機
        /// </summary>
        private Random ran = new Random();
        #endregion

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="remain">所持金</param>
        /// <param name="betCoin">賭け金</param>
        private GameLogic(int remain, int betCoin)
        {
            this.remainCoin = remain;
            this.betCoin = betCoin;
            this.getCoinWhenYouWin = (int)(betCoin * NumberDefinition.BetRate);
        }

        #region プロパティ

        /// <summary>
        /// 勝利時獲得金
        /// </summary>
        public int GetCoinWhenYouWin
        {
            get { return this.getCoinWhenYouWin; }
        }

        /// <summary>
        /// プレイヤーのポイント
        /// </summary>
        public int PlayerScore
        {
            get;
            set;
        }

        /// <summary>
        /// ディーラーのポイント
        /// </summary>
        public int DealerScore
        {
            get;
            set;
        }

        /// <summary>
        /// バーストしたか:プレーヤー
        /// </summary>
        public bool PlayerIsBurst
        {
            get { return this.PlayerScore > NumberDefinition.BlackJackScore; }
        }

        /// <summary>
        /// ブラックジャックか:プレーヤー
        /// </summary>
        public bool PlayerIsBlackJack
        {
            get 
            {
                return this.playerCards.Count == 2
                    && this.PlayerScore == NumberDefinition.BlackJackScore;
            }
        }

        /// <summary>
        /// バーストしたか：ディーラー
        /// </summary>
        public bool DealerIsBurst
        {
            get { return this.DealerScore > NumberDefinition.BlackJackScore; }
        }

        /// <summary>
        /// ブラックジャックか:ディーラー
        /// </summary>
        public bool DealerIsBlackJack
        {
            get
            {
                return this.dealerCards.Count == 2
                    && this.DealerScore == NumberDefinition.BlackJackScore;
            }
        }

        /// <summary>
        /// インシュランスしたか
        /// </summary>
        public bool IsInsuranced
        {
            get;
            set;
        }

        public bool CanInsurance
        {
            get
            {
                if (this.dealerCards.Count > 0)
                {
                    return !this.IsInsuranced
                        && this.playerCards.Count == 2
                        && this.dealerCards[0].Number == Number.One
                        && this.remainCoin > this.betCoin / 2
                        && this.remainCoin > 0;
                }
                else
                {
                    return false;
                }
            }
        }

        #endregion

        #region メソッド

        /// <summary>
        /// カードを引きます
        /// </summary>
        /// <returns>Card</returns>
        public Card Hit()
        {
            var card = this.GetUnusedCard();
            this.usingCards.Add(card);
            this.playerCards.Add(card);

            this.PlayerScore = this.CalculateScore(this.playerCards);

            return card;
        }

        /// <summary>
        /// ディーラーがヒットします
        /// </summary>
        /// <returns>Card</returns>
        public Card DealerHit()
        {
            var card = this.GetUnusedCard();
            this.usingCards.Add(card);
            this.dealerCards.Add(card);

            this.DealerScore = this.CalculateScore(this.dealerCards);

            return card;
        }

        /// <summary>
        /// スタンドします
        /// </summary>
        public void Stand()
        {
            if(this.PlayerIsBlackJack)
            {
                this.getCoinWhenYouWin = (int)(this.getCoinWhenYouWin * NumberDefinition.BlackJackRate);
            }
        }

        /// <summary>
        /// ダブルします
        /// </summary>
        public Card Double()
        {
            this.remainCoin -= this.betCoin;
            this.betCoin *= 2;
            this.getCoinWhenYouWin = (int)(this.getCoinWhenYouWin * NumberDefinition.DoubleRate);
            return this.Hit();
        }

        /// <summary>
        /// ディーラーのAI
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Card> DealerThinking()
        {
            var cards = new List<Card>();
            while(this.DealerScore <= 16)
            {
                var card = this.DealerHit();
                cards.Add(card);
            }
            return cards;
        }

        /// <summary>
        /// ゲーム結果の獲得金を取得します
        /// </summary>
        /// <returns>獲得金</returns>
        public int GetResultCoin()
        {
            if(this.PlayerIsBlackJack && this.DealerIsBlackJack
                || this.PlayerIsBurst && this.DealerIsBurst
                || this.PlayerScore == this.DealerScore)
            {
                return this.betCoin;
            }
            else if(this.PlayerIsBurst && !this.DealerIsBurst
                || (this.PlayerScore < this.DealerScore && !this.DealerIsBurst))
            {
                return 0;
            }
            else if (!this.PlayerIsBurst && this.DealerIsBurst
                || (!this.PlayerIsBurst && this.PlayerScore > this.DealerScore))
            {
                return this.GetCoinWhenYouWin;
            }
            throw new InvalidOperationException();
        }

        #endregion

        #region private methods

        /// <summary>
        /// 未使用のカードを取得します
        /// </summary>
        /// <returns>未使用のカード</returns>
        private Card GetUnusedCard()
        {
            var card = Card.Get(this.GetRandamMark(), this.GetRandamNumber());
            if (this.usingCards.Contains(card))
                return this.GetUnusedCard();
            else
                return card;
        }

        /// <summary>
        /// ランダムにマークを作成します
        /// </summary>
        /// <returns>Mark</returns>
        private Mark GetRandamMark()
        {
            return (Mark)this.ran.Next(0, 3);
        }

        /// <summary>
        /// ランダムにNumberを作成します
        /// </summary>
        /// <returns>Number</returns>
        private Number GetRandamNumber()
        {
            return (Number)this.ran.Next(1, 13);
        }

        /// <summary>
        /// ポイントを計算します
        /// </summary>
        /// <param name="cards">計算対象</param>
        /// <returns>ポイント</returns>
        private int CalculateScore(IList<Card> cards)
        {
            int score = 0;
            foreach (var card in cards.Where(c => c.Number != Number.One))
            {
                int num = (int)card.Number;
                score += num < 10 ? num : 10;
            }
            var aces = cards.Where(c => c.Number == Number.One).ToList();

            // バーストしていない場合で、1回11足せる場合
            if (aces.Count > 0)
            {
                if (score + 11 + aces.Count - 1 <= NumberDefinition.BlackJackScore)
                {
                    score += 11 + aces.Count - 1;
                }
                else
                {
                    score += aces.Count;
                }
            }
            return score;
        }

        #endregion
    }
}
