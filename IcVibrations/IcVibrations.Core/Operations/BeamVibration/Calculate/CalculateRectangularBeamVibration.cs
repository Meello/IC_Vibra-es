using IcVibrations.Calculator.GeometricProperties;
using IcVibrations.Calculator.MainMatrixes;
using IcVibrations.Core.Calculator.ArrayOperations;
using IcVibrations.Core.DTO;
using IcVibrations.Core.Mapper;
using IcVibrations.Core.Operations.BeamVibration.Calculate;
using IcVibrations.Core.Validators.BeamRequest;
using IcVibrations.DataContracts;
using IcVibrations.DataContracts.Beam;
using IcVibrations.DataContracts.Beam.Calculate;
using IcVibrations.Methods.AuxiliarMethods;
using IcVibrations.Methods.NewmarkMethod;
using IcVibrations.Models.Beam;
using IcVibrations.Models.Beam.Characteristics;
using System;
using System.Collections.Generic;
using System.Text;

namespace IcVibrations.Core.Operations.BeamVibration.Calculate
{
    public class CalculateRectangularBeamVibration : AbstractCalculateBeamVibration<RectangularBeamRequestData>
    {
        private readonly IArrayOperation _arrayOperation;
        private readonly IGeometricProperty _geometricProperty;
        private readonly IMainMatrix _mainMatrix;
        private readonly IAuxiliarMethod _auxiliarMethod;
        private readonly IMappingResolver _mappingResolver;

        public CalculateRectangularBeamVibration(
            IArrayOperation arrayOperation,
            IGeometricProperty geometricProperty,
            IMainMatrix mainMatrix,
            IAuxiliarMethod auxiliarMethod,
            IBeamRequestValidator<RectangularBeamRequestData> validator,
            INewmarkMethod newmarkMethod,
            IMappingResolver mappingResolver) : base(validator, newmarkMethod, mappingResolver)
        {
            this._arrayOperation = arrayOperation;
            this._geometricProperty = geometricProperty;
            this._mainMatrix = mainMatrix;
            this._auxiliarMethod = auxiliarMethod;
            this._mappingResolver = mappingResolver;
        }

        protected override NewmarkMethodInput CalculateParameters(CalculateBeamRequest<RectangularBeamRequestData> request, int degreesFreedomMaximum, OperationResponseBase response)
        {
            NewmarkMethodInput input = new NewmarkMethodInput();

            Beam beam = this._mappingResolver.AddValues(request.Data);

            // Como ter acesso aos valores do perfil retangular?
            beam.Profile = new RectangularProfile
            {
                Height = request.Data.Height,
                Width = request.Data.Width,
                Thickness = request.Data.Thickness
            };

            // Input values
            double mi = 0.005;

            double area = this._geometricProperty.Area(request.Data.Height, request.Data.Width, request.Data.Thickness);

            double momentInertia = this._geometricProperty.MomentInertia(request.Data.Height, request.Data.Width, request.Data.Thickness);

            // Calculate values
            beam.Profile.Area = this._arrayOperation.Create(area, request.Data.ElementCount);

            beam.Profile.MomentInertia = this._arrayOperation.Create(momentInertia, request.Data.ElementCount);
            
            double[,] mass = this._mainMatrix.CreateMass(beam, degreesFreedomMaximum, request.Data.ElementCount);
            
            double[,] hardness = this._mainMatrix.CreateHardness(beam, degreesFreedomMaximum, request.Data.ElementCount);
            
            double[,] damping = this._mainMatrix.CreateDamping(input.Mass,input.Hardness, mi, degreesFreedomMaximum);
            
            double[] force = this._mainMatrix.CreateForce(request.Data.Forces, request.Data.ForceNodePositions, degreesFreedomMaximum);

            bool[] bondaryCondition = this._mainMatrix.CreateBondaryCondition(beam.FirstFastening, beam.LastFastening, degreesFreedomMaximum);

            // Output values

            input.Mass = this._auxiliarMethod.AplyBondaryConditions(mass, bondaryCondition);

            input.Hardness = this._auxiliarMethod.AplyBondaryConditions(hardness, bondaryCondition);

            input.Damping = this._auxiliarMethod.AplyBondaryConditions(damping, bondaryCondition);

            input.Force = this._auxiliarMethod.AplyBondaryConditions(force, bondaryCondition);

            input.DegreesFreedomMaximum = degreesFreedomMaximum;

            return input;
        }
    }
}
