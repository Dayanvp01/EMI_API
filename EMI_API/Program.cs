using EMI_API.EndPoints;
using EMI_API.Utils;

var builder = WebApplication.CreateBuilder(args);

#region Area de servicios


//Agrega servicio de Configuracion de politicas de CORS
var allowedHosts = builder.Configuration.GetValue<string>("AllowedHosts")!;
builder.Services.AddCors(opt =>
{
    opt.AddDefaultPolicy(conf =>
    {
        conf.WithOrigins(allowedHosts).AllowAnyHeader().AllowAnyMethod();
    });
});

//Servicios de Swagger
builder.Services.AddEndpointsApiExplorer();//explora los endpoints para que swagger los liste y muestre
builder.Services.AddSwaggerGen();

//agrega Automapper
builder.Services.AddAutoMapper(typeof(Program));
#endregion

var app = builder.Build();

#region Area de Middleware

app.UseSwagger();
app.UseSwaggerUI();
app.UseCors(); // utiliza la politica de CORS configurada previamente en los middleware 
app.UseMiddleware<CustomMiddleware>();
app.MapGroup("api/employees").MapEmployees();

#endregion



app.Run();
