using System.ComponentModel.DataAnnotations;

namespace eTickets.PL.ViewModels.Actors
{
    public class ActorsViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "FullName is Required")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "ProfilePictureUrl is Required")]
        public string ProfilePictureUrl { get; set; }

        [Required(ErrorMessage = "biography is Required")]
        public string Bio { get; set; }
    }
}
