using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SPSD.WebApi.Model;
using SPSD.WebApi.ParamModel;

namespace SPSD.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController(AppDbContext dbContext,ILogger<ProjectController> logger) : ControllerBase
    {
        // GET: api/<ProjectController>
        [HttpGet]
        public async Task<IEnumerable<ProjectInfo>> Get()
        {
            var list = await dbContext.ProjectInfos.AsNoTracking().ToListAsync();
            return list;
        }

        [HttpGet("list")]
        public async Task<IEnumerable<ProjectInfo>> GetList(int category)
        {
            var list = await dbContext.ProjectInfos.Where(x => x.Category == category).AsNoTracking().ToListAsync();
            return list;
        }

        // GET api/<ProjectController>/5
        [HttpGet("{id}")]
        public async Task<ProjectInfo?> Get(int id)
        {
            var model = await dbContext.ProjectInfos.FindAsync(id);
            return model;
        }

        // POST api/<ProjectController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AddProject addModel)
        {
            var exist = await dbContext.ProjectInfos.AnyAsync(x => (x.ProjectName == addModel.ProjectName || x.ProjectCode == addModel.ProjectCode) && x.Category == addModel.Category);
            if (exist)
            {
                return BadRequest("项目名称或编号已存在，添加失败");
            }

            //addModel.CreateTime = DateTime.Now;
            addModel.CreateUserId = 1; // 假设创建人ID为1

            var time = DateTime.Now;
            if (!string.IsNullOrEmpty(addModel.CreateTime))
            {
                time = DateTime.Parse(addModel.CreateTime);
            }

            var project = new ProjectInfo
            {
                Category = addModel.Category,
                ProjectName = addModel.ProjectName,
                ProjectCode = addModel.ProjectCode,
                CreateTime = time,
                CreateUserId = addModel.CreateUserId,
                CreateUser = addModel.CreateUser,
                SimulationTypeOne = addModel.SimulationTypeOne,
                SimulationTypeTwo = addModel.SimulationTypeTwo,
                WorkDirectory = addModel.WorkDirectory
            };

            var res = await dbContext.ProjectInfos.AddAsync(project);
            await dbContext.SaveChangesAsync();
            logger.LogInformation("添加项目信息,项目名称：{ProjectName}", project.ProjectName);
            dbContext.Entry(project);
            // 添加默认防护结构模型
            if (addModel.Category == 1)
            {
                var boardInfo = new BoardInfo()
                {
                    //ProjectId = res.Entity.Id,
                    ProjectId = project.Id,
                    TopTier = 6,
                    TopThickness = 0.33,
                    TopMaterialItemId = 0,
                    BottomTier = 38,
                    BottomThickness = 0.33,
                    BottomMaterialItemId = 0,

                    FwcellInfo = new Fwcell
                    {
                        InnerTopLength = 9,
                        InnerBottomLength = 14,
                        Thickness = 1.5,
                        Height = 10,
                        XCount = 6,
                        YCount = 5,
                        FwcellMaterialItemId = 0,
                    }
                };
                await dbContext.BoardInfos.AddAsync(boardInfo);
                logger.LogInformation("添加靶板模型信息");
                // 添加默认的仿真参数设置
                {
                    // 仿真参数设置
                    // 速度 ： 800
                    // 时间步长：0.67
                    // 输出间隔：2
                    // 接触刚度：2
                    // 计算时长：120
                    // 输出间隔：0.01

                    var kineticEnergyInfo = new KineticEnergyInfo()
                    {
                        //ProjectId = res.Entity.Id,
                        ProjectId = project.Id,
                        Speed = 800,
                        TSSFAC = 0.67,
                        D3plots = 2,
                        SLSFAC = 2,
                        ENDTIM = 120,
                        DT = 0.01,
                    };
                    await dbContext.KineticEnergyInfos.AddAsync(kineticEnergyInfo);
                    logger.LogInformation("添加动能混伤建模信息");
                }
            }
            else
            {
                var kangBaoInfo = new KangBaoInfo()
                {
                    //ProjectId = res.Entity.Id,
                    ProjectId = project.Id,
                    Charge_l = 1.1,
                    Charge_r = 1,
                    Charge_z = 0.55,
                    SubL1_y = 10,
                    SubL1_z = 5,
                    L1_x = 150,
                    L1_y = 200,
                    L1_z = 150,
                    L2_y = 80,
                    StructWallThickness = 1.5,
                    BoxThickness = 0.1,
                    Edx = 1,
                    AirMaterialItemId = 0,
                    BoxMaterialItemId = 0,
                    DynamiteMaterialItemId = 0,
                    StructMaterialItemId = 0,
                };
                await dbContext.KangBaoInfos.AddAsync(kangBaoInfo);
                logger.LogInformation("添加抗爆模型信息");
                var kineticEnergyInfo = new KineticEnergyInfo()
                {
                    //ProjectId = res.Entity.Id,
                    ProjectId = project.Id,
                    Speed = 800,
                    TSSFAC = 0.67,
                    D3plots = 100,
                    SLSFAC = 2,
                    ENDTIM = 20000,
                    DT = 1,
                };
                await dbContext.KineticEnergyInfos.AddAsync(kineticEnergyInfo);
                logger.LogInformation("添加动能混伤建模信息");
            }
            await dbContext.SaveChangesAsync();

            return Ok();
        }

        // PUT api/<ProjectController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] ProjectInfo editModel)
        {
            var model = await dbContext.ProjectInfos.FindAsync(id);
            if (model is not null)
            {
                var exist = await dbContext.ProjectInfos.AnyAsync(x => (x.ProjectName == editModel.ProjectName ||
                x.ProjectCode == editModel.ProjectCode) && x.Id != id && x.Category == editModel.Category);
                if (exist)
                {
                    return BadRequest("项目名称或编号已存在，修改失败");
                }

                model.ProjectName = editModel.ProjectName;
                model.ProjectCode = editModel.ProjectCode;
                model.SimulationTypeOne = editModel.SimulationTypeOne;
                model.SimulationTypeTwo = editModel.SimulationTypeTwo;
                model.WorkDirectory = editModel.WorkDirectory;
                //model.CreateUserId = editModel.CreateUserId;
                model.UpdateTime = DateTime.Now;
                dbContext.ProjectInfos.Update(model);
                await dbContext.SaveChangesAsync();
                return Ok();
            }

            return NoContent();
        }

        // DELETE api/<ProjectController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await dbContext.ProjectInfos.Where(x => x.Id == id).ExecuteDeleteAsync();
            return Ok();
        }

        [HttpPost("addImages")]
        public async Task<IActionResult> AddImages([FromBody] AddProjectImage addModel)
        {
            var project = await dbContext.ProjectInfos.SingleOrDefaultAsync(x => x.Id == addModel.Id);
            if (project != null)
            {
                project.ImageOnePath = addModel.ImageOnePath;
                project.ImageTwoPath = addModel.ImageTwoPath;
                project.ImageThreePath = addModel.ImageThreePath;
                project.ImageFourPath = addModel.ImageFourPath;

                dbContext.ProjectInfos.Update(project);
                await dbContext.SaveChangesAsync();
            }
            return Ok();
        }

        /// <summary>
        /// 修改项目计算结果状态
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("editComputeResultStatus")]
        public async Task<IActionResult> EditComputeResultStatus([FromBody] EditComputeResultStatusModel model)
        {
            var project = await dbContext.ProjectInfos.SingleOrDefaultAsync(x => x.Id == model.Id);
            if (project != null)
            {
                project.ComputeResultStatus = model.ComputeResultStatus;
                dbContext.ProjectInfos.Update(project);
                await dbContext.SaveChangesAsync();
            }
            return Ok();
        }


        [HttpPost("editFolderPath")]
        public async Task<IActionResult> EditFolderPath([FromBody] EditFolderPathModel model)
        {
            var project = await dbContext.ProjectInfos.SingleOrDefaultAsync(x => x.Id == model.Id);
            if (project != null)
            {
                project.WorkDirectory = model.WorkDirectory;
                dbContext.ProjectInfos.Update(project);
                await dbContext.SaveChangesAsync();
            }
            return Ok();
        }

    }
}