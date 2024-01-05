using eTickets.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTickets.BL.Specifications.MovieSpecifications
{
    public class MovieAndCinemaSpecification:BaseSpecifications<Movie>
    {
        public MovieAndCinemaSpecification():base()
        {
            Includes.Add(m => m.Cinema);
        }

    }
}
