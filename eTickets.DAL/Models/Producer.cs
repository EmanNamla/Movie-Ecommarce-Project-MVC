﻿namespace eTickets.DAL.Models
{
    public class Producer:BaseEntity
    {
        public string FullName { get; set; }

        public string ProfilePictureUrl { get; set; }

        public string Bio { get; set; }

        public ICollection<Movie> Movies { get; set; }=new HashSet<Movie>();
    }
}
