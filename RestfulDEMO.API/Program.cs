using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using RestfulDEMO.API.Data;
using RestfulDEMO.API.Mappings;
using RestfulDEMO.API.Repositories;
using System.Text;
using Microsoft.OpenApi.Models;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);

/* A. ADD SERVICES TO THE CONTAINER. */

/* A.0. Add controllers */
builder.Services.AddControllers();
builder.Services.AddHttpContextAccessor();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(
    // Add authentication and authorization feature to be available on swagger.
    // Note that without it, it would be already available on the API using other
    // tools like Postman for instance.
    options =>
    {
        options.SwaggerDoc("v1", new OpenApiInfo { Title = "RestFulDEMO API", Version = "v1" });
        options.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
        {
            Name = "Authorization",
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.ApiKey,
            Scheme = JwtBearerDefaults.AuthenticationScheme
        });

        options.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = JwtBearerDefaults.AuthenticationScheme
                    },
                    Scheme = "Oauth2",
                    Name = JwtBearerDefaults.AuthenticationScheme,
                    In = ParameterLocation.Header
                },
                new List<string>()
            }
        });
    });

/* A.1. DB Context class injection:
    Add connection string to the DB context class.
    DB Context Class now available to be used accross all the application
    from any controler and from any repository.
*/

builder.Services.AddDbContext<RestfulDbContextA>(
    (options) => options.UseSqlServer(
        builder.Configuration.GetConnectionString(
            "RestFulDEMOConnectionString")));

builder.Services.AddDbContext<RestFulAuthDbContext>(
    (options) => options.UseSqlServer(
        builder.Configuration.GetConnectionString(
            "RestFulDEMOAuthConnectionString")));

/* A.2. Inject SQL db and auth db repositories */
builder.Services.AddScoped<IRegionRepository, SQLRegionRepository>();
builder.Services.AddScoped<IWalkRepository, SQLWalkRepository>();
builder.Services.AddScoped<ITokenRepository, TokenRepository>();
builder.Services.AddScoped<IImageRepository, LocalImageRepository>();

/* A.3. Inject automapper*/
builder.Services.AddAutoMapper(typeof(AutoMapperProfiles));
//builder.Services.AddAutoMapper(typeof(AutoMapperProfiles));

/* A.4. Inject identity dependencies*/
builder.Services.AddIdentityCore<IdentityUser>()
    .AddRoles<IdentityRole>()
    .AddTokenProvider<DataProtectorTokenProvider<IdentityUser>>("Restful")
    .AddEntityFrameworkStores<RestFulAuthDbContext>()
    .AddDefaultTokenProviders();

builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 6;
    options.Password.RequiredUniqueChars = 1;
});

/* A.5. Add authentication
    When a protected enpoint is attempted to be accessed, the authentication middleware will kick in.
    It will validate the JWT token again the token validation configured parameters.
    User get authenticated if succeds, otherwise the moddleware responds accordingly.
*/
builder.Services.AddAuthentication(
    options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(
    options => options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    });

/* B. BUILD APPLICATION */
var app = builder.Build();

/* C. CONFIGURE THE HTTP REQUEST MIDDLEWARE PIPELINE */
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.UseStaticFiles(
    /*
     * Serving StatiC/Local files.
        Note taht dotNet is unable to serve static files by itself and therefore UseStaticFiles needs to be made to be added here.
        - By doing this every time we want to access the request path "/Images" -> httpS://localhost:1234/Images
        we will be redirected to FileProvider physical path.
        - By doing this we should have our static/local images being served:
        For instance: https://localhost:7008/Images/MeWolf.jpg
     */
    new StaticFileOptions
    {
        FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "Images")),
        RequestPath = "/Images"
    });

app.MapControllers();

app.Run();
