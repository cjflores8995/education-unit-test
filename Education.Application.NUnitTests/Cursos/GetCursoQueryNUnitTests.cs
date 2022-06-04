using AutoFixture;

using AutoMapper;

using Education.Application.Helper;
using Education.Domain;
using Education.Persistence;

using Microsoft.EntityFrameworkCore;

using NUnit.Framework;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Education.Application.Cursos
{
    [TestFixture]
    public class GetCursoQueryNUnitTests
    {
        private GetCursoQuery.GetCursoQueryHandler handlerAllCursos;

        [SetUp]
        public void Setup()
        {
            // Permite crear data de prueba
            var fixture = new Fixture();

            //Data de prueba
            var cursoRecords = fixture.CreateMany<Curso>().ToList();

            // agrega un registro con id vacio
            cursoRecords.Add(
                fixture.Build<Curso>()
                .With(tr => tr.CursoId, Guid.Empty)
                .Create()
            );

            // creamos el fake options para el dbContext
            var options = new DbContextOptionsBuilder<EducationDbContext>()
                .UseInMemoryDatabase(databaseName: $"EducationDbContext-{Guid.NewGuid}")
                .Options;

            // creamos el fake dbContext
            var educationDbContextFake = new EducationDbContext(options);

            // agregamos la data falsa
            educationDbContextFake.Curso.AddRange(cursoRecords);
            educationDbContextFake.SaveChanges();

            // Emulacion del Mapper
            var mapConfing = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingTest());
            });
            var mapper = mapConfing.CreateMapper();

            //instanciamos el objeto GetCursoQuery.GetCursoQueryHandler
            handlerAllCursos = new GetCursoQuery.GetCursoQueryHandler(educationDbContextFake, mapper);
        }


        [Test]
        public async Task GetCursoQueryHandler_ConsultaCursos_ReturnsTrue()
        {
            GetCursoQuery.GetCursoQueryRequest request = new();
            var resultados = await handlerAllCursos.Handle(request, new System.Threading.CancellationToken());

            Assert.IsNotNull(resultados);
        }

    }
}
