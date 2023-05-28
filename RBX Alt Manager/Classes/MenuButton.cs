using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace RBX_Alt_Manager.Classes
{
    public class MenuButton : Button // Thank you, fellow developer: https://stackoverflow.com/a/24087828
    {
        [DefaultValue(null)]
        public ContextMenuStrip Menu { get; set; }

        [DefaultValue(false)]
        public bool ShowMenuUnderCursor { get; set; }

        protected override void OnMouseDown(MouseEventArgs mevent)
        {
            if (Menu != null && mevent.Button == MouseButtons.Left && mevent.X > ClientRectangle.Width - Padding.Right - (15 * Program.Scale))
            {
                Point menuLocation;

                if (ShowMenuUnderCursor)
                    menuLocation = mevent.Location;
                else
                    menuLocation = new Point(0, Height - 1);

                Menu.Show(this, menuLocation);

                return;
            }

            base.OnMouseDown(mevent);
        }

        protected override void OnPaint(PaintEventArgs pevent)
        {
            base.OnPaint(pevent);

            if (Menu != null)
            {
                int arrowX = ClientRectangle.Width - Padding.Right - (int)(12 * Program.Scale);
                int arrowY = (ClientRectangle.Height / 2) - (int)(1 * Program.Scale);

                Color color = Enabled ? ForeColor : SystemColors.ControlDark;

                using (Brush brush = new SolidBrush(color))
                {
                    Point[] arrows = new Point[] { new Point(arrowX, arrowY), new Point((int)(arrowX + 7 * Program.Scale), arrowY), new Point((int)(arrowX + 3 * Program.Scale), (int)(arrowY + 4 * Program.Scale) )};
                    pevent.Graphics.FillPolygon(brush, arrows);
                }
            }
        }
    }
}