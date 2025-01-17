using Microsoft.AspNetCore.Mvc;
using mvccoremigration.Data;
using mvccoremigration.Models;
namespace mvccoremigration.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _context;
        public CategoryController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var objCategoryList = _context.categories.ToList();
            return View(objCategoryList);
        }
        public IActionResult create()
        {
            return View();
        }
       
        [HttpPost]
        public IActionResult Createnew(category obj)
        {
            _context.categories.Add(obj);
            _context.SaveChanges();
            return RedirectToAction(nameof(create));
        }
        public async Task<IActionResult> edit( int id)
        {
         if( id==null || _context.categories == null)
            {
                return NotFound();
            }
            var cat = await _context.categories.FindAsync(id);
            if(cat==null)
            {
                return NotFound();
            }
            return View(cat);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> edit(int id,category category)
        {
            if(id!=category.id)
            {
                return NotFound();
            }
            _context.Update(category);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> delete(int id)
        {
            if (id == null || _context.categories == null)
            {
                return NotFound();
            }
            var cat = await _context.categories.FindAsync(id);
            if (cat == null)
            {
                return NotFound();
            }
            return View(cat);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult>deleteconfirmed(int id)
        {
            if (_context.categories==null)
            {
                return Problem("entitiy set 'ApplicationDbContext.Category' is empty ");
            }
            var product = await _context.categories.FindAsync(id);
            if(product!=null)
            {
                _context.categories.Remove(product);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
