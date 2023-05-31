using BrightIdeasSoftware;
using Microsoft.WindowsAPICodePack.Dialogs;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RBX_Alt_Manager;
using RBX_Alt_Manager.Classes;
using RBX_Alt_Manager.Forms;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Management;
using System.Net.Http;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

public static class Utilities
{
    [DllImport("user32.dll")]
    public static extern bool PostMessage(IntPtr hWnd, UInt32 Msg, int wParam, int lParam);

    [DllImport("wininet.dll")]
    private extern static bool InternetGetConnectedState(out int Description, int ReservedValue);

    public static T Next<T>(this T src) where T : struct
    {
        if (!typeof(T).IsEnum) throw new ArgumentException(string.Format("Argument {0} is not an Enum", typeof(T).FullName));

        T[] Arr = (T[])Enum.GetValues(src.GetType());
        int j = Array.IndexOf<T>(Arr, src) + 1;
        return (Arr.Length == j) ? Arr[0] : Arr[j];
    }

    public static T Clamp<T>(this T val, T min, T max) where T : IComparable<T>
    {
        if (val.CompareTo(min) < 0) return min;
        else if (val.CompareTo(max) > 0) return max;
        else return val;
    }

    public static Control GetSource(this ToolStripMenuItem item) => item?.Owner is ContextMenuStrip strip ? strip.SourceControl : null;

    public static bool TryParseJson<T>(this string @this, out T result) // https://stackoverflow.com/a/51428508
    {
        bool success = true;
        var settings = new JsonSerializerSettings
        {
            Error = (sender, args) => { success = false; args.ErrorContext.Handled = true; },
            MissingMemberHandling = MissingMemberHandling.Error
        };
        result = JsonConvert.DeserializeObject<T>(@this, settings);
        return success;
    }

    public static void InvokeIfRequired(this Control _Control, MethodInvoker _Action)
    {
        if (_Control.InvokeRequired)
            _Control.Invoke(_Action);
        else
            _Action();
    }

    public static string MD5(string input)
    {
        MD5 md5 = System.Security.Cryptography.MD5.Create();
        byte[] inputBytes = Encoding.ASCII.GetBytes(input);
        byte[] hashBytes = md5.ComputeHash(inputBytes);

        StringBuilder sb = new StringBuilder();

        for (int i = 0; i < hashBytes.Length; i++)
            sb.Append(hashBytes[i].ToString("X2"));

        return sb.ToString();
    }

    public static string FileSHA256(this string FileName)
    {
        if (!File.Exists(FileName)) return "E3B0C44298FC1C149AFBF4C8996FB92427AE41E4649B934CA495991B7852B855";

        using SHA256 SHA256 = SHA256.Create();
        using FileStream fileStream = File.OpenRead(FileName);

        return BitConverter.ToString(SHA256.ComputeHash(fileStream)).Replace("-", "");
    }

public static Color Lerp(this Color s, Color t, float k)
    {
        var bk = 1 - k;
        var a = s.A * bk + t.A * k;
        var r = s.R * bk + t.R * k;
        var g = s.G * bk + t.G * k;
        var b = s.B * bk + t.B * k;

        return Color.FromArgb((int)a, (int)r, (int)g, (int)b);
    }

    public static double ToRobloxTick(this DateTime Date) => ((Date - Epoch).Ticks / TimeSpan.TicksPerSecond) + ((double)Date.Millisecond / 1000);

    public static string GetCommandLine(this Process process)
    {
        using ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT CommandLine FROM Win32_Process WHERE ProcessId = " + process.Id);
        using ManagementObjectCollection objects = searcher.Get();
        return objects.Cast<ManagementBaseObject>().SingleOrDefault()?["CommandLine"]?.ToString();
    }

