

using KonectaPrueba.WebApi.Interface;
using KonectaPrueba.WebApi.Repository;
using KonectaPrueba.WebApi.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DatabaseContext>
    (options => options.UseSqlServer(builder.Configuration.GetConnectionString("DBConnectionString")));
builder.Services.AddTransient<IUser, UserRepository>();
// Add services to the container.

builder.Services.AddScoped<IUser, UserRepository>();
builder.Services.AddScoped(typeof(IAuthService), typeof(AuthService));

builder.Services.AddControllers();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
          
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidAudience = builder.Configuration["Jwt:Audience"],
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSession();
builder.Services.AddDistributedMemoryCache();

var app = builder.Build();

app.UseSession();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}
///////////



/////////////
///
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();