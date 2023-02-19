using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Store_API.Models;

public class Admin 
{
    [JsonIgnore]
    public int AdminId { get; set; }

    [Required]
    [EmailAddress]
    public string? Email { get; set; }
    [Required]
    public string? Password { get; set; }
}