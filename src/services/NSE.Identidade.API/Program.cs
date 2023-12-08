using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NSE.Identidade.API.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddDefaultIdentity<IdentityUser>()
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "NerdStore Enterprise Identity API",
        Description = "Esta API faz parte de estudos pessoais sobre Enterprise Applications.",
        Contact = new Microsoft.OpenApi.Models.OpenApiContact() { Name = "Diego Coelho", Email = "diegocoelhosp@gmail.com" },
        License = new Microsoft.OpenApi.Models.OpenApiLicense() { Name = "MIT", Url = new Uri("https://opensource.org/licenses/MIT") }
    });
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
});

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
