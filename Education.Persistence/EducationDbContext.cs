using Education.Domain;

using Microsoft.EntityFrameworkCore;

namespace Education.Persistence
{
    public class EducationDbContext: DbContext
    {
        public EducationDbContext() { }

        public EducationDbContext(DbContextOptions<EducationDbContext> options): base(options)
        { }

        public DbSet<Curso> Curso { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            if (!options.IsConfigured)
            {
                options.UseSqlServer("Server=DESKTOP-GRPRQTH;database=Education;Trusted_Connection=True;MultipleActiveResultSets=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Curso>().Property(c => c.Precio).HasPrecision(14,2);

            modelBuilder.Entity<Curso>().HasData(
                    new Curso
                    {
                        CursoId = Guid.NewGuid(),
                        Titulo = "C# desde cero hasta avanzado",
                        Descripcion = "Curso de C# básico",
                        FechaCreacion = DateTime.Now,
                        FechaPublicacion = DateTime.Now.AddYears(2),
                        Precio = 56
                    }, 
                    new Curso
                    {
                        CursoId = Guid.NewGuid(),
                        Titulo = "Curso de Java",
                        Descripcion = "Master en Java Spring desde las raices",
                        FechaCreacion = DateTime.Now,
                        FechaPublicacion = DateTime.Now.AddYears(2),
                        Precio = 25
                    },
                    new Curso
                    {
                        CursoId = Guid.NewGuid(),
                        Titulo = "Curso de Unit Test para NET Core",
                        Descripcion = "Master en Unit Test con CQRS",
                        FechaCreacion = DateTime.Now,
                        FechaPublicacion = DateTime.Now.AddYears(2),
                        Precio = 1000
                    }
                );
        }

    }
}