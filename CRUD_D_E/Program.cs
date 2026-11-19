using Microsoft.EntityFrameworkCore;
using CRUD_D_E.Data;

var builder = WebApplication.CreateBuilder(args);/// <summary> Crea un generador de aplicaciones web, que se utiliza para configurar y construir la aplicación web.</summary>

/// <summary> Configura los servicios de la aplicación web, que son componentes que se utilizan para manejar las solicitudes HTTP y generar respuestas HTTP.</summary>
builder.Services.AddControllers()/// <summary> Agrega los controladores a la aplicación web, que son clases que manejan las solicitudes HTTP y generan respuestas HTTP.</summary>
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;/// <summary> Configura el serializador JSON para ignorar los ciclos de referencia, que son referencias circulares entre objetos que pueden causar errores al serializar a JSON.</summary>

    });

/// <summary> Configura la cadena de conexión a la base de datos, que se utiliza para conectarse a la base de datos y realizar operaciones sobre ella.</summary>
var constr = builder.Configuration.GetConnectionString("Bd");
builder.Services.AddDbContext<Bd>(options => options.UseSqlServer(constr));

/// <summary> Configura Swagger, que es una herramienta que permite generar documentación interactiva de la API y probar los endpoints de la API desde el navegador.</summary>
builder.Services.AddSwaggerGen();


/// <summary> Configura CORS, que es un mecanismo que permite a los navegadores web realizar solicitudes HTTP a dominios diferentes al dominio de origen de la página web.</summary>
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

var app = builder.Build();/// <summary> Construir la aplicacion web</summary>
/// <summary> Configurar la tuberia de procesamiento de solicitudes HTTP. Es un conjunto de componentes que se ejecutan en orden para procesar las solicitudes HTTP entrantes y generar respuestas HTTP salientes.</summary>
app.Use(async (context, next) => 
{
    if (context.Request.Path == "/") /// <summary> Si la ruta es la raiz</summary>
    {
    context.Response.Redirect("/swagger/index.html", permanent: false); /// <summary> Redirigir a Swagger</summary>
    return;
}
    await next(); /// <summary> Continuar con la siguiente tarea en la tuberia de procesamiento de solicitudes</summary>
});

if (app.Environment.IsDevelopment())/// <summary> Si el entorno es de desarrollo</summary>
{
    app.MapOpenApi();/// <summary> Mapear la API abierta</summary>
    app.UseSwagger();/// <summary> Habilitar Swagger</summary>
    app.UseSwaggerUI();/// <summary> Habilitar Swagger UI</summary>
}
app.UseHttpsRedirection();/// <summary> Redirigir las solicitudes HTTP a HTTPS</summary>
app.UseAuthorization();/// <summary> Habilitar la autorizacion</summary>
app.UseCors("AllowAll");/// <summary> Habilitar CORS</summary>
app.MapControllers();/// <summary> Mapear los controladores a la aplicacion web</summary>
app.Run();/// <summary> Ejecutar la aplicacion web</summary>