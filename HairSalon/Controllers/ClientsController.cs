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
    }
}