using eTickets.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTickets.DAL.Data.Configurations
{
    public class Actors_MoviesConfig : IEntityTypeConfiguration<Actors_Movies>
    {
        public void Configure(EntityTypeBuilder<Actors_Movies> builder)
        {
            builder.HasKey(Am=> new {Am.MoviesId,Am.ActorId});
            builder.HasOne(Am => Am.Actor).WithMany(Am=>Am.Actors_Movies).HasForeignKey(Am=>Am.ActorId);
            builder.HasOne(Am => Am.Movie).WithMany(Am => Am.Actors_Movies).HasForeignKey(Am => Am.MoviesId);
        }
    }
}
