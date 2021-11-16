using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace RBX_Alt_Manager.Classes
{
    public class BorderedTextBox : TextBox // https://github.com/r-aghaei/TextBoxBorderColor/blob/master/src/TextBoxBorderColor/MyTextBox.cs
    {
        const int WM_NCPAINT = 0x85;
        const uint RDW_INVALIDATE = 0x1;
        const uint RDW_IUPDATENOW = 0x100;
        const uint RDW_FRAME = 0x400;
        [DllImport("user32.dll")]
        static extern IntPtr GetWindowDC(IntPtr hWnd);
        [DllImport("user32.dll")]
        static extern int ReleaseDC(IntPtr hWnd, IntPtr hDC);
        [DllImport("user32.dll")]
        static extern bool RedrawWindow(IntPtr hWnd, IntPtr lprc, IntPtr hrgn, uint flags);
        Color borderColor = Color.FromArgb(0x7A7A7A);

        [StructLayout(LayoutKind.Sequential)]
        public struct ANIMATIONINFO
        {
            public uint cbSize;
            public int iMinAnimate;
        };

        [DllImport("user32", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SystemParametersInfo(uint uiAction, uint uiParam, ref ANIMATIONINFO pvParam, uint fWinIni);

        public static uint SPIF_SENDCHANGE = 0x02;
        public static uint SPI_SETANIMATION = 0x0049;

        public Color BorderColor
        {
            get { return borderColor; }
            set
            {
                borderColor = value;
                RedrawWindow(Handle, IntPtr.Zero, IntPtr.Zero, RDW_FRAME | RDW_IUPDATENOW | RDW_INVALIDATE);
            }
        }

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            if (m.Msg == WM_NCPAINT && BorderColor != Color.Transparent && BorderStyle == BorderStyle.Fixed3D)
            {
                var hdc = GetWindowDC(this.Handle);

                using (var g = Graphics.FromHdcInternal(hdc))
                using (var p = new Pen(BorderColor, 1f))
                {
                    SolidBrush opaqueBrush = new SolidBrush(Color.FromArgb(128, BorderColor.R, BorderColor.G, BorderColor.B));

                    g.DrawRectangle(new Pen(opaqueBrush), new Rectangle(0, 0, Width - 1, Height - 1));
                    g.DrawRectangle(p, new Rectangle(1, 1, Width - 3, Height - 3));
                }
                ReleaseDC(this.Handle, hdc);
            }
        }
        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            RedrawWindow(Handle, IntPtr.Zero, IntPtr.Zero,
                   RDW_FRAME | RDW_IUPDATENOW | RDW_INVALIDATE);
        }
    }
}