using System;
using System.ComponentModel.DataAnnotations;

namespace ApiBasicsService.Models;

public class City
{
    [Key]
    public Guid CityID {get; set;}
    public string? CityName {get; set;}
}
