using blazor_buoi24.Services;

var builder = WebApplication.CreateBuilder(args);

//setup server blazor
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor(); 

builder.Services.AddHttpClient(); //Thư viện dùng để call api từ server khác

//Trước app là DI (tiêm các service vào ứng dụng)
//Sử dụng các hàm từ service
builder.Services.AddSignalR(); //service socket của dotnet




//DI các service management
builder.Services.AddScoped<NumberService>();
builder.Services.AddScoped<CarService>();
builder.Services.AddScoped<CartService>();
builder.Services.AddScoped<BurgerService>();
builder.Services.AddScoped<StoreService>();


var app = builder.Build();


// Su dung cac ham tu service

app.UseRouting();
app.MapBlazorHub(); //kích hoạt server socket của blazor web
app.MapFallbackToPage("/_Host");

app.UseStaticFiles(); // middleware để sử dụng file tĩnh như css, js, img

app.Run();
