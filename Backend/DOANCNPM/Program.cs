using DOANCNPM.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Đăng ký DbContext kết nối tới SQL Server
builder.Services.AddDbContext<ChbntContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Cấu hình chống vòng lặp JSON và tự động định dạng thụt lề dữ liệu trả về
// Tìm và sửa đổi cụm đăng ký AddControllers thành thế này:
builder.Services.AddControllers(options =>
{
    // Tắt tính năng tự động coi các thuộc tính Non-Nullable là [Required]
    options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true;
})
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
        options.JsonSerializerOptions.WriteIndented = true;
    });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Cấu hình chính sách CORS cho phép tất cả các nguồn (Frontend) gọi vào API này
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

// Cấu hình HTTP request pipeline cho môi trường Development
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Tự động chuyển hướng HTTP sang HTTPS
app.UseHttpsRedirection();

// Kích hoạt chính sách CORS toàn cục
app.UseCors("AllowAll");

// Kiểm tra quyền truy cập hệ thống
app.UseAuthorization();

app.MapControllers();

app.Run();