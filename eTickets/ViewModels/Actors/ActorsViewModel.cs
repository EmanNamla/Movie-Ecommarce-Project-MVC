using System.ComponentModel.DataAnnotations;

namespace eTickets.PL.ViewModels.Actors
{
    public class ActorsViewModel
    {
        public int Id { get; set; }
        public string FullName { get; set; }

        public string ProfilePictureUrl { get; set; }

        public string Bio { get; set; }
    }
}
