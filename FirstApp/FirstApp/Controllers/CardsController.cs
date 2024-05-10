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
    public class CardsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CardsController(AppDbContext context)
        {
            _context = context;
        }

        // POST: api/Cards
        [HttpPost]
        public async Task<ActionResult<Card>> CreateCard(Card card)
        {
            // Создание карточки
            _context.Cards.Add(card);
            await _context.SaveChangesAsync();

            // Добавление записи в лог
            _context.Activity.Add(new Activity { Date = DateTime.Now, Type = 1, NewValue = card.CardName });
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCard), new { id = card.Id }, card);
        }

        // GET: api/Cards
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Card>>> GetCards()
        {
            return await _context.Cards.ToListAsync();
        }

        // GET: api/Cards/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Card>> GetCard(int id)
        {
            var card = await _context.Cards.FindAsync(id);

            if (card == null)
            {
                return NotFound();
            }

            return card;
        }

        // PUT: api/Cards/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCard(int id, Card card)
        {
            if (id != card.Id)
            {
                return BadRequest();
            }

            var oldCard = await _context.Cards.FindAsync(id);
            var oldValue = oldCard.CardName; // Сохраняем старые значения полей карточки
            var oldPriority = oldCard.Priority;

            _context.Entry(card).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CardExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            // Добавление записи в лог
            if (oldCard.CardName != card.CardName)
            {
                _context.Activity.Add(new Activity { Date = DateTime.Now, Type = 3, OldValue = oldValue, NewValue = card.CardName });
                await _context.SaveChangesAsync();
            }
            if (oldCard.Priority != card.Priority)
            {
                _context.Activity.Add(new Activity { Date = DateTime.Now, Type = 5, OldValue = oldPriority, NewValue = card.Priority });
                await _context.SaveChangesAsync();
            }
            if (oldCard.ListId != card.ListId)
            {
                _context.Activity.Add(new Activity { Date = DateTime.Now, Type = 4, OldValue = oldCard.ListId.ToString(), NewValue = card.ListId.ToString() });
                await _context.SaveChangesAsync();
            }
            
            return NoContent();
        }

        // DELETE: api/Cards/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCard(int id)
        {
            var card = await _context.Cards.FindAsync(id);
            if (card == null)
            {
                return NotFound();
            }

            _context.Cards.Remove(card);
            await _context.SaveChangesAsync();

            // Добавление записи в лог
            _context.Activity.Add(new Activity { Date = DateTime.Now, Type = 2, OldValue = card.CardName });
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CardExists(int id)
        {
            return _context.Cards.Any(e => e.Id == id);
        }
    }
}
