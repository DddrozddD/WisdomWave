using BLL.Infrastructure;
using BLL.Services;
using DAL.Context;
using DAL.Repositories.UnitsOfWork;
using Domain.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Azure.Identity;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<WwContext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("connStr")));

// Add services to the container.
builder.Services.AddControllersWithViews();

ConfigurationService(builder.Services);
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAuthorization();

app.UseRouting();

app.UseCors("AllowSpecificOrigin");

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapControllerRoute(
    name: "emailConfirmation",
    pattern: "confirmation/",
    defaults: new { controller = "EmailConfirm", action = "Confirm" });

app.Run();

void ConfigurationService(IServiceCollection serviceCollection)
{
    serviceCollection.AddCors(options =>
    {
        options.AddPolicy("AllowSpecificOrigin", builder =>
        {
            builder.WithOrigins("http://localhost:3000")
                   .AllowAnyHeader()
                   .AllowAnyMethod();
        });
    });

    serviceCollection.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    }).AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true, // �������� �������� ������
            ValidateAudience = true, // �������� ��������� ������
            ValidateLifetime = true, // �������� ����� �������� ������
            ValidateIssuerSigningKey = true, // �������� ����� ������� ������
            ValidIssuer = "your_issuer_here", // ����� ����������� ���������� �������� ������
            ValidAudience = "your_audience_here", // ����� ����������� ���������� ��������� ������
            IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("your_secret_key_here")) // ����� ����������� ��������� ���� ��� �������� ������� ������
        };
        options.Events = new JwtBearerEvents
        {
            OnTokenValidated = async context =>
            {
                // ��������� �������������� ������������ �� ������
                var userId = context.Principal.Identity.Name;

                // ����� �� ������ ��������� �������������� ��������, ��������� ������������� ������������
                // ��������, ��������� ���������� � ������������ �� ���� ������, ��������� userId
                // ��� ������ ���������� ���������� �������
            }
        };
    });
    serviceCollection.AddDbContext<WwContext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("connStr")));
    serviceCollection.AddIdentity<WwUser, IdentityRole>(op => op.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<WwContext>().AddDefaultTokenProviders().
       AddTokenProvider<EmailConfirmationTokenProvider<WwUser>>("emailConfirmationProvider");

    serviceCollection.Configure<EmailConfirmationProviderOption>(op => op.TokenLifespan = TimeSpan.FromDays(5));

    serviceCollection.Configure<SendGridSenderOptions>(op => builder.Configuration.GetSection("SendGridOptions").Bind(op));
    serviceCollection.AddTransient<IUnitOfWork, UnitOfWork>();
    serviceCollection.AddTransient<IEmailSender, EmailSenderService>();
    serviceCollection.AddTransient<CourseService>();
    serviceCollection.AddTransient<CategoryService>();
    serviceCollection.AddTransient<LikeDislikeService>();
    serviceCollection.AddTransient<ReviewService>();
    serviceCollection.AddTransient<SubscriptionService>();
    serviceCollection.AddTransient<AnswerService>();
    serviceCollection.AddTransient<ParagraphService>();
    serviceCollection.AddTransient<QuestionService>();
    serviceCollection.AddTransient<SubQuestionService>();
    serviceCollection.AddTransient<TestService>();
    serviceCollection.AddTransient<UnitService>();
    serviceCollection.AddTransient<CategoryService>();
    serviceCollection.AddTransient<PageService>();
    serviceCollection.AddTransient<RatingService>();

    BllConfiguration.ConfigurationService(serviceCollection);
}