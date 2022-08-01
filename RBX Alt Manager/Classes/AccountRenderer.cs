using System;
using System.Drawing;

namespace RBX_Alt_Manager.Classes
{
    public class AccountRenderer : BrightIdeasSoftware.BaseRenderer
    {
        public override void Render(Graphics g, Rectangle r)
        {
            base.Render(g, r);

            Account account = RowObject as Account;
            TimeSpan diff = DateTime.Now - account.LastUse;

            if (diff.TotalDays > 20) {
                diff -= TimeSpan.FromDays(20);

                using (Brush b = new SolidBrush(Color.FromArgb(255, 255, 204, 77).Lerp(Color.FromArgb(255, 250, 26, 13), (float)Utilities.MapValue(diff.TotalSeconds, 0, 864000, 0, 1).Clamp(0, 1))))
                    g.FillEllipse(b, new Rectangle(r.X + 3, r.Y + 2, 4, 4));
            }
        }
    }
}