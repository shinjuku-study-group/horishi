using System.Windows;
using System.Windows.Controls;

namespace Minesweeper.Common
{
    /// <summary>
    /// セルとして使用するボタンコントロール
    /// </summary>
    public sealed class CellButton : Button
    {
        /// <summary>
        /// DependencyProperty：Column
        /// </summary>
        public static readonly DependencyProperty CellColumnProperty =
            DependencyProperty.Register("Column", typeof(int), typeof(CellButton), new FrameworkPropertyMetadata(new PropertyChangedCallback(OnCellPointChanged)));

        /// <summary>
        /// DependencyProperty：Row
        /// </summary>
        public static readonly DependencyProperty CellRowProperty =
            DependencyProperty.Register("Row", typeof(int), typeof(CellButton), new FrameworkPropertyMetadata(new PropertyChangedCallback(OnCellPointChanged)));

        /// <summary>
        /// 座標に変更があった場合のイベント処理
        /// </summary>
        /// <param name="obj">変更があったDependencyObject</param>
        /// <param name="e">変更イベント</param>
        private static void OnCellPointChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            var cell = obj as CellButton;

            if (cell != null)
            {
                int newValue = (int)e.NewValue;
                if(e.Property == CellColumnProperty)
                {
                    cell.Column = newValue;
                }
                if(e.Property == CellRowProperty)
                {
                    cell.Row = newValue;
                }
            }
        }

        /// <summary>
        /// GridのX座標
        /// </summary>
        public int Column { get; set; }

        /// <summary>
        /// GridのY座標
        /// </summary>
        public int Row { get; set; }
    }
}
