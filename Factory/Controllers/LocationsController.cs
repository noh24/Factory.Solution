using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Factory.Models;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Factory.Controllers
{
  public class LocationsController : Controller
  {
    private readonly FactoryContext _db;

    public LocationsController(FactoryContext db)
    {
      _db = db;
    }

    public ActionResult Index()
    {
      List<Location> model = _db.Locations.ToList();
      return View(model);
    }

    public ActionResult Create()
    {
      return View();
    }
    [HttpPost]
    public ActionResult Create(Location location)
    {
      if (!ModelState.IsValid)
      {
        return View(location);
      }
      _db.Locations.Add(location);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Details(int id)
    {
      Location thisLocation = _db.Locations
        .Include(location => location.Engineers)
          .ThenInclude(engineer => engineer.EngineerMachines)
          .ThenInclude(engineerMachine => engineerMachine.Machine)
        .Include(location => location.Machines)
          .ThenInclude(machine => machine.EngineerMachines)
          .ThenInclude(engineerMachine => engineerMachine.Engineer)
        .FirstOrDefault(location => location.LocationId == id);
      return View(thisLocation);
    }

    public ActionResult Edit(int id)
    {
      Location thisLocation = _db.Locations.FirstOrDefault(location => location.LocationId == id);
      return View(thisLocation);
    }
    [HttpPost]
    public ActionResult Edit(Location location)
    {
      _db.Locations.Update(location);
      _db.SaveChanges();
      return RedirectToAction("Details", new { id = location.LocationId});
    }
    [HttpPost]
    public ActionResult Delete(int id)
    {
      Location thisLocation = _db.Locations.FirstOrDefault(location => location.LocationId == id);
      _db.Locations.Remove(thisLocation);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
  }
}