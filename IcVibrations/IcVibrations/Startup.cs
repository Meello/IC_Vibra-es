using IcVibrations.Calculator.GeometricProperties;
using IcVibrations.Calculator.MainMatrixes;
using IcVibrations.Core.Calculator.ArrayOperations;
using IcVibrations.Core.Mapper;
using IcVibrations.Core.Operations;
using IcVibrations.Core.Operations.BeamVibration.Calculate;
using IcVibrations.Core.Operations.BeamVibration.CalculateWithDynamicVibrationAbsorber;
using IcVibrations.Core.Operations.PiezoelectricVibration.Calculate;
using IcVibrations.Core.Validators.Beam;
using IcVibrations.Core.Validators.MethodParameters;
using IcVibrations.DataContracts.Beam;
using IcVibrations.DataContracts.Beam.Calculate;
using IcVibrations.DataContracts.Beam.CalculateWithDynamicVibrationAbsorber;
using IcVibrations.DataContracts.Piezoelectric;
using IcVibrations.DataContracts.Piezoelectric.Calculate;
using IcVibrations.Methods.AuxiliarOperations;
using IcVibrations.Methods.NewmarkMethod;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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
            services.AddScoped<IArrayOperation, ArrayOperation>();
            services.AddScoped<IGeometricProperty, GeometricProperty>();
            services.AddScoped<IMainMatrix, MainMatrix>();
            
            // Mapper
            services.AddScoped<IMappingResolver, MappingResolver>();
            
            // Methods
            services.AddScoped<INewmarkMethod, NewmarkMethod>();

            // Validators
            services.AddScoped<IBeamRequestValidator<RectangularBeamRequestData>, RectangularBeamRequestValidator>();
            services.AddScoped<IBeamRequestValidator<CircularBeamRequestData>, CircularBeamRequestValidator>();
            services.AddScoped<IMethodParameterValidator, MethodParameterValidator>();

            // Auxiliar Operations
            services.AddScoped<IAuxiliarOperation, AuxiliarOperation>();

            // Beam Operations
            services.AddScoped<AbstractCalculateBeamVibration<CircularBeamRequestData>, CalculateCircularBeamVibration>();
            services.AddScoped<AbstractCalculateBeamVibration<RectangularBeamRequestData>, CalculateRectangularBeamVibration>();

            // BeamWithDva Operations
            services.AddScoped<AbstractCalculateBeamVibration<CircularBeamWithDvaRequestData>, CalculateCircularBeamWithDvaVibration>();

            // Piezoelectric Operations
            services.AddScoped<ICalculatePiezoelectricVibration, CalculatePiezoelectricVibration>();

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

            // POSTMAN NÃO ESTÁ FUNCIONANDO --> TENTAR CONSERTAR

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
