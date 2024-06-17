using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics; // Para IExceptionHandlerFeature
using Microsoft.IdentityModel.Tokens;
using System.Net;
using System.Text;
using System.Text.Json; // Para JsonSerializer
using UserManagementAPI.Services;
using UserManagementAPI.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(options =>
	options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// builder.Services.AddAntiforgery(options =>
// {
// 	options.HeaderName = "X-XSRF-TOKEN"; // Nombre del encabezado para el token
// });


builder.Services.AddControllers();

builder.Services.AddScoped<IAuthService, AuthService>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
		.AddJwtBearer(options =>
		{
			options.TokenValidationParameters = new TokenValidationParameters
			{
				ValidateIssuer = false, // Ya no validamos el emisor ni la audiencia 
				ValidateAudience = false,
				ValidateLifetime = true, // Validar la expiración del token
				ValidateIssuerSigningKey = true, // Validar la firma del token
				IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration["Jwt:Key"]))
			};
		});

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

// Configure the HTTP request pipeline.›
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseExceptionHandler(appError =>
{
	appError.Run(async context =>
	{
		context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
		context.Response.ContentType = "application/json";

		var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
		if (contextFeature != null)
		{
			// Aquí puedes personalizar el manejo de diferentes tipos de excepciones
			var errorResponse = new
			{
				statusCode = context.Response.StatusCode,
				message = contextFeature.Error.Message
			};
			await context.Response.WriteAsync(JsonSerializer.Serialize(errorResponse));
		}
	});
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