    public static async Task<string> GetRandomJobId(long PlaceId, bool ChooseLowestServer = false)
    {
        Random RNG = new Random();
        List<string> ValidServers = new List<string>();
        int StopAt = Math.Max(AccountManager.General.Get<int>("ShufflePageCount"), 1);
        int PageCount = 0;

        async Task GetServers(string Cursor = "")
        {
            if (PageCount >= StopAt) return;

            PageCount++;

            RestRequest request = new RestRequest("v1/games/" + PlaceId + "/servers/public?sortOrder=Asc&limit=100" + (string.IsNullOrEmpty(Cursor) ? "" : "&cursor=" + Cursor), Method.Get);
            var response = await ServerList.GamesClient?.ExecuteAsync(request);

            if (response == null || !response.IsSuccessful) return;

            JObject Servers = JObject.Parse(response.Content);

            if (!Servers.ContainsKey("data")) return;

            Cursor = Servers["nextPageCursor"]?.Value<string>() ?? string.Empty;

            foreach (JToken a in Servers["data"])
                if (a["playing"]?.Value<int>() != a["maxPlayers"]?.Value<int>() && a["playing"]?.Value<int>() > 0 && a["maxPlayers"]?.Value<int>() > 1)
                    ValidServers.Add(a["id"].Value<string>());

            if (!string.IsNullOrEmpty(Cursor) && !ChooseLowestServer)
                await GetServers(Cursor);
        }

        await GetServers();

        if (ValidServers.Count == 0) return string.Empty;

        return ValidServers[ChooseLowestServer ? 0 : RNG.Next(ValidServers.Count)];
    }

    // probably not the best way to do it but it works so whatever
    public static void Rescale(this Control control, bool UseControlFont = false)
    {
        if (control.Tag is string Tag && Tag == "NoScaling") return;

        if (Program.ScaleFonts)
        {
            Font font = control.FindForm()?.Font ?? SystemFonts.DefaultFont;

            if (UseControlFont) font = control?.Font;

            control.Font = new Font(font.FontFamily.Name, font.SizeInPoints * Program.Scale);
        }

        if (control is Button btn && btn.Image != null)
            btn.Image = new Bitmap(btn.Image, new Size((int)(btn.Image.Width * Program.Scale), (int)(btn.Image.Height * Program.Scale)));

        if (control is TabControl tc && Program.Scale > 1)
            foreach (TabPage tab in tc.Controls)
                tab.Text = tab.Text.PadRight((int)(2 + tab.Text.Length * Program.Scale)); // Bad. Very Bad. But it works...

        if (control is ObjectListView olv)
            foreach (OLVColumn col in olv.Columns)
                col.Width = (int)(col.Width * Program.Scale);
    }

    public static void Rescale(this Form form)
    {
        form.MaximumSize = new Size((int)(form.MaximumSize.Width * Program.Scale), (int)(form.MaximumSize.Height * Program.Scale));
        form.Scale(new SizeF(Program.Scale, Program.Scale));

        static void RescaleControls(Control.ControlCollection controls)
        {
            foreach (Control control in controls)
            {
                control.Rescale(control is ObjectListView || (control.Tag is string Tag && Tag == "UseControlFont"));

                RescaleControls(control.Controls);
            }
        }

        RescaleControls(form.Controls);
    }

    public static void RecursiveDelete(this DirectoryInfo baseDir)
    {
        if (!baseDir.Exists)
            return;

        foreach (var dir in baseDir.EnumerateDirectories())
            RecursiveDelete(dir);

        foreach (var file in baseDir.GetFiles())
        {
            file.IsReadOnly = false;
            file.Delete();
        }

        baseDir.Delete(true);
    }

