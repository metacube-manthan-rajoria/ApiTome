using System.ComponentModel.DataAnnotations;

namespace ApiTome.Models;

public class City
{
    [Key]
    public Guid CityID {get; set;}
    public string? CityName {get; set;}
}
