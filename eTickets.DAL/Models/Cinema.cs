namespace eTickets.DAL.Models
{
    public class Cinema:BaseEntity
    {
        public string Logo { get; set; }

        public string Description { get; set; }

        public string Name { get; set; }

        public ICollection<Movie> Movies { get; set; }=new HashSet<Movie>();
    }
}
