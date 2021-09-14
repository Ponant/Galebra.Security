
using Galebra.Security.Headers.Csp;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddContentSecurityPolicy(builder.Configuration.GetSection("Csp"));

//builder.Services.AddContentSecurityPolicy(c =>
//{
//    c.IsDisabled = false;
//    c.Add("Policy1", g =>
//    {
//        g.Csp.Fixed = "default-src 'self';connect-src ws://localhost:65412";
//        g.Csp.Nonceable.Add("style-src 'self'");
//        g.CspReportOnly.Nonceable.Add("script-src");
//        g.IsDefault = true;
//        g.NumberOfNonceBytes = 32;
//    });
//    c.Add("Policy2", g =>
//    {
//        g.Csp.Fixed = "default-src 'self';connect-src ws://localhost:65412";
//        g.CspReportOnly.Fixed = "default-src";
//        g.IsDefault = false;
//    });
//});

//builder.Services.AddRazorPages();

////Apply globally
//builder.Services.AddRazorPages()
//    .AddMvcOptions(options =>
//    {
//        //options.Filters.Add(new EnableCspPageFilter { PolicyGroupName = "PolicyGroup1" });
//        options.Filters.Add(new DisableCspPageFilter());
//    });

//Apply on specific folders
builder.Services.AddRazorPages(options =>
{
    options.Conventions.AddFolderApplicationModelConvention(
        "/Movies",
        model => model.Filters.Add(new EnableCspPageFilter { PolicyGroupName = "PolicyGroup1" }));
    //model => model.Filters.Add(new DisableCspPageFilter { EnforceMode = false }));

    options.Conventions.AddFolderApplicationModelConvention(
     "/Movies/Adventure",
     model => model.Filters.Add(new EnableCspPageFilter { PolicyGroupName = "PolicyGroup3" }));

    options.Conventions.AddPageApplicationModelConvention("/Error",
        model => model.Filters.Add(new EnableCspPageFilter { PolicyGroupName = "PolicyGroup3" }));

    //You need ASP.NET Identity for this to work
    //options.Conventions.AddAreaFolderApplicationModelConvention("Identity", "/Account",
    //    model => model.Filters.Add(new EnableCspPageFilter { PolicyGroupName = "PolicyGroup1" }));

    //options.Conventions.AddAreaPageApplicationModelConvention("Identity", "/Account/Manage/ChangePassword",
    //    model => model.Filters.Add(new EnableCspPageFilter { PolicyGroupName = "PolicyGroup3" }));
});

builder.Services.AddControllersWithViews();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseContentSecurityPolicy();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.Run();