    public static bool YesNoPrompt(string Caption, string Instruction, string Text, bool CanSave = true, bool SaveIfNo = true)
    {
        string Hash = MD5($"{Caption}.{Instruction}.{Text}");

        if (CanSave && AccountManager.Prompts.Exists(Hash))
            return AccountManager.Prompts.Get<bool>(Hash);

        TaskDialog Dialog = TaskDialog.IsPlatformSupported == true ? new TaskDialog()
        {
            Caption = Caption,
            InstructionText = Instruction,
            Text = Text,
            FooterCheckBoxText = CanSave ? "Don't show this again and remember my choice" : null,
            StandardButtons = TaskDialogStandardButtons.Yes | TaskDialogStandardButtons.No,
        } : null;

        var DR = Dialog?.Show();

        if (CanSave && Dialog?.FooterCheckBoxChecked == true)
        {
            if (SaveIfNo || (!SaveIfNo && DR == TaskDialogResult.Yes))
            {
                AccountManager.Prompts.Set(Hash, DR == TaskDialogResult.Yes ? "true" : "false");
                AccountManager.IniSettings.Save("RAMSettings.ini");
            }
        }

        return DR != null ? DR == TaskDialogResult.Yes : MessageBox.Show($"{Instruction}\n{Text}", Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes;
    }

    public static void ApplyTheme(this Control.ControlCollection Controls)
    {
        foreach (Control control in Controls)
        {
            if (control is PictureBox)
            {
                control.BackColor = Color.Transparent;
                if (ThemeEditor.LightImages && control.GetLuminance(out float L) && L < 0.3) control.ColorImage(255, 255, 255);
            }
            else if (control is Button || control is CheckBox)
            {
                if (control is Button)
                {
                    if (ThemeEditor.LightImages && control.GetLuminance(out float L) && L < 0.3)
                        control.ColorImage(255, 255, 255);

                    Button b = control as Button;
                    b.FlatStyle = ThemeEditor.ButtonStyle;
                    b.FlatAppearance.BorderColor = ThemeEditor.ButtonsBorder;
                }

                if (!(control is CheckBox)) control.BackColor = ThemeEditor.ButtonsBackground;
                control.ForeColor = ThemeEditor.ButtonsForeground;
            }
            else if (control is TextBox || control is RichTextBox)
            {
                if (control is BorderedTextBox)
                {
                    BorderedTextBox b = control as BorderedTextBox;
                    b.BorderColor = ThemeEditor.TextBoxesBorder;
                }

                if (control is BorderedRichTextBox)
                {
                    BorderedRichTextBox b = control as BorderedRichTextBox;
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
            else if (control is ProgressBar)
                control.BackColor = ThemeEditor.LabelBackground;
            else if (control is Panel)
                control.Controls.ApplyTheme();
        }
    }

    public static Color DarkenOrBrighten(this Color color, float Percent) => color.GetBrightness() < 0.5 ? ControlPaint.Light(color, Percent) : ControlPaint.Dark(color, Percent);

    public static double MapValue(double Input, double IL, double IH, double OL, double OH) => (Input - IL) / (IH - IL) * (OH - OL) + OL;

    private static readonly DateTime Epoch = new DateTime(1970, 1, 1);

    public static bool IsConnectedToInternet() => InternetGetConnectedState(out int _, 0);

}

public static class ImageExtensions
{
    /// <summary>
    /// Changes the colors of every pixel in an image
    /// </summary>
    /// <param name="control">Control containing an Image to color</param>
    /// <param name="R">Red</param>
    /// <param name="G">Green</param>
    /// <param name="B">Blue</param>
    public static void ColorImage(this Control control, int R, int G, int B)
    {
        Bitmap Image = control.GetImage(out PropertyInfo ImageProperty);

        for (int x = 0; x < Image.Width; x++)
            for (int y = 0; y < Image.Height; y++)
            {
                Color Pixel = Image.GetPixel(x, y);

                if (Pixel.A == 0) continue;

                Pixel = Color.FromArgb(Pixel.A, R, G, B);
                Image.SetPixel(x, y, Pixel);
            }

        ImageProperty.SetValue(control, Image); // Required for some controls
    }

    /// <summary>
    /// Obtain the Image Bitmap of a Control
    /// </summary>
    /// <param name="control">Control containing an Image</param>
    /// <param name="ImageProperty">PropertyInfo of the Image Property</param>
    /// <returns>Returns the Image Bitmap of a Control</returns>
    /// <exception cref="ArgumentException">Control doesn't contain the Image Property</exception>
    public static Bitmap GetImage(this Control control, out PropertyInfo ImageProperty)
    {
        List<PropertyInfo> Properties = control.GetType().GetProperties().ToList();
        ImageProperty = Properties.FirstOrDefault(Property => Property.Name == "Image");

        if (ImageProperty == null) throw new ArgumentException("Control passed does not contain Image property");

        object ImageObject = ImageProperty.GetValue(control); if (ImageObject == null) return null;

        return ImageObject as Bitmap;
    }

    /// <summary>
    /// Get the average Luminance of a Control's Image
    /// </summary>
    /// <param name="control">Control containing an Image</param>
    /// <param name="Luminance">Average Luminance of a Control's Image</param>
    /// <returns>Returns false if Control doesn't contain an Image</returns>
    public static bool GetLuminance(this Control control, out float Luminance)
    {
        Luminance = 0f;
        Bitmap Image = control.GetImage(out _);

        if (Image == null) return false;

        for (int x = 0; x < Image.Width; x++)
            for (int y = 0; y < Image.Height; y++)
            {
                Color Pixel = Image.GetPixel(x, y);

                if (Pixel.A == 0) continue;

                Luminance += Pixel.GetBrightness();
            }

        Luminance /= (Image.Width * Image.Height);

        return true;
    }

    public static bool IsImageMostlyDark(this Control control, double Threshold = 0.25) => control.GetLuminance(out float Luminance) && Luminance < Threshold;
}

public static class HttpExtensions
{
    // https://stackoverflow.com/a/46497896
    public static async Task DownloadAsync(this HttpClient client, string requestUri, Stream destination, IProgress<float> progress = null, CancellationToken cancellationToken = default)
    {
        // Get the http headers first to examine the content length
        using var response = await client.GetAsync(requestUri, HttpCompletionOption.ResponseHeadersRead);
        var contentLength = response.Content.Headers.ContentLength;

        using var download = await response.Content.ReadAsStreamAsync();
        // Ignore progress reporting when no progress reporter was 
        // passed or when the content length is unknown
        if (progress == null || !contentLength.HasValue)
        {
            await download.CopyToAsync(destination);
            return;
        }

        // Convert absolute progress (bytes downloaded) into relative progress (0% - 100%)
        var relativeProgress = new Progress<long>(totalBytes => progress.Report((float)totalBytes / contentLength.Value));
        // Use extension method to report progress while downloading
        await CopyToAsync(download, destination, 81920, relativeProgress, cancellationToken);
        progress.Report(1);
    }

    public static async Task CopyToAsync(Stream source, Stream destination, int bufferSize, IProgress<long> progress = null, CancellationToken cancellationToken = default)
    {
        if (source == null)
            throw new ArgumentNullException(nameof(source));
        if (!source.CanRead)
            throw new ArgumentException("Has to be readable", nameof(source));
        if (destination == null)
            throw new ArgumentNullException(nameof(destination));
        if (!destination.CanWrite)
            throw new ArgumentException("Has to be writable", nameof(destination));
        if (bufferSize < 0)
            throw new ArgumentOutOfRangeException(nameof(bufferSize));

        var buffer = new byte[bufferSize];
        long totalBytesRead = 0;
        int bytesRead;
        while ((bytesRead = await source.ReadAsync(buffer, 0, buffer.Length, cancellationToken).ConfigureAwait(false)) != 0)
        {
            await destination.WriteAsync(buffer, 0, bytesRead, cancellationToken).ConfigureAwait(false);
            totalBytesRead += bytesRead;
            progress?.Report(totalBytesRead);
        }
    }
}