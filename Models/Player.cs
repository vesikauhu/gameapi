using System;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace gameapi.Models
{
  public class Player
  {
    [BsonId]
    public Guid Id { get; set; }
    public string Name { get; set; }
    public int Level { get; set; }
    public List<Item> Items;

    public List<string> Tags { get; set; }
  }

  public class NewPlayer
  {
    [Required]
    public string Name { get; set; }
    public List<string> Tags { get; set; }
  }

  public class ModifiedPlayer
  {
    [Required]
    [Range (2, 100)]
    public int Level{get;set;}
  }
}