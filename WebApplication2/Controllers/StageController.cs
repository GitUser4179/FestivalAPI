using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebApplication2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StageController : ControllerBase
    {
        private readonly FestivalDbContext _dbContext;

        public StageController(FestivalDbContext db)
        {
            _dbContext = db;
        }

        [HttpGet]
        [Route("GetStages")]
        public async Task<ActionResult<List<Stage>>> GetAllStages()
        {
            try
            {
                var stages = await _dbContext.Stages.ToListAsync();
                if (stages != null)
                {
                    return Ok(stages);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

        }
        [HttpGet]
        [Route("{id}/Performance")]
        public async Task <ActionResult<List<Stage>>> GetPerformanceByStage(int id)
        {
            try
            {
                var stages = await _dbContext.Performances.Where(i => i.StageId == id).ToListAsync();
                if (stages != null)
                {
                    return Ok(stages);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

        }
    }
}
