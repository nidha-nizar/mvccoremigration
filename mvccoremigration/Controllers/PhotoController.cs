using Microsoft.AspNetCore.Mvc;
using mvccoremigration.Data;
using mvccoremigration.Models;
using Microsoft.EntityFrameworkCore;

namespace mvccoremigration.Controllers
{
    public class PhotoController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _iweb;

        public PhotoController(ApplicationDbContext context,IWebHostEnvironment iweb)
        {
            _context = context;
            _iweb = iweb;
        }
        public  async Task<IActionResult> Index()
        {
            return _context.file_tb != null ?
              View(await _context.file_tb.ToListAsync()):
            Problem("entity set application.categpries is null");
        }
        public IActionResult create()
        {
            return View();
        }
        

        [HttpPost]
        public async Task <IActionResult> Createpost(IFormFile fileobj,photo ph)
        {
            var imgtxt = Path.GetExtension(fileobj.FileName);
            if(imgtxt ==".jpg"|| imgtxt==".gif" ||imgtxt==".png")
            {
                var uploading = Path.Combine(_iweb.WebRootPath, "Images", fileobj.FileName);
                var stream = new FileStream(uploading, FileMode.Create);
                await fileobj.CopyToAsync(stream);
                stream.Close();
                ph.filename = fileobj.FileName;
                ph.filepath = uploading;
                _context.file_tb.Add(ph);

                await _context.SaveChangesAsync();

            }
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> edit(int? id)
        {
            if (id == null || _context.file_tb == null)
            {
                return NotFound();
            }
            var cat = await _context.file_tb.FindAsync(id);
            if (cat == null)
            {
                return NotFound();
            }
            return View(cat);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> edit(int id, photo ph, IFormFile fileobj)
        {
            if (id != ph.photo_id)
            {
                return NotFound();
            }
            var imgtxt = Path.GetExtension(fileobj.FileName);
            if(imgtxt ==".jpg"|| imgtxt==".gif" ||imgtxt==".png")
            {
                var uploading = Path.Combine(_iweb.WebRootPath, "Images", fileobj.FileName);
                var stream = new FileStream(uploading, FileMode.Create);
                await fileobj.CopyToAsync(stream);
                stream.Close();
                ph.filename = fileobj.FileName;
                ph.filepath = uploading;
                _context.Update(ph);
                await _context.SaveChangesAsync();

            }
           
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> delete(int id)
        {
            if (_context.file_tb == null)
            {
                return Problem("entitiy set 'ApplicationDbContext.Category' is empty ");
            }
            var product = await _context.file_tb.FindAsync(id);
            if (product != null)
            {
                _context.file_tb.Remove(product);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
