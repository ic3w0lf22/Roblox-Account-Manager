using RBX_Alt_Manager.Forms;
using System;
using System.Linq;
using System.Threading;
using WebSocketSharp;
using WebSocketSharp.Server;

namespace RBX_Alt_Manager.Nexus
{
    public class WebsocketServer : WebSocketBehavior
    {
        private static int _num = 0;

        private string _name;
        private string _prefix;

        public WebsocketServer() : this("anon#")
        {
        }

        public WebsocketServer(string prefix) => _prefix = prefix;

        private string getName() => Context.QueryString["name"] ?? (_prefix + getNum());
        private int getNum() => Interlocked.Increment(ref _num);

        protected override void OnOpen()
        {
            if (string.IsNullOrEmpty(Context.QueryString["name"]) || string.IsNullOrEmpty(Context.QueryString["id"])) { Context.WebSocket.Close(); return; }

            string jobID = string.IsNullOrEmpty(Context.QueryString["jobId"]) ? "UNKNOWN" : Context.QueryString["jobId"];

            long.TryParse(Context.QueryString["id"], out long UserId);

            _name = getName();

            ControlledAccount Account = AccountControl.Instance.Accounts.FirstOrDefault(x => x.Username == Context.QueryString["name"]);

            if (Account != null)
            {
                Account.Connect(Context);
                Account.InGameJobId = jobID;
                AccountControl.Instance.AccountsView.RefreshObject(Account);
            }
            else
                Context.WebSocket.Close();
        }

        protected override void OnMessage(MessageEventArgs e)
        {
            if (AccountControl.Instance.ContextList.TryGetValue(Context, out ControlledAccount account))
                account.HandleMessage(e.Data);
        }

        protected override void OnClose(CloseEventArgs e)
        {
            if (AccountControl.Instance.ContextList.TryGetValue(Context, out ControlledAccount Account))
                Account.Disconnect();
        }

        protected override void OnError(ErrorEventArgs e) => Program.Logger.Error($"WebsocketServer Error {_name}: {e.Message} {e.Exception}");
    }
}