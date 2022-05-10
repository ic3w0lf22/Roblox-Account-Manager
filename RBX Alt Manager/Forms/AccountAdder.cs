using CefSharp;
using CefSharp.WinForms;
using RBX_Alt_Manager.Forms;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;
using Cookie = CefSharp.Cookie;

namespace RBX_Alt_Manager
{
    public partial class AccountAdder : Form
    {
        private delegate void SafeCallDelegate();
        private string SecurityToken;
        private string Password;
        public bool BrowserMode = false;
        public string SetUsername = "";

        public AccountAdder()
        {
            AccountManager.SetDarkBar(Handle);

            InitializeComponent();

            chromeBrowser = new ChromiumWebBrowser("https://roblox.com/login");
            chromeBrowser.AddressChanged += OnNavigated;
            chromeBrowser.FrameLoadEnd += OnPageLoaded;

            Controls.Add(chromeBrowser);
        }

        public void ApplyTheme()
        {
            BackColor = ThemeEditor.FormsBackground;
            ForeColor = ThemeEditor.FormsForeground;

            foreach (Control control in this.Controls)
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
                else if (control is TextBox || control is RichTextBox || control is Label)
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
                else if (control is ListBox)
                {
                    control.BackColor = ThemeEditor.ButtonsBackground;
                    control.ForeColor = ThemeEditor.ButtonsForeground;
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e) => chromeBrowser.Dock = DockStyle.Fill;

        private void CloseForm()
        {
            if (this.InvokeRequired)
            {
                var close = new SafeCallDelegate(CloseForm);
                this.Invoke(close, new object[] { });
            }
            else
                Close();
        }

        public void HideForm()
        {
            if (this.InvokeRequired)
            {
                var hide = new SafeCallDelegate(HideForm);
                this.Invoke(hide, new object[] { });
            }
            else
            {
                Hide();
                ClearData();
                chromeBrowser.Load("https://roblox.com/login");
            }
        }

        public void ShowForm() => Show();

        public void ClearData() => Cef.GetGlobalCookieManager().DeleteCookies();

        private void OnPageLoaded(object sender, FrameLoadEndEventArgs args)
        {
            Task.Factory.StartNew(async () =>
            {
                await Task.Delay(200);

                while (Visible && args.Url == chromeBrowser.Address)
                {
                    JavascriptResponse response = await chromeBrowser.EvaluateScriptAsync("document.getElementById('login-password').value");

                    if (!response.Success)
                        response = await chromeBrowser.EvaluateScriptAsync("document.getElementById('signup-password').value");

                    if (!response.Success)
                    {
                        await Task.Delay(50);
                        continue;
                    }

                    Password = (string)response.Result;

                    await Task.Delay(50);
                }
            });

            chromeBrowser.ExecuteScriptAsyncWhenPageLoaded(@"document.body.classList.remove(""light-theme"");document.body.classList.add(""dark-theme"");");

            if (!string.IsNullOrEmpty(SetUsername))
            {
                chromeBrowser.ExecuteScriptAsyncWhenPageLoaded($"document.getElementById('login-username').value='{SetUsername}'");
                SetUsername = "";
            }
        }

        private async void OnNavigated(object sender, AddressChangedEventArgs args)
        {
            string url = args.Address;

            if (!BrowserMode && url.Contains("/home"))
            {
                var cookieManager = Cef.GetGlobalCookieManager();

                await cookieManager.VisitAllCookiesAsync().ContinueWith(t =>
                {
                    if (t.Status == TaskStatus.RanToCompletion)
                    {
                        List<Cookie> cookies = t.Result;

                        Cookie RSec = cookies.Find(x => x.Name == ".ROBLOSECURITY");

                        if (RSec != null)
                        {
                            SecurityToken = RSec.Value;
                            AccountManager.AddAccount(SecurityToken, Password);
                            HideForm();
                            chromeBrowser.Load("https://roblox.com/login");
                        }

                        Password = string.Empty;
                    }
                });
            }
        }

        private void AccountAdder_FormClosing(object sender, FormClosingEventArgs e)
        {
            HideForm();
            e.Cancel = true;
        }
    }
}