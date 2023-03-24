using APIDemo.Models;
using APIDemo.Service;
//using System.Runtime.InteropServices;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Scheme = "Bearer",
        BearerFormat = "Jwt",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Description = "Bearer Authentication with Jwt Token",
        Type = SecuritySchemeType.Http
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Id = "Bearer",
                    Type = ReferenceType.SecurityScheme
                }
            },
            new List<string>()
        }
    });
});
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateActor = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});
builder.Services.AddAuthorization();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSingleton<ITodoService,TodoService>();
builder.Services.AddSingleton<IUserService, UserService>();
var app = builder.Build();
app.UseSwagger();
app.UseAuthorization();
app.UseAuthentication();
app.MapGet("/", () => "Hello World!");

app.MapPost("/create",[Authorize(AuthenticationSchemes =JwtBearerDefaults.AuthenticationScheme,Roles ="Administrator")] (Todo todo, ITodoService service) => Create(todo, service));
app.MapGet("/get", [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Standard, Administrator")] (int id, ITodoService service) => Get(id, service));
app.MapGet("/list", (ITodoService service) => List(service));
app.MapPut("/update", [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator")] (Todo newTodo, ITodoService service) => Update(newTodo, service));
app.MapDelete("/delete", [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator")] (int id, ITodoService service) => Delete(id, service));

IResult Create(Todo todo, ITodoService service)
{
    var result = service.Create(todo);
    return Results.Ok(result);
}
IResult Get(int id, ITodoService service)
{
    var todo = service.Get(id);
    if (todo == null) return Results.NotFound("To do is not found");
    return Results.Ok(todo);
}
IResult List(ITodoService service)
{
    var todo = service.List();
    return Results.Ok(todo);
}
IResult Update(Todo newTodo, ITodoService service)
{
    var updateTodo = service.Update(newTodo);
    if (updateTodo == null) Results.NotFound("To do not found");
    return Results.Ok(updateTodo);
}
IResult Delete(int id, ITodoService service)
{
    var result = service.Delete(id);
    if (!result) Results.NotFound("Something went wrong");
    return Results.Ok(result);
}
app.UseSwaggerUI();
app.Run();
