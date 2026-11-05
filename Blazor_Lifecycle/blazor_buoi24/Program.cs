var builder = WebApplication.CreateBuilder(args);

// setup server blazor
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

builder.Services.AddHttpClient(); // thu vien dung de call api tu server khac

// Truoc app la DI (tiem cac service vao ung dung)


var app = builder.Build();


// Su dung cac ham tu service

app.UseRouting();
app.MapBlazorHub(); //kích hoạt server socket của blazor web
app.MapFallbackToPage("/_Host");

app.UseStaticFiles(); // middleware để sử dụng file tĩnh như css, js, img

app.Run();
