using Minesweeper.Common;
using Minesweeper.Properties;
using System;
using System.Collections.Generic;

namespace Minesweeper.ViewModel
{
    /// <summary>
    /// メイン画面のViewModelクラス(t4テンプレート生成部分)
    /// </summary>
    public sealed partial class MainWindowViewModel : ViewModelBase
    {

        #region フィールド

        private IDictionary<Point, CellViewModel> cellGridItemsSource
            = new Dictionary<Point, CellViewModel>()
            {
                    { Point.GetPoint(0, 0), new CellViewModel(string.Empty) },
                    { Point.GetPoint(1, 0), new CellViewModel(string.Empty) },
                    { Point.GetPoint(2, 0), new CellViewModel(string.Empty) },
                    { Point.GetPoint(3, 0), new CellViewModel(string.Empty) },
                    { Point.GetPoint(4, 0), new CellViewModel(string.Empty) },
                    { Point.GetPoint(0, 1), new CellViewModel(string.Empty) },
                    { Point.GetPoint(1, 1), new CellViewModel(string.Empty) },
                    { Point.GetPoint(2, 1), new CellViewModel(string.Empty) },
                    { Point.GetPoint(3, 1), new CellViewModel(string.Empty) },
                    { Point.GetPoint(4, 1), new CellViewModel(string.Empty) },
                    { Point.GetPoint(0, 2), new CellViewModel(string.Empty) },
                    { Point.GetPoint(1, 2), new CellViewModel(string.Empty) },
                    { Point.GetPoint(2, 2), new CellViewModel(string.Empty) },
                    { Point.GetPoint(3, 2), new CellViewModel(string.Empty) },
                    { Point.GetPoint(4, 2), new CellViewModel(string.Empty) },
                    { Point.GetPoint(0, 3), new CellViewModel(string.Empty) },
                    { Point.GetPoint(1, 3), new CellViewModel(string.Empty) },
                    { Point.GetPoint(2, 3), new CellViewModel(string.Empty) },
                    { Point.GetPoint(3, 3), new CellViewModel(string.Empty) },
                    { Point.GetPoint(4, 3), new CellViewModel(string.Empty) },
                    { Point.GetPoint(0, 4), new CellViewModel(string.Empty) },
                    { Point.GetPoint(1, 4), new CellViewModel(string.Empty) },
                    { Point.GetPoint(2, 4), new CellViewModel(string.Empty) },
                    { Point.GetPoint(3, 4), new CellViewModel(string.Empty) },
                    { Point.GetPoint(4, 4), new CellViewModel(string.Empty) },
            };

        private string clearTextLableContext = string.Empty;
        private string clearTime = string.Empty;

        #endregion

        #region プロパティ

        public IDictionary<Point, CellViewModel> CellGridItemsSource
        {
            get
            {
                return this.cellGridItemsSource;
            }
        }

        public string WindowTitle
        {
            get { return Resources.WindowTitle; }
        }

        public string WindowWidth
        {
            get { return Resources.WindowWidth; }
        }

        public string WindowHeight
        {
            get { return Resources.WindowHeight; }
        }

        public string CellWidth
        {
            get { return Resources.CellWidth; }
        }

        public string CellHeight
        {
            get { return Resources.CellHeight; }
        }

        public string ContainerMargin
        {
            get { return Resources.ContainerMargin; }
        }

        public string StartButtonContext
        {
            get { return Resources.StartButtonContext; }
        }

        public string RemainMineCountLabel
        {
            get { return Resources.RemainMineCountLabel; }
        }

        public string ClearTimeLabelContext
        {
            get { return Resources.ClearTimeLabelContext; }
        }

        public string ClearTextLableContext
        {
            get
            {
                return this.clearTextLableContext;
            }
            set
            {
                this.clearTextLableContext = value;
                base.NotifyChanged("ClearTextLableContext");
            }
        }

        public string ClearTime
        {
            get
            {
                return this.clearTime;
            }
            set
            {
                this.clearTime = value;
                base.NotifyChanged("ClearTime");
            }
        }

        #endregion
    }
}
