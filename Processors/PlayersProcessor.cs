using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using gameapi.Models;
using gameapi.Repositories;

namespace gameapi.Processors
{
  // Business logic (logic for verifying and updating the data) happens here
  public class PlayersProcessor
  {
    private readonly IRepository _repository;
    public PlayersProcessor(IRepository repository)
    {
      _repository = repository;
    }

    public Task<List<Player>> GetAll()
    {
      return _repository.GetAllPlayers();
    }

    public Task<Player> Get(Guid id)
    {
        return _repository.GetPlayer(id);
    }

    public Task<List<Player>> GetByLevel(int minLevel)
    {
      return _repository.GetByLevel(minLevel);
    }

    public Task<Player> GetByName(string name)
    {
      return _repository.GetByName(name);
    }

    public Task<List<Player>> GetByTag (string tag)
    {
      return _repository.GetByTag(tag);
    }

    public Task<List<Player>> GetByItemAmount (int itemAmount)
    {
      return _repository.GetByItemAmount (itemAmount);
    }

    public Task<Player> Create(NewPlayer newPlayer)
    {
      Player player = new Player()
      {
          Id = Guid.NewGuid(),
          Name = newPlayer.Name,
          Level = 1,
          Items = new List<Item>(),
          Tags = new List<string>()
      };
      foreach (string s in newPlayer.Tags) {
        player.Tags.Add(s);
      }
      return _repository.CreatePlayer(player);
    }

    public Task<Player> Delete(Guid id)
    {
        return _repository.DeletePlayer(id);
    }

    public async Task<Player> Update(Guid id, ModifiedPlayer modifiedPlayer)
    {
        Player player = await _repository.GetPlayer(id);
        player.Level = modifiedPlayer.Level;
        await _repository.UpdatePlayer(player);
        return player;
    }
  }
}