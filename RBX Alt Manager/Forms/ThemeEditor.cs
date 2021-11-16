using System;
using System.Drawing;
using System.Windows.Forms;

namespace RBX_Alt_Manager.Forms
{
    public partial class ThemeEditor : Form
    {
        public static Color AccountBackground = SystemColors.Control;
        public static Color AccountForeground = SystemColors.ControlText;

        public static Color ButtonsBackground = SystemColors.Control;
        public static Color ButtonsForeground = SystemColors.ControlText;
        public static Color ButtonsBorder = SystemColors.Control;
        public static FlatStyle ButtonStyle = FlatStyle.Standard;

        public static Color FormsBackground = SystemColors.Control;
        public static Color FormsForeground = SystemColors.ControlText;
        public static bool UseDarkTopBar = true;
        public static bool ShowHeaders = true;

        public static Color TextBoxesBackground = SystemColors.Control;
        public static Color TextBoxesForeground = SystemColors.ControlText;
        public static Color TextBoxesBorder = Color.FromArgb(0x7A7A7A);

        public static string ToHexString(Color c) => $"#{c.R:X2}{c.G:X2}{c.B:X2}";

        private static IniFile Theme;

        public ThemeEditor()
        {
            AccountManager.SetDarkBar(Handle);

            InitializeComponent();
        }

        public void ApplyTheme()
        {
            BackColor = FormsBackground;
            ForeColor = FormsForeground;

            foreach (Control control in this.Controls)
            {
                if (control is Button || control is CheckBox)
                {
                    if (control is Button)
                    {
                        Button b = control as Button;
                        b.FlatStyle = ButtonStyle;
                        b.FlatAppearance.BorderColor = ButtonsBorder;
                    }

                    if (!(control is CheckBox)) control.BackColor = ButtonsBackground;
                    control.ForeColor = ButtonsForeground;
                }
                else if (control is TextBox || control is RichTextBox || control is Label)
                {
                    if (control is Classes.BorderedTextBox)
                    {
                        Classes.BorderedTextBox b = control as Classes.BorderedTextBox;
                        b.BorderColor = TextBoxesBorder;
                    }

                    if (control is Classes.BorderedRichTextBox)
                    {
                        Classes.BorderedRichTextBox b = control as Classes.BorderedRichTextBox;
                        b.BorderColor = TextBoxesBorder;
                    }

                    control.BackColor = TextBoxesBackground;
                    control.ForeColor = TextBoxesForeground;
                }
                else if (control is ListBox)
                {
                    control.BackColor = ButtonsBackground;
                    control.ForeColor = ButtonsForeground;
                }
            }
        }

        public static void LoadTheme()
        {
            if (Theme == null) Theme = new IniFile("RAMTheme.ini");

            if (Theme.KeyExists("AccountsBG")) AccountBackground = ColorTranslator.FromHtml(Theme.Read("AccountsBG"));
            if (Theme.KeyExists("AccountsFG")) AccountForeground = ColorTranslator.FromHtml(Theme.Read("AccountsFG"));

            if (Theme.KeyExists("ButtonsBG")) ButtonsBackground = ColorTranslator.FromHtml(Theme.Read("ButtonsBG"));
            if (Theme.KeyExists("ButtonsFG")) ButtonsForeground = ColorTranslator.FromHtml(Theme.Read("ButtonsFG"));
            if (Theme.KeyExists("ButtonsBC")) ButtonsBorder = ColorTranslator.FromHtml(Theme.Read("ButtonsBC"));
            if (Theme.KeyExists("ButtonStyle") && Enum.TryParse(Theme.Read("ButtonStyle"), out FlatStyle BS)) ButtonStyle = BS;

            if (Theme.KeyExists("FormsBG")) FormsBackground = ColorTranslator.FromHtml(Theme.Read("FormsBG"));
            if (Theme.KeyExists("FormsFG")) FormsForeground = ColorTranslator.FromHtml(Theme.Read("FormsFG"));
            if (Theme.KeyExists("DarkTopBar") && bool.TryParse(Theme.Read("DarkTopBar"), out bool DarkTopBar)) UseDarkTopBar = DarkTopBar;
            if (Theme.KeyExists("ShowHeaders") && bool.TryParse(Theme.Read("ShowHeaders"), out bool bShowHeaders)) ShowHeaders = bShowHeaders;

            if (Theme.KeyExists("TextBoxesBG")) TextBoxesBackground = ColorTranslator.FromHtml(Theme.Read("TextBoxesBG"));
            if (Theme.KeyExists("TextBoxesFG")) TextBoxesForeground = ColorTranslator.FromHtml(Theme.Read("TextBoxesFG"));
            if (Theme.KeyExists("TextBoxesBC")) TextBoxesBorder = ColorTranslator.FromHtml(Theme.Read("TextBoxesBC"));
        }

