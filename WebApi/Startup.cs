using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Utility.DbHelper;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Newtonsoft.Json;
using WebApi.Middlewares;
using Microsoft.AspNetCore.Diagnostics;
using Utility;
using Utility.Exceptions;
using System.Threading.Tasks;
using System.Net;
using Microsoft.AspNetCore.Http;

namespace WebApi
{
    public class Startup
    {
        private static readonly SymmetricSecurityKey secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("a secret that needs to be at least 16 characters long"));
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().AddJsonOptions(option=>
            {
                option.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
            });
            services.AddCors(options =>
            {
                options.AddPolicy("any", builder =>
                {
                    builder.AllowAnyOrigin() //允许任何来源的主机访问
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials();//指定处理cookie
                });
            });
            Configuration.GetSection("Settings:SecretKey");
            services.Add(new ServiceDescriptor(typeof(DbContext), new DbContext(Configuration.GetConnectionString("mysqlConnection"))));
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })  
            .AddJwtBearer(jwtBearerOptions =>
            {
                jwtBearerOptions.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = secretKey,


                    ValidateIssuer = false,
                    //ValidIssuer = "sigeer",

                    ValidateAudience = false,
                    //ValidAudience = "test",

                    ValidateLifetime = true, //validate the expiration and not before values in the token

                    ClockSkew = TimeSpan.FromMinutes(5), //5 minute tolerance for the expiration date
                };
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (!env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler((handle)=>
                {
                    handle.Run(async context =>
                    {
                        var feature = context.Features.Get<IExceptionHandlerFeature>();
                        var error = feature?.Error;
                        if(error is SigeerException)
                        {
                            var myException = error as SigeerException;
                            var jsonModel = new ResponseMessage("Request",Message.Error,myException.Message,myException.Params).ToString();
                            Console.WriteLine(jsonModel);
                            context.Response.StatusCode = (int)HttpStatusCode.OK;
                            await context.Response.WriteAsync(jsonModel);
                        }
                    });
                    
                });
            }

            app.UseAuthentication();
            app.UseStaticFiles();
            //app.UseMvc();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "api/{controller}/{action}/{id?}");
            });

        }
    }
}
