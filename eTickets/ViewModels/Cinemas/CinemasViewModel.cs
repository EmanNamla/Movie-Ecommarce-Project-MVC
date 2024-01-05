using eTickets.DAL.Models;

namespace eTickets.PL.ViewModels.Cinemas
{
    public class CinemasViewModel
    {
        public string Logo { get; set; }

        public string Description { get; set; }

        public string Name { get; set; }

        public ICollection<Movie> Movies { get; set; } = new HashSet<Movie>();
    }
}
