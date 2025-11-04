using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SPSD.WebApi.Model;

namespace SPSD.WebApi.Controllers
{
    /// <summary>
    /// 弹体控制器
    /// </summary>
    /// <param name="dbContext"></param>
    [Route("api/[controller]")]
    [ApiController]
    public class WarbodyController(AppDbContext dbContext) : ControllerBase
    {
        //[HttpGet("{id}")]
        //public async Task<WarbodyInfo?> Get(int id)
        //{
        //    var model = await dbContext.WarbodyInfos.FindAsync(id);
        //    return model;
        //}

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var model = await dbContext.WarbodyInfos.FindAsync(id);
            return Ok(model);
        }

        // POST api/<ProjectController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] WarbodyInfo addModel)
        {
            //var exist = await dbContext.WarbodyInfos.AnyAsync(x => x.ProjectId == addModel.ProjectId);
            //exist
            var model = await dbContext.WarbodyInfos.Where(x => x.ProjectId == addModel.ProjectId).FirstOrDefaultAsync();
            if (model is not null)
            {
                model.WarbodyType = addModel.WarbodyType;
                model.TargetPlatesLengthOne = addModel.TargetPlatesLengthOne;
                model.TargetPlatesLengthTwo = addModel.TargetPlatesLengthTwo;
                model.WarbodyLength = addModel.WarbodyLength;
                //model.MaterialId = addModel.MaterialId;
                model.OneMaterialItemId = addModel.OneMaterialItemId;
                model.TwoMaterialItemId = addModel.TwoMaterialItemId;
                model.ThreeMaterialItemId = addModel.ThreeMaterialItemId;
                dbContext.WarbodyInfos.Update(model);
            }
            else 
            {
                await dbContext.WarbodyInfos.AddAsync(addModel);
            }

            await dbContext.SaveChangesAsync();

            return Ok();
        }

        // PUT api/<ProjectController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] WarbodyInfo editModel)
        {
            //var model = await dbContext.WarbodyInfos.FindAsync(id);
            var model = await dbContext.WarbodyInfos.Where(x=>x.ProjectId == editModel.ProjectId).FirstOrDefaultAsync();
            if (model is not null)
            {
                //var exist = await dbContext.WarbodyInfos.AnyAsync(x => x.ProjectId == editModel.ProjectId && x.Id != id);
                //if (exist)
                //{
                //    return BadRequest("项目id已存在，修改失败");
                //}

                model.WarbodyType = editModel.WarbodyType;
                model.TargetPlatesLengthOne = editModel.TargetPlatesLengthOne;
                model.TargetPlatesLengthTwo = editModel.TargetPlatesLengthTwo;
                model.WarbodyLength = editModel.WarbodyLength;
                //model.MaterialId = editModel.MaterialId;
                model.OneMaterialItemId = editModel.OneMaterialItemId;
                model.TwoMaterialItemId = editModel.TwoMaterialItemId;
                model.ThreeMaterialItemId = editModel.ThreeMaterialItemId;
                dbContext.WarbodyInfos.Update(model);
                await dbContext.SaveChangesAsync();
                return Ok();
            }

            return NoContent();
        }

        // DELETE api/<ProjectController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await dbContext.WarbodyInfos.Where(x => x.Id == id).ExecuteDeleteAsync();
            return Ok();
        }
    }
}