        public static void SaveTheme()
        {
            if (Theme == null) Theme = new IniFile("RAMTheme.ini");

            Theme.Write("AccountsBG", ToHexString(AccountBackground));
            Theme.Write("AccountsFG", ToHexString(AccountForeground));

            Theme.Write("ButtonsBG", ToHexString(ButtonsBackground));
            Theme.Write("ButtonsFG", ToHexString(ButtonsForeground));
            Theme.Write("ButtonsBC", ToHexString(ButtonsBorder));
            Theme.Write("ButtonStyle", ButtonStyle.ToString());

            Theme.Write("FormsBG", ToHexString(FormsBackground));
            Theme.Write("FormsFG", ToHexString(FormsForeground));
            Theme.Write("DarkTopBar", UseDarkTopBar.ToString());
            Theme.Write("ShowHeaders", ShowHeaders.ToString());

            Theme.Write("TextBoxesBG", ToHexString(TextBoxesBackground));
            Theme.Write("TextBoxesFG", ToHexString(TextBoxesForeground));
            Theme.Write("TextBoxesBC", ToHexString(TextBoxesBorder));
        }

        private void SetBG_Click(object sender, EventArgs e)
        {
            string Selected = Selection.SelectedItem as string;

            if (string.IsNullOrEmpty(Selected)) return;

            if (SelectColor.ShowDialog() == DialogResult.OK)
            {
                switch (Selected)
                {
                    case "Accounts":
                        AccountBackground = SelectColor.Color;
                        break;

                    case "Buttons":
                        ButtonsBackground = SelectColor.Color;
                        break;

                    case "Forms":
                        FormsBackground = SelectColor.Color;
                        break;

                    case "Text Boxes":
                        TextBoxesBackground = SelectColor.Color;
                        break;
                }

                Program.MainForm.ApplyTheme();
                SaveTheme();
            }
        }

        private void SetFG_Click(object sender, EventArgs e)
        {
            string Selected = Selection.SelectedItem as string;

            if (string.IsNullOrEmpty(Selected)) return;

            if (SelectColor.ShowDialog() == DialogResult.OK)
            {
                switch (Selected)
                {
                    case "Accounts":
                        AccountForeground = SelectColor.Color;
                        break;

                    case "Buttons":
                        ButtonsForeground = SelectColor.Color;
                        break;

                    case "Forms":
                        FormsForeground = SelectColor.Color;
                        break;

                    case "Text Boxes":
                        TextBoxesForeground = SelectColor.Color;
                        break;
                }

                Program.MainForm.ApplyTheme();
                SaveTheme();
            }
        }

        private void ThemeEditor_FormClosing(object sender, FormClosingEventArgs e)
        {
            Hide();
            e.Cancel = true;
        }

        private void Selection_SelectedIndexChanged(object sender, EventArgs e)
        {
            string Selected = Selection.SelectedItem as string;

            if (string.IsNullOrEmpty(Selected)) return;

            if (Selected == "Buttons")
            {
                SetBorder.Visible = true;
                ChangeStyle.Visible = true;
                HideHeaders.Visible = false;
                ToggleDarkTopBar.Visible = false;
            }
            else if (Selected == "Text Boxes")
            {
                SetBorder.Visible = true;
                ChangeStyle.Visible = false;
                HideHeaders.Visible = false;
                ToggleDarkTopBar.Visible = false;
            }
            else if (Selected == "Accounts")
            {
                SetBorder.Visible = false;
                ChangeStyle.Visible = false;
                HideHeaders.Visible = true;
                ToggleDarkTopBar.Visible = false;
            }
            else if (Selected == "Forms")
            {
                SetBorder.Visible = false;
                ChangeStyle.Visible = false;
                HideHeaders.Visible = false;
                ToggleDarkTopBar.Visible = true;
            }
            else
            {
                SetBorder.Visible = false;
                ChangeStyle.Visible = false;
                HideHeaders.Visible = false;
                ToggleDarkTopBar.Visible = false;
            }
        }

        private void SetBorder_Click(object sender, EventArgs e)
        {
            string Selected = Selection.SelectedItem as string;

            if (string.IsNullOrEmpty(Selected)) return;

            if (SelectColor.ShowDialog() == DialogResult.OK)
            {
                switch (Selected)
                {
                    case "Buttons":
                        ButtonsBorder = SelectColor.Color;
                        break;

                    case "Text Boxes":
                        TextBoxesBorder = SelectColor.Color;
                        break;
                }

                Program.MainForm.ApplyTheme();
                SaveTheme();
            }
        }

        private void ChangeStyle_Click(object sender, EventArgs e)
        {
            ButtonStyle = ButtonStyle.Next();
            Program.MainForm.ApplyTheme();
            SaveTheme();
        }

        private void HideHeaders_Click(object sender, EventArgs e)
        {
            ShowHeaders = !ShowHeaders;
            Program.MainForm.ApplyTheme();
            SaveTheme();
        }

        private void ToggleDarkTopBar_Click(object sender, EventArgs e)
        {
            UseDarkTopBar = !UseDarkTopBar;
            SaveTheme();
            MessageBox.Show("This option requires RAM to be restarted.\nThis way not work on older versions of windows.\nEnabled: " + (UseDarkTopBar ? "True" : "False"), "Roblox Account Manager", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}