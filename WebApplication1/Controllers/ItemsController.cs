using BusinessLogic.Services;
using BusinessLogic.ViewModels;
using Microsoft.AspNetCore.Authorization;
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
        private CategoriesServices categoriesService;
        private IWebHostEnvironment host;
        public ItemsController(ItemsServices _itemsService, IWebHostEnvironment _host, CategoriesServices _categoriesService)
        { itemsService = _itemsService;
            host = _host;
            categoriesService = _categoriesService;
        }

        //a method to open the page, then the user starts typing
        [HttpGet][Authorize]
        public IActionResult Create()
        {
            var categories = categoriesService.GetCategories();
            CreateItemViewModel myModel = new CreateItemViewModel();
            myModel.Categories = categories.ToList();

            return View(myModel);
        }

        //a method to handle the submission of the form
        [HttpPost]
        [Authorize]
        public IActionResult Create(CreateItemViewModel data, IFormFile file)
        { //.....
            try
            {
                if (ModelState.IsValid)
                {

                    //check that the category exists in the db

                    //if not
                  //  ModelState.AddModelError("CategoryId", "Category is not valid");
                 //   return View(data);


                    string username = User.Identity.Name; //gives you the email/username of the currently logged in user

                    if (file != null)
                    {
                        //1 change filename
                        string uniqueFilename = Guid.NewGuid().ToString() + System.IO.Path.GetExtension(file.FileName);

                        //2. i need the absolute path of the folder where the image is going....
                        //e.g. C:\Users\attar\Source\Repos\SWD62AEP2022v1\WebApplication1\wwwroot\Images\

                        string absolutePath = host.WebRootPath;

                        //3. saving file
                        using (FileStream fsOut = new FileStream(absolutePath + "\\Images\\" + uniqueFilename, FileMode.CreateNew))
                        {
                            file.CopyTo(fsOut);
                        }

                        //4. save the path to the image in the database
                        //http://localhost:xxxx/Images/filename.jpg
                        data.PhotoPath = "/Images/" + uniqueFilename;
                    }

                    //data.Author = username
                    itemsService.AddItem(data); //to test
                                                //dynamic object - it builds the declared properties on-the-fly i.e. the moment you declare the property
                                                //"Message" it builds in realtime in memory
                    ViewBag.Message = "Item successfully inserted in database";
                }
               

            }
            catch(Exception ex)
            {
                ViewBag.Error = "Item wasn't inserted successfully. Please check your inputs";
            }

            var categories = categoriesService.GetCategories();
            data.Categories = categories.ToList();
            return View(data);
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

        [HttpPost]
        public IActionResult Search(string keyword)
        {
            if (string.IsNullOrEmpty(keyword))
                return RedirectToAction("List");
            else
            {
                var list = itemsService.Search(keyword);
                return View("List", list);
            }
        }

        [Authorize]
        public IActionResult Delete(int id)
        {
            try
            {
                itemsService.DeleteItem(id);
                //ViewBag will not work here because Viewbag is lost when there is a redirection
                //TempData survives redirections (up to 1 redirection)
                TempData["message"] = "Item has been deleted";

            }
            catch (Exception ex)
            {
                TempData["error"] = "Item has not been deleted";
            }
            return RedirectToAction("List");
        }


        [HttpGet]
        public IActionResult Edit(int id)
        {
            var originalItem = itemsService.GetItem(id);
            var categories = categoriesService.GetCategories();

            CreateItemViewModel model = new CreateItemViewModel();
            model.Categories = categories.ToList();
            model.Name = originalItem.Name;
            model.CategoryId = categories.SingleOrDefault(x => x.Title == originalItem.Category).Id;
            model.Description = originalItem.Description;
            model.PhotoPath = originalItem.PhotoPath;
            model.Stock = originalItem.Stock;
            model.Price = originalItem.Price;
            
            return View(model);
        }

        public IActionResult Edit(int id, CreateItemViewModel data, IFormFile file)
        {
            try
            {
                var oldItem = itemsService.GetItem(id);
                if (ModelState.IsValid)
                {
                 //   string username = User.Identity.Name; //gives you the email/username of the currently logged in user

                    if (file != null)
                    {
                        //1 change filename
                        string uniqueFilename = Guid.NewGuid().ToString() + System.IO.Path.GetExtension(file.FileName);

                        //2. i need the absolute path of the folder where the image is going....
                        //e.g. C:\Users\attar\Source\Repos\SWD62AEP2022v1\WebApplication1\wwwroot\Images\
                        string absolutePath = host.WebRootPath;

                        //3. saving file
                        using (FileStream fsOut = new FileStream(absolutePath + "\\Images\\" + uniqueFilename, FileMode.CreateNew))
                        {
                            file.CopyTo(fsOut);
                        }

                        //4. save the path to the image in the database
                        //http://localhost:xxxx/Images/filename.jpg
                        data.PhotoPath = "/Images/" + uniqueFilename;

                        //delete the old physical file (image)
                   
                        string absolutePathOfOldImage = host.WebRootPath + "\\Images\\" + Path.GetFileName(oldItem.PhotoPath);
                        if (System.IO.File.Exists(absolutePathOfOldImage) == true)
                        {
                            System.IO.File.Delete(absolutePathOfOldImage);
                        }
                    }
                    else
                    {
                        data.PhotoPath = oldItem.PhotoPath;
                    }
 
                    itemsService.EditItem(id, data);  
                                                
                    ViewBag.Message = "Item updated successfully";
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Item wasn't updated successfully. Please check your inputs";
            }

            var categories = categoriesService.GetCategories();
            data.Categories = categories.ToList();
            return View(data);
        }
    }
}
