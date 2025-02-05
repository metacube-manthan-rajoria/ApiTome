using System.Data.Common;
using ApiTome.DatabaseContext;
using ApiTome.Models;
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
    public string PostCity([Bind(nameof(City.CityID), nameof(City.CityName))] City city){
        if(_context.Cities == null) return "DbContext is Null";
        _context.Cities.Add(city);
        try{
            _context.SaveChanges();
            return "Post Request Successful";
        }catch(DbException e){
            return "We ran into an error while saving...\n" + e.Message;
        }
    }

    [HttpPut("{id}")]
    public string PutCity(Guid id,[Bind(nameof(City.CityID), nameof(City.CityName))] City city){
        if(id != city.CityID) return "Error 400";

        _context.Cities.Update(city);
        try{
            _context.SaveChanges();
            return "Put Request Successful";
        }catch(DbUpdateException){
            return "We ran into an error while updating...";
        }
    }

    [HttpDelete("{id}")]
    public string DeleteCity(Guid id){
        City? city = _context.Cities.Find(id);
        if(city == null) return "No such city";

        _context.Cities.Remove(city);
        try{
            _context.SaveChanges();
            return "Delete Request Successful";
        }catch(DbException e){
            return "We ran into an error while deleting...\n" + e.Message;
        }
    }
}