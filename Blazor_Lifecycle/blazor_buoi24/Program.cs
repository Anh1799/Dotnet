var builder = WebApplication.CreateBuilder(args);

// setup server blazor
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

// Truoc app la DI (tiem cac service vao ung dung)


var app = builder.Build();


// Su dung cac ham tu service

app.MapBlazorHub(); // kich hoat server socket cua blazor web
app.MapFallbackToPage("/_Host");

app.Run();
