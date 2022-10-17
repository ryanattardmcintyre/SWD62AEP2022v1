using DataAccess.Context;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccess.Repositories
{
    public class ItemsRepository
    {
        //Dependency Injection: it centralizes the creation of instances to manage efficiently these
        //                      inside memory

        private ShoppingCartContext context { get; set; }
        
        //Constructor injection - we are shifting creation of instances such as ShoppingCartContext to 
        //                        a more centralized place i.e. Startup.cs

        //declaring by using constructor injection that the ItemsRepository when consumed
        //it must be given an instance of ShoppingCartContext
        public ItemsRepository(ShoppingCartContext _context)
        {
            context = _context;
        }
        //in the dataaccess we code methods that directly add/read data to/from the database
        public IQueryable<Item> GetItems()
        {
            return context.Items;
        }

        public void AddItem(Item i) 
        {
            context.Items.Add(i);
            context.SaveChanges();
        }

        public void DeleteItem(Item i)
        {
            context.Items.Remove(i);
            context.SaveChanges();
               
        }
    }
}
