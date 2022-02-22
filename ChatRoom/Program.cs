using BusinessLogic;
using Repository;

var builder = WebApplication.CreateBuilder(args);
var config = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json")
    .Build();


// Add services to the container.

builder.Services.AddControllersWithViews();
var connStr = config["ConnectionString"];
builder.Services.AddNpgsql<Repository.ChatRoomContext>(connStr)
    .AddDbContext<Repository.ChatRoomContext>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IChatRoomContext, ChatRoomContext>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IChatRoomService, ChatRoomService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    options.RoutePrefix = "swagger";
});


app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html");;

app.Run();
