using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FirstApp.Data;
using FirstApp.Models;

namespace FirstApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ListsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ListsController(AppDbContext context)
        {
            _context = context;
        }

        // POST: api/Lists
        [HttpPost]
        public async Task<ActionResult<List>> CreateList(List list)
        {
            // Создание списка дел
            _context.Lists.Add(list);
            await _context.SaveChangesAsync();

            // Добавление записи в лог
            _context.Activity.Add(new Activity { Date = DateTime.Now, Type = 1, NewValue = list.ListName });
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetList), new { id = list.Id }, list);
        }

        // GET: api/Lists
        [HttpGet]
        public async Task<ActionResult<IEnumerable<List>>> GetLists()
        {
            return await _context.Lists.ToListAsync();
        }

        // GET: api/Lists/5
        [HttpGet("{id}")]
        public async Task<ActionResult<List>> GetList(int id)
        {
            var list = await _context.Lists.FindAsync(id);

            if (list == null)
            {
                return NotFound();
            }

            return list;
        }

        // PUT: api/Lists/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateList(int id, List list)
        {
            if (id != list.Id)
            {
                return BadRequest();
            }

            var oldList = await _context.Lists.FindAsync(id);
            var oldValue = oldList.ListName; // Сохраняем старое значение названия списка

            _context.Entry(list).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ListExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            // Добавление записи в лог
            _context.Activity.Add(new Activity { Date = DateTime.Now, Type = 3, OldValue = oldValue, NewValue = list.ListName });
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Lists/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteList(int id)
        {
            var list = await _context.Lists.FindAsync(id);
            if (list == null)
            {
                return NotFound();
            }

            _context.Lists.Remove(list);
            await _context.SaveChangesAsync();

            // Добавление записи в лог
            _context.Activity.Add(new Activity { Date = DateTime.Now, Type = 2, OldValue = list.ListName });
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ListExists(int id)
        {
            return _context.Lists.Any(e => e.Id == id);
        }
    }
}
