using AutoFixture;

using AutoMapper;

using Education.Application.Helper;
using Education.Domain;
using Education.Persistence;

using MediatR;

using Microsoft.EntityFrameworkCore;

using NUnit.Framework;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Education.Application.Cursos
{
    public class CreateCursoCommandNUnitTests
    {
        private CreateCursoCommand.CreateCursoCommandHandler handlerCursoCreate;

        [SetUp]
        public void Setup()
        {
            // Permite crear data de prueba
            var fixture = new Fixture();

            //Data de prueba
            var cursoRecords = fixture.CreateMany<Curso>().ToList();

            // agrega un registro con id vacio
            cursoRecords.Add(fixture.Build<Curso>().With(tr => tr.CursoId, Guid.Empty).Create());

            // creamos el fake options para el dbContext
            var options = new DbContextOptionsBuilder<EducationDbContext>()
                .UseInMemoryDatabase(databaseName: $"EducationDbContext-{Guid.NewGuid}").Options;

            // creamos el fake dbContext
            var educationDbContextFake = new EducationDbContext(options);

            // agregamos la data falsa
            educationDbContextFake.Curso.AddRange(cursoRecords);

            // Emulacion del Mapper
            var mapConfing = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingTest());
            });
            var mapper = mapConfing.CreateMapper();

            //instanciamos el objeto GetCursoQuery.GetCursoQueryHandler
            handlerCursoCreate = new CreateCursoCommand.CreateCursoCommandHandler(educationDbContextFake);
        }

        [Test]
        public async Task CreateCursoCommand_InputCurso_ReturnsNumber()
        {
            CreateCursoCommand.CreateCursoCommandRequest request = new();
            request.FechaPublicacion = DateTime.UtcNow.AddDays(59);
            request.Titulo = "Libro de pruebas automaticas en .NET";
            request.Descripcion = "Aprendee a crear Unit Test desde Cero";
            request.Precio = 99;

            var resultados = await handlerCursoCreate.Handle(request, new System.Threading.CancellationToken());

            Assert.That(resultados, Is.EqualTo(Unit.Value));
        }
    }
}
