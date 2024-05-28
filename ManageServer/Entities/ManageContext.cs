using Microsoft.EntityFrameworkCore;

namespace ManageServer.Entities
{
    public class ManageContext : DbContext
    {
        public ManageContext()
        {
        }

        public ManageContext(DbContextOptions<ManageContext> options)
           : base(options)
        {
        }
        public virtual DbSet<Account> Accounts { get; set; } = null!;
        public virtual DbSet<Home> Homes { get; set; } = null!;
        public virtual DbSet<Project> Projects { get; set; } = null!;
        public virtual DbSet<Tag> Tags { get; set; } = null!;

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    if (!optionsBuilder.IsConfigured)
        //    {
        //        //optionsBuilder.UseSqlServer("Data Source=ES-TRUNGTD18\\SQLEXPRESS01;Initial Catalog=ManageImage;Integrated Security=True");
        //        //optionsBuilder.Use("Server=localhost;User ID=root;Password=trung123Aa;Database=ManageImage");
        //    }
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>(entity =>
            {
                entity.ToTable("Account");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.FirstName).HasMaxLength(256).IsRequired();
                entity.Property(e => e.LastName).HasMaxLength(256).IsRequired();
                entity.Property(e => e.Email).HasMaxLength(256).IsRequired();
                entity.Property(e => e.Password).HasMaxLength(256).IsRequired();

            });


            modelBuilder.Entity<Home>(entity =>
            {
                entity.ToTable("Home");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Title).IsRequired();

            });

            modelBuilder.Entity<Project>(entity =>
            {
                entity.ToTable("Project");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired();
                entity.Property(e => e.TagId).HasColumnName("tagId");
                entity.HasOne(p => p.Tag).WithMany(t => t.Projects).HasForeignKey(p => p.TagId).HasConstraintName("FK_Project_Tag");

            });

            modelBuilder.Entity<Tag>(entity =>
            {
               entity.ToTable("Tag");
               entity.HasKey(e => e.Id);
               entity.Property(e => e.Name).IsRequired();

            });
        }
    }
}
