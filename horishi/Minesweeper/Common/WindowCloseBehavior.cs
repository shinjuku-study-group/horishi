using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Minesweeper.Common
{
    /// <summary>
    /// ウィンドウを閉じるビヘイビアクラス
    /// </summary>
    public sealed class WindowCloseBehavior
    {
        public static bool GetClose(DependencyObject obj)
        {
            return (bool)obj.GetValue(CloseProperty);
        }

        public static void SetClose(DependencyObject obj, bool value)
        {
            obj.SetValue(CloseProperty, value);
        }

        public static readonly DependencyProperty CloseProperty =
            DependencyProperty.RegisterAttached("Close", typeof(bool), typeof(WindowCloseBehavior), new PropertyMetadata(false, OnCloseChanged));

        private static void OnCloseChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            var win = obj as Window;
            if (win == null)
            {
                // Window以外のコントロールにこの添付ビヘイビアが付けられていた場合は、
                // コントロールの属しているWindowを取得
                win = Window.GetWindow(obj);
            }

            if (GetClose(obj))
            {
                win.Close();
            }
        }
    }
}
