using IcVibrations.Calculator.GeometricProperties;
using IcVibrations.Calculator.MainMatrixes;
using IcVibrations.Common.Profiles;
using IcVibrations.Core.Calculator.ArrayOperations;
using IcVibrations.Core.Calculator.MainMatrixes.Beam.Circular;
using IcVibrations.Core.Calculator.MainMatrixes.Beam.Rectangular;
using IcVibrations.Core.Calculator.MainMatrixes.BeamWithDva;
using IcVibrations.Core.Calculator.MainMatrixes.BeamWithPiezoelectric.Circular;
using IcVibrations.Core.Calculator.MainMatrixes.BeamWithPiezoelectric.Rectangular;
using IcVibrations.Core.Mapper;
using IcVibrations.Core.Mapper.Profiles.Circular;
using IcVibrations.Core.Mapper.Profiles.Rectangular;
using IcVibrations.Core.Models.BeamWithDynamicVibrationAbsorber;
using IcVibrations.Core.Models.Piezoelectric;
using IcVibrations.Core.NewmarkNumericalIntegration;
using IcVibrations.Core.Operations;
using IcVibrations.Core.Operations.Beam.Circular;
using IcVibrations.Core.Operations.Beam.Rectangular;
using IcVibrations.Core.Operations.BeamWithDva.Circular;
using IcVibrations.Core.Operations.BeamWithDva.Rectangular;
using IcVibrations.Core.Operations.BeamWithPiezoelectric.Circular;
using IcVibrations.Core.Operations.BeamWithPiezoelectric.Rectangular;
using IcVibrations.Core.Validators.Profiles.Circular;
using IcVibrations.Core.Validators.Profiles.Rectangular;
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
            // Calculator - ArrayOperation
            services.AddScoped<IArrayOperation, ArrayOperation>();
            // Calculator - CalculateGeometricProperty
            services.AddScoped<ICalculateGeometricProperty, CalculateGeometricProperty>();
            // Calculator - MainMatrix
            services.AddScoped<IRectangularBeamMainMatrix, RectangularBeamMainMatrix>();
            services.AddScoped<ICircularBeamMainMatrix, CircularBeamMainMatrix>();
            services.AddScoped<IBeamWithDvaMainMatrix, BeamWithDvaMainMatrix>();
            services.AddScoped<IRectangularBeamWithPiezoelectricMainMatrix, RectangularBeamWithPiezoelectricMainMatrix>();
            services.AddScoped<ICircularBeamWithPiezoelectricMainMatrix, CircularBeamWithPiezoelectricMainMatrix>();
            services.AddScoped<ICommonMainMatrix, CommonMainMatrix>();
            
            // Mapper
            services.AddScoped<IMappingResolver, MappingResolver>();
            services.AddScoped<ICircularProfileMapper, CircularProfileMapper>();
            services.AddScoped<IRectangularProfileMapper, RectangularProfileMapper>();

            // NewmarkNumericalIntegration
            services.AddScoped<INewmarkMethod, NewmarkMethod>();

            // Auxiliar Operations
            services.AddScoped<IAuxiliarOperation, AuxiliarOperation>();

            // Beam Operations
            services.AddScoped<ICalculateCircularBeamVibration, CalculateCircularBeamVibration>();
            services.AddScoped<ICalculateRectangularBeamVibration, CalculateRectangularBeamVibration>();
            
            // BeamWithDva Operations
            services.AddScoped<ICalculateVibration<CalculateBeamWithDvaVibrationRequest<CircularProfile>, BeamWithDvaRequestData<CircularProfile>, CircularProfile, BeamWithDva<CircularProfile>>, CalculateCircularBeamWithDvaVibration>();
            services.AddScoped<ICalculateVibration<CalculateBeamWithDvaVibrationRequest<RectangularProfile>, BeamWithDvaRequestData<RectangularProfile>, RectangularProfile, BeamWithDva<RectangularProfile>>, CalculateRectangularBeamWithDvaVibration>();
            
            // Piezoelectric Operations
            services.AddScoped<ICalculateVibration<CalculateBeamWithPiezoelectricVibrationRequest<CircularProfile>,PiezoelectricRequestData<CircularProfile>, CircularProfile, BeamWithPiezoelectric<CircularProfile>>, CalculateCircularBeamWithPiezoelectricVibration>();
            services.AddScoped<ICalculateVibration<CalculateBeamWithPiezoelectricVibrationRequest<RectangularProfile>, PiezoelectricRequestData<RectangularProfile>, RectangularProfile, BeamWithPiezoelectric<RectangularProfile>>, CalculateRectangularBeamWithPiezoelectricVibration>();

            // Validators
            services.AddScoped<IRectangularProfileValidator, RectangularProfileValidator>();
            services.AddScoped<ICircularProfileValidator, CircularProfileValidator>();

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
