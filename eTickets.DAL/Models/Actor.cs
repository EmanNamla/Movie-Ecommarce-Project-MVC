namespace eTickets.DAL.Models
{
    public class Actor:BaseEntity
    {
        public string FullName { get; set; }

        public string ProfilePictureUrl { get; set; }
      
        public string Bio { get; set; }

        public ICollection<Actors_Movies> Actors_Movies { get; set; } = new HashSet<Actors_Movies>();
    }
}
