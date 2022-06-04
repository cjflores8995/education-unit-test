using AutoMapper;

using Education.Application.DTO;
using Education.Domain;
using Education.Persistence;

using MediatR;

using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Education.Application.Cursos
{
    public class GetCursoByIdQuery
    {
        public class GetCursoByIdQueryRequest : IRequest<CursoDTO> { 
            public Guid Id { get; set; }
        }

        public class GetCursoByIdQueryHandler : IRequestHandler<GetCursoByIdQueryRequest, CursoDTO>
        {
            private readonly EducationDbContext _context;
            private readonly IMapper _mapper;
            public GetCursoByIdQueryHandler(EducationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<CursoDTO> Handle(GetCursoByIdQueryRequest request, CancellationToken cancellationToken)
            {
                var curso = await _context.Curso.FirstOrDefaultAsync(x => x.CursoId == request.Id);

                var cursoDTO = _mapper.Map<Curso, CursoDTO>(curso);

                return cursoDTO;

            }
        }
    }
}
