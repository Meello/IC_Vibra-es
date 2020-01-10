using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IcVibrations.Calculator.GeometricProperties;
using IcVibrations.Calculator.MainMatrixes;
using IcVibrations.Core.Calculator;
using IcVibrations.Core.Calculator.ArrayOperations;
using IcVibrations.Core.Calculator.Variables;
using IcVibrations.Core.Operations;
using IcVibrations.Core.Operations.Beam.Calculate;
using IcVibrations.Core.Operations.BeamVibration.Calculate;
using IcVibrations.Core.Operations.Piezoelectric.Calculate;
using IcVibrations.Core.Validators.BeamRequest;
using IcVibrations.DataContracts.Beam;
using IcVibrations.DataContracts.Beam.Calculate;
using IcVibrations.DataContracts.Piezoelectric.Calculate;
using IcVibrations.Methods.AuxiliarMethods;
using IcVibrations.Methods.NewmarkMethod;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;

namespace IcVibrations
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Calculator
            services.AddScoped<IGeometricProperties, GeometricProperties>();
            services.AddScoped<IMainMatrix, MainMatrix>();
            services.AddScoped<IArrayOperation, ArrayOperation>();
            services.AddScoped<IVariable, Variable>();
            services.AddScoped<ICalculate, Calculate>();
            services.AddScoped<ICalculate, Calculate>();
            
            // Methods
            services.AddScoped<IAuxiliarMethod, AuxiliarMethod>();
            services.AddScoped<INewmarkMethod, NewmarkMethod>();
            
            // Beam Operations
            services.AddScoped<IOperationBase<CalculateBeamRequest<RectangularBeamRequestData>, CalculateBeamResponse>, CalculateRectangularBeamVibration>();
            services.AddScoped<IOperationBase<CalculateBeamRequest<CircularBeamRequestData>, CalculateBeamResponse>, CalculateCircularBeamVibration>();
            services.AddScoped<IBeamRequestValidator<RectangularBeamRequestData>, RectangularBeamRequestValidator>();
            services.AddScoped<IBeamRequestValidator<CircularBeamRequestData>, CircularBeamRequestValidator>();

            // Piezoelectric Operations
            services.AddScoped<ICalculatePiezoelectricVibration, CalculatePiezoelectricVibration>();
            // Beam Operations Base
            services.AddScoped<IOperationBase<CalculateBeamRequest<RectangularBeamRequestData>, CalculateBeamResponse>, CalculateRectangularBeamVibration>();
            // Piezoelectric Operations Base
            services.AddScoped<IOperationBase<CalculatePiezoelectricRequest, CalculatePiezoelectricResponse>, CalculatePiezoelectricVibration>();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "IC Vibrations", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "IC Vibration V1");
            });

            app.UseMvc();
        }
    }
}
