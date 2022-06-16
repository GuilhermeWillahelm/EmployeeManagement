using System.Text;
using EmployeeManagement.Data;
using EmployeeManagement.Repositories;
using EmployeeManagement.Services;
using EmployeeManagement.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using EmployeeManagement.AutoMapperHelper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.Authorization;

//Variables
var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("EmployeeDb");
var appSettingsToken = builder.Configuration.GetSection("AppSettings:Token").Value;
var MyAllowSpecificOrigins = "_myAllowSpecifiOrigins";

// Add services to the container.
builder.Services.AddHttpContextAccessor();
//Services
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<IOfficeService, OfficeService>();
//Repositories
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IOfficeRepository, OfficeRepository>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DataBaseDBContext>(option => option.UseSqlServer(connectionString));
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

builder.Services.AddCors(opt => 
{
    opt.AddPolicy(name: MyAllowSpecificOrigins, builder => { builder.WithOrigins("http://localhost:4200").AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin(); });
});

IdentityBuilder identityBuilder = builder.Services.AddIdentityCore<User>(opt => opt.SignIn.RequireConfirmedEmail = true);
identityBuilder = new IdentityBuilder(identityBuilder.UserType, typeof(Role), builder.Services);
identityBuilder.AddEntityFrameworkStores<DataBaseDBContext>();
identityBuilder.AddRoleValidator<RoleValidator<Role>>();
identityBuilder.AddRoleManager<RoleManager<Role>>();
identityBuilder.AddSignInManager<SignInManager<User>>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(opts =>
    {
        opts.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(appSettingsToken)),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });

builder.Services.AddMvc(opts =>
{
    var policy = new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser().Build();
    opts.Filters.Add(new AuthorizeFilter(policy));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(MyAllowSpecificOrigins);

app.UseAuthorization();
app.UseAuthentication();

app.MapControllers();

app.Run();
