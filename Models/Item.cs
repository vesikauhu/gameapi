using System;
using System.ComponentModel.DataAnnotations;

namespace gameapi.Models
{
  public class Item
  {
    public string Name { get; set; }
    public string Type { get; set; }
    public Guid Id { get; set; }
    public int Level { get; set; }
    public int Price { get; set; }
    public DateTime CreationDate { get; set; }
  }

  public class NewItem
  {
    [Required]
    public string Name { get; set; }
    [Required]
    public string Type {get; set; }
    [Required]
    [Range (1, 100)]
    public int Level { get; set; }
  }

  public class ModifiedItem
  {
    [Required]
    [Range (1, 100)]
    public int Level { get; set; }
    [Required]
    [Range (1, 1000)]
    public int Price { get; set; }
  }
}