using Framework.Command;
using Framework.Common.Serialize;
using Framework.ViewModel;
using Minesweeper.Common;
using Minesweeper.Model;
using Minesweeper.Model.GameResult;
using Minesweeper.Properties;
using System;
using System.Collections.Generic;

namespace Minesweeper.ViewModel
{
    /// <summary>
    /// メイン画面のViewModelクラス
    /// </summary>
    public sealed partial class MainWindowViewModel : ViewModelBase
    {
        #region フィールド
        /// <summary>
        /// グリッドのバインド先
        /// </summary>
        private IDictionary<Point, CellViewModel> cellGridItemsSource;

        /// <summary>
        /// グリッドのバインド先
        /// </summary>
        private IDictionary<Point, CellViewModel> cellGridItemsSourceForEasy = new Dictionary<Point, CellViewModel>();

        /// <summary>
        /// グリッドのバインド先
        /// </summary>
        private IDictionary<Point, CellViewModel> cellGridItemsSourceForNormal = new Dictionary<Point, CellViewModel>();

        /// <summary>
        /// グリッドのバインド先
        /// </summary>
        private IDictionary<Point, CellViewModel> cellGridItemsSourceForHard = new Dictionary<Point, CellViewModel>();

        /// <summary>
        /// ウィンドウを閉じるか
        /// </summary>
        private bool closeWindow = false;

        /// <summary>
        /// 左クリックコマンド
        /// </summary>
        private DelegateCommand<CellButton> clickLeftCellCommand;

        /// <summary>
        /// 右クリックコマンド
        /// </summary>
        private DelegateCommand<CellButton> clickRightCellCommand;

        /// <summary>
        /// スタートクリックコマンド
        /// </summary>
        private DelegateCommand clickStartButtonCommand;

        #endregion

        #region プロパティ
        /// <summary>
        /// グリッドのバインド先
        /// </summary>
        public IDictionary<Point, CellViewModel> CellGridItemsSource
        {
            get
            {
                return this.cellGridItemsSource;
            }
            set
            {
                this.cellGridItemsSource = value;
                base.NotifyChanged("CellGridItemsSource");
            }
        }

        /// <summary>
        /// マージン幅
        /// </summary>
        public double ContainerMargin
        {
            get { return ControlSizeDefinition.ContainerMargin; }
        }

        /// <summary>
        /// メニューバーのviewmodel
        /// </summary>
        public MenubarViewModel MenubarViewModel
        {
            get;
            private set;
        }

        /// <summary>
        /// ウィンドウを閉じるか
        /// </summary>
        public bool CloseWindow
        {
            get
            {
                return this.closeWindow;
            }
            set
            {
                this.closeWindow = value;
                base.NotifyChanged("CloseWindow");
            }
        }

        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public MainWindowViewModel()
        {
            this.InitializeGrid();
            this.MenubarViewModel = new MenubarViewModel(this.ExecuteStartButtonClick, this.SetCloseWindow);
        }

        #endregion

        #region コマンド
        /// <summary>
        /// 左クリックコマンド
        /// </summary>
        public DelegateCommand<CellButton> ClickLeftCellCommand
        {
            get
            {
                if(this.clickLeftCellCommand == null)
                {
                    this.clickLeftCellCommand = new DelegateCommand<CellButton>(ExecuteLeftCellClick, CanExecuteCellClick);
                }
                return this.clickLeftCellCommand;
            }
        }

        /// <summary>
        /// 右クリックコマンド
        /// </summary>
        public DelegateCommand<CellButton> ClickRightCellCommand
        {
            get
            {
                if (this.clickRightCellCommand == null)
                {
                    this.clickRightCellCommand = new DelegateCommand<CellButton>(ExecuteRightCellClick, CanExecuteCellClick);
                }
                return this.clickRightCellCommand;
            }
        }

        /// <summary>
        /// スタートクリックコマンド
        /// </summary>
        public DelegateCommand ClickStartButtonCommand
        {
            get
            {
                if (this.clickStartButtonCommand == null)
                {
                    this.clickStartButtonCommand = new DelegateCommand(ExecuteStartButtonClick, () => true);
                }
                return this.clickStartButtonCommand;
            }
        }

        /// <summary>
        /// 左クリック実行時のコマンド処理
        /// </summary>
        /// <param name="parameter">クリック対象のボタンコントロール</param>
        private void ExecuteLeftCellClick(CellButton parameter)
        {
            int col = parameter.Column;
            int row = parameter.Row;
            if (GameLogic.Instance.GetCell(col, row).IsMarked)
            {
                return;
            }
            IEnumerable<Tuple<int, Point>> aroundMineInfo = GameLogic.Instance.Open(col, row);
            if (GameLogic.Instance.IsGameOver)
            {
                this.WriteLooseResult();
                this.OpenMineCell();
                this.ClearTextLableContext = Resources.GameOverTextLabelContext;
                this.cellGridItemsSource[Point.GetPoint(col, row)].Display = Resources.MineClickedMark;
                return;
            }

            foreach (Tuple<int, Point> t in aroundMineInfo)
            {
                this.cellGridItemsSource[t.Item2].Display = t.Item1.ToString();
            }
            if (GameLogic.Instance.IsCleared)
            {
                this.WriteWinResult();
                this.OpenMineCell();
                this.ClearTextLableContext = Resources.ClearTextLableContext;
                this.RemainMineCount = 0;
                this.ClearTime = GameLogic.Instance.GetClearTime().ToString(Resources.ClearTimeFormat);
                return;
            }
        }

