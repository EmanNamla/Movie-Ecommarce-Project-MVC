using eTickets.DAL.Data.Enums;
using eTickets.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTickets.DAL.Models
{
    public class Movie:BaseEntity
    { 
        public string Name { get; set; }
        public string Description { get; set; }

        public double Price { get; set; }

        public double ImageURL { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public MovieCategory MovieCategory { get; set; }

        [ForeignKey("Cinema")]
        public int CinemaId { get; set; }

        public Cinema Cinema { get; set; }

        [ForeignKey("Producer")]
        public int ProducerId { get; set; }

        public Producer Producer { get; set; }

        public ICollection<Actors_Movies > Actors_Movies=new HashSet<Actors_Movies>();  


    }
}
