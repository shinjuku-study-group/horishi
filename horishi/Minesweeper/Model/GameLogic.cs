using Minesweeper.Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Minesweeper.Model
{
    /// <summary>
    /// マインスイーパーのロジッククラス
    /// </summary>
    public sealed class GameLogic
    {
        #region フィールド
        /// <summary>
        /// シングルトンインスタンス
        /// </summary>
        static GameLogic instance = new GameLogic(new EasyLevel());

        /// <summary>
        /// セル一覧
        /// </summary>
        private IDictionary<Point, Cell> cellDic = new Dictionary<Point, Cell>();

        /// <summary>
        /// 開いたセルの数
        /// </summary>
        private int openedCellCount = 0;

        /// <summary>
        /// タイム計測用
        /// </summary>
        private Stopwatch stopwatch = new Stopwatch();

        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="level">ゲーム難易度</param>
        private GameLogic(GameLevel level)
        {
            this.GameLevel = level;
            this.Start();
        }

        #endregion

        #region プロパティ
        /// <summary>
        /// シングルトンインスタンスを取得します
        /// </summary>
        public static GameLogic Instance { get { return instance; } }

        /// <summary>
        /// ゲーム難易度
        /// </summary>
        public GameLevel GameLevel { get; private set; }

        /// <summary>
        /// クリア済みかどうか
        /// </summary>
        public bool IsCleared
        {
            get { return (this.GameLevel.Column * this.GameLevel.Row - this.GameLevel.MineNum) == this.openedCellCount; }
        }

        /// <summary>
        /// ゲームオーバー済みかどうか
        /// </summary>
        public bool IsGameOver
        {
            get;
            private set;
        }

        #endregion

        #region メソッド
        /// <summary>
        /// 次のインスタンスを作成します
        /// </summary>
        /// <param name="level">ゲーム難易度</param>
        /// <returns>シングルトンインスタンス</returns>
        public static GameLogic CreateNextInstance(GameLevel level)
        {
            instance = new GameLogic(level);
            
            return instance;
        }

        /// <summary>
        /// セルを取得します
        /// </summary>
        /// <param name="col">X座標</param>
        /// <param name="row">Y座標</param>
        /// <returns>セル</returns>
        public Cell GetCell(int col, int row)
        {
            if (this.cellDic.ContainsKey(Point.GetPoint(col, row)))
            {
                return this.cellDic[Point.GetPoint(col, row)];
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// ゲームを開始します
        /// </summary>
        public void Start()
        {
            this.CellInitialize();
            this.SetMineCell();
            this.IsGameOver = false;
            this.openedCellCount = 0;
            this.stopwatch.Restart();
        }

        /// <summary>
        /// セルを開く
        /// </summary>
        /// <param name="col">X座標</param>
        /// <param name="row">Y座標</param>
        /// <returns>周りの地雷の数</returns>
        public IEnumerable<Tuple<int, Point>> Open(int col, int row)
        {
            var point = Point.GetPoint(col, row);
            Cell target = this.cellDic[point];
            target.Open();
            if(this.openedCellCount == 0)
            {
                this.stopwatch.Restart();
            }
            this.openedCellCount++;

            var result = new List<Tuple<int, Point>>();
            if (target.IsMineCell)
            {
                this.IsGameOver = true;
                result.Add(new Tuple<int, Point>(-1, point));
                return result;
            }

            int aroundCount = this.GetAroundMineCount(col, row);
            result.Add(new Tuple<int, Point>(aroundCount, point));
            if(aroundCount == 0)
            {
                result.AddRange(this.OpenAroundCell(col, row));
            }
            
            if(this.IsCleared)
            {
                this.stopwatch.Stop();
            }
            return result;
        }

        /// <summary>
        /// クリア時間を取得します
        /// </summary>
        /// <returns>クリア時間</returns>
        public TimeSpan GetClearTime()
        {
            return this.IsCleared ? this.stopwatch.Elapsed : new TimeSpan();
        }

        /// <summary>
        /// 地雷セルのポイントを取得します
        /// </summary>
        /// <returns>地雷セルのポイント一覧</returns>
        public IEnumerable<Point> GetMineCellPoints()
        {
            return this.cellDic.Where(kv => kv.Value.IsMineCell).Select(kv => kv.Key);
        }

        /// <summary>
        /// 開いているセルのポイントを取得します
        /// </summary>
        /// <returns>開いているセルのポイント一覧</returns>
        public IEnumerable<Point> GetOpenedCellPoints() 
        {
            return this.cellDic.Where(kv => kv.Value.IsOpened).Select(kv => kv.Key);
        }
        #endregion

        #region private methods
        /// <summary>
        /// フィールドを通常セルで初期化します
        /// </summary>
        private void CellInitialize()
        {
            for (int i = 0; i < this.GameLevel.Row; i++)
            {
                for (int j = 0; j < this.GameLevel.Column; j++)
                {
                    this.cellDic[Point.GetPoint(j, i)] = new Cell();
                }
            }
        }

        /// <summary>
        /// フィールドに地雷セルをセットします
        /// </summary>
        private void SetMineCell()
        {
            Random ranInd = new Random();
            int col;
            int row;
            for (int setCount = 0; setCount < this.GameLevel.MineNum; )
            {
                col = ranInd.Next(0, this.GameLevel.Column - 1);
                row = ranInd.Next(0, this.GameLevel.Row - 1);
                Cell target = this.GetCell(col, row);
                if(!target.IsMineCell)
                {
                    this.cellDic[Point.GetPoint(col, row)] = new MineCell();
                    setCount++;
                }
            }
        }

        /// <summary>
        /// 対象のセルの周りの地雷の数を取得します
        /// </summary>
        /// <param name="col">対象のX</param>
        /// <param name="row">対象のY</param>
        /// <returns>周りの地雷の数</returns>
        private int GetAroundMineCount(int col, int row)
        {
            IEnumerable<int> colArounds = new int[] { col - 1, col, col + 1 }.Where(i => 0 <= i && i < this.GameLevel.Column);
            IEnumerable<int> rowArounds = new int[] { row - 1, row, row + 1 }.Where(i => 0 <= i && i < this.GameLevel.Row);
            int mineCount = 0;
            foreach(int rowAround in rowArounds)
            {
                foreach(int colAround in colArounds)
                {
                    if(colAround == col && rowAround == row)
                    {
                        continue;
                    }
                    Cell target = this.GetCell(colAround, rowAround);
                    if(target.IsMineCell)
                    {
                        mineCount++;
                    }
                }
            }
            return mineCount;
         }

        /// <summary>
        /// 再帰的に周りのセルを開きます
        /// </summary>
        /// <param name="col">対象のX</param>
        /// <param name="row">対象のY</param>
        /// <returns>開いたセルの情報(周りの地雷数,座標)</returns>
        private IEnumerable<Tuple<int, Point>> OpenAroundCell(int col, int row)
        {
            IEnumerable<int> colArounds = new int[] { col - 1, col, col + 1 }.Where(i => 0 <= i && i < this.GameLevel.Column);
            IEnumerable<int> rowArounds = new int[] { row - 1, row, row + 1 }.Where(i => 0 <= i && i < this.GameLevel.Row);
            var result = new List<Tuple<int, Point>>();
            foreach (int rowAround in rowArounds)
            {
                foreach (int colAround in colArounds)
                {
                    Cell target = this.GetCell(colAround, rowAround);
                    if(!target.IsOpened)
                    {
                        result.AddRange(this.Open(colAround, rowAround));
                    }
                }
            }
            return result;
        }

        #endregion
    }
}
