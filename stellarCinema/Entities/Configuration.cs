using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace stellarCinema.Entities;

public class Configuration
{
    [Key]
    public int Id { get; set; }
    [Precision(10, 2)]
    public decimal KeyValue { get; set; }

}
