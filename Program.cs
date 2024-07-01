using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using QLVT_BE.Data;
using QLVT_BE.HubSignalR;
using QLVT_BE.Providers;
using QLVT_BE.Repositorys;
using QLVT_BE.Service;
using QLVT_BE.Singleton;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddHttpContextAccessor();

builder.Services.AddSwaggerGen(setup =>
{
    // Include 'SecurityScheme' to use JWT Authentication
    var jwtSecurityScheme = new OpenApiSecurityScheme
    {
        BearerFormat = "JWT",
        Name = "JWT Authentication",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = JwtBearerDefaults.AuthenticationScheme,
        Description = "Nhập Token vào ",

        Reference = new OpenApiReference
        {
            Id = JwtBearerDefaults.AuthenticationScheme,
            Type = ReferenceType.SecurityScheme
        }
    };

    setup.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);

    setup.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        { jwtSecurityScheme, Array.Empty<string>() }
    });
});



// khai báo kết nối DTB
builder.Services.AddDbContext<QuanLyVatTuContext>(o =>
        o.UseSqlServer(builder.Configuration.GetConnectionString("MyDB")));



// Cho phép truy cập SSL https



// khai báo Repository
builder.Services.AddScoped<ILoginRepository, LoginRepository>();
builder.Services.AddScoped<IPhongBanRepository, PhongBanRepo>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IChucVuRepository, ChucVuRepo>();
builder.Services.AddScoped<IVatTuRepository, VatTuRepo>();
builder.Services.AddScoped<IVTQuanTamRepository, VTQuanTamRepo>();
builder.Services.AddScoped<IPhieuDeNghiVatTuRepository, PhieuDeNghiVatTuRepository>();
builder.Services.AddScoped<IThuKhoRepo,ThuKhoRepo>();
builder.Services.AddScoped<INguoiMuaRepo, NguoiMuaRepo>();
builder.Services.AddScoped<INotificaRepo, NotificaRepo>();
builder.Services.AddScoped<IPhieuDeNghiSuaRepo, PhieuDeNghiSuaRepo>();
builder.Services.AddScoped<IWriteFileService, WriteFileService>();
builder.Services.AddScoped<IDashboardRepo,DashboardRepo>();


builder.Services.AddScoped<ChatHub>();
builder.Services.AddScoped<HubFunction>();
builder.Services.AddScoped<Dashboard>();

builder.Services.AddSingleton<IConnectionManager, ConnectionManager>();
builder.Services.AddSingleton<INotificationsManager, NotificationsManager>();
builder.Services.AddSingleton<IPhieuVTProviders, PhieuVTProviders>();
builder.Services.AddSingleton<IPhieuSuaProviders, PhieuSuaProviders>();

// Lấy một tham chiếu đến provider và cập nhật dữ liệu khi ứng dụng bắt đầu khởi động
builder.Services.AddHostedService<LoadDataStatsServices>();



// Đăng ký sử dụng dịch vụ xác thực, phân quyền
var autKey = builder.Configuration.GetValue<string>("Jwt:SecretKey");

builder.Services.AddAuthentication(opt =>
{
    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
 
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        ValidateAudience = false,
        ValidateIssuer = false,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                builder.Configuration.GetSection("Jwt:SecretKey").Value!))
    };

    options.Events = new JwtBearerEvents
    {
        OnMessageReceived = context => // kiểm tra token được gửi kèm Hub xác thực kết nối Hub bằng username
        {
            var accessToken = context.Request.Query["access_token"];
            var path = context.HttpContext.Request.Path;
            if (!string.IsNullOrEmpty(accessToken) && (path.StartsWithSegments("/chat")))
            {
                context.Token = accessToken;
            }
            return Task.CompletedTask;
        }
    };
});

builder.Services.Configure<FormOptions>(o =>
{
    o.ValueLengthLimit = int.MaxValue;
    o.MultipartBodyLengthLimit = int.MaxValue;
    o.MemoryBufferThreshold = int.MaxValue;
});

builder.Services.AddSignalR();
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", builder =>
        builder.WithOrigins("http://localhost:4200")
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials());
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("CorsPolicy");
app.UseHttpsRedirection();
// sử dụng 
app.UseAuthentication();
app.UseAuthorization();

app.UseStaticFiles();

app.UseStaticFiles(new StaticFileOptions()
{
    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"Upload")),
    RequestPath = new PathString("/Upload")
});

app.MapControllers();
app.MapHub<ChatHub>("/chat");
app.MapHub<PhieuVT_Hub>("/PhieuVT_Hub");

app.Run();

