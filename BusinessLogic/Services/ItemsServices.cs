using BusinessLogic.ViewModels;
using DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessLogic.Services
{
    //Item.cs (Domain) - is used to model/shape/generate/engineer the database
    //e.g. User.cs

    //CreateItemViewModel.cs (BusinessLogic)

    public class ItemsServices
    {

        //Constructor Injection
        //Dependency Injection is a design pattern which handles the creation of instances in a centralized location for better
        //efficiency

        private ItemsRepository ir;
        public ItemsServices(ItemsRepository _itemRepository) {
            ir = _itemRepository;
        }

        public void AddItem(CreateItemViewModel item)
        {
             
            if (ir.GetItems().Any(myItem => myItem.Name == item.Name))
                throw new Exception("Item with the same name already exists");
            else
            {
                ir.AddItem(new Domain.Models.Item()
                {
                    CategoryId = item.CategoryId, //AutoMapper
                    Description = item.Description,
                    Name = item.Name,
                    PhotoPath = item.PhotoPath,
                    Price = item.Price,
                    Stock = item.Stock
                });
            }
        }

        public void DeleteItem(int id)
        {  
        }

        public void Checkout()
        {
             
        }

        //it is not recommended that you use the Domain Models as a return type
        //in other words, do not use the classes that model the database to transfer data into the presentation layer
        public IQueryable<ItemViewModel> GetItems()
        {
            //to be explained next time!!
            //including why did we use IQueryable

            var list = from i in ir.GetItems()
                       select new ItemViewModel()
                       {
                           Id = i.Id,
                           Category = i.Category.Title,
                           Description = i.Description,
                           Name = i.Name,
                           PhotoPath = i.PhotoPath,
                           Price = i.Price,
                           Stock = i.Stock
                       };
            return list;

        }

       
    }
}
