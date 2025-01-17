using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using mvccoremigration.Data;
using mvccoremigration.Models;

namespace mvccoremigration.Controllers
{
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _context;
        public ProductController( ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var joinedData = from t1 in _context.categories
                             join t2 in _context.products on t1.id equals t2.id
                             select new viemodel
                             {
                                 id = t1.id,
                                 name = t1.name,
                                 p_id = t2.p_id,
                                 pro_name=t2.pro_name

                             };
            return View(joinedData.ToList());
        }

        public IActionResult create()
        {
            List<category> cl = new List<category>();
            cl = (from c in _context.categories select c).ToList();
            cl.Insert(0, new category { id = 0, name = "--swelect category name" });
            ViewBag.message = cl;
            return View();

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
         public async Task<IActionResult> create(product product)
        {
            _context.Add(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(create));
         }
        
        public async Task<ActionResult> Edit(int? id )
        {
            if(id==null || _context.products==null)
            {
                return NotFound();
            }
            var product = await _context.products.FindAsync(id);
            if(product==null)
            {
                return NotFound();
            }
            List<category> cl = new List<category>();
            cl = (from c in _context.categories select c).ToList();
            cl.Insert(0, new category { id = 0, name = "select name--" });
            ViewBag.message = cl;
            return View(product);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,product product)
        {
            //if(id!= product.p_id)
            //{
            //    return NotFound();
            //}
            _context.Update(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> delete(int id)
        {
            if (_context.products == null)
            {
                return Problem("entitiy set 'ApplicationDbContext.Category' is empty ");
            }
            var product = await _context.products.FindAsync(id);
            if (product != null)
            {
                _context.products.Remove(product);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
} 
