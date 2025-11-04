using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SPSD.WebApi.Model;

namespace SPSD.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BoardController(AppDbContext dbContext) : ControllerBase
    {
        //[HttpGet("{id}")]
        //public async Task<BoardInfo?> Get(int id)
        //{
        //    var model = await dbContext.BoardInfos.FindAsync(id);
        //    return model;
        //}

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var model = await dbContext.BoardInfos.FindAsync(id);
            return Ok(model);
        }

        // POST api/<ProjectController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] BoardInfo addModel)
        {
            var model = await dbContext.BoardInfos.Where(x => x.ProjectId == addModel.ProjectId).FirstOrDefaultAsync();
            if (model is not null)
            {
                model.TopTier = addModel.TopTier;
                model.TopThickness = addModel.TopThickness;
                model.TopMaterialItemId = addModel.TopMaterialItemId;
                model.BottomTier = addModel.BottomTier;
                model.BottomThickness = addModel.BottomThickness;
                model.BottomMaterialItemId = addModel.BottomMaterialItemId;

                model.FwcellInfo.InnerTopLength = addModel.FwcellInfo.InnerTopLength;
                model.FwcellInfo.InnerBottomLength = addModel.FwcellInfo.InnerBottomLength;
                //model.FwcellInfo.OutTopLength = addModel.FwcellInfo.OutTopLength;
                //model.FwcellInfo.OutBottomLength = addModel.FwcellInfo.OutBottomLength;
                model.FwcellInfo.Thickness = addModel.FwcellInfo.Thickness;
                model.FwcellInfo.Height = addModel.FwcellInfo.Height;
                model.FwcellInfo.XCount = addModel.FwcellInfo.XCount;
                model.FwcellInfo.YCount = addModel.FwcellInfo.YCount;
                model.FwcellInfo.FwcellMaterialItemId = addModel.FwcellInfo.FwcellMaterialItemId;
                dbContext.BoardInfos.Update(model);
            }
            else
            {
                await dbContext.BoardInfos.AddAsync(addModel);
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
