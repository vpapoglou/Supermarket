using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SupermarketSklavenitis.Data;
using SupermarketSklavenitis.Models;
using SupermarketSklavenitis.Utility;

namespace SupermarketSklavenitis.Areas.Admin.Controllers
{
    [Authorize(Roles = SD.ManagerUser)]
    [Area("Admin")]
    public class CouponController : Controller
    {
        private readonly ApplicationDbContext _db;

        //this controller will have this BindProperty attached with it. So we can use it directly without passing
        // it as an argument in parenthesis when we use Create, Edit etc.
        [BindProperty]
        public Coupon Coupon { get; set; }

        public CouponController(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _db.Coupon.ToListAsync());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost, ActionName("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreatePOST()
        {
            if (ModelState.IsValid)
            {
                var files = HttpContext.Request.Form.Files;
                if (files.Count > 0)
                {
                    byte[] p1 = null;
                    using (var fs1 = files[0].OpenReadStream())
                    {
                        using (var ms1 = new MemoryStream())
                        {
                            fs1.CopyTo(ms1);
                            p1 = ms1.ToArray();
                        }
                    }
                    Coupon.Picture = p1;
                }
                _db.Coupon.Add(Coupon);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(Coupon);
        }


        //GET - EDIT
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            //no need to create model item because we have the Bind Property
            Coupon = await _db.Coupon.SingleOrDefaultAsync(m => m.Id == id);

            if (Coupon == null)
            {
                return NotFound();
            }

            return View(Coupon);
        }


        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPOST(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                Coupon = await _db.Coupon.SingleOrDefaultAsync(m => m.Id == id);
                return View(Coupon);
            }

            //Work on the image saving section

            var files = HttpContext.Request.Form.Files;
            var couponFromDb = await _db.Coupon.FindAsync(Coupon.Id);

            if (files.Count > 0)
            {
                byte[] p1 = null;
                using (var fs1 = files[0].OpenReadStream())
                {
                    using (var ms1 = new MemoryStream())
                    {
                        fs1.CopyTo(ms1);
                        p1 = ms1.ToArray();
                    }
                }
                couponFromDb.Picture = p1;
            }

            couponFromDb.Name = Coupon.Name;
            couponFromDb.CouponType = Coupon.CouponType;
            couponFromDb.Discount = Coupon.Discount;
            couponFromDb.MinimumAmount = Coupon.MinimumAmount;
            couponFromDb.IsActive = Coupon.IsActive;

            await _db.SaveChangesAsync();

            return RedirectToAction(nameof(Index));

        }


        //GET - DETAILS
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            //no need to create model item because we have the Bind Property
            Coupon = await _db.Coupon.SingleOrDefaultAsync(m => m.Id == id);

            if (Coupon == null)
            {
                return NotFound();
            }

            return View(Coupon);
        }


        //GET - DELETE
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            //no need to create model item because we have the Bind Property
            Coupon = await _db.Coupon.SingleOrDefaultAsync(m => m.Id == id);

            if (Coupon == null)
            {
                return NotFound();
            }
            return View(Coupon);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            Coupon = await _db.Coupon.SingleOrDefaultAsync(m => m.Id == id);

            if (Coupon == null)
            {
                return View();
            }

            _db.Coupon.Remove(Coupon);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
