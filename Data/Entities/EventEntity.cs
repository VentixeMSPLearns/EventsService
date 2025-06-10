using Microsoft.EntityFrameworkCore.ValueGeneration;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static System.Net.WebRequestMethods;

namespace Data.Entities;

public class EventEntity
{
    [Key]
    [Required]
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Title { get; set; } = null!;
    public string ImageUrl { get; set; } = "https://picsum.photos/seed/300/200";
    [Required]
    public DateTime DateTime { get; set; } 
    public string Location { get; set; } = null!;
    public string? Description { get; set; }
    [Required]
    [Column(TypeName = "decimal(18,2)")]
    public decimal Price { get; set; }
    [Required]
    public int TotalTickets { get; set; }
    [Required]
    public int AvailableTickets { get; set; }
}
