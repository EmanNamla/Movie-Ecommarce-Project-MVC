using eTickets.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTickets.BL.Specifications.Actors_MoviesSpecifications
{
    public class ActorsMoviesSpecifications:BaseSpecifications<Actors_Movies>
    {
        public ActorsMoviesSpecifications(int id):base(am=>am.MovieId==id)
        {

        }
    }
}
