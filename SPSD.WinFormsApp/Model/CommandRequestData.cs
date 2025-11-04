using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPSD.WinFormsApp.Model
{
    /// <summary>
    /// webView前端给后端发送的命令
    /// </summary>
    public class CommandRequestData
    {
        public string Cmd { get; set; } = string.Empty;

        public string? JsonData { get; set; }
    }

    /// <summary>
    /// 后端响应给WebView前端内容
    /// </summary>
    public class CommandResponseData
    { 
        public bool Success { get; set; }

        public string CmdCategory { get; set; } = null!;

        public int Code { get; set; }

        public string? Message { get; set; }
    }

    public class CommandResponseData<T> : CommandResponseData
    {
        public T? Data { get; set; }
    }
}
