using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using gameapi.Models;
using gameapi.Processors;
using gameapi.ModelValidation;
using Microsoft.AspNetCore.Mvc;

namespace gameapi.Controllers
{
    [Route("api/players/{playerId}/items")]
    public class ItemsController
    {
        private ItemsProcessor _processor;
        public ItemsController(ItemsProcessor processor)
        {
            _processor = processor;
        }
        
        [HttpGet]
        public Task<List<Item>> GetAll(Guid playerId)
        {
            return _processor.GetAll(playerId);
        }

        [HttpGet("{id}")]
        public Task<Item> Get(Guid playerId, Guid id)
        {
            return _processor.Get(playerId, id);
        }

        [HttpPost]
        [ValidateModel]
        public Task<Item> Create(Guid playerId, [FromBody]NewItem item)
        {
            return _processor.Create(playerId, item);
        }

        [HttpDelete("{id}")]
        public Task<Item> Delete(Guid playerId, Guid id)
        {
            return _processor.Delete(playerId, id);
        }

        [HttpPut("{id}")]
        [ValidateModel]
        public Task<Item> Update(Guid playerId, Guid id, [FromBody] ModifiedItem item)
        {
            return _processor.Update(playerId, id, item);
        }
    }
}