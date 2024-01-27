using eTickets.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTickets.BL.Specifications.MovieSpecifications
{
    public class MovieandCinemaandActorsandProducerSpecifications : BaseSpecifications<Movie>
    {
        public MovieandCinemaandActorsandProducerSpecifications():base()
        {
            Includes.Add(m => m.Cinema);
            Includes.Add(m => m.Producer);
            Includes.Add(m => m.Actors_Movies);
            OrderByAsyn(m => m.Name);
        }

        public MovieandCinemaandActorsandProducerSpecifications(int id) : base(m => m.Id == id)
        {
            Includes.Add(m => m.Cinema);
            Includes.Add(m => m.Producer);
            Includes.Add(m => m.Actors_Movies);
         


        }
    
    }
}
