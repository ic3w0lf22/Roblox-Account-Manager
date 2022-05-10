using Newtonsoft.Json;
using RBX_Alt_Manager.Classes;
using RBX_Alt_Manager.Forms;
using System;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using WebSocketSharp.Net.WebSockets;

namespace RBX_Alt_Manager.Nexus
{
    public class ControlledAccount
    {
        [JsonIgnore] public Account LinkedAccount;
        [JsonIgnore] public AccountStatus Status;

        [JsonIgnore] public DateTime LastPing;

        public string Username;
        public string JobId;
        [JsonIgnore] public string InGameJobId;
        public string AutoExecute = "";

        public long PlaceId;
        public double RelaunchDelay = 30;

        public bool AutoRelaunch;
        public bool IsChecked;
        public bool ClientCanReceive;

        [JsonIgnore] public WebSocketContext Context;

        public ControlledAccount(Account account)
        {
            LinkedAccount = account;
            Username = LinkedAccount?.Username;
            Status = AccountStatus.Offline;
            LastPing = DateTime.Now;
        }

        public void Connect(WebSocketContext Context)
        {
            Status = AccountStatus.Online;

            this.Context = Context;

            AccountControl.Instance.ContextList.Add(Context, this);
            AccountControl.Instance.AccountsView.RefreshObject(this);

            new Task(() =>
            {
                if (!string.IsNullOrEmpty(AutoExecute))
                {
                    while (!ClientCanReceive)
                        Task.Delay(50);

                    SendMessage("execute " + AutoExecute);
                }
            }).Start();
        }

        public void Disconnect()
        {
            Status = AccountStatus.Offline;
            ClientCanReceive = false;

            AccountControl.Instance.ContextList.Remove(Context);
            AccountControl.Instance.AccountsView.RefreshObject(this);
        }

        public void HandleMessage(string Message)
        {
            if (string.IsNullOrEmpty(Message)) return;

            if (Message.TryParseJson(out Command command))
            {
                /*if (command.Name != "ping")
                    Console.WriteLine($"{command.Name}: {Message}");*/

                if (command.Name == "ping")
                {
                    LastPing = DateTime.Now;
                    ClientCanReceive = true;
                }
                else if (command.Name == "Log")
                    AccountControl.Instance.InvokeIfRequired(() => { AccountControl.Instance.LogMessage(command.Payload["Content"]); });
                else if (command.Name == "GetText")
                    SendMessage($"ElementText:{AccountControl.Instance.GetTextFromElement(command.Payload["Name"])}");
                else if (command.Name == "SetRelaunch" && double.TryParse(command.Payload["Seconds"], out double Delay))
                    RelaunchDelay = Delay;
                else if (Enum.TryParse(command.Name, out CommandCreateElement elementType))
                {
                    if (elementType != CommandCreateElement.NewLine && !(command.Payload.ContainsKey("Name") && command.Payload.ContainsKey("Content")))
                        return;

                    Size size = new Size(75, 22);
                    Padding margin = new Padding(3, 2, 3, 3);

                    if (command.Payload != null)
                    {
                        if (command.Payload.TryGetValue("Margin", out string Margins))
                        {
                            int[] i = Margins.Split(',').Select(int.Parse).ToArray();

                            if (i.Count() == 4)
                                margin = new Padding(i[0], i[1], i[2], i[3]);
                        }

                        if (command.Payload.TryGetValue("Size", out string Size))
                        {
                            int[] i = Size.Split(',').Select(int.Parse).ToArray();

                            if (i.Count() == 2)
                                size = new Size(i[0], i[1]);
                        }
                    }

                    switch (elementType)
                    {
                        case CommandCreateElement.CreateButton:
                            Utilities.InvokeIfRequired(AccountControl.Instance, () => AccountControl.Instance.AddCustomButton(command.Payload["Name"], command.Payload["Content"], size, margin));
                            return;

                        case CommandCreateElement.CreateTextBox:
                            Utilities.InvokeIfRequired(AccountControl.Instance, () => AccountControl.Instance.AddCustomTextBox(command.Payload["Name"], command.Payload["Content"], size, margin));
                            return;

                        case CommandCreateElement.CreateNumeric:
                            if (decimal.TryParse(command.Payload["Content"], out decimal DefaultValue) && int.TryParse(command.Payload["DecimalPlaces"], out int Decimals) && decimal.TryParse(command.Payload["Increment"], out decimal Increment))
                                Utilities.InvokeIfRequired(AccountControl.Instance, () => AccountControl.Instance.AddCustomNumericUpDown(command.Payload["Name"], DefaultValue, Decimals, Increment, size, margin));

                            return;

                        case CommandCreateElement.CreateLabel:
                            Utilities.InvokeIfRequired(AccountControl.Instance, () => AccountControl.Instance.AddCustomLabel(command.Payload["Name"], command.Payload["Content"], margin));
                            return;

                        case CommandCreateElement.NewLine:
                            Utilities.InvokeIfRequired(AccountControl.Instance, () => AccountControl.Instance.NewLine());
                            return;
                    }
                }
            }
        }

        public void SendMessage(string Message)
        {
            if (Status == AccountStatus.Offline) return;

            Context.WebSocket.Send(Message);
        }
    }
}