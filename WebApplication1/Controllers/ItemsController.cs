using BusinessLogic.Services;
using BusinessLogic.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Controllers
{
    //are going to handle the incoming requests and outgoing responses
    public class ItemsController : Controller
    {
        private ItemsServices itemsService;
        private IWebHostEnvironment host;
        public ItemsController(ItemsServices _itemsService, IWebHostEnvironment _host)
        { itemsService = _itemsService;
            host = _host;
        }

        //a method to open the page, then the user starts typing
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        //a method to handle the submission of the form
        [HttpPost]
        public IActionResult Create(CreateItemViewModel data, IFormFile file)
        { //.....
            try
            {
                if(file != null)
                {
                    //1 change filename
                    string uniqueFilename = Guid.NewGuid().ToString() + System.IO.Path.GetExtension(file.FileName);
                    
                    //2. i need the absolute path of the folder where the image is going....
                    //e.g. C:\Users\attar\Source\Repos\SWD62AEP2022v1\WebApplication1\wwwroot\Images\

                    string absolutePath = host.WebRootPath ;

                    //3. saving file
                    using (FileStream fsOut = new FileStream(absolutePath + "\\Images\\" + uniqueFilename, FileMode.CreateNew))
                    {
                        file.CopyTo(fsOut);
                    }

                    //4. save the path to the image in the database
                    //http://localhost:xxxx/Images/filename.jpg
                    data.PhotoPath = "/Images/" + uniqueFilename;
                }


                itemsService.AddItem(data); //to test
                //dynamic object - it builds the declared properties on-the-fly i.e. the moment you declare the property
                //"Message" it builds in realtime in memory
                ViewBag.Message = "Item successfully inserted in database";

            }
            catch(Exception ex)
            {
                ViewBag.Error = "Item wasn't inserted successfully. Please check your inputs";
            }
            return View();
        }

        public IActionResult List()
        {
            var list = itemsService.GetItems();
            return View(list);
        }

        public IActionResult Details(int id)
        {
            var myItem = itemsService.GetItem(id);
            return View(myItem);
        }
    }
}
