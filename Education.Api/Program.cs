using Education.Application.Cursos;
using Education.Persistence;

using FluentValidation.AspNetCore;

using MediatR;

using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<EducationDbContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddMediatR(typeof(GetCursoQuery.GetCursoQueryHandler).Assembly);

builder.Services.AddAutoMapper(typeof(GetCursoByIdQuery.GetCursoByIdQueryHandler));

builder.Services.AddControllers()
    .AddFluentValidation(cfg => cfg.RegisterValidatorsFromAssemblyContaining<CreateCursoCommand>());

builder.Services.AddCors(x => x.AddPolicy("corsApp", builder =>
{
    builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
}));

var app = builder.Build();
app.UseCors("corsApp");

// Configure the HTTP request pipeline.

app.UseAuthorization();

app.MapControllers();

app.Run();
