using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace RBX_Alt_Manager.Classes
{
    internal class NBTabControl : TabControl
    {
        // https://dotnetrix.co.uk/tabcontrol.htm

        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private Container components = null;

        public NBTabControl()
        {
            InitializeComponent();

            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.DoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.UserPaint, true);
        }

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
                if (components != null)
                    components.Dispose();

            base.Dispose(disposing);
        }

        #region Component Designer generated code
        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() =>
            components = new System.ComponentModel.Container();
        #endregion

        #region Interop

        [StructLayout(LayoutKind.Sequential)]
        private struct NMHDR
        {
            public IntPtr HWND;
            public uint idFrom;
            public int code;
            public override String ToString()
            {
                return String.Format("Hwnd: {0}, ControlID: {1}, Code: {2}", HWND, idFrom, code);
            }
        }

        private const int TCN_FIRST = 0 - 550;
        private const int TCN_SELCHANGING = (TCN_FIRST - 2);

        private const int WM_USER = 0x400;
        private const int WM_NOTIFY = 0x4E;
        private const int WM_REFLECT = WM_USER + 0x1C00;

        #endregion

        #region BackColor Manipulation

        private Color m_Backcolor = Color.Empty;
        [Browsable(true), Description("The background color used to display text and graphics in a control.")]
        public override Color BackColor
        {
            get
            {
                if (m_Backcolor.Equals(Color.Empty))
                {
                    if (Parent == null)
                        return Control.DefaultBackColor;
                    else
                        return Parent.BackColor;
                }
                return m_Backcolor;
            }
            set
            {
                if (m_Backcolor.Equals(value)) return;
                m_Backcolor = value;
                Invalidate();

                base.OnBackColorChanged(EventArgs.Empty);
            }
        }

        public bool ShouldSerializeBackColor() => !m_Backcolor.Equals(Color.Empty);

        public override void ResetBackColor()
        {
            m_Backcolor = Color.Empty;
            Invalidate();
        }

        #endregion

        protected override void OnParentBackColorChanged(EventArgs e)
        {
            base.OnParentBackColorChanged(e);
            Invalidate();
        }

        protected override void OnSelectedIndexChanged(EventArgs e)
        {
            base.OnSelectedIndexChanged(e);
            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            e.Graphics.Clear(BackColor);
            Rectangle r = ClientRectangle;

            if (TabCount <= 0) return;

            StringFormat sf = new StringFormat();
            sf.Alignment = StringAlignment.Center;
            sf.LineAlignment = StringAlignment.Center;
            r = SelectedTab.Bounds;
                // new Rectangle(SelectedTab.Bounds.X, SelectedTab.Bounds.Y, (int)(SelectedTab.Bounds.Width * Program.Scale), (int)(SelectedTab.Bounds.Height * Program.Scale));

            r.Inflate(3, 3);

            TabPage tp = TabPages[SelectedIndex];
            SolidBrush PaintBrush = new SolidBrush(tp.BackColor);

            e.Graphics.FillRectangle(PaintBrush, r);

            Color br = PaintBrush.Color.GetBrightness() < 0.4 ? ControlPaint.Light(PaintBrush.Color, 0.7f) : ControlPaint.Dark(PaintBrush.Color, 0.7f);
            ControlPaint.DrawBorder(e.Graphics, r,
                br, 1, ButtonBorderStyle.Solid,
                br, 1, ButtonBorderStyle.Solid,
                br, 1, ButtonBorderStyle.Solid,
                br, 1, ButtonBorderStyle.Solid);

            for (int index = 0; index <= TabCount - 1; index++)
                PaintTabButton(index, e, r, PaintBrush, sf);

            PaintBrush.Dispose();
        }

        private void PaintTabButton(int index, PaintEventArgs e, Rectangle r, SolidBrush PaintBrush, StringFormat sf)
        {
            TabPage tp = TabPages[index];
            r = GetTabRect(index);
            // r = new Rectangle((int)(r.X*Program.Scale), r.Y, (int)(r.Width * Program.Scale), (int)(r.Height * 1f));
            bool isSelected = index == SelectedIndex;
            ButtonBorderStyle bs = index == SelectedIndex ? ButtonBorderStyle.Solid : ButtonBorderStyle.Solid;

            PaintBrush.Color = tp.BackColor;
            e.Graphics.FillRectangle(PaintBrush, r);

            Color br = PaintBrush.Color.GetBrightness() < 0.4 ? ControlPaint.Light(PaintBrush.Color, isSelected ? 1f : 0.4f) : ControlPaint.Dark(PaintBrush.Color, isSelected ? 1f : 0.4f);
            ControlPaint.DrawBorder(e.Graphics, r,
                br, 1, bs,
                br, 1, bs,
                br, 1, bs,
                br, 1, isSelected ? ButtonBorderStyle.None : bs);
            PaintBrush.Color = tp.ForeColor;

            if (Alignment == TabAlignment.Left || Alignment == TabAlignment.Right)
            {
                float RotateAngle = 90;
                if (Alignment == TabAlignment.Left) RotateAngle = 270;
                PointF cp = new PointF(r.Left + (r.Width >> 1), r.Top + (r.Height >> 1));
                e.Graphics.TranslateTransform(cp.X, cp.Y);
                e.Graphics.RotateTransform(RotateAngle);
                r = new Rectangle(-(r.Height >> 1), -(r.Width >> 1), r.Height, r.Width);
            }

            if (tp.Enabled)
                e.Graphics.DrawString(tp.Text, Font, PaintBrush, (RectangleF)r, sf);
            else
                ControlPaint.DrawStringDisabled(e.Graphics, tp.Text, Font, tp.BackColor, (RectangleF)r, sf);

            e.Graphics.ResetTransform();
        }

        [Description("Occurs as a tab is being changed.")]
        public event SelectedTabPageChangeEventHandler SelectedIndexChanging;

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == (WM_REFLECT + WM_NOTIFY))
            {
                NMHDR hdr = (NMHDR)(Marshal.PtrToStructure(m.LParam, typeof(NMHDR)));
                if (hdr.code == TCN_SELCHANGING)
                {
                    TabPage tp = TestTab(PointToClient(Cursor.Position));
                    if (tp != null)
                    {
                        TabPageChangeEventArgs e = new TabPageChangeEventArgs(SelectedTab, tp);
                        if (SelectedIndexChanging != null)
                            SelectedIndexChanging(this, e);
                        if (e.Cancel || tp.Enabled == false)
                        {
                            m.Result = new IntPtr(1);
                            return;
                        }
                    }
                }
            }
            base.WndProc(ref m);
        }

        private TabPage TestTab(Point pt)
        {
            for (int index = 0; index <= TabCount - 1; index++)
                if (GetTabRect(index).Contains(pt.X, pt.Y))
                    return TabPages[index];

            return null;
        }
    }

    public class TabPageChangeEventArgs : EventArgs
    {
        private TabPage _Selected = null;
        private TabPage _PreSelected = null;
        public bool Cancel = false;

        public TabPage CurrentTab => _Selected;
        public TabPage NextTab => _PreSelected;

        public TabPageChangeEventArgs(TabPage CurrentTab, TabPage NextTab)
        {
            _Selected = CurrentTab;
            _PreSelected = NextTab;
        }
    }

    public delegate void SelectedTabPageChangeEventHandler(Object sender, TabPageChangeEventArgs e);
}