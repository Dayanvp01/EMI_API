using EMI_API.Business.Interfaces;
using EMI_API.Business.Services;
using EMI_API.Business.Util;
using EMI_API.Commons.Enums;
using EMI_API.Data;
using EMI_API.Data.Interfaces;
using EMI_API.Data.Repository;
using EMI_API.EndPoints;
using EMI_API.Utils;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

#region Area de servicios

// Configuración de base de datos
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Configuración de Identity
builder.Services.AddIdentityCore<IdentityUser>()
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

// Configuración de CORS
var allowedHosts = builder.Configuration.GetValue<string>("AllowedHosts")!;
builder.Services.AddCors(opt =>
{
    opt.AddDefaultPolicy(conf =>
    {
        conf.WithOrigins(allowedHosts).AllowAnyHeader().AllowAnyMethod();
    });
});

// Servicios de Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header
    });

    c.OperationFilter<AuthorizationFilter>();
});

// Fluent Validation
builder.Services.AddValidatorsFromAssemblyContaining<Program>();

// AutoMapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Configuración del Serializador JSON para manejar ciclos de referencia
builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(options => options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

// Manejador de excepciones y problemas
builder.Services.AddProblemDetails();

builder.Services.AddHttpContextAccessor();

// Configuración de autenticación y autorización
builder.Services.AddAuthentication().AddJwtBearer(opt =>
{
    opt.MapInboundClaims = false;
    opt.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKeys = Keys.GetAllKeys(builder.Configuration),
        ClockSkew = TimeSpan.Zero
    };
});
builder.Services.AddAuthorization(opt =>
{
    opt.AddPolicy(Policy.ADMIN, policy => policy.RequireRole(Roles.ADMIN));
    opt.AddPolicy(Policy.USER, policy => policy.RequireRole(Roles.USER));
});

builder.Services.AddScoped<UserManager<IdentityUser>>(); // Servicios para crear y manejar los usuarios mediante Identity
builder.Services.AddScoped<SignInManager<IdentityUser>>(); // Servicio para loggear los usuarios
builder.Services.AddScoped<RoleManager<IdentityRole>>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<IProjectService, ProjectService>();
builder.Services.AddScoped<IDepartmentService, DepartmentService>();
builder.Services.AddScoped<IPositionHistoryService, PositionHistoryService>();

builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();
builder.Services.AddScoped<IEmployeeProjectRepository, EmployeeProjectRepository>();
builder.Services.AddScoped<IPositionHistoryRepository, PositionHistoryRepository>();
builder.Services.AddScoped<IProjectRepository, ProjectRepository>();

#endregion

var app = builder.Build();

#region Area de Middleware

app.UseSwagger();
app.UseSwaggerUI();

app.UseCors(); // Utiliza la política de CORS configurada previamente en los middleware
app.UseAuthorization();
app.UseMiddleware<CustomMiddleware>();

app.MapGroup("api/employees").MapEmployees();
app.MapGroup("api/users").MapUsers();
app.MapGroup("api/departments").MapDepartments();
app.MapGroup("api/projects").MapProjects();
app.MapGroup("api/positionsHistories").MapPositionsHistories();

#endregion

app.Run();
