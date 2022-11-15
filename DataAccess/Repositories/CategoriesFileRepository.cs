using Domain.Interfaces;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace DataAccess.Repositories
{
    //this repository class will read the Categories from
    //a file (And not from a database)
    public class CategoriesFileRepository : ICategoriesRepository
    {
        //FileInfo is a built in class representing a file
        private FileInfo fi;
        public CategoriesFileRepository(FileInfo _fi)
        {
            fi = _fi;
        }
        public IQueryable<Category> GetCategories()
        {
            //StreamReader is another built-in  class which
            //facilitates the reading of text from a file
            List<Category> categories = new List<Category>();
            string line = "";
            using(StreamReader sr = fi.OpenText())
            {
                //sr.Peek return next index of what's to be read next
                //meaning that if it retuns -1: there's nothing more to read
               while(sr.Peek() != -1)
                {
                    line = sr.ReadLine();
                    categories.Add(new Category()
                    {
                        Id = Convert.ToInt32(line.Split(';')[0]),
                        Title = (line.Split(';')[1]).ToString()
                    });
                }
            }
            return categories.AsQueryable();

        }
    }
}
