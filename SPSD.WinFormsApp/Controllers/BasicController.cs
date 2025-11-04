using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SPSD.WinFormsApp.Model;

namespace SPSD.WinFormsApp.Controllers
{
    //[Route("api/v1/[controller]")]
    [Route("WinFormApi/[controller]")]
    [ApiController]
    public class BasicController : ControllerBase
    {

        /// <summary>
        /// 显示/隐藏第三方窗体事件
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("showForm")]
        public IActionResult ShowForm(RequestShowForm request)
        {

            DataResult result = new DataResult()
            {
                Success = false,
            };
            try
            {
                LocalEventBus.PublishShowFormEventAsync(request);
                result.Success = true;

                return Ok(result);
            }
            catch (Exception ex)
            {
                result.Message = "服务器错误：" + ex.Message;
                return StatusCode(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError, result);
            }
        }

        [HttpPost("GenerateTgFile")]
        public async Task<IActionResult> GenerateTgFile(CreateTgFileModel requsetModel)
        {
            DataResult result = new DataResult()
            {
                Success = false,
            };
            try
            {
               await  LocalEventBus.PublishCreateTgFileEventAsync(requsetModel);
                result.Success = true;

                return Ok(result);
            }
            catch (Exception ex)
            {
                result.Message = "服务器错误：" + ex.Message;
                return StatusCode(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError, result);
            }
        }
    }
}
