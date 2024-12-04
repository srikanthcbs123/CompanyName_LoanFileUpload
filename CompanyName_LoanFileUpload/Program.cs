using CompanyName_LoanFileUpload.BusinessEntities.Interfaces;
using CompanyName_LoanFileUpload_DBConectivity;
using CompanyName_LoanFileUpload_RepositoryLayer;
using CompanyName_LoanFileUpload_ServiceLayer;
using Microsoft.AspNetCore.Http.HttpResults;

var builder = WebApplication.CreateBuilder(args);
//builder is the inbuilt dependency injection conatiner.
// Add the project dependenciy  services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//Add the classes &interface to inbult dependecy injection container like below way.
#region AddScopedService
/*
Services.Addscoped():
By using Addscoped service every httprequest new instance is created.
(whether it is get(or) post(or)put(or)delete, each and every request new instance is created by scoped service).
*/
builder.Services.AddScoped<IFilesUploadService, FilesUploadService>();
builder.Services.AddScoped<IFilesUploadRepository, FilesUploadRepository>();
#endregion

#region AddSingletonService
/*
Services.Addsingleton():
By using singleton service it created  only one object and it is used for all httpRequests.
that same object is used for all the times.
if you call  same method multiple times also it generates only one object.
this is called singleton design pattern.
*/

builder.Services.AddSingleton<IConnectionFactory, ConnectionFactory>();
#endregion 

#region AutoMapper Adding To DependencyInjection Container
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
#endregion

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
