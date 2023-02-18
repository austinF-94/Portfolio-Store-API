using System.ComponentModel.DataAnnotations;

namespace Store_API.Models;

public class Product 
{
    public int ProductId { get; set; }

    [Required]
    public string? Title { get; set; }

    [Required]
    public double Price { get; set; }

    [Required]
    public string? Description { get; set; }

    [Required]
    public string? Picture { get; set; }
}