using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// MVCDemoContext
builder.Services.AddDbContext<MVCDemoContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("LocalDBConnection")));

builder.Services.AddIdentity<CustomUser, IdentityRole>()
    .AddEntityFrameworkStores<MVCDemoContext>()
    .AddDefaultTokenProviders();

TypeAdapterConfig.GlobalSettings.Scan(Assembly.GetExecutingAssembly());

// UnitOfWork
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Gebruiker/Login"; // Pad naar de inlogpagina
    options.AccessDeniedPath = "/Home/Index"; // Pad naar de homepagina
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
