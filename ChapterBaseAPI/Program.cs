using ChapterBaseAPI.Repositories;
using ChapterBaseAPI.Services;
using ChapterBaseAPI.Data;
using admin_bff.Exceptions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Register services
builder.Services.AddSingleton<UserService>();
builder.Services.AddSingleton<BookService>();
builder.Services.AddSingleton<JwtUtilService>();

// Register repositories
builder.Services.AddSingleton<ApplicationDBContext>();
builder.Services.AddSingleton<UserRepository>();
builder.Services.AddSingleton<BookRepository>();

// Configure CORS to allow all
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseMiddleware<ExceptionHandlingMiddleware>();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Add CORS middleware
app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

app.Run();
