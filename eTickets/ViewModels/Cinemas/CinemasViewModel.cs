using eTickets.DAL.Models;
using Microsoft.Build.Framework;

namespace eTickets.PL.ViewModels.Cinemas
{
    public class CinemasViewModel
    {
        public int Id { get; set; }
    
        public string Logo { get; set; }

        public string? Description { get; set; }

        public string Name { get; set; }

        public ICollection<Movie> Movies { get; set; } = new HashSet<Movie>();
    }
}
