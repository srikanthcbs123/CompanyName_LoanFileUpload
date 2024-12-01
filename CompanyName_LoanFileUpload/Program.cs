using CompanyName_LoanFileUpload.BusinessEntities.Interfaces;
using CompanyName_LoanFileUpload_DBConectivity;
using CompanyName_LoanFileUpload_RepositoryLayer;
using CompanyName_LoanFileUpload_ServiceLayer;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//Add the classes &interface to inbult dependecy injection container.
builder.Services.AddScoped<IFilesUploadService, FilesUploadService>();
builder.Services.AddScoped<IFilesUploadRepository, FilesUploadRepository>();
builder.Services.AddSingleton<IConnectionFactory, ConnectionFactory>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
