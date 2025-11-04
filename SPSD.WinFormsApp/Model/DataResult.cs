using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPSD.WinFormsApp.Model;

public class DataResult
{
    /// <summary>
    /// 是否处理成功
    /// </summary>
    public bool Success { get; set; }
    /// <summary>
    /// 消息
    /// </summary>
    public string? Message { get; set; }
}
