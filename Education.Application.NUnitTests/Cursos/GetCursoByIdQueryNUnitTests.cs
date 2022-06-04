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
    public class GetCursoByIdQueryNUnitTests
    {
        private GetCursoByIdQuery.GetCursoByIdQueryHandler handlerCursoById;
        private Guid CursoIdTest;

        [SetUp]
        public void Setup()
        {
            CursoIdTest = new Guid("8d94a11f-961d-4e20-91e4-f0ac0fa1f83b");
            // Permite crear data de prueba
            var fixture = new Fixture();

            //Data de prueba
            var cursoRecords = fixture.CreateMany<Curso>().ToList();

            // agrega un registro con id vacio
            cursoRecords.Add(
                fixture.Build<Curso>()
                .With(tr => tr.CursoId, CursoIdTest)
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
            handlerCursoById = new GetCursoByIdQuery.GetCursoByIdQueryHandler(educationDbContextFake, mapper);
        }


        [Test]
        public async Task GetCursoByIdQueryHandler_InputCursoId_ReturnsNotNull()
        {
            GetCursoByIdQuery.GetCursoByIdQueryRequest request = new()
            {
                Id = CursoIdTest
            };
            var resultados = await handlerCursoById.Handle(request, new System.Threading.CancellationToken());

            Assert.IsNotNull(resultados);
        }
    }
}
