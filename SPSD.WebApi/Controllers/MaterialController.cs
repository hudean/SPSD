using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SPSD.WebApi.Model;
using SPSD.WebApi.ParamModel;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SPSD.WebApi.Controllers
{
    /// <summary>
    /// 材料属性
    /// </summary>
    /// <param name="dbContext"></param>
    [Route("api/[controller]")]
    [ApiController]
    public class MaterialController(AppDbContext dbContext) : ControllerBase
    {
        // GET: api/<MaterialController>
        [HttpGet]
        public async Task<PaginationResultModel<MaterialInfo>> Get([FromQuery] PaginationQueryModel queryModel)
        {
            var list = await dbContext.MaterialInfos.AsNoTracking().OrderBy(x => x.Id).Skip((queryModel.PageIndex - 1) * queryModel.PageSize).Take(queryModel.PageSize).ToListAsync();
            int count = await dbContext.MaterialInfos.CountAsync();
            PaginationResultModel<MaterialInfo> paginationList = new()
            {
                List = list,
                //(int)Math.Ceiling((double)await dbContext.MaterialInfos.CountAsync() / queryModel.PageSize)
                PageCount = count
            };
            return paginationList;
        }

        [HttpGet("getList")]
        public async Task<IEnumerable<MaterialInfo>> GetList()
        {
            var list = await dbContext.MaterialInfos.AsNoTracking().OrderBy(x => x.Id).ToListAsync();
            return list;
        }

        // GET api/<MaterialController>/5
        [HttpGet("{id}")]
        public async Task<MaterialInfo?> Get(int id)
        {
            var model = await dbContext.MaterialInfos.FindAsync(id);
            return model;
        }

        // POST api/<MaterialController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] MaterialInfo addModel)
        {
            var exist = await dbContext.MaterialInfos.AnyAsync(x => x.MaterialName == addModel.MaterialName);
            if (exist)
            {
                return BadRequest("材料名称已存在，添加失败");
            }
            await dbContext.MaterialInfos.AddAsync(addModel);
            await dbContext.SaveChangesAsync();

            return Ok();
        }

        // PUT api/<MaterialController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] MaterialInfo editModel)
        {
            var model = await dbContext.MaterialInfos.FindAsync(id);
            if (model is not null)
            {
                var exist = await dbContext.MaterialInfos.AnyAsync(x => x.MaterialName == editModel.MaterialName && x.Id != id);
                if (exist)
                {
                    return BadRequest("材料名称已存在，修改失败");
                }

                model.MaterialName = editModel.MaterialName;
                model.StrengthModelName = editModel.StrengthModelName;
                model.StrengthModelParameter = editModel.StrengthModelParameter;
                model.StrengthModelValue = editModel.StrengthModelValue;
                model.EOSName = editModel.EOSName;
                model.EOSParameter = editModel.EOSParameter;
                model.EOSValue = editModel.EOSValue;
                model.ReferenceCount = editModel.ReferenceCount;
                dbContext.MaterialInfos.Update(model);
                await dbContext.SaveChangesAsync();
                return Ok();
            }

            return NoContent();
        }

        // DELETE api/<MaterialController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await dbContext.MaterialInfos.Where(x => x.Id == id).ExecuteDeleteAsync();
            return Ok();
        }
    }
}