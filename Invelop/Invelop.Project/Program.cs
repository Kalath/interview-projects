using Invelop.Project.Client;
using Invelop.Project.Client.Filters;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(options =>
                                {
                                    options.Filters.Add(typeof(UnhandledExceptionsFilter));
                                    options.Filters.Add(typeof(HttpResponseExceptionFilter));
                                });
builder.Services.AddAntiforgery();
builder.Services.AddResponseCompression();
builder.Services.ConfigureDependencies();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
    app.UseExceptionHandler("/error");
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseResponseCompression();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html");

app.Run();
