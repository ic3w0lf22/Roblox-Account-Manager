using System.Collections.Generic;

namespace RBX_Alt_Manager.Nexus
{
    public class Command
    {
        public string Name;
        public Dictionary<string, string> Payload;
    }

    public enum CommandCreateElement
    {
        CreateButton,
        CreateTextBox,
        CreateNumeric,
        CreateLabel,
        NewLine
    }
}