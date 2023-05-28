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
            bool isOld = diff.TotalDays > 20;

            if (isOld)
            {
                diff -= TimeSpan.FromDays(20);

                using (Brush b = new SolidBrush(Color.FromArgb(255, 255, 204, 77).Lerp(Color.FromArgb(255, 250, 26, 13), (float)Utilities.MapValue(diff.TotalSeconds, 0, 864000, 0, 1).Clamp(0, 1))))
                    g.FillEllipse(b, new Rectangle((int)(r.X + 3f * Program.Scale), (int)(r.Y + 2 * Program.Scale), (int)(4f * Program.Scale), (int)(4f * Program.Scale)));
            }

            if (AccountManager.General.Get<bool>("ShowPresence") && account.Presence != null && account.Presence.userPresenceType != UserPresenceType.Offline)
                using (Brush b = new SolidBrush(Presence.Colors[account.Presence.userPresenceType]))
                    g.FillEllipse(b, new Rectangle((int)(r.X + 3f * Program.Scale + (isOld ? (int)(6f * Program.Scale) : 0)), (int)(r.Y + 2 * Program.Scale), (int)(4f * Program.Scale), (int)(4f * Program.Scale)));
        }
    }
}