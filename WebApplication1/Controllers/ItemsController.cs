using BusinessLogic.Services;
using BusinessLogic.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Controllers
{
    //are going to handle the incoming requests and outgoing responses
    public class ItemsController : Controller
    {
        private ItemsServices itemsService;
        public ItemsController(ItemsServices _itemsService)
        { itemsService = _itemsService; }

        //a method to open the page, then the user starts typing
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        //a method to handle the submission of the form
        [HttpPost]
        public IActionResult Create(CreateItemViewModel data)
        { //.....
            itemsService.AddItem(data); //to test

            return View();
        }
    }
}
