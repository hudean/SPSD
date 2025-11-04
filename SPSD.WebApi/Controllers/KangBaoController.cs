using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SPSD.WebApi.Model;

namespace SPSD.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KangBaoController(AppDbContext dbContext) : ControllerBase
    {
        //[HttpGet("{id}")]
        //public async Task<KangBaoInfo?> Get(int id)
        //{
        //    var model = await dbContext.KangBaoInfos.FindAsync(id);
        //    return model;
        //}
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var model = await dbContext.KangBaoInfos.FindAsync(id);
            return Ok(model);
        }

        // POST api/<ProjectController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] KangBaoInfo addModel)
        {
            var model = await dbContext.KangBaoInfos.Where(x => x.ProjectId == addModel.ProjectId).FirstOrDefaultAsync();
            if (model is not null)
            {
                model.Charge_l = addModel.Charge_l;
                model.Charge_r = addModel.Charge_r;
                model.Charge_z = addModel.Charge_z;
                model.SubL1_z = addModel.SubL1_z;
                model.SubL1_y = addModel.SubL1_y;
                model.L1_x = addModel.L1_x;
                model.L1_y = addModel.L1_y;
                model.L1_z = addModel.L1_z;
                model.L2_y = addModel.L2_y;
                model.Edx = addModel.Edx;
                model.StructWallThickness = addModel.StructWallThickness;
                model.BoxThickness = addModel.BoxThickness;
                model.DynamiteMaterialItemId = addModel.DynamiteMaterialItemId;
                model.AirMaterialItemId = addModel.AirMaterialItemId;
                model.BoxMaterialItemId = addModel.BoxMaterialItemId;
                model.StructMaterialItemId = addModel.StructMaterialItemId;

                dbContext.KangBaoInfos.Update(model);
            }
            else
            {
                await dbContext.KangBaoInfos.AddAsync(addModel);
            }

            await dbContext.SaveChangesAsync();

            return Ok();
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
