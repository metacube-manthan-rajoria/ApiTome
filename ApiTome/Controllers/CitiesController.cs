using System.Data.Common;
using ApiTome.DatabaseContext;
using ApiTome.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiTome.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CitiesController : ControllerBase {

    private readonly ApplicationDbContext _context;

    public CitiesController(ApplicationDbContext context){
        _context = context;
    }

    [HttpGet]
    public IEnumerable<City> GetCities(){
        if(_context.Cities == null) return null;
        
        return _context.Cities.ToList();
    }

    [HttpGet("{id}")]
    public City? GetCity(Guid id){
        if(_context.Cities == null) return null;

        return _context.Cities.Find(id);
    }

    [HttpPost]
    public IActionResult PostCity([Bind(nameof(City.CityID), nameof(City.CityName))] City city){
        if(_context.Cities == null) return StatusCode(500, "DbContext is Null");
        _context.Cities.Add(city);
        try{
            _context.SaveChanges();
            return Ok("Post Request Successful");
        }catch(DbException e){
            return StatusCode(500, "We ran into an error while saving...\n" + e.Message);
        }
    }

    [HttpPut("{id}")]
    public IActionResult PutCity(Guid id,[Bind(nameof(City.CityID), nameof(City.CityName))] City city){
        if(id != city.CityID) return BadRequest("Invalid CityID");

        _context.Cities.Update(city);
        try{
            _context.SaveChanges();
            return Ok("Put Request Successful");
        }catch(DbUpdateException){
            return StatusCode(500, "We ran into an error while updating...");
        }
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteCity(Guid id){
        City? city = _context.Cities.Find(id);
        if(city == null) return StatusCode(500, "DbContext is Null");

        _context.Cities.Remove(city);
        try{
            _context.SaveChanges();
            return Ok("Delete Request Successful");
        }catch(DbException e){
            return StatusCode(500, "We ran into an error while deleting...\n" + e.Message);
        }
    }
}