﻿using FormInterviewProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace FormInterviewProject.Controllers

{
    
    public class PersonController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PersonController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Person
        public async Task<IActionResult> Index()
        {
            var people = await _context.Persons
                .Include(p => p.Country)
                .Include(p => p.State)
                .Include(p => p.City)
                .ToListAsync();

            return View(people);
        }

        public IActionResult CreatePerson()
        {
            ViewBag.Countries = new SelectList(_context.Countries, "CountryId", "Name");
            ViewBag.States = new SelectList(_context.States, "StateId", "Name");
            ViewBag.Cities = new SelectList(_context.Cities, "CityId", "Name");

            return View();
        }

<<<<<<< HEAD

=======
>>>>>>> 2cb3d58b5d25c7cfb09f1640dc3457860ecd17dc


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreatePerson(Person person)
        {
            if (ModelState.IsValid)
            {
                var selectedCountry = await _context.Countries.FindAsync(person.Country.CountryId);
                var selectedState = await _context.States.FindAsync(person.State.StateId);
                var selectedCity = await _context.Cities.FindAsync(person.City.CityId);

                person.Country = selectedCountry;
                person.State = selectedState;
                person.City = selectedCity;

                _context.Add(person);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Countries = new SelectList(_context.Countries, "CountryId", "Name", person.Country.CountryId);
            ViewBag.States = new SelectList(_context.States, "StateId", "Name", person.State.StateId);
            ViewBag.Cities = new SelectList(_context.Cities, "CityId", "Name", person.City.CityId);


            ViewBag.FirstName = person.FirstName;
            ViewBag.LastName = person.LastName;
            ViewBag.Gender = person.Gender;

            return View(person);
        }

        public async Task<IActionResult> EditPerson(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var person = await _context.Persons
                .Include(p => p.Country)
                .Include(p => p.State)
                .Include(p => p.City)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (person == null)
            {
                return NotFound();
            }

            ViewBag.Countries = new SelectList(_context.Countries, "CountryId", "Name");

            ViewBag.States = new SelectList(_context.States.Where(s => s.CountryId == person.Country.CountryId), "StateId", "Name");

            ViewBag.Cities = new SelectList(_context.Cities.Where(c => c.StateId == person.State.StateId), "CityId", "Name");

            return View(person);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPerson(int id, Person person)
        {
            if (id != person.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var selectedCountry = await _context.Countries.FindAsync(person.Country.CountryId);
                    var selectedState = await _context.States.FindAsync(person.State.StateId);
                    var selectedCity = await _context.Cities.FindAsync(person.City.CityId);

                    person.Country = selectedCountry;
                    person.State = selectedState;
                    person.City = selectedCity;

                    _context.Update(person);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PersonExists(person.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Countries = new SelectList(_context.Countries, "CountryId", "Name");

            ViewBag.States = new SelectList(_context.States.Where(s => s.CountryId == person.Country.CountryId), "StateId", "Name");

            ViewBag.Cities = new SelectList(_context.Cities.Where(c => c.StateId == person.State.StateId), "CityId", "Name");

            ViewBag.FirstName = person.FirstName;
            ViewBag.LastName = person.LastName;
            ViewBag.Gender = person.Gender;

            return View(person);
        }



        public async Task<IActionResult> DeletePerson(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var person = await _context.Persons
                .Include(p => p.Country)
                .Include(p => p.State)
                .Include(p => p.City)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (person == null)
            {
                return NotFound();
            }

            return View(person);
        }

        [HttpPost, ActionName("DeletePerson")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletePersonConfirmed(int id)
        {
            var person = await _context.Persons.FindAsync(id);
            _context.Persons.Remove(person);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PersonExists(int id)
        {
            return _context.Persons.Any(p => p.Id == id);
        }




        [HttpGet]
        public IActionResult GetStates(int countryId)
        {
            var states = _context.States
                .Where(s => s.CountryId == countryId)
                .Select(s => new { Id = s.StateId, Name = s.Name })
                .ToList();

            return Json(states);
        }


        [HttpGet]
        public IActionResult GetCities(int stateId)
        {
            var cities = _context.Cities
                .Where(c => c.StateId == stateId)
                .Select(c => new { Id = c.CityId, Name = c.Name })
                .ToList();

            return Json(cities);
        }

    }
}

