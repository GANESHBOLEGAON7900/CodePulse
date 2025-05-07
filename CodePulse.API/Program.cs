using CodePulse.API.Data;
using CodePulse.API.Mappings;
using CodePulse.API.Models.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.



builder.Services.AddControllers();
builder.Services.AddHttpContextAccessor();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ApplicationDbContext>(options => 
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("CodePulseConnectionString"));
});


builder.Services.AddScoped<ICategoriesRepository,CategoriesRepository>();
builder.Services.AddScoped<IBlogPostRepository,BlogPostRepository>();
builder.Services.AddScoped<IImageRepository,ImageRepository>();
builder.Services.AddScoped<MapCategoriesInBlogPost>();
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors(opt =>
{
    opt.AllowAnyOrigin();
    opt.AllowAnyMethod();
    opt.AllowAnyHeader();
});

app.UseAuthorization();

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider=new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(),"Images")),
    RequestPath="/Images"
});

app.MapControllers();

app.Run();