        /// <summary>
        /// 右クリック実行時のコマンド処理
        /// </summary>
        /// <param name="parameter">クリック対象のボタンコントロール</param>
        private void ExecuteRightCellClick(CellButton parameter)
        {
            Cell target = GameLogic.Instance.GetCell(parameter.Column, parameter.Row);
            CellState state = target.ToNextState();
            if (state == CellState.Default)
            {
                this.CellGridItemsSource[Point.GetPoint(parameter.Column, parameter.Row)].Display = string.Empty;
            }
            else if (state == CellState.MineMarked)
            {
                this.CellGridItemsSource[Point.GetPoint(parameter.Column, parameter.Row)].Display = Resources.MineRightClickMark;
                this.RemainMineCount--;
            }
            else if (state == CellState.QuestionMarked)
            {
                this.CellGridItemsSource[Point.GetPoint(parameter.Column, parameter.Row)].Display = Resources.QuestionMarked;
                this.RemainMineCount++;
            }
        }

        /// <summary>
        /// クリックが実行可能か
        /// </summary>
        /// <param name="parameter">クリック対象のボタンコントロール</param>
        /// <returns>実行可否</returns>
        private bool CanExecuteCellClick(CellButton parameter)
        {
            Cell target = GameLogic.Instance.GetCell(parameter.Column, parameter.Row);

            return !GameLogic.Instance.IsGameOver
                && !GameLogic.Instance.IsCleared
                && target != null
                && !target.IsOpened;
        }

        /// <summary>
        /// 地雷セルを表示します
        /// </summary>
        private void OpenMineCell()
        {
            foreach (Point p in GameLogic.Instance.GetMineCellPoints())
            {
                this.cellGridItemsSource[p].Display = Resources.MineCellMark;
            }
        }

        /// <summary>
        /// スタートボタンをクリックしたときのコマンド処理
        /// </summary>
        private void ExecuteStartButtonClick()
        {
            if(!GameLogic.Instance.IsCleared && !GameLogic.Instance.IsGameOver)
            {
                this.WriteLooseResult();
            }

            foreach (var source in this.CellGridItemsSource)
            {
                source.Value.Display = string.Empty;
            }
            GameLogic.Instance.Start();
            this.ClearTextLableContext = string.Empty;
            this.ClearTime = string.Empty;
            this.InitializeGrid();
        }

        /// <summary>
        /// Gridの初期化を行います
        /// </summary>
        private void InitializeGrid()
        {
            this.Column = GameLogic.Instance.GameLevel.Column;
            this.Row = GameLogic.Instance.GameLevel.Row;
            this.RemainMineCount = GameLogic.Instance.GameLevel.MineNum;
            this.GridWidth = GameLogic.Instance.GameLevel.Column * ControlSizeDefinition.CellWidth;
            this.GridHeight = GameLogic.Instance.GameLevel.Row * ControlSizeDefinition.CellHeight;
            this.WindowWidth = this.GridWidth * 2;
            this.WindowHeight = this.GridHeight * 3;
            if(GameLogic.Instance.GameLevel.GetType() == typeof(EasyLevel))
            {
                this.CreateCellItemsSource(this.cellGridItemsSourceForEasy);
            }
            if (GameLogic.Instance.GameLevel.GetType() == typeof(NormalLevel))
            {
                this.CreateCellItemsSource(this.cellGridItemsSourceForNormal);
            }
            if (GameLogic.Instance.GameLevel.GetType() == typeof(HardLevel))
            {
                this.CreateCellItemsSource(this.cellGridItemsSourceForHard);
            }
        }

        /// <summary>
        /// セルのバインド先を作成します
        /// </summary>
        /// <param name="itemsSource"></param>
        private void CreateCellItemsSource(IDictionary<Point, CellViewModel> itemsSource)
        {
            if (itemsSource.Count == 0)
            {
                for (int c = 0; c < GameLogic.Instance.GameLevel.Column; c++)
                    for (int r = 0; r < GameLogic.Instance.GameLevel.Row; r++)
                        itemsSource[Point.GetPoint(c, r)] = new CellViewModel(string.Empty);
            }
            this.CellGridItemsSource = itemsSource;
        }

        /// <summary>
        /// 負け記録を追記します
        /// </summary>
        private void WriteLooseResult()
        {
            var serializer = new XmlSerializer<TotalGameResult>();
            var result = serializer.Read();
            if (GameLogic.Instance.GameLevel.GetType() == typeof(EasyLevel))
                result.Easy.LooseCount++;
            if (GameLogic.Instance.GameLevel.GetType() == typeof(NormalLevel))
                result.Normal.LooseCount++;
            if (GameLogic.Instance.GameLevel.GetType() == typeof(HardLevel))
                result.Hard.LooseCount++;
            serializer.Write(result);
        }

        /// <summary>
        /// 勝ち記録を追記します
        /// </summary>
        private void WriteWinResult()
        {
            var serializer = new XmlSerializer<TotalGameResult>();
            var result = serializer.Read();
            if (GameLogic.Instance.GameLevel.GetType() == typeof(EasyLevel))
                result.Easy.WinCount++;
            if (GameLogic.Instance.GameLevel.GetType() == typeof(NormalLevel))
                result.Normal.WinCount++;
            if (GameLogic.Instance.GameLevel.GetType() == typeof(HardLevel))
                result.Hard.WinCount++;
            serializer.Write(result);
        }

        /// <summary>
        /// CloseWindowをカプセル化したメソッド
        /// </summary>
        /// <param name="val">セットする値</param>
        private void SetCloseWindow(bool val)
        {
            if (!val && !GameLogic.Instance.IsCleared && !GameLogic.Instance.IsGameOver)
            {
                this.WriteLooseResult();
            }
            this.CloseWindow = val;
        }

        #endregion
    }
}
