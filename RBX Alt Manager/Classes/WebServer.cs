using System;
using System.Net;
using System.Text;
using System.Threading;

namespace RBX_Alt_Manager
{
    public class WebServer
    {
        private readonly HttpListener _listener = new HttpListener();
        private readonly Func<HttpListenerContext, string> _responderMethod;

        public WebServer(string[] prefixes, Func<HttpListenerContext, string> method) // scrapped from some old project, if it works, it works i guess
        {
            if (!HttpListener.IsSupported)
                throw new NotSupportedException(
                    "Needs Windows XP SP2, Server 2003 or later.");

            if (prefixes == null || prefixes.Length == 0)
                throw new ArgumentException("prefixes");

            foreach (string s in prefixes)
                _listener.Prefixes.Add(s);

            _responderMethod = method ?? throw new ArgumentException("method");
            _listener.Start();
        }

        public WebServer(Func<HttpListenerContext, string> method, params string[] prefixes) : this(prefixes, method) { }

        public void Run()
        {
            ThreadPool.QueueUserWorkItem((o) =>
            {
                try
                {
                    while (_listener.IsListening)
                    {
                        ThreadPool.QueueUserWorkItem((c) =>
                        {
                            var ctx = c as HttpListenerContext;

                            try
                            {
                                string rstr = _responderMethod(ctx);
                                byte[] buf = Encoding.UTF8.GetBytes(rstr);

                                if (ctx.Response.OutputStream.CanWrite)
                                {
                                    if (ctx.Response.StatusCode > 299)
                                        ctx.Response.Headers.Add("ws-error", rstr);
                                    ctx.Response.ContentType = "text/plain; charset=utf-8";
                                    ctx.Response.OutputStream.Write(buf, 0, buf.Length);
                                }
                            }
                            catch (Exception x) { Program.Logger.Error($"WebServer error: {x.Message}{x.StackTrace}"); }
                            finally
                            {
                                try { ctx.Response.OutputStream.Close(); } catch { } // couldn't find any good solution :( so i just wrapped it in a try-catch block ):
                            }
                        }, _listener.GetContext());
                    }
                }
                catch { }
            });
        }

        public void Stop()
        {
            _listener.Stop();
            _listener.Close();
        }
    }
}