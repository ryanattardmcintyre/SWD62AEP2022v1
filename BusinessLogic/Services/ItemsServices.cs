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
            var item = ir.GetItem(id);
            if(item != null)
                ir.DeleteItem(item);
        }

        public void Checkout()
        {
             
        }

        //it is not recommended that you use the Domain Models as a return type
        //in other words, do not use the classes that model the database to transfer data into the presentation layer
        public IQueryable<ItemViewModel> GetItems()
        {
            //Linq
            //sql may be complicated vis-a-vis linq e.g. you need inner joins
            //linq is more C# like rather than having to learn a completely new language
            //linq is compiled therefore the compile will point out any errors for you

            //note: i am wrapping item info into List<ItemViewModel> because the ir.GetItems() returns List<Item>
            var list = from i in ir.GetItems() //flatten this into 1 line using AutoMapper
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

        public ItemViewModel GetItem(int id)
        {
            return GetItems().SingleOrDefault(x => x.Id == id);
        }


        public IQueryable<ItemViewModel> Search(string keyword)
        {
            return GetItems().Where(x => x.Name.Contains(keyword)); // Like %%
        }

        //note: List vs IQueryable
        //List is more inefficient
        //1st call result would have been that 1000 items fetched and loaded into the server's memory
        //2nd call result would have implemented a filter on those 1000 items and result would have been also loaded into memory
        //3rd call result would have implemented a filter on those 500 items and result would have been also loaded into memory

        //with Iqueryable:
        //1st call result would have been a preperation of a LINQ query and query would have been placed in memory
        //2nd call result would have been an amendment  to the first LINQ query to include the Where clause
        //3rd call result would have been a further amendment to the same linq query to include an additional where clause
        //not yet executed
        //When does the execution take place? Answer: the execution happens once only when you convert IQueryable to List
        //                                            (or that happens) the moment you pass data to the View
        //iqueryable makes the prepared LINQ statement run and therefore filters the data within the database

        public IQueryable<ItemViewModel> Search(string keyword, double minPrice, double maxPrice)
        {
            return Search(keyword).Where(x => x.Price >= minPrice && x.Price <= maxPrice);
        }

       
    }
}
