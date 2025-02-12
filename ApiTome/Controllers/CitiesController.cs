using System.Data.Common;
using ApiTome.DatabaseContext;
using ApiTome.Models;
using ApiTome.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using Microsoft.EntityFrameworkCore;

namespace ApiTome.Controllers;

[ApiController]
[DataFilter]
[Route("api/[controller]")]
public class CitiesController : ControllerBase {

    private readonly ApplicationDbContext _context;

    public CitiesController(ApplicationDbContext context){
        _context = context;
    }

    [OutputCache(Duration = 10)]
    [HttpGet("fetchcities")]
    public async Task<ActionResult<IEnumerable<City>>> GetCities(){
        if(_context.Cities == null) return StatusCode(500);
        List<City> cities = await _context.Cities.ToListAsync();
        
        return cities;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<City?>> GetCity(Guid id){
        if(_context.Cities == null) return StatusCode(500);

        return await _context.Cities.FindAsync(id);
    }

    [HttpPost]
    public async Task<IActionResult> PostCity([Bind(nameof(City.CityID), nameof(City.CityName))] City city){
        if(_context.Cities == null) return StatusCode(500, "DbContext is Null");
        city.CityID = Guid.NewGuid();
        await _context.Cities.AddAsync(city);
        try{
            await _context.SaveChangesAsync();
            return Ok("Post Request Successful");
        }catch(DbException e){
            return StatusCode(500, "We ran into an error while saving...\n" + e.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutCity(Guid id,[Bind(nameof(City.CityID), nameof(City.CityName))] City city){
        if(id != city.CityID) return BadRequest("Invalid CityID");

        _context.Entry(city).State = EntityState.Modified;
        try{
            await _context.SaveChangesAsync();
            return Ok("Put Request Successful");
        }catch(DbUpdateException e){
            return StatusCode(500, "We ran into an error while updating..." + e.Message);
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCity(Guid id){
        City? city = await _context.Cities.FindAsync(id);
        if(city == null) return StatusCode(500, "DbContext is Null");

        _context.Cities.Remove(city);
        try{
            await _context.SaveChangesAsync();
            return Ok("Delete Request Successful");
        }catch(DbException e){
            return StatusCode(500, "We ran into an error while deleting...\n" + e.Message);
        }
    }
}