using BrightIdeasSoftware;
using Microsoft.WindowsAPICodePack.Dialogs;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RBX_Alt_Manager;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Management;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

public static class Utilities
{
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

    public static Color Lerp(this Color s, Color t, float k)
    {
        var bk = (1 - k);
        var a = s.A * bk + t.A * k;
        var r = s.R * bk + t.R * k;
        var g = s.G * bk + t.G * k;
        var b = s.B * bk + t.B * k;

        return Color.FromArgb((int)a, (int)r, (int)g, (int)b);
    }

    public static double ToRobloxTick(this DateTime Date)
    {
        TimeSpan TS = Date - Epoch;
        double Ticks = TS.Ticks / TimeSpan.TicksPerSecond;
        Ticks += (double)Date.Millisecond / 1000;

        return Ticks;
    }

    public static string GetCommandLine(this Process process)
    {
        using (ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT CommandLine FROM Win32_Process WHERE ProcessId = " + process.Id))
        using (ManagementObjectCollection objects = searcher.Get())
            return objects.Cast<ManagementBaseObject>().SingleOrDefault()?["CommandLine"]?.ToString();
    }

    [DllImport("user32.dll")]
    public static extern bool PostMessage(IntPtr hWnd, UInt32 Msg, int wParam, int lParam);

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

            RestRequest request = new RestRequest("v1/games/" + PlaceId + "/servers/public?sortOrder=Asc&limit=100" + (string.IsNullOrEmpty(Cursor) ? "" : "&cursor=" + Cursor), Method.GET);
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
        if (Program.ScaleFonts)
        {
            Font font = control.FindForm()?.Font ?? SystemFonts.DefaultFont;

            if (UseControlFont) font = control?.Font;

            control.Font = new Font(font.FontFamily.Name, font.SizeInPoints * Program.Scale);
        }

        if (control is Button btn && btn.Image != null)
            btn.Image = new Bitmap(btn.Image, new Size((int)(btn.Image.Width * Program.Scale), (int)(btn.Image.Height * Program.Scale)));

        if (control is ObjectListView olv)
            foreach (OLVColumn col in olv.Columns)
                col.Width = (int)(col.Width * Program.Scale);
    }

    public static void Rescale(this Form form)
    {
        form.Scale(new SizeF(Program.Scale, Program.Scale));

        void RescaleControls(Control.ControlCollection controls)
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

    public static bool YesNoPrompt(string Caption, string Instruction, string Text, bool CanSave = true)
    {
        string Hash = MD5($"{Caption}.{Instruction}.{Text}");

        if (AccountManager.Prompts.Exists(Hash))
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

        if (Dialog?.FooterCheckBoxChecked == true)
        {
            AccountManager.Prompts.Set(Hash, DR == TaskDialogResult.Yes ? "true" : "false");
            AccountManager.IniSettings.Save("RAMSettings.ini");
        }

        return DR != null ? DR == TaskDialogResult.Yes : MessageBox.Show($"{Instruction}\n{Text}", Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes;
    }

    public static Color DarkenOrBrighten(this Color color, float Percent) => color.GetBrightness() < 0.5 ? ControlPaint.Light(color, Percent) : ControlPaint.Dark(color, Percent);

    public static double MapValue(double Input, double IL, double IH, double OL, double OH) => (Input - IL) / (IH - IL) * (OH - OL) + OL;

    private static readonly DateTime Epoch = new DateTime(1970, 1, 1);
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