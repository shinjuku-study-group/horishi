using Framework.ViewModel;

namespace Minesweeper.ViewModel
{
    /// <summary>
    /// 難易度ごとの戦歴ViewModel
    /// </summary>
    public sealed class GameResultViewModel : ViewModelBase
    {
        /// <summary>
        /// 勝ち回数
        /// </summary>
        private int winCount = 0;

        /// <summary>
        /// 負け回数
        /// </summary>
        private int looseCount = 0;

        /// <summary>
        /// 難易度
        /// </summary>
        private string level = string.Empty;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="level">難易度</param>
        public GameResultViewModel(string level)
        {
            this.Level = level;
        }

        /// <summary>
        /// 難易度表示文字列
        /// </summary>
        public string Level
        {
            get
            {
                return this.level;
            }
            private set
            {
                this.level = value;
                base.NotifyChanged("Level");
            }
        }

        /// <summary>
        /// 勝ち回数
        /// </summary>
        public string WinCount
        {
            get 
            {
                return this.winCount.ToString() + "回";
            }
            set
            {
                this.winCount = int.Parse(value);
                base.NotifyChanged("WinCount");
            }
        }

        /// <summary>
        /// 負け回数
        /// </summary>
        public string LooseCount
        {
            get
            {
                return this.looseCount.ToString() + "回";
            }
            set
            {
                this.looseCount = int.Parse(value);
                base.NotifyChanged("LooseCount");
            }
        }

        /// <summary>
        /// 勝率
        /// </summary>
        public string WinningPercentage
        {
            get
            {
                if (this.looseCount == 0)
                    return "100%";
                else
                    return ((double)this.winCount / (double)this.looseCount).ToString(".00") + "%";
            }
        }
    }
}
