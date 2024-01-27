using eTickets.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTickets.BL.Specifications.MovieSpecifications
{
    public class MovieFilterByName:BaseSpecifications<Movie>
    {
        public MovieFilterByName(string MovieNameorDesc):base(m=>m.Name== MovieNameorDesc || m.Description== MovieNameorDesc)
        {

        }
    }
}
