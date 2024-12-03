using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ExpenseManagerApi.Data;
using ExpenseManagerApi.Models;
using ExpenseManagerApi.Data;
using ExpenseManagerApi.Models;

namespace ExpenseManagerAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExpensesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ExpensesController(AppDbContext context)
        {
            _context = context;
        }

        // Получить все расходы
        [HttpGet]
        public async Task<IActionResult> GetAllExpenses()
        {
            var expenses = await _context.Expenses.Include(e => e.User).ToListAsync();
            return Ok(expenses);
        }

        // Получить расход по ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetExpenseById(int id)
        {
            var expense = await _context.Expenses.Include(e => e.User).FirstOrDefaultAsync(e => e.Id == id);
            if (expense == null) return NotFound();
            return Ok(expense);
        }

        // Создать новый расход
        [HttpPost]
        public async Task<IActionResult> CreateExpense([FromBody] Expense expense)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            _context.Expenses.Add(expense);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetExpenseById), new { id = expense.Id }, expense);
        }

        // Обновить существующий расход
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateExpense(int id, [FromBody] Expense expense)
        {
            if (id != expense.Id) return BadRequest();
            if (!await _context.Expenses.AnyAsync(e => e.Id == id)) return NotFound();

            _context.Expenses.Update(expense);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // Удалить расход
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExpense(int id)
        {
            var expense = await _context.Expenses.FindAsync(id);
            if (expense == null) return NotFound();

            _context.Expenses.Remove(expense);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
