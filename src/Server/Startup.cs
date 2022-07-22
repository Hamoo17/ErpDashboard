using DevExpress.AspNetCore;
using DevExpress.XtraReports.Web.Extensions;
using ErpDashboard.Application.Extensions;
using ErpDashboard.Application.Reports;
using ErpDashboard.Infrastructure.Extensions;
using ErpDashboard.Server.Extensions;
using ErpDashboard.Server.Filters;
using ErpDashboard.Server.Managers.Preferences;
using ErpDashboard.Server.Middlewares;
using Hangfire;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Localization;
using System.Text.Json.Serialization;
using X.Paymob.CashIn;

namespace ErpDashboard.Server
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private readonly IConfiguration _configuration;

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940

        public void ConfigureServices(IServiceCollection services)
        {

            services.AddCors();
            services.AddDevExpressControls();
            //  services.AddTransient<ProductValidationServices, ProductValidationService>();
            services.AddMvc().AddNewtonsoftJson().AddJsonOptions(o=> o.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter())).ConfigureApplicationPartManager(x =>
            {
                var parts = x.ApplicationParts;
                var aspNetCoreReportingAssemblyName = typeof(DevExpress.AspNetCore.Reporting.WebDocumentViewer.WebDocumentViewerController).Assembly.GetName().Name;
                var reportingPart = parts.FirstOrDefault(part => part.Name == aspNetCoreReportingAssemblyName);
                if (reportingPart != null)
                {
                    parts.Remove(reportingPart);
                }
            });
            services.AddScoped<ReportStorageWebExtension, CustomReportStorageWebExtension>();
            services.AddSignalR();
            services.AddLocalization(options =>
            {
                options.ResourcesPath = "Resources";
            });
            // services.AddDatabase(_configuration);
            services.AddCurrentUserService();
            services.AddSerialization();
            services.AddDatabase(_configuration);
            services.AddServerStorage(); //TODO - should implement ServerStorageProvider to work correctly!
            services.AddScoped<ServerPreferenceManager>();
            services.AddScoped<AllCustomersReport>();
            services.AddScoped<AllKetchenReports>();
            services.AddPaymobCashIn(config =>
                {
                    config.ApiKey = "ZXlKaGJHY2lPaUpJVXpVeE1pSXNJblI1Y0NJNklrcFhWQ0o5LmV5SndjbTltYVd4bFgzQnJJam94T0RBME1Ea3NJbU5zWVhOeklqb2lUV1Z5WTJoaGJuUWlMQ0p1WVcxbElqb2lhVzVwZEdsaGJDSjkuRmVRVHZXQ3ExclFlTTkycDR2T0RtMWVsMzVXVHJOUEsyamd5WEg1U2luRDRuUVVjc18tS0MtcUJsNlc1T1lqRHlpczBJZzluMVJNdG40N1Rqd0k5MHc=";
                    config.Hmac = "C2B14604F4E81EB7DDD675ACF3F8CAFC";
                    ;
                });
            services.AddServerLocalization();
            services.AddIdentity();
            services.AddJwtAuthentication(services.GetApplicationSettings(_configuration));
            services.AddApplicationLayer();
            services.AddApplicationServices();
            services.AddRepositories();
            services.AddExtendedAttributesUnitOfWork();
            services.AddSharedInfrastructure(_configuration);
            services.RegisterSwagger();
            services.AddInfrastructureMappings();
            services.AddHangfire(x => x.UseSqlServerStorage(_configuration.GetConnectionString("DefaultConnection")));
            services.AddHangfireServer();
            services.AddControllers().AddValidators();
            services.AddExtendedAttributesValidators();
            services.AddExtendedAttributesHandlers();
            services.AddRazorPages();
            services.AddApiVersioning(config =>
            {
                config.DefaultApiVersion = new ApiVersion(1, 0);
                config.AssumeDefaultVersionWhenUnspecified = true;
                config.ReportApiVersions = true;
            });
            services.AddLazyCache();
            services.AddResponseCompression(opts =>
            {
                opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
                    new[] { "application/octet-stream" });
            });
            Application.ServicesHelper.provider = services.BuildServiceProvider();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IStringLocalizer<Startup> localizer)
        {
#if !DEBUG
            app.UseResponseCompression();
#endif
            app.UseCors();
            //  app.UseResponseCompression();

            app.UseExceptionHandling(env);
            app.UseHttpsRedirection();
            app.UseMiddleware<ErrorHandlerMiddleware>();
            app.UseBlazorFrameworkFiles();
            var provider = new FileExtensionContentTypeProvider();

            provider.Mappings[".res"] = "application/octet-stream";
            provider.Mappings[".pexe"] = "application/x-pnacl";
            provider.Mappings[".nmf"] = "application/octet-stream";
            provider.Mappings[".mem"] = "application/octet-stream";
            provider.Mappings[".wasm"] = "application/wasm";

            app.UseStaticFiles();
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"Files")),
                RequestPath = new PathString("/Files"),
                ContentTypeProvider = provider
            });
            app.UseRequestLocalizationByCulture();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseHangfireDashboard("/jobs", new DashboardOptions
            {
                DashboardTitle = localizer["BlazorHero Jobs"],
                Authorization = new[] { new HangfireAuthorizationFilter() }
            });
            app.UseEndpoints();
            app.ConfigureSwagger();
            app.Initialize(_configuration);
        }
    }
}