using eTickets.DAL.Data.Enums;
using eTickets.DAL.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace eTickets.PL.ViewModels.Movies
{
    public class MovieViewModel
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public double Price { get; set; }

        public string ImageURL { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public MovieCategory MovieCategory { get; set; }

        [ForeignKey("Cinema")]
        public int CinemaId { get; set; }

        public Cinema Cinema { get; set; }

        [ForeignKey("Producer")]
        public int ProducerId { get; set; }

        public Producer Producer { get; set; }

        public ICollection<Actors_Movies> Actors_Movies = new HashSet<Actors_Movies>();
    }
}
