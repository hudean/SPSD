using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SPSD.WebApi.Model;

namespace SPSD.WebApi.Controllers
{
    /// <summary>
    /// 动能混伤建模控制器
    /// </summary>
    /// <param name="dbContext"></param>
    [Route("api/[controller]")]
    [ApiController]
    public class KineticEnergyController(AppDbContext dbContext) : ControllerBase
    {

        //[HttpGet("{id}")]
        //public async Task<KineticEnergyInfo?> Get(int id)
        //{
        //    var model = await dbContext.KineticEnergyInfos.FindAsync(id);
        //    return model;
        //}

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var model = await dbContext.KineticEnergyInfos.FindAsync(id);
            return Ok(model);
        }

        // POST api/<ProjectController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] KineticEnergyInfo addOrEditModel)
        {
            //var exist = await dbContext.KineticEnergyInfos.AnyAsync(x => x.ProjectId == addModel.ProjectId);
            //if (exist)
            //{
            //    return BadRequest("项目id已存在，添加失败");
            //}


            var model = await dbContext.KineticEnergyInfos.Where(x => x.ProjectId == addOrEditModel.ProjectId).FirstOrDefaultAsync();
            if (model is not null)
            {
                //model.LandingAngle = addOrEditModel.LandingAngle;
                //model.PictureContact = addOrEditModel.PictureContact;
                model.Speed = addOrEditModel.Speed;
                //model.SpeedDirection = addOrEditModel.SpeedDirection;
                model.TSSFAC = addOrEditModel.TSSFAC;
                //model.ComputeDuration = addOrEditModel.ComputeDuration;
                model.D3plots = addOrEditModel.D3plots;
                model.SLSFAC = addOrEditModel.SLSFAC;
                model.ENDTIM = addOrEditModel.ENDTIM;
                model.DT = addOrEditModel.DT;
                dbContext.KineticEnergyInfos.Update(model);
            }
            else
            {
                await dbContext.KineticEnergyInfos.AddAsync(addOrEditModel);
            }
          
            await dbContext.SaveChangesAsync();

            return Ok();
        }

        // PUT api/<ProjectController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] KineticEnergyInfo editModel)
        {
            //var model = await dbContext.WarbodyInfos.FindAsync(id);
            var model = await dbContext.KineticEnergyInfos.Where(x => x.ProjectId == editModel.ProjectId).FirstOrDefaultAsync();
            if (model is not null)
            {
                //var exist = await dbContext.WarbodyInfos.AnyAsync(x => x.ProjectId == editModel.ProjectId && x.Id != id);
                //if (exist)
                //{
                //    return BadRequest("项目id已存在，修改失败");
                //}

                //model.LandingAngle = editModel.LandingAngle;
                //model.PictureContact = editModel.PictureContact;
                model.Speed = editModel.Speed;
                //model.SpeedDirection = editModel.SpeedDirection;
                model.TSSFAC = editModel.TSSFAC;
                //model.ComputeDuration = editModel.ComputeDuration;
                model.D3plots = editModel.D3plots;
                model.SLSFAC = editModel.SLSFAC;
                model.ENDTIM = editModel.ENDTIM;
                model.DT = editModel.DT;

                dbContext.KineticEnergyInfos.Update(model);
                await dbContext.SaveChangesAsync();
                return Ok();
            }

            return NoContent();
        }

        // DELETE api/<ProjectController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await dbContext.KineticEnergyInfos.Where(x => x.Id == id).ExecuteDeleteAsync();
            return Ok();
        }

    }
}
