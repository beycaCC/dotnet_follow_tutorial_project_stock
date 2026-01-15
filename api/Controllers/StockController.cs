using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Stock;
using api.Helpers;
using api.Interfaces;
using api.Mappers;
using api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{
    // [Route("api/[Controller]")]   // Both ways work.
    [Route("api/stock")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        private readonly IStockRepository _stockRepo;
        public StockController(ApplicationDBContext context, IStockRepository stockRepo)
        {
            _stockRepo = stockRepo;
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] QueryObject query)
        {
            if (!ModelState.IsValid) 
                return BadRequest(ModelState);

            // ToList() method is makiing sure that the stuff returned is not db itself
            // Without ToList(), _context.Stock is returning the database itself.
            var stocks = await _stockRepo.GetAllAsync(query);
            var stockDto = stocks.Select(s => s.ToStockDto());  // .Select is .Net version of map
                                            // Here is going to return an immutable array or any immutable list of this two stock dtos

            // only `var stocks = _context.Stock.ToList()` is returning a list of things and it's expecting just a solo stock dto

            return Ok(stockDto);
        }

        [HttpGet("{id:int}")]
        // the 'IactionResult' will automatically return the API call status (e.g. 500, 200, 404,...)
        //dotnet will use "model binding" to extract the string out from "[HttpGet("{id}")]" and turn it into an int and then pass it right down into our actual code
        public async Task<IActionResult> GetFromId([FromRoute] int id) 
        {
            if (!ModelState.IsValid) 
                return BadRequest(ModelState);
                
            var stock = await _stockRepo.GetByIdAsync(id);

            if (stock == null)
            {
                return NotFound();  // NotFound() is also a fancy stuff to return notfound request instead of typing the status code manually
            }

            return Ok(stock.ToStockDto());
        }

        [HttpPost]
        // [FromBody] is required because our data is been sent in json format
        // we're passing the data in body of the http, not url
        public async Task<IActionResult> Create([FromBody] CreateStockRequestDto stockDto)
        {
            if (!ModelState.IsValid) 
                return BadRequest(ModelState);

            var stockModel = stockDto.ToStockFromCreateDto();
            // we only need to add await for the lines executing the database
            await _stockRepo.CreateAsync(stockModel);

            return CreatedAtAction(nameof(GetFromId), new { id = stockModel.Id }, stockModel.ToStockDto());
        }

        [HttpPut]
        [Route("{id:int}")] // used for finding the existing records
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateStockRequestDto updateDto)
        {
            if (!ModelState.IsValid) 
                return BadRequest(ModelState);
                
            var stockModel = await _stockRepo.UpdateAsync(id, updateDto.ToStockFromupdateDto());

            if (stockModel == null)
            {
                return NotFound();
            }

            return Ok(stockModel.ToStockDto());
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if (!ModelState.IsValid) 
                return BadRequest(ModelState);

            var stockModel = await _stockRepo.DeleteAsync(id);
            if (stockModel == null) 
            {
                return NotFound();
            }

            return NoContent(); // NoContent() means success in Delete call.
        }
    }
}