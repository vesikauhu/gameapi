using System;
using System.Collections.Generic;
using gameapi.Exceptions;
using System.Threading.Tasks;
using gameapi.Models;
using gameapi.mongodb;
using MongoDB.Driver;

namespace gameapi.Repositories
{
  //Gets from and updates data to MongoDb 
  public class MongoDbRepository : IRepository
  {
    private IMongoCollection<Player> _collection;
    public MongoDbRepository(MongoDBClient client)
    {
      //Getting the database with name "game"
      IMongoDatabase database = client.GetDatabase("game");

      //Getting collection with name "players"
      _collection = database.GetCollection<Player>("players");
    }

    public async Task<Item> CreateItem(Guid playerId, Item item)
    {
        var player = await GetPlayer(playerId);
        player.Items.Add(item);
        await UpdatePlayer(player);
        return item;
    }

    public async Task<Player> CreatePlayer(Player player)
    {
      await _collection.InsertOneAsync(player);
      return player;
    }

    public async Task<Item> DeleteItem(Guid playerId, Item item)
    {

        Player player = await GetPlayer(playerId);
        player.Items.RemoveAll(i => i.Id == item.Id);
        await UpdatePlayer(player);
        return item;
    }

    public async Task<Player> DeletePlayer(Guid playerId)
    {
        Player player = await GetPlayer(playerId);
        var filter = Builders<Player>.Filter.Eq(p => p.Id, playerId);
        await _collection.DeleteOneAsync(filter);
        return player;
    }

    public async Task<List<Item>> GetAllItems(Guid playerId)
    {
        var player = await GetPlayer(playerId);
        return player.Items;
    }

    public async Task<List<Player>> GetAllPlayers()
    {
      List<Player> players = new List<Player>();
      var filter = FilterDefinition<Player>.Empty;
      using (IAsyncCursor<Player> cursor = await _collection.FindAsync(filter))
      {
        while (await cursor.MoveNextAsync())
          {
            IEnumerable<Player> batch = cursor.Current;
            foreach (Player p in batch)
            {
              players.Add(p);
            }
          }
      }
      return players;
    }

    public async Task<Item> GetItem(Guid playerId, Guid itemId)
    {
        Item itemToReturn = null;
        Player player = await GetPlayer(playerId);
        foreach (Item i in player.Items)
        {
            if (i.Id.Equals(itemId))
            {
              itemToReturn = i;
              break;
            }
            else
            {
              continue;
            }
        }

        if (itemToReturn == null) {
          throw new ItemNotFoundException();
        }
        return itemToReturn;
    }

    public async Task<Player> GetPlayer(Guid playerId)
    {
        var filter = Builders<Player>.Filter.Eq(p => p.Id, playerId);
        var cursor = await _collection.FindAsync(filter);
        bool playerFound = await cursor.AnyAsync();
        if (playerFound)
        {
          cursor = await _collection.FindAsync(filter);
          return await cursor.FirstAsync();
        }
        else
        {
          throw new NotFoundException();
        }
    }

    public async Task<List<Player>> GetByLevel (int minLevel)
    {
      var filter = Builders<Player>.Filter.Gte("Level", minLevel);
      var cursor = await _collection.FindAsync(filter);
      bool playerFound = await cursor.AnyAsync();
      if (playerFound)
        {
          cursor = await _collection.FindAsync(filter);
          List<Player> players = await cursor.ToListAsync();
          return players;
        }
        else
        {
          throw new NotFoundException();
        }
    }

    public async Task<Player> GetByName (string name)
    {
      var filter = Builders<Player>.Filter.Eq("Name", name);
      var cursor = await _collection.FindAsync(filter);
      bool playerFound = await cursor.AnyAsync();
      if (playerFound)
      {
        cursor = await _collection.FindAsync(filter);
        var player = await cursor.FirstAsync();
        return player;
      }
      else
      {
        throw new NotFoundException();
      }
    }

    public async Task<List<Player>> GetByTag (string tag)
    { 
      var filter = Builders<Player>.Filter.Eq("Tags", tag);
      var cursor = await _collection.FindAsync(filter);
      List<Player> players = await cursor.ToListAsync();
      return players;
    }

    public async Task<List<Player>> GetByItemAmount (int itemAmount)
    {
      var filter = Builders<Player>.Filter.Size("Items", itemAmount);
      var cursor = await _collection.FindAsync(filter);
      List<Player> players = await cursor.ToListAsync();
      return players;
    }
    public async Task<Item> UpdateItem(Guid playerId, Item item)
    {
      Player player = await GetPlayer(playerId);
      await DeleteItem(playerId, item);
      await CreateItem(playerId, item);
      return item;
    }

    public async Task<Player> UpdatePlayer(Player player)
    {
      var filter = Builders<Player>.Filter.Eq(p => p.Id, player.Id);
      await _collection.ReplaceOneAsync(filter, player);
      return player;
    }
  }
}