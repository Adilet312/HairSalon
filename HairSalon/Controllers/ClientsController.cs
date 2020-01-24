using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HairSalon.Models;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
namespace HairSalon
{
    public class ClientsController:Controller
    {
        private readonly SalonContext _dataBase;
        public ClientsController(SalonContext dataBase)
        {
            _dataBase = dataBase;
        }
        [HttpGet]
        public ActionResult Index()
        {
            List<Client> clients = _dataBase.Clients.Include(rowOfClients => rowOfClients.Stylist).ToList();
            return View(clients);
        }
        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.StylistId = new SelectList(_dataBase.Stylists,"StylistId","StylistName");
            return View();
        }
        [HttpPost]
        public ActionResult Create(Client new_client)
        {
            _dataBase.Add(new_client);
            _dataBase.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Read(int readID)
        {
            Client displayingClient = _dataBase.Clients.FirstOrDefault(rowOfClients => rowOfClients.ClientId==readID);
            return View(displayingClient);
        }
        [HttpGet]
        public ActionResult Update(int updateID)
        {
            ViewBag.StylistId = new SelectList(_dataBase.Stylists,"StylistId","StylistName");
            Client updatingClient = _dataBase.Clients.FirstOrDefault(rowOfClients => rowOfClients.ClientId==updateID);
            return View(updatingClient);
        }
        [HttpPost]
        public ActionResult Update(Client new_client)
        {
            _dataBase.Entry(new_client).State = EntityState.Modified;
            _dataBase.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Delete(int deleteID)
        {
            Client client_for_deleting = _dataBase.Clients.FirstOrDefault(rowOfClients => rowOfClients.ClientId==deleteID);
            return View(client_for_deleting);
        }
        [HttpPost,ActionName("Delete")]
        public ActionResult DeleteClient(int deleteID)
        {
            Client deletingClient = _dataBase.Clients.FirstOrDefault(rowOfClients => rowOfClients.ClientId==deleteID);
            _dataBase.Remove(deletingClient);
            _dataBase.SaveChanges();
            return RedirectToAction("Index");

        }
        [HttpGet]
        public ActionResult SearchClientByName()
        {
            return View();
        }
        [HttpPost("/clients/SearchClientByName")]
        public ActionResult SearchClientByName(string givenName)
        {
            
            Client foundClients = _dataBase.Clients.Where(rows => rows.ClientName==givenName).FirstOrDefault<Client>();
            return View("ShowSearch",foundClients);
        }
        
    }
}