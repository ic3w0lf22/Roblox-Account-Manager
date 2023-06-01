using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace RBX_Alt_Manager.Forms
{
    public partial class SettingsForm : Form
    {
        private bool SettingsLoaded = false;
        private RegistryKey StartupKey;

        public SettingsForm()
        {
            AccountManager.SetDarkBar(Handle);

            InitializeComponent();
            this.Rescale();
        }

        private void SettingsForm_Load(object sender, EventArgs e)
        {
            AutoUpdateCB.Checked = AccountManager.General.Get<bool>("CheckForUpdates");
            AsyncJoinCB.Checked = AccountManager.General.Get<bool>("AsyncJoin");
            LaunchDelayNumber.Value = AccountManager.General.Get<decimal>("AccountJoinDelay");
            SavePasswordCB.Checked = AccountManager.General.Get<bool>("SavePasswords");
            DisableAgingAlertCB.Checked = AccountManager.General.Get<bool>("DisableAgingAlert");
            HideMRobloxCB.Checked = AccountManager.General.Get<bool>("HideRbxAlert");
            DisableImagesCB.Checked = AccountManager.General.Get<bool>("DisableImages");
            ShuffleLowestServerCB.Checked = AccountManager.General.Get<bool>("ShuffleChoosesLowestServer");
            MultiRobloxCB.Checked = AccountManager.General.Get<bool>("EnableMultiRbx");
            RegionFormatTB.Text = AccountManager.General.Get<string>("ServerRegionFormat");
            MaxRecentGamesNumber.Value = AccountManager.General.Get<int>("MaxRecentGames");

            EnableDMCB.Checked = AccountManager.Developer.Get<bool>("DevMode");
            EnableWSCB.Checked = AccountManager.Developer.Get<bool>("EnableWebServer");
            ERRPCB.Checked = AccountManager.WebServer.Get<bool>("EveryRequestRequiresPassword");
            AllowGCCB.Checked = AccountManager.WebServer.Get<bool>("AllowGetCookie");
            AllowGACB.Checked = AccountManager.WebServer.Get<bool>("AllowGetAccounts");
            AllowLACB.Checked = AccountManager.WebServer.Get<bool>("AllowLaunchAccount");
            AllowAECB.Checked = AccountManager.WebServer.Get<bool>("AllowAccountEditing");
            AllowExternalConnectionsCB.Checked = AccountManager.WebServer.Get<bool>("AllowExternalConnections");
            PasswordTextBox.Text = AccountManager.WebServer.Get("Password");
            PortNumber.Value = AccountManager.WebServer.Get<decimal>("WebServerPort");

            PresenceCB.Checked = AccountManager.General.Get<bool>("ShowPresence");
            PresenceUpdateRateNum .Value = AccountManager.General.Get<int>("PresenceUpdateRate");
            UnlockFPSCB.Checked = AccountManager.General.Get<bool>("UnlockFPS");
            MaxFPSValue.Value = AccountManager.General.Get<int>("MaxFPSValue");

            if (AccountManager.General.Exists("CustomClientSettings") && File.Exists(AccountManager.General.Get<string>("CustomClientSettings")))
            {
                OverrideWithCustomCB.Checked = true;
                UnlockFPSCB.Enabled = false;
            }

            try { StartupKey = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", true); } catch { }

            if (StartupKey != null && StartupKey.GetValue(Application.ProductName) is string ExistingPath)
            {
                if (ExistingPath != Application.ExecutablePath) // fix the path if moved
                    StartupKey.SetValue(Application.ProductName, Application.ExecutablePath);

                StartOnPCStartup.Checked = true;
            }

            SettingsLoaded = true;

            ApplyTheme();
        }

        private void SettingsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            Hide();
        }

        #region General

        private void AutoUpdateCB_CheckedChanged(object sender, EventArgs e)
        {
            if (!SettingsLoaded) return;

            AccountManager.General.Set("CheckForUpdates", AutoUpdateCB.Checked ? "true" : "false");
            AccountManager.IniSettings.Save("RAMSettings.ini");
        }

        private void AsyncJoinCB_CheckedChanged(object sender, EventArgs e)
        {
            LaunchDelayNumber.Enabled = !AsyncJoinCB.Checked;

            if (!SettingsLoaded) return;

            AccountManager.General.Set("AsyncJoin", AsyncJoinCB.Checked ? "true" : "false");
            AccountManager.IniSettings.Save("RAMSettings.ini");
        }

        private void LaunchDelayNumber_ValueChanged(object sender, EventArgs e)
        {
            if (!SettingsLoaded) return;

            AccountManager.General.Set("AccountJoinDelay", LaunchDelayNumber.Value.ToString());
            AccountManager.IniSettings.Save("RAMSettings.ini");
        }

        private void MaxRecentGamesNumber_ValueChanged(object sender, EventArgs e)
        {
            if (!SettingsLoaded) return;

            AccountManager.General.Set("MaxRecentGames", MaxRecentGamesNumber.Value.ToString());
            AccountManager.IniSettings.Save("RAMSettings.ini");
        }

        private void SavePasswordCB_CheckedChanged(object sender, EventArgs e)
        {
            if (!SettingsLoaded) return;

            AccountManager.General.Set("SavePasswords", SavePasswordCB.Checked ? "true" : "false");
            AccountManager.IniSettings.Save("RAMSettings.ini");
        }

        private void DisableAgingAlertCB_CheckedChanged(object sender, EventArgs e)
        {
            if (!SettingsLoaded) return;

            AccountManager.General.Set("DisableAgingAlert", DisableAgingAlertCB.Checked ? "true" : "false");
            AccountManager.IniSettings.Save("RAMSettings.ini");
        }

        private void HideMRobloxCB_CheckedChanged(object sender, EventArgs e)
        {
            if (!SettingsLoaded) return;

            AccountManager.General.Set("HideRbxAlert", HideMRobloxCB.Checked ? "true" : "false");
            AccountManager.IniSettings.Save("RAMSettings.ini");
        }

        private void RegionFormatTB_TextChanged(object sender, EventArgs e)
        {
            if (!SettingsLoaded) return;

            AccountManager.General.Set("ServerRegionFormat", RegionFormatTB.Text, "Visit http://ip-api.com/json to see available format options");
            AccountManager.IniSettings.Save("RAMSettings.ini");
        }

        private void DisableImagesCB_CheckedChanged(object sender, EventArgs e)
        {
            AccountManager.General.Set("DisableImages", DisableImagesCB.Checked ? "true" : "false");
            AccountManager.IniSettings.Save("RAMSettings.ini");
        }

        private void ShuffleLowestServerCB_CheckedChanged(object sender, EventArgs e)
        {
            AccountManager.General.Set("ShuffleChoosesLowestServer", ShuffleLowestServerCB.Checked ? "true" : "false");
            AccountManager.IniSettings.Save("RAMSettings.ini");
        }

        private void MultiRobloxCB_CheckedChanged(object sender, EventArgs e)
        {
            AccountManager.General.Set("EnableMultiRbx", MultiRobloxCB.Checked ? "true" : "false");
            AccountManager.IniSettings.Save("RAMSettings.ini");

            if (!AccountManager.Instance.UpdateMultiRoblox())
                MessageBox.Show("Roblox is currently running, multi roblox will not work if roblox is open.", "Roblox Account Manager", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void AutoCookieRefreshCB_CheckedChanged(object sender, EventArgs e)
        {
            AccountManager.General.Set("AutoCookieRefresh", AutoCookieRefreshCB.Checked ? "true" : "false");
            AccountManager.IniSettings.Save("RAMSettings.ini");

            if (AccountManager.Instance.AutoCookieRefresh != null)
                AccountManager.Instance.AutoCookieRefresh.Enabled = AutoCookieRefreshCB.Checked;
        }

        private void StartOnPCStartup_CheckedChanged(object sender, EventArgs e)
        {
            if (!SettingsLoaded) return;

            if (StartOnPCStartup.Checked)
                StartupKey?.SetValue(Application.ProductName, Application.ExecutablePath);
            else
                StartupKey?.DeleteValue(Application.ProductName);
        }

        private void EncryptionSelectionButton_Click(object sender, EventArgs e)
        {
            if (Utilities.YesNoPrompt("Settings", "Change Encryption Method", "Are you sure you want to change how your data is encrypted?", false))
                AccountManager.Instance.ResetEncryption(true);
        }

        #endregion

        #region Developer

        private void EnableDMCB_CheckedChanged(object sender, EventArgs e)
        {
            if (!SettingsLoaded) return;

            AccountManager.Developer.Set("DevMode", EnableDMCB.Checked ? "true" : "false");
            AccountManager.IniSettings.Save("RAMSettings.ini");
        }

        private void EnableWSCB_CheckedChanged(object sender, EventArgs e)
        {
            if (!SettingsLoaded) return;

            AccountManager.Developer.Set("EnableWebServer", EnableWSCB.Checked ? "true" : "false");
            AccountManager.IniSettings.Save("RAMSettings.ini");

            MessageBox.Show("Roblox Account Manager must be restarted to enable this setting", "Roblox Account Manager", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ERRPCB_CheckedChanged(object sender, EventArgs e)
        {
            if (!SettingsLoaded) return;

            AccountManager.WebServer.Set("EveryRequestRequiresPassword", ERRPCB.Checked ? "true" : "false");
            AccountManager.IniSettings.Save("RAMSettings.ini");
        }

        private void AllowGCCB_CheckedChanged(object sender, EventArgs e)
        {
            if (!SettingsLoaded) return;

            AccountManager.WebServer.Set("AllowGetCookie", AllowGCCB.Checked ? "true" : "false");
            AccountManager.IniSettings.Save("RAMSettings.ini");
        }

        private void AllowGACB_CheckedChanged(object sender, EventArgs e)
        {
            if (!SettingsLoaded) return;

            AccountManager.WebServer.Set("AllowGetAccounts", AllowGACB.Checked ? "true" : "false");
            AccountManager.IniSettings.Save("RAMSettings.ini");
        }

        private void AllowLACB_CheckedChanged(object sender, EventArgs e)
        {
            if (!SettingsLoaded) return;

            AccountManager.WebServer.Set("AllowLaunchAccount", AllowLACB.Checked ? "true" : "false");
            AccountManager.IniSettings.Save("RAMSettings.ini");
        }

        private void AllowAECB_CheckedChanged(object sender, EventArgs e)
        {
            if (!SettingsLoaded) return;

            AccountManager.WebServer.Set("AllowAccountEditing", AllowAECB.Checked ? "true" : "false");
            AccountManager.IniSettings.Save("RAMSettings.ini");
        }

        private void AllowExternalConnectionsCB_CheckedChanged(object sender, EventArgs e)
        {
            if (!SettingsLoaded) return;

            AccountManager.WebServer.Set("AllowExternalConnections", AllowExternalConnectionsCB.Checked ? "true" : "false");
            AccountManager.IniSettings.Save("RAMSettings.ini");

            MessageBox.Show("Roblox Account Manager must be restarted to enable this setting\n\nThis setting requires admin privileges", "Roblox Account Manager", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void PortNumber_ValueChanged(object sender, EventArgs e)
        {
            if (!SettingsLoaded) return;

            AccountManager.WebServer.Set("WebServerPort", PortNumber.Value.ToString());
            AccountManager.IniSettings.Save("RAMSettings.ini");
        }

        private void PasswordTextBox_TextChanged(object sender, EventArgs e)
        {
            if (!SettingsLoaded) return;

            PasswordTextBox.Text = Regex.Replace(PasswordTextBox.Text, "[^0-9a-zA-Z ]", "");

            AccountManager.WebServer.Set("Password", PasswordTextBox.Text);
            AccountManager.IniSettings.Save("RAMSettings.ini");
        }

        #endregion

        #region Miscellaneous

        private void PresenceCB_CheckedChanged(object sender, EventArgs e)
        {
            if (!SettingsLoaded) return;

            AccountManager.General.Set("ShowPresence", PresenceCB.Checked ? "true" : "false");
            AccountManager.IniSettings.Save("RAMSettings.ini");
        }

        private void PresenceUpdateRateNum_ValueChanged(object sender, EventArgs e)
        {
            if (!SettingsLoaded) return;

            AccountManager.General.Set("PresenceUpdateRate", PresenceUpdateRateNum.Value.ToString());
            AccountManager.IniSettings.Save("RAMSettings.ini");
        }

        private void UnlockFPSCB_CheckedChanged(object sender, EventArgs e)
        {
            if (!SettingsLoaded) return;

            AccountManager.General.Set("UnlockFPS", UnlockFPSCB.Checked ? "true" : "false");
            AccountManager.IniSettings.Save("RAMSettings.ini");
        }

        private void MaxFPSValue_ValueChanged(object sender, EventArgs e)
        {
            if (!SettingsLoaded) return;

            AccountManager.General.Set("MaxFPSValue", MaxFPSValue.Value.ToString());
            AccountManager.IniSettings.Save("RAMSettings.ini");
        }

        private void OverrideWithCustomCB_CheckedChanged(object sender, EventArgs e)
        {
            if (!SettingsLoaded) return;

            UnlockFPSCB.Enabled = !OverrideWithCustomCB.Checked;

            void Remove()
            {
                AccountManager.General.RemoveProperty("CustomClientSettings");
                OverrideWithCustomCB.Checked = false;
            }

            if (OverrideWithCustomCB.Checked)
            {
                if (CustomClientSettingsDialog.ShowDialog() == DialogResult.OK)
                {
                    if (File.Exists(CustomClientSettingsDialog.FileName) && File.ReadAllText(CustomClientSettingsDialog.FileName).TryParseJson<object>(out _))
                    {
                        string FileName = Path.Combine(Environment.CurrentDirectory, "CustomClientAppSettings.json");

                        File.Copy(CustomClientSettingsDialog.FileName, FileName);
                        AccountManager.General.Set("CustomClientSettings", FileName);
                    }
                    else
                        MessageBox.Show("Invalid file selected, make sure it contains valid JSON", "Settings", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                    Remove();
            }
            else
                Remove();
            
            AccountManager.IniSettings.Save("RAMSettings.ini");
        }

        private void ForceUpdateButton_Click(object sender, EventArgs e)
        {
            if (!Utilities.YesNoPrompt("Auto Update", "Are you sure you want to update?", "", false)) return;

            string AFN = Path.Combine(Directory.GetCurrentDirectory(), "Auto Update.exe");
            File.WriteAllBytes(AFN, File.ReadAllBytes(Application.ExecutablePath));
            Process.Start(AFN, "-update");
            Environment.Exit(1);
        }

        #endregion

        #region Themes

        public void ApplyTheme()
        {
            BackColor = ThemeEditor.FormsBackground;
            ForeColor = ThemeEditor.FormsForeground;

            ApplyTheme(Controls);
        }

        public void ApplyTheme(Control.ControlCollection _Controls)
        {
            foreach (Control control in _Controls)
            {
                if (control is Button || control is CheckBox)
                {
                    if (control is Button)
                    {
                        Button b = control as Button;
                        b.FlatStyle = ThemeEditor.ButtonStyle;
                        b.FlatAppearance.BorderColor = ThemeEditor.ButtonsBorder;
                    }

                    if (!(control is CheckBox)) control.BackColor = ThemeEditor.ButtonsBackground;
                    control.ForeColor = ThemeEditor.ButtonsForeground;
                }
                else if (control is TextBox || control is RichTextBox)
                {
                    if (control is Classes.BorderedTextBox)
                    {
                        Classes.BorderedTextBox b = control as Classes.BorderedTextBox;
                        b.BorderColor = ThemeEditor.TextBoxesBorder;
                    }

                    if (control is Classes.BorderedRichTextBox)
                    {
                        Classes.BorderedRichTextBox b = control as Classes.BorderedRichTextBox;
                        b.BorderColor = ThemeEditor.TextBoxesBorder;
                    }

                    control.BackColor = ThemeEditor.TextBoxesBackground;
                    control.ForeColor = ThemeEditor.TextBoxesForeground;
                }
                else if (control is Label)
                {
                    control.BackColor = ThemeEditor.LabelTransparent ? Color.Transparent : ThemeEditor.LabelBackground;
                    control.ForeColor = ThemeEditor.LabelForeground;
                }
                else if (control is ListBox)
                {
                    control.BackColor = ThemeEditor.ButtonsBackground;
                    control.ForeColor = ThemeEditor.ButtonsForeground;
                }
                else if (control is TabPage)
                {
                    ApplyTheme(control.Controls);

                    control.BackColor = ThemeEditor.ButtonsBackground;
                    control.ForeColor = ThemeEditor.ButtonsForeground;
                }
                else if (control is FastColoredTextBoxNS.FastColoredTextBox)
                    control.ForeColor = Color.Black;
                else if (control is FlowLayoutPanel || control is Panel || control is TabControl)
                    ApplyTheme(control.Controls);
            }
        }

        #endregion
    }
}