using ChapterBaseAPI.Repositories;
using ChapterBaseAPI.Services;
using ChapterBaseAPI.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Register services
builder.Services.AddSingleton<UserService>();
builder.Services.AddSingleton<JwtUtilService>();

// Register repositories
builder.Services.AddSingleton<ApplicationDBContext>();
builder.Services.AddSingleton<UserRepository>();

// Configure JWT Authentication
//builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//    .AddJwtBearer(options =>
//    {
//        options.TokenValidationParameters = new TokenValidationParameters
//        {
//            ValidateIssuer = true,
//            ValidIssuer = builder.Configuration["Jwt:Issuer"],
//            ValidateAudience = true,
//            ValidAudience = builder.Configuration["Jwt:Audience"],
//            ValidateLifetime = true,
//            ValidateIssuerSigningKey = true,
//            IssuerSigningKeyResolver = (token, securityToken, kid, validationParameters) =>
//            {
//                var jwksUri = $"{builder.Configuration["Jwt:Issuer"]}/.well-known/jwks.json";
//                var keys = new JsonWebKeySet(new HttpClient().GetStringAsync(jwksUri).Result);
//                return keys.Keys;
//            }
//        };

//    });

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {

            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidateIssuerSigningKey = true,
            ValidateIssuer = true,
            ValidateLifetime = true,
            ValidAudience = builder.Configuration["Jwt:Audience"],
            ValidateAudience = false
        };

        options.MetadataAddress = builder.Configuration["Jwt:Issuer"] + "/.well-known/openid-configuration";
    });


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

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Add CORS middleware
app.UseCors("AllowAll");

// Add authentication middleware
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
