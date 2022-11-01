using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kompiuterija.Entities;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Kompiuterija.Repository;

namespace Kompiuterija
***REMOVED***
    public class Startup
    ***REMOVED***
        public Startup(IConfiguration configuration)
        ***REMOVED***
            Configuration = configuration;
***REMOVED***

        public IConfiguration Configuration ***REMOVED*** get; ***REMOVED***

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        ***REMOVED***

            services.AddAuthentication(x =>
            ***REMOVED***
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    ***REMOVED***).AddJwtBearer(o =>
            ***REMOVED***
                var Key = Encoding.UTF8.GetBytes(Configuration["JWT:Key"]);
                o.SaveToken = true;
                o.TokenValidationParameters = new TokenValidationParameters
                ***REMOVED***
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Key)
        ***REMOVED***;
    ***REMOVED***);
            services.AddSingleton<IJWTManagerRepository, JWTManagerRepository>();
            services.AddDbContext<kompiuterijaContext>();
            services.AddControllers();
            services.AddSwaggerGen(options =>
            ***REMOVED***
                options.SwaggerDoc("v1", new OpenApiInfo
                ***REMOVED***
                    Version = "v1",
                    Title = "Kompiuterija API"
        ***REMOVED***);
    ***REMOVED***);
***REMOVED***

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        ***REMOVED***
            if (env.IsDevelopment())
            ***REMOVED***
                app.UseSwagger();
                app.UseSwaggerUI(options =>
                ***REMOVED***
                    options.SwaggerEndpoint("/swagger/v1/swagger.json", "api");
                    options.RoutePrefix = string.Empty;
        ***REMOVED***);
                app.UseDeveloperExceptionPage();
    ***REMOVED***

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            ***REMOVED***
                endpoints.MapControllers();
    ***REMOVED***);
***REMOVED***
***REMOVED***
***REMOVED***
