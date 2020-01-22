using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HairSalon.Models;
using System.Collections.Generic;
using System.Linq;
namespace HairSalon
{
    public class StylistsController:Controller
    {
        private readonly SalonContext _dataBase;
        public StylistsController(SalonContext dataBase)
        {
            _dataBase = dataBase;
        }
       
        [HttpGet]
        public ActionResult Create() // Create.cshtml view is called in order fill the form.
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Stylist new_stylist) // Add new stylist object into table.
        {
            _dataBase.Stylists.Add(new_stylist);
            _dataBase.SaveChanges();
            return RedirectToAction("Index");
        }
         [HttpGet]
        public ActionResult Index()
        {
            List<Stylist> list_Stylists = _dataBase.Stylists.ToList();
            return View(list_Stylists);
        }
        [HttpGet]
        public ActionResult Read(int readID)
        {
            Stylist stylist_for_displaying = _dataBase.Stylists.FirstOrDefault(iterateRowsOfStylistTable => iterateRowsOfStylistTable.StylistId==readID);
            stylist_for_displaying.Clients = _dataBase.Clients.Where(iterateRowsOfStylistTable => iterateRowsOfStylistTable.StylistId==readID).ToList();
            return View(stylist_for_displaying);
        }
        [HttpGet]
        public ActionResult Update(int updateID)
        {
            Stylist stylist_for_updating = _dataBase.Stylists.FirstOrDefault(iterateRowsOfStylistTable => iterateRowsOfStylistTable.StylistId==updateID);
            return View(stylist_for_updating);
        }
        [HttpPost]
        public ActionResult Update(Stylist new_stylist)
        {
            _dataBase.Entry(new_stylist).State = EntityState.Modified;
            _dataBase.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Delete(int deleteID)
        {
            Stylist stylist_For_Deletion = _dataBase.Stylists.FirstOrDefault(iterateRowsOfStylistTable => iterateRowsOfStylistTable.StylistId==deleteID);
            return View(stylist_For_Deletion);
        }
        [HttpPost,ActionName("Delete")]
        public ActionResult DeleteConfirmed(int deleteID)
        {
            Stylist stylist_For_Deletion = _dataBase.Stylists.FirstOrDefault(iterateRowsOfStylistTable => iterateRowsOfStylistTable.StylistId==deleteID);
            _dataBase.Remove(stylist_For_Deletion);
            _dataBase.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult SearchStylistByName()
        {
            return View();
        }
        [HttpPost("/stylists/SearchStylistByName")]
        public ActionResult SearchStylistByName(string givenName)
        {
            List<Stylist> fondStylist = _dataBase.Stylists.Where(iterateRowsOfStylistTable => iterateRowsOfStylistTable.StylistName==givenName).ToList();
            if(fondStylist==null)
            {
                return RedirectToAction("SearchStylistByName");
            }
            return View("ShowSearch",fondStylist);
            
        }
        [HttpGet]
        public ActionResult SearchStylistByNameAndSpecialty()
        {
            return View();

        }
        [HttpPost("/stylists/SearchStylistByNameAndSpecialty")]
        public ActionResult SearchStylistByNameAndSpecialty(string givenName,string givenSpecialty)
        {
             List<Stylist> fondStylist = _dataBase.Stylists.Where(iterateRowsOfStylistTable => (iterateRowsOfStylistTable.StylistName==givenName) && ( iterateRowsOfStylistTable.StylistSpecialty==givenSpecialty)).ToList();
            return View("ShowSearch",fondStylist);
        }
        

      
    }
}