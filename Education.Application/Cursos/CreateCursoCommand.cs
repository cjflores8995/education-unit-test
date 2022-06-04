using Education.Domain;
using Education.Persistence;

using FluentValidation;

using MediatR;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Education.Application.Cursos
{
    public class CreateCursoCommand
    {
        public class CreateCursoCommandRequest: IRequest
        {
            public string Titulo { get; set; }

            public string Descripcion { get; set; }

            public DateTime FechaPublicacion { get; set; }

            public Decimal Precio { get; set; }
        }

        public class CreateCursoCommandRequestValidation: AbstractValidator<CreateCursoCommandRequest>
        {
            public CreateCursoCommandRequestValidation()
            {
                RuleFor(x => x.Titulo).NotEmpty();
                RuleFor(x => x.Descripcion).NotEmpty();
            }
        }

        public class CreateCursoCommandHandler : IRequestHandler<CreateCursoCommandRequest>
        {
            private readonly EducationDbContext _context;
            public CreateCursoCommandHandler(EducationDbContext context)
            {
                _context = context;
            }
            public async Task<Unit> Handle(CreateCursoCommandRequest request, CancellationToken cancellationToken)
            {
                var curso = new Curso
                {
                    CursoId = Guid.NewGuid(),
                    Titulo = request.Titulo,
                    Descripcion = request.Descripcion,
                    FechaPublicacion = request.FechaPublicacion,
                    FechaCreacion = DateTime.UtcNow,
                    Precio = request.Precio
                };

                _context.Add(curso);
                var resultado = await _context.SaveChangesAsync();

                if(resultado > 0)
                    return Unit.Value;

                throw new Exception("No se pudo agregar el curso");
            }
        }
    }
}
