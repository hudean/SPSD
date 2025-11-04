using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SPSD.WebApi.Model;
using SPSD.WebApi.ParamModel;

namespace SPSD.WebApi.Controllers
{
    /// <summary>
    /// 材料项接口
    /// </summary>
    /// <param name="dbContext"></param>
    [Route("api/[controller]")]
    [ApiController]
    public class MaterialItemController(AppDbContext dbContext) : ControllerBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="queryModel"></param>
        /// <returns></returns>
        //[HttpGet("getList")]
        [HttpGet]
        //public async Task<IActionResult> Get(int projectId)
        public async Task<IActionResult> Get([FromQuery]PaginationQueryMaterialItemModel queryModel)
        {
            // 这里可以返回一个示例的材料项列表
            var list = await dbContext.MaterialItems.Where(x=>x.ProjectId == queryModel.ProjectId).AsNoTracking().OrderBy(x => x.Id).Skip((queryModel.PageIndex - 1) * queryModel.PageSize).Take(queryModel.PageSize).ToListAsync();
            int count = await dbContext.MaterialItems.Where(x => x.ProjectId == queryModel.ProjectId).CountAsync();
            PaginationResultModel<MaterialItem> paginationList = new()
            {
                List = list,
                PageCount = count
            };
            return Ok(paginationList);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        //[HttpGet("getById")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var model = await dbContext.MaterialItems.FindAsync(id);
            if (model is null)
            {
                return NotFound("材料项未找到");
            }
            return Ok(model);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        [HttpPost("addList")]
        public async Task<IActionResult> Add(List<MaterialItem> list)
        {
            await dbContext.MaterialItems.AddRangeAsync(list);
            await dbContext.SaveChangesAsync();
            return Ok();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="addModel"></param>
        /// <returns></returns>
        //[HttpPost("add")]
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] MaterialItem addModel)
        {
            if (addModel is null)
            {
                return BadRequest("添加的材料项数据不能为空");
            }
            var exist = await dbContext.MaterialItems.AnyAsync(x => x.MaterialName == addModel.MaterialName && x.ProjectId == addModel.ProjectId);
            if (exist)
            {
                return BadRequest("材料项名称已存在，添加失败");
            }
            await dbContext.MaterialItems.AddAsync(addModel);
            await dbContext.SaveChangesAsync();
            return Ok();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="editModel"></param>
        /// <returns></returns>
        //[HttpPost("edit")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromBody] MaterialItem editModel)
        {
            if (editModel is null)
            {
                return BadRequest("修改的材料项数据不能为空");
            }
            var model = await dbContext.MaterialItems.FindAsync(id);
            if (model is not null)
            {
                var exist = await dbContext.MaterialItems.AnyAsync(x => x.MaterialName == editModel.MaterialName && x.ProjectId == editModel.ProjectId && x.Id != id);
                if (exist)
                {
                    return BadRequest("材料项名称已存在，修改失败");
                }
                model.MaterialName = editModel.MaterialName;
                model.StrengthModelName = editModel.StrengthModelName;
                model.StrengthModelParameter = editModel.StrengthModelParameter;
                model.StrengthModelValue = editModel.StrengthModelValue;
                model.EOSName = editModel.EOSName;
                model.EOSParameter = editModel.EOSParameter;
                model.EOSValue = editModel.EOSValue;
                model.ReferenceCount = editModel.ReferenceCount;
                await dbContext.SaveChangesAsync();
                return Ok();
            }
            return NotFound("材料项未找到");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="editModel"></param>
        /// <returns></returns>
        [HttpPost("editMaterialName")]
        public async Task<IActionResult> EditMaterialName([FromBody] EditMaterialItem editModel)
        {
            if (editModel is null)
            {
                return BadRequest("修改的材料项数据不能为空");
            }
            var model = await dbContext.MaterialItems.FindAsync(editModel.Id);
            if (model is not null)
            {
                var exist = await dbContext.MaterialItems.AnyAsync(x => x.MaterialName == editModel.MaterialName && x.ProjectId == editModel.ProjectId && x.Id != editModel.Id);
                if (exist)
                {
                    return BadRequest("材料项名称已存在，修改失败");
                }
                model.MaterialName = editModel.MaterialName;
                
                await dbContext.SaveChangesAsync();
                return Ok();
            }
            return NotFound("材料项未找到");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await dbContext.MaterialItems.Where(x => x.Id == id).ExecuteDeleteAsync();
            return Ok();
        }

        [HttpGet("getMaterialItemsByProjectId")]
        public async Task<IActionResult> GetMaterialItemsByProjectId(int projectId)
        {
            var list = await dbContext.MaterialItems.Where(x => x.ProjectId == projectId)
            //.Select(x => new { 
            //    Id = x.Id,
            //    MaterialName = x.MaterialName,
            //})
            .AsNoTracking().OrderBy(x => x.Id).ToListAsync();
           
            return Ok(list);
        }


    }
}
