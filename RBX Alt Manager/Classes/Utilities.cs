using Microsoft.WindowsAPICodePack.Dialogs;
using Newtonsoft.Json;
using RBX_Alt_Manager;
using System;
using System.Drawing;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Text;
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

    public static bool YesNoPrompt(string Caption, string Instruction, string Text)
    {
        string Hash = MD5($"{Caption}.{Instruction}.{Text}");

        if (AccountManager.Prompts.Exists(Hash))
            return AccountManager.Prompts.Get<bool>(Hash);

        TaskDialog Dialog = TaskDialog.IsPlatformSupported == true ? new TaskDialog()
        {
            Caption = Caption,
            InstructionText = Instruction,
            Text = Text,
            FooterCheckBoxText = "Don't show this again and remember my choice",
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

    public static double MapValue(double Input, double IL, double IH, double OL, double OH) => (Input - IL) / (IH - IL) * (OH - OL) + OL;

    private static DateTime Epoch = new DateTime(1970, 1, 1);
}