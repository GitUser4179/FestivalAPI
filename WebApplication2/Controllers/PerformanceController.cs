using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Dtos;

namespace WebApplication2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PerformanceController : ControllerBase
    {
        public PerformanceController(FestivalDbContext db)
        {
            _dbContext = db;
        }
        private readonly FestivalDbContext _dbContext;

        [HttpGet]
        [Route("GetAll")]
        public async Task<ActionResult<List<PerformanceDto>>> GetAllPerformances()
        {
            try
            {
                var perfromances = await _dbContext.Performances.ToListAsync();
                List<PerformanceDto> performanceDtos = new List<PerformanceDto>();
                foreach (var per in perfromances) 
                {
                    var converted = MapToDto(per);
                    performanceDtos.Add(converted);
                }

                if (perfromances != null && perfromances.Count > 0)
                {
                    return Ok(performanceDtos);

                }
                else if(perfromances != null && perfromances.Count == 0)
                {
                    return NotFound("No performances found.");
                }
                else
                {
                    return BadRequest("Unable to get performances.");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> CreatePerformance(PerformanceDto newPerf)
        {
            try
            {
                if (newPerf == null)
                {
                    return BadRequest("Performance data is null.");
                }
                else
                {
                    var performance = MapFromDto(newPerf);
                    _dbContext.Performances.Add(performance);
                    await _dbContext.SaveChangesAsync();
                    return Ok(performance);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

        }

        [HttpPut]
        [Route("UpdatePerformance")]
        public async Task<IActionResult> UpdatePerformance(PerformanceDto updatedPerf)
        {
            try
            {
                if(updatedPerf == null || updatedPerf.Id == null)
                {
                    return BadRequest("Performance data is null or Id is missing.");
                }
                else
                {
                    var performance = await _dbContext.Performances.FindAsync(updatedPerf.Id);
                    if (performance == null)
                    {
                        return NotFound($"Performance with Id {updatedPerf.Id} not found.");
                    }

                    performance.Genre = updatedPerf.Genre;
                    performance.LengthMinutes = updatedPerf.LengthMinutes;
                    performance.Artists = updatedPerf.Artists;
                    performance.StageId = updatedPerf.StageId;
                    performance.PerformanceTime = updatedPerf.PerformanceTime;

                    _dbContext.Performances.Update(performance);
                    await _dbContext.SaveChangesAsync();
                    return Ok(performance);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

        }

        [HttpDelete]
        [Route("DeletePerformance/{id}")]
        public async Task<IActionResult> DeletePerformanceById(int id)
        {
            try
            {
                if (id == 0)
                {
                    return BadRequest("Performance data is null or Id is missing.");
                }
                else
                {
                    var performance = await _dbContext.Performances.FindAsync(id);
                    if (performance == null)
                    {
                        return NotFound($"Performance with Id {id} not found.");
                    }
                    _dbContext.Performances.Remove(performance);
                    await _dbContext.SaveChangesAsync();
                    return Ok($"Performance with Id {id } deleted successfully.");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }


        [HttpGet]
        [Route("GetPerformanceByTime")]
        public async Task<ActionResult<List<PerformanceDto>>> GetPerformancesByTime(DateTime ChosenTime)
        {
            try
            {
                var performances = await _dbContext.Performances
                    .Where(p => p.PerformanceTime >= ChosenTime)
                    .ToListAsync();

                if (performances != null && performances.Count > 0)
                {
                    List<PerformanceDto> performanceDtos = new List<PerformanceDto>();
                    foreach (var per in performances)
                    {
                        var converted = MapToDto(per);
                        performanceDtos.Add(converted);
                    }
                    return Ok(performanceDtos);
                }
                else if (performances != null && performances.Count == 0)
                {
                    return NotFound("No performances found for the specified time.");
                }
                else
                {
                    return BadRequest("Unable to get performances.");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }



        public PerformanceDto MapToDto(Performance perf)
        {
            if(perf == null)
            {
                return null;
            }

            var performanceDto = new PerformanceDto
            {
                Id = perf.Id,
                Genre = perf.Genre,
                LengthMinutes = perf.LengthMinutes,
                Artists = perf.Artists,
                StageId = perf.StageId,
                StageNavigation = perf.StageNavigation,
                PerformanceTime = perf.PerformanceTime
            };

            return performanceDto;
        }



        public Performance MapFromDto(PerformanceDto dto)
        {
            if (dto == null)
            {
                return null;
            }

            var performance = new Performance
            {
                Id = (int)dto.Id,
                Genre = dto.Genre,
                LengthMinutes = dto.LengthMinutes,
                Artists = dto.Artists,
                StageId = dto.StageId,
                StageNavigation = dto.StageNavigation,
                PerformanceTime = dto.PerformanceTime
            };

            return performance;
        }
    }
}
