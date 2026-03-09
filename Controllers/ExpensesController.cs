using Finance.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Reflection.Metadata.Ecma335;


namespace Finance.Controllers
{
    public class ExpenseController : Controller
    {
        // Fake Database
        private static List<Expense> _expenses = new List<Expense>
        {
            new Expense {Id=1, Description="Buy groceries", Amount=50, Category="Food"},
            new Expense {Id=2, Description="Netflix subscription", Amount=15, Category="Entertainment"}
        };      


        // Index action
        public IActionResult Index(string category, string search, string sort)
        {
            var expenses = _expenses;
            
            // Select
            if (!string.IsNullOrEmpty(category) && category != "All")
            {
                expenses = expenses.Where(e => e.Category == category).ToList(); // if category = food => (e=>e.Category==food)
            }

            // Search
            if (!string.IsNullOrEmpty(search)) 
            {
                expenses = expenses.Where(e => e.Description.Contains(search.ToLower())).ToList();  
            }

            // Sort
            if (sort == "amount")
            {
                expenses = expenses.OrderBy(e=>e.Amount).ToList();
            }

            ViewBag.Categories = _expenses.Select(e=>e.Category).Distinct().ToList();
            return View(expenses);

        }


        // Step 3: I create action to add new expense, I need two..
        // 1. GET: show the empty form
        public IActionResult Create()
        {
            return View();
        }

        // 2. POST: handle form submission. It receives the data the user types
        [HttpPost]
        public IActionResult Create(Expense expense)
        {

            if (!ModelState.IsValid) // If the form has errors
            {
                return View(expense); // return it, and show errors
            }

            expense.Id = _expenses.Any() ? _expenses.Max(e => e.Id) + 1 : 1;
            _expenses.Add(expense);
            return RedirectToAction("Index"); 
        }



        public IActionResult Delete(int? id) // ? = it can be null
        {
            if (id == null)
            {
                return NotFound();
            }
            var item = _expenses.FirstOrDefault(e => e.Id == id);
            if (item == null)
            {
                return NotFound();
            }
            return View(item);
        }

        [HttpPost]
        public IActionResult DeleteConfirmed(int id)
        {
            var item = _expenses.FirstOrDefault(e => e.Id == id);
            if (item != null)
            {
                _expenses.Remove(item);
            }
            return RedirectToAction("Index");
        }


        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var item = _expenses.FirstOrDefault(e => e.Id == id);

            if (item == null)
            {
                return NotFound();                
            }

            return View(item);
        }


        [HttpPost]
        public IActionResult Edit(Expense updatedExpense)
        {
            var expense = _expenses.FirstOrDefault(e => e.Id == updatedExpense.Id);

            if (expense == null)
            {
                return NotFound();
            }

            expense.Description = updatedExpense.Description;
            expense.Amount = updatedExpense.Amount;
            expense.Category = updatedExpense.Category;
            return RedirectToAction("Index");
        }
        


    }
}




/*
Create():


_expenses.Any() ? _expenses.Max(e => e.Id) + 1 : 1; =====> Condition ? value if true : value if false.
= So it means, get the highest id if there's already an expense, else set the id 1 value.

_expenses.Any() = It checks if there's already any expense in the list
_expenses.Max(e => e.Id) = It gets the highest existing id.
+1 = Creates the next id for the new expense
:1 = If the list is empty, start id from 1





*/