using System;
using System.Drawing;
using System.IO;
using System.Reflection;
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

        private static IniFile ThemeIni;
        private static IniSection Theme;

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
            if (ThemeIni == null) ThemeIni = File.Exists("RAMTheme.ini") ? new IniFile("RAMTheme.ini") : new IniFile();

            Theme = ThemeIni.Section(Assembly.GetExecutingAssembly().GetName().Name);

            if (Theme.Exists("AccountsBG")) AccountBackground = ColorTranslator.FromHtml(Theme.Get("AccountsBG"));
            if (Theme.Exists("AccountsFG")) AccountForeground = ColorTranslator.FromHtml(Theme.Get("AccountsFG"));

            if (Theme.Exists("ButtonsBG")) ButtonsBackground = ColorTranslator.FromHtml(Theme.Get("ButtonsBG"));
            if (Theme.Exists("ButtonsFG")) ButtonsForeground = ColorTranslator.FromHtml(Theme.Get("ButtonsFG"));
            if (Theme.Exists("ButtonsBC")) ButtonsBorder = ColorTranslator.FromHtml(Theme.Get("ButtonsBC"));
            if (Theme.Exists("ButtonStyle") && Enum.TryParse(Theme.Get("ButtonStyle"), out FlatStyle BS)) ButtonStyle = BS;

            if (Theme.Exists("FormsBG")) FormsBackground = ColorTranslator.FromHtml(Theme.Get("FormsBG"));
            if (Theme.Exists("FormsFG")) FormsForeground = ColorTranslator.FromHtml(Theme.Get("FormsFG"));
            if (Theme.Exists("DarkTopBar") && bool.TryParse(Theme.Get("DarkTopBar"), out bool DarkTopBar)) UseDarkTopBar = DarkTopBar;
            if (Theme.Exists("ShowHeaders") && bool.TryParse(Theme.Get("ShowHeaders"), out bool bShowHeaders)) ShowHeaders = bShowHeaders;

            if (Theme.Exists("TextBoxesBG")) TextBoxesBackground = ColorTranslator.FromHtml(Theme.Get("TextBoxesBG"));
            if (Theme.Exists("TextBoxesFG")) TextBoxesForeground = ColorTranslator.FromHtml(Theme.Get("TextBoxesFG"));
            if (Theme.Exists("TextBoxesBC")) TextBoxesBorder = ColorTranslator.FromHtml(Theme.Get("TextBoxesBC"));
        }

        public static void SaveTheme()
        {
            if (ThemeIni == null) ThemeIni = File.Exists("RAMTheme.ini") ? new IniFile("RAMTheme.ini") : new IniFile();
            if (Theme == null) Theme = ThemeIni.Section(Assembly.GetExecutingAssembly().GetName().Name);

            Theme.Set("AccountsBG", ToHexString(AccountBackground));
            Theme.Set("AccountsFG", ToHexString(AccountForeground));

            Theme.Set("ButtonsBG", ToHexString(ButtonsBackground));
            Theme.Set("ButtonsFG", ToHexString(ButtonsForeground));
            Theme.Set("ButtonsBC", ToHexString(ButtonsBorder));
            Theme.Set("ButtonStyle", ButtonStyle.ToString());

            Theme.Set("FormsBG", ToHexString(FormsBackground));
            Theme.Set("FormsFG", ToHexString(FormsForeground));
            Theme.Set("DarkTopBar", UseDarkTopBar.ToString());
            Theme.Set("ShowHeaders", ShowHeaders.ToString());

            Theme.Set("TextBoxesBG", ToHexString(TextBoxesBackground));
            Theme.Set("TextBoxesFG", ToHexString(TextBoxesForeground));
            Theme.Set("TextBoxesBC", ToHexString(TextBoxesBorder));
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

                AccountManager.Instance.ApplyTheme();
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

                AccountManager.Instance.ApplyTheme();
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

                AccountManager.Instance.ApplyTheme();
                SaveTheme();
            }
        }

        private void ChangeStyle_Click(object sender, EventArgs e)
        {
            ButtonStyle = ButtonStyle.Next();
            AccountManager.Instance.ApplyTheme();
            SaveTheme();
        }

        private void HideHeaders_Click(object sender, EventArgs e)
        {
            ShowHeaders = !ShowHeaders;
            AccountManager.Instance.ApplyTheme();
            SaveTheme();
        }

        private void ToggleDarkTopBar_Click(object sender, EventArgs e)
        {
            UseDarkTopBar = !UseDarkTopBar;
            SaveTheme();
            MessageBox.Show("This option requires RAM to be restarted.\nThis way not work on older versions of windows.\nEnabled: " + (UseDarkTopBar ? "True" : "false"), "Roblox Account Manager", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}