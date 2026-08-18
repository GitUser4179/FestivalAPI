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

            if (artists == null)
            {
                return NotFound();
            }

            return Ok(artists);
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return NotFound();
            }
        }

        [HttpGet]
        [Route("GetSingle/{id}")]
        public async Task<ActionResult<List<Artist>>> GetArtistById(int id)
        {

            var artist = _dbContext.Artists.FirstOrDefault(i => i.Id == id);

            if (artist == null)
            {
                return NotFound();
            }

            return Ok(artist);
        }

        [HttpPost]
        [Route("CreateArtist")]
        public async Task<ActionResult<Artist>> CreateArtist(Artist artist)
        {

            if (artist == null)
            {
                return NotFound();
            }

            var artists = await _dbContext.Artists.ToListAsync();

            _dbContext.Artists.Add(
                new Artist { Id = 4, Name = "Artist 4"}    
            );

            await _dbContext.SaveChangesAsync();
            return Ok(artists);
        }

        [HttpPut]
        [Route("UpdateArtist/{id}")]
        public async Task<ActionResult<List<Artist>>> UpdateArtist(int id)
        {
            var artist = await _dbContext.Artists.FirstOrDefaultAsync(i => i.Id == id);
            
            if (artist != null) artist.Name = $"Updated Artist {id}";

            await _dbContext.SaveChangesAsync();
            return Ok(artist);
        }

        [HttpDelete]
        [Route("DeleteArtist/{id}")]
        public async Task<ActionResult<List<Artist>>> DeleteArtist(int id) 
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
    };
}
