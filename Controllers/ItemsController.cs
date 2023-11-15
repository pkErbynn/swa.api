using System;
using System.Runtime.InteropServices;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SWA.API.Data;
using SWA.API.Dtos;
using SWA.API.Properties;

namespace SWA.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
	{
        private readonly IItemRepository _itemRepo;
        private readonly IMapper _mapper;

        public ItemsController(
            IItemRepository itemRepo,
            IMapper mapper)
		{
            _itemRepo = itemRepo;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<ItemReadDto>> GetItems()
        {
            try
            {
                var items = _itemRepo.GetAllItems();
                return Ok(_mapper.Map<IEnumerable<ItemReadDto>>(items));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data");
            }
        }

        [HttpPost]
        public ActionResult<ItemReadDto> CreateItem([FromBody]ItemAddDto itemAddDto)
        {
            try
            {
                if (itemAddDto == null)
                {
                    return BadRequest();
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var itemModel = _mapper.Map<Item>(itemAddDto);
                itemModel.Id = Guid.NewGuid();  // no-effect

                _itemRepo.CreateItem(itemModel);

                var itemReadDto = _mapper.Map<ItemReadDto>(itemModel);
                return Created($"/items/{itemReadDto.Id}", itemReadDto);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error creating new item record");
            }
        }

        [HttpPost("process-concurrently")]
        public async Task<IActionResult> ProcessItemsConcurrently()
        {
            try
            {
                var items = _itemRepo.GetAllItems(); // Retrieve items from your data store

                var itemsWithRowNumbers = items.Select((item, index) => new Item
                {
                    Id = item.Id,
                    Name = item.Name,
                    RowNumber = index + 1
                });

                var tasks = new List<Task>();
                foreach (var item in itemsWithRowNumbers)
                {
                    tasks.Add(Task.Run(async () => await _itemRepo.ProcessItemConcurrently(item)));
                }

                await Task.WhenAll(tasks);

                return Ok("Concurrent processing completed.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}

