using IcVibrations.Calculator.GeometricProperties;
using IcVibrations.Calculator.MainMatrixes;
using IcVibrations.Common.Profiles;
using IcVibrations.Core.Calculator.ArrayOperations;
using IcVibrations.Core.Mapper;
using IcVibrations.Core.Models.Beam;
using IcVibrations.Core.Models.BeamWithDynamicVibrationAbsorber;
using IcVibrations.Core.Models.Piezoelectric;
using IcVibrations.Core.NewmarkNumericalIntegration;
using IcVibrations.Core.Operations;
using IcVibrations.Core.Operations.Beam;
using IcVibrations.Core.Operations.BeamWithDva;
using IcVibrations.Core.Operations.BeamWithPiezoelectric;
using IcVibrations.DataContracts.CalculateVibration.Beam;
using IcVibrations.DataContracts.CalculateVibration.BeamWithDynamicVibrationAbsorber;
using IcVibrations.DataContracts.CalculateVibration.BeamWithPiezoelectric;
using IcVibrations.Methods.AuxiliarOperations;
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
            services.AddScoped<ICalculateGeometricProperty, CalculateGeometricProperty>();
            services.AddScoped<ICommonMainMatrix, CommonMainMatrix>();
            
            // Mapper
            services.AddScoped<IMappingResolver, MappingResolver>();

            // NewmarkNumericalIntegration
            services.AddScoped<INewmarkMethod, NewmarkMethod>();

            // Auxiliar Operations
            services.AddScoped<IAuxiliarOperation, AuxiliarOperation>();

            // Beam Operations
            services.AddScoped<ICalculateVibration<CalculateBeamVibrationRequest<CircularProfile>, BeamRequestData<CircularProfile>, CircularProfile, Beam<CircularProfile>>, CalculateCircularBeamVibration>();
            services.AddScoped<ICalculateVibration<CalculateBeamVibrationRequest<RectangularProfile>, BeamRequestData<RectangularProfile>, RectangularProfile, Beam<RectangularProfile>>, CalculateRectangularBeamVibration>();
            
            // BeamWithDva Operations
            services.AddScoped<ICalculateVibration<CalculateBeamWithDvaVibrationRequest<CircularProfile>, BeamWithDvaRequestData<CircularProfile>, CircularProfile, BeamWithDva<CircularProfile>>, CalculateCircularBeamWithDvaVibration>();
            services.AddScoped<ICalculateVibration<CalculateBeamWithDvaVibrationRequest<RectangularProfile>, BeamWithDvaRequestData<RectangularProfile>, RectangularProfile, BeamWithDva<RectangularProfile>>, CalculateRectangularBeamWithDvaVibration>();
            
            // Piezoelectric Operations
            services.AddScoped<ICalculateVibration<CalculateBeamWithPiezoelectricVibrationRequest<CircularProfile>,PiezoelectricRequestData<CircularProfile>, CircularProfile, BeamWithPiezoelectric<CircularProfile>>, CalculateCircularBeamWithPiezoelectricVibration>();
            services.AddScoped<ICalculateVibration<CalculateBeamWithPiezoelectricVibrationRequest<RectangularProfile>, PiezoelectricRequestData<RectangularProfile>, RectangularProfile, BeamWithPiezoelectric<RectangularProfile>>, CalculateRectangularBeamWithPiezoelectricVibration>();

            // Validators


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
