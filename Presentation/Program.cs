using AutoMapper;
using LibrarySystem.BusinessLogic.ServicesClasses;
using LibrarySystem.BusnissLogic.ServicesClasses;
using LibrarySystem.BusnissLogic.ServicesInterfaces;
using LibrarySystem.BusnissLogic.UnitOfWork;
using LibrarySystem.DataAccessLayer;
using LibrarySystem.DataAccessLayer.DbSeeder;
using LibrarySystem.DataAccessLayer.Repositories;
using LibrarySystem.Domain.Entities;
using LibrarySystem.Domain.Interfaces;
using LibrarySystem.Domain.ReposInterfaces;
using LibrarySystem.Presentation.Middlewares;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

var jwt = builder.Configuration.GetSection("jwt");
var keyString = jwt["key"];

if (string.IsNullOrEmpty(keyString))
    throw new Exception("JWT key is missing from configuration.");

var key = Encoding.ASCII.GetBytes(keyString);




builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        b => b.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod());
});



builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "Library API", Version = "v1" });
    c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());

}); 
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddMemoryCache();






builder.Services.AddDbContext<LibraryDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<LibraryDbContext>()
    .AddDefaultTokenProviders();





builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwt["Issuer"],
        ValidAudience = jwt["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(key)
    };
}
);

  






    builder.Services.AddScoped<IbookRepository, BookRepository>();
    builder.Services.AddScoped<IGenericRepository<Category>, CategoriesRepository>();
    builder.Services.AddScoped<ICategoryRepository, CategoriesRepository>();
    builder.Services.AddScoped<IAuthorRepository, AuthorRepository>();
    builder.Services.AddScoped<IGenericRepository<Author>, AuthorRepository>();
    builder.Services.AddScoped<IGenericRepository<Loan>, LoanRepository>();
    builder.Services.AddScoped<ILoanRepository, LoanRepository>();
    builder.Services.AddScoped<IUserRepository, UserRepository>();
    builder.Services.AddScoped<IMemberRepository, MemberRepository>();
    builder.Services.AddScoped<ILibrarianRepository, LibrarianRepository>();
    builder.Services.AddScoped<AuthService>();







    builder.Services.AddScoped<ILoanService, LoanService>();
    builder.Services.AddScoped<IAuthorService, AuthorService>();
    builder.Services.AddScoped<IBookService,BookServicecs>();
    builder.Services.AddScoped<ICategoryService, CategoryService>();
    builder.Services.AddScoped<IMemberService, MemberService>();
    builder.Services.AddScoped<ILibrarianService, LibrarianService>();
    builder.Services.AddScoped<IFileUploadsService, FileUploadService>();
builder.Services.AddScoped< FileUploadService>();









builder.Services.AddScoped<IUnitofWorks, UnitOfWork>();

builder.Services.AddTransient<RolesSeeder>();
builder.Services.AddTransient<CategoriesSeeder>();
builder.Services.AddTransient<AuthorsSeeder>();
builder.Services.AddTransient<BooksSeeder>();



var app = builder.Build();


app.UseCors("AllowAll");



using (var scope = app.Services.CreateScope())
    {
    var seeder = scope.ServiceProvider.GetRequiredService<RolesSeeder>();
    await seeder.SeedRolesAsync();
    var Categoriesseeder = scope.ServiceProvider.GetRequiredService<CategoriesSeeder>();
    await Categoriesseeder.SeedCategoriesAsync();
    var authorsSeeder = scope.ServiceProvider.GetRequiredService<AuthorsSeeder>();
    await authorsSeeder.SeedAuthorsAsync();
    var Booksseeder = scope.ServiceProvider.GetRequiredService<BooksSeeder>();
    await Booksseeder.SeedBooksAsync();
   
    
}





if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Library API V1");
        c.RoutePrefix = string.Empty; 
    });
}
    app.UseStaticFiles();
    app.UseHttpsRedirection();
app.UseRouting();

app.UseMiddleware<GlobalExceptionHandler>(); 

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

await  app.RunAsync();

