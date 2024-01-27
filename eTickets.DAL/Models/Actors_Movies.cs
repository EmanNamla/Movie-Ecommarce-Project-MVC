using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTickets.DAL.Models
{
    public class Actors_Movies
    {
        [ForeignKey("Actor")]
        public int ActorId { get; set; }
        [ForeignKey("Movie")]
        public int MovieId { get; set; }

        public Actor? Actor { get; set; }

        public Movie? Movie { get; set; }
    }
}
