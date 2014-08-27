using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CountryCityApp.Models;

namespace CountryCityApp.Controllers
{
    public class CountryController : Controller
    {
        private CountryDbContext db = new CountryDbContext();

        // GET: /Country/
        public ActionResult Index(string sortOrder)
        {

            ViewBag.CountryId = new SelectList(db.Countries, "CountryId", "Name");

            ViewBag.NameSortParm = string.IsNullOrEmpty(sortOrder) ? "Name_desc" : "";
           
            var countries = db.Countries.Include(country => country.Cities);

            switch (sortOrder)
            {
                case "Name_desc":
                    countries = countries.OrderByDescending(c => c.Name);
                    break;
                default:
                    countries = countries.OrderBy(c => c.Name);
                    break;

            }
            return View(countries.ToList());
        }

        // GET: /Country/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Country country = db.Countries.Find(id);
            if (country == null)
            {
                return HttpNotFound();
            }
            return View(country);
        }

        // GET: /Country/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Country/Create
        [HttpPost]
        public ActionResult Create([Bind(Include="CountryId,Name,About")] Country country)
        {
            
            if (ModelState.IsValid)
            {
                db.Countries.Add(country);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(country);
        }

        //Searching Country data

        public ActionResult MySearch()
        {
            ViewBag.CountryId = new SelectList(db.Countries, "CountryId", "Name");
            List<Country> searchList = new List<Country>();

            return View(searchList);
            
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult MySearch(string SearchType, string CountryName, int? CountryId)
        {
            ViewBag.CountryId = new SelectList(db.Countries, "CountryId", "Name");
            var SearchCountries = new List<Country>();
            
            if (ModelState.IsValid)
            {
                if (SearchType == "ByCountry")
                {
                    SearchCountries = db.Countries.Where(c => c.Name.Contains(CountryName)).ToList();
                }
                else if (SearchType == "ByCountryId")
                {
                    SearchCountries = db.Countries.Where(c => c.CountryId==CountryId).ToList();
                }
                else
                {
                    return View("Index");
                }
            }
            return View(SearchCountries.ToList());

           
        }

        // GET: /Country/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Country country = db.Countries.Find(id);
            if (country == null)
            {
                return HttpNotFound();
            }
            return View(country);
        }

        // POST: /Country/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="CountryId,Name,About")] Country country)
        {
            if (ModelState.IsValid)
            {
                db.Entry(country).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(country);
        }

        // GET: /Country/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Country country = db.Countries.Find(id);
            if (country == null)
            {
                return HttpNotFound();
            }
            return View(country);
        }

        // POST: /Country/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Country country = db.Countries.Find(id);
            db.Countries.Remove(country);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
