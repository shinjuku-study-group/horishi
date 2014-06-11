using Framework.Command;
using Framework.ViewModel;
using Minesweeper.Properties;

namespace Minesweeper.ViewModel
{
    /// <summary>
    /// メイン画面のViewModelクラス(t4テンプレート生成部分)
    /// </summary>
    public sealed partial class MainWindowViewModel : ViewModelBase
    {

        #region フィールド
        private System.Double windowWidth ;

        private System.Double windowHeight ;

        private System.Double gridWidth ;

        private System.Double gridHeight ;

        private System.Int32 column ;

        private System.Int32 row ;

        private System.Int32 remainMineCount ;

        private System.String clearTextLableContext ;

        private System.String clearTime ;


        #endregion

        #region プロパティ

        public System.String WindowTitle
        {
            get { return Resources.WindowTitle; }
        }

        public System.String StartButtonContext
        {
            get { return Resources.StartButtonContext; }
        }

        public System.String RemainMineCountLabel
        {
            get { return Resources.RemainMineCountLabel; }
        }

        public System.String ClearTimeLabelContext
        {
            get { return Resources.ClearTimeLabelContext; }
        }

        public System.Double WindowWidth
        {
            get
            {
                return this.windowWidth;
            }
            set
            {
                this.windowWidth = value;
                base.NotifyChanged("WindowWidth");
            }
        }


        public System.Double WindowHeight
        {
            get
            {
                return this.windowHeight;
            }
            set
            {
                this.windowHeight = value;
                base.NotifyChanged("WindowHeight");
            }
        }


        public System.Double GridWidth
        {
            get
            {
                return this.gridWidth;
            }
            set
            {
                this.gridWidth = value;
                base.NotifyChanged("GridWidth");
            }
        }


        public System.Double GridHeight
        {
            get
            {
                return this.gridHeight;
            }
            set
            {
                this.gridHeight = value;
                base.NotifyChanged("GridHeight");
            }
        }


        public System.Int32 Column
        {
            get
            {
                return this.column;
            }
            set
            {
                this.column = value;
                base.NotifyChanged("Column");
            }
        }


        public System.Int32 Row
        {
            get
            {
                return this.row;
            }
            set
            {
                this.row = value;
                base.NotifyChanged("Row");
            }
        }


        public System.Int32 RemainMineCount
        {
            get
            {
                return this.remainMineCount;
            }
            set
            {
                this.remainMineCount = value;
                base.NotifyChanged("RemainMineCount");
            }
        }


        public System.String ClearTextLableContext
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


        public System.String ClearTime
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
