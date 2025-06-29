using GroceryStore.Data;
using GroceryStore.DTO;
using GroceryStore.Mapper;
using GroceryStore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GroceryStore.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class ItemsController:ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public ItemsController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var data = await _context.Items.AsNoTracking().ToListAsync();
            var selected = data.Select(s => s.ShowDTOItems());
            return Ok(selected);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var data = await _context.Items.FindAsync(id);
            if (data is null)
            {
                return NotFound();
            }
            return Ok(data.ShowDTOItems());
        }
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Item>> Post(PostItemDTO model)
        {
            if (model == null)
            {
                return BadRequest();
            }
            else if (model.Price < 0)
            {
                return StatusCode(500, "Price Cannot be less than 0");
            }
            else
            {
                _context.Items.Add(model.ToItemPostDTO());
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(Get), new { id = model.ToItemPostDTO().ItemId }, model);
            }
        }
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> Put([FromBody]Item model, [FromRoute] int id)
        {
            var Data = await _context.Items.FindAsync(id);
            if(Data is null)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                Data.Name = model.Name;
                Data.Price = model.Price;
                Data.ComanyName = model.ComanyName;
                Data.SupplierName = model.SupplierName;
                Data.DateOfPurchase = model.DateOfPurchase;
            }
            _context.Update(Data);
            await _context.SaveChangesAsync();
            return Ok(Data);
        }
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            var Data = await _context.Items.FindAsync(id);
            if(Data is null)
            {
                return NotFound();
            }
            _context.Items.Remove(Data);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
