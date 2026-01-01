using BibliothequeWeb.Data;
using BibliothequeWeb.Repositories;
using BibliothequeWeb.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Configuration de la base de données
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Configuration des sessions pour l'authentification
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// Enregistrement des repositories
builder.Services.AddScoped<ICompteRepository, CompteRepository>();
builder.Services.AddScoped<ILivreRepository, LivreRepository>();
builder.Services.AddScoped<ICategorieRepository, CategorieRepository>();
builder.Services.AddScoped<IFavoriRepository, FavoriRepository>();
builder.Services.AddScoped<ITelechargementRepository, TelechargementRepository>();

// Enregistrement des services
builder.Services.AddScoped<IAuthentificationService, AuthentificationService>();
builder.Services.AddScoped<ILivreService, LivreService>();
builder.Services.AddScoped<ICategorieService, CategorieService>();
builder.Services.AddScoped<IFavoriService, FavoriService>();
builder.Services.AddScoped<ITelechargementService, TelechargementService>();

// Configuration MVC et Razor Pages
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

// Configuration de l'autorisation (utilisée avec les sessions)
builder.Services.AddAuthorization();

var app = builder.Build();

// Configuration du pipeline HTTP
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseSession();

app.UseAuthorization();

app.MapRazorPages();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Initialisation de la base de données au démarrage
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    await BibliothequeWeb.Data.DbInitializer.InitializeAsync(context);
}

await app.RunAsync();

