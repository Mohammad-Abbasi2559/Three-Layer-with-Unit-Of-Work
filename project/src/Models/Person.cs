using System.ComponentModel.DataAnnotations;

namespace Models;

public class Person
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }
}
