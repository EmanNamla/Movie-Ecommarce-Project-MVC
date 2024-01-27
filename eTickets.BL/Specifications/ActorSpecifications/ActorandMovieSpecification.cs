using eTickets.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTickets.BL.Specifications.ActorSpecifications
{
    public class ActorandMovieSpecification:BaseSpecifications<Actor>
    {
        public ActorandMovieSpecification(int id) :base(m=>m.Id==id)
        {
            Includes.Add(a => a.Actors_Movies);
        }
    }
}
