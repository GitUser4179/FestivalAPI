using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication2.Dtos;
using Microsoft.EntityFrameworkCore;

namespace WebApplication2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArtistController : ControllerBase
    {
        public ArtistController(FestivalDbContext db)
        {
            _dbContext = db;
        }

        private readonly FestivalDbContext _dbContext;

        [HttpGet]
        [Route("GetAll")]
        public async Task<ActionResult<List<Artist>>> GetAllArtists()
        {
            try
            {
                var artists = await _dbContext.Artists.ToListAsync();

                if (artists != null)
                {
                    return Ok(artists);
                }
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet]
        [Route("GetSingle/{id}")]
        public async Task<ActionResult<List<Artist>>> GetArtistById(int id)
        {
            try
            {
                var artist = _dbContext.Artists.FirstOrDefault(i => i.Id == id);

                if (artist != null)
                {
                    return Ok(artist);
                }
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost]
        [Route("CreateArtist")]
        public async Task<ActionResult<Artist>> CreateArtist(Artist artist)
        {
            try
            {
                if (artist != null)
                {
                    var artists = await _dbContext.Artists.ToListAsync();

                    _dbContext.Artists.Add(
                        new Artist { Id = 4, Name = "Artist 4"}    
                    );

                    await _dbContext.SaveChangesAsync();
                    return Ok(artists);
                }
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPut]
        [Route("UpdateArtist/{id}")]
        public async Task<ActionResult<List<Artist>>> UpdateArtist(int id)
        {
            try
            {
                var artist = await _dbContext.Artists.FirstOrDefaultAsync(i => i.Id == id);

                if (artist != null)
                {
                    artist.Name = $"Updated Artist {id}";

                    await _dbContext.SaveChangesAsync();
                    return Ok(artist);
                }
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpDelete]
        [Route("DeleteArtist/{id}")]
        public async Task<ActionResult<List<Artist>>> DeleteArtist(int id) 
        {
            try
            {
                var artist = await _dbContext.Artists.FirstOrDefaultAsync(i => i.Id == id);
            
                if (artist != null) 
                {
                    _dbContext.Artists.Remove(artist);

                    await _dbContext.SaveChangesAsync();
                    return Ok("Artist deleted");
                }
                else 
                    return NotFound("");
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    };
}
