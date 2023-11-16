var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers();
builder.Services.AddCors(options => {
    options.AddPolicy("CorsPolicy", corsBuilder =>
    {
        //corsBuilder.WithOrigins("http://localhost:4200");
        corsBuilder.WithOrigins("http://localhost:3000");
        corsBuilder.WithOrigins("http://localhost:5173");
        corsBuilder.WithOrigins("http://localhost");
        corsBuilder.WithHeaders("Access-Control-Allow-Origin").AllowAnyHeader().AllowAnyMethod();

        //builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader().AllowCredentials();

    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("CorsPolicy");

app.UseAuthorization();

app.MapControllers();

app.Run();
