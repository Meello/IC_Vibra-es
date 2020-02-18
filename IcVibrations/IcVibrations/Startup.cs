using IcVibrations.Calculator.GeometricProperties;
using IcVibrations.Calculator.MainMatrixes;
using IcVibrations.Core.Calculator.ArrayOperations;
using IcVibrations.Core.Calculator.MainMatrixes.Beam.Circular;
using IcVibrations.Core.Calculator.MainMatrixes.Beam.Rectangular;
using IcVibrations.Core.Calculator.MainMatrixes.BeamWithDva;
using IcVibrations.Core.Calculator.MainMatrixes.BeamWithPiezoelectric.Circular;
using IcVibrations.Core.Calculator.MainMatrixes.BeamWithPiezoelectric.Rectangular;
using IcVibrations.Core.Mapper;
using IcVibrations.Core.Mapper.Profiles.Circular;
using IcVibrations.Core.Mapper.Profiles.Rectangular;
using IcVibrations.Core.NewmarkNumericalIntegration.Beam;
using IcVibrations.Core.NewmarkNumericalIntegration.BeamWithPiezoelectric;
using IcVibrations.Core.Operations.Beam.Circular;
using IcVibrations.Core.Operations.Beam.Rectangular;
using IcVibrations.Core.Operations.BeamWithDva.Circular;
using IcVibrations.Core.Operations.BeamWithDva.Rectangular;
using IcVibrations.Core.Operations.BeamWithPiezoelectric.Circular;
using IcVibrations.Core.Operations.BeamWithPiezoelectric.Rectangular;
using IcVibrations.Core.Validators.Profiles.Circular;
using IcVibrations.Core.Validators.Profiles.Rectangular;
using IcVibrations.Methods.AuxiliarOperations;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
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
            services.AddScoped<IBeamWithPiezoelectricNewmarkMethod, BeamWithPiezoelectricNewmarkMethod>();
            services.AddScoped<IBeamNewmarkMethod, BeamNewmarkMethod>();

            // Auxiliar Operations
            services.AddScoped<IAuxiliarOperation, AuxiliarOperation>();

            // Beam Operations
            services.AddScoped<ICalculateCircularBeamVibration, CalculateCircularBeamVibration>();
            services.AddScoped<ICalculateRectangularBeamVibration, CalculateRectangularBeamVibration>();

            // BeamWithDva Operations
            services.AddScoped<ICalculateCircularBeamWithDvaVibration, CalculateCircularBeamWithDvaVibration>();
            services.AddScoped<ICalculateRectangularBeamWithDvaVibration, CalculateRectangularBeamWithDvaVibration>();

            // Piezoelectric Operations
            services.AddScoped<ICalculateCircularBeamWithPiezoelectricVibration, CalculateCircularBeamWithPiezoelectricVibration>();
            services.AddScoped<ICalculateRectangularBeamWithPiezoelectricVibration, CalculateRectangularBeamWithPiezoelectricVibration>();

            // Validators
            services.AddScoped<IRectangularProfileValidator, RectangularProfileValidator>();
            services.AddScoped<ICircularProfileValidator, CircularProfileValidator>();

            services.AddControllers();

            ConfigureSwagger(services);
        }

        private static void ConfigureSwagger(IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "IC Vibrations", Version = "v1" });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "IC Vibration V1");
            });

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        //// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        //public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        //{
        //    if (env.IsDevelopment())
        //    {
        //        app.UseDeveloperExceptionPage();
        //    }
        //    else
        //    {
        //        app.UseHsts();
        //    }

        //    // Enable middleware to serve generated Swagger as a JSON endpoint.
        //    app.UseSwagger();

        //    // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
        //    // specifying the Swagger JSON endpoint.
        //    app.UseSwaggerUI(c =>
        //    {
        //        c.SwaggerEndpoint("/swagger/v1/swagger.json", "IC Vibration V1");
        //    });

        //    app.UseMvc();
        //}
    }
}
