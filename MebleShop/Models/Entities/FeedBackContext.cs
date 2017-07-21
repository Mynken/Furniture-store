using MebleShop.Models.Entities.OurServices;
using MebleShop.Models.Entities.OurWorks;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MebleShop.Models.Entities
{
    public class FeedBackContext : DbContext
    {
        public DbSet<Feedback> Feedbacks { get; set; }
        public DbSet<Work> Works { get; set; }
        public DbSet<FileWorkDetail> FileWorkDetails { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<FileServiceDetail> FileServiceDetails { get; set; }
    }
}