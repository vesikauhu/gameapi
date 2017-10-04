using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using gameapi.Exceptions;
using gameapi.Models;
using gameapi.ModelValidation;
using gameapi.Processors;
using Microsoft.AspNetCore.Mvc;

namespace gameapi.Controllers
{

    [Route("api/players")]
    public class PlayersController : Controller
    {
        private PlayersProcessor _processor;
        public PlayersController(PlayersProcessor processor)
        {
            _processor = processor;
        }
        
        [HttpGet]
        public Task<List<Player>> GetAll()
        {
            return _processor.GetAll();
        }

        [HttpGet("{id:guid}")]
        public Task<Player> Get(Guid id)
        {
            return _processor.Get(id);
        }
        
        [HttpGet("minlevel={minlevel}")]
        public Task<List<Player>> GetByLevel(int minLevel)
        {
            return _processor.GetByLevel(minLevel);
        }

        [HttpGet("{name}")]
        public Task<Player> GetByName(string name)
        {
            return _processor.GetByName(name);
        }

        [HttpGet("tag={tag}")]
        public Task<List<Player>> GetByTag (string tag)
        {
            return _processor.GetByTag(tag);
        }

        [HttpGet("itemAmount={itemAmount}")]
        public Task<List<Player>> GetByItemAmount (int itemAmount)
        {
            return _processor.GetByItemAmount (itemAmount);
        }

        [HttpPost]
        [ValidateModel]
        public Task<Player> Create([FromBody]NewPlayer player)
        {
            return _processor.Create(player);
        }

        [HttpDelete("{id}")]
        public Task<Player> Delete(Guid id)
        {
            return _processor.Delete(id);
        }

        [HttpPut("{id}")]
        [ValidateModel]
        public Task<Player> Update(Guid id, [FromBody]ModifiedPlayer player)
        {
            return _processor.Update(id, player);
        }
    }
}