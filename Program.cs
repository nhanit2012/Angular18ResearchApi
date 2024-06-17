

using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Newtonsoft.Json.Converters;
using TutorialApi.Models;

var builder = WebApplication.CreateBuilder(args);
builder. Services.AddTransient<AngularDBContext>();


string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddHttpContextAccessor();
builder.Services.AddResponseCompression();//
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddAuthentication(option =>
{
    option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    option.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(); 
  
builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
    //option.SerializerSettings.MaxDepth = 2;
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;// tham chiếu vòng lặp 
    options.SerializerSettings.MetadataPropertyHandling = Newtonsoft.Json.MetadataPropertyHandling.Ignore;
    options.SerializerSettings.DateTimeZoneHandling = Newtonsoft.Json.DateTimeZoneHandling.Unspecified;//Bỏ time local 
    options.SerializerSettings.Converters.Add(new StringEnumConverter());
}).AddFluentValidation();

builder.Services.AddControllers().ConfigureApiBehaviorOptions(options =>
{
    //options.SuppressConsumesConstraintForFormFileParameters = true;//Multipart/form-data request inference

    options.SuppressInferBindingSourcesForParameters = true; //Disable inference rules
                                                             //options.SuppressModelStateInvalidFilter = true;
                                                             //options.SuppressMapClientErrors = true; //Disable ProblemDetails
                                                             //options.ClientErrorMapping[StatusCodes.Status404NotFound].Link = "https://httpstatuses.com/404";
});
builder.Services.AddMvcCore().AddApiExplorer();  // Allow use parameter do not define [FromForm]
                                                 //services.AddOptions();
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
       builder =>
       {
           //builder.WithOrigins("http://example.com", "http://www.contoso.com"); 
           //builder.WithOrigins("http://koolselling.com", "https://koolselling.com")
           //.SetIsOriginAllowedToAllowWildcardSubdomains()
           builder.AllowAnyOrigin()
           .AllowAnyHeader()
           .AllowAnyMethod()
           .SetPreflightMaxAge(TimeSpan.FromSeconds(300));
       });
});
builder.Services.AddSwaggerGen(); 
var app = builder.Build();




// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseStaticFiles();
app.UseHttpsRedirection();
app.UseRouting();
app.UseCors(MyAllowSpecificOrigins);
app.UseAuthorization();

app.MapControllers();

app.Run();
