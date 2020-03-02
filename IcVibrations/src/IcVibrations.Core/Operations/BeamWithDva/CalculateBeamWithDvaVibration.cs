﻿using IcVibrations.Common.Classes;
using IcVibrations.Common.Profiles;
using IcVibrations.Core.Calculator.ArrayOperations;
using IcVibrations.Core.Calculator.MainMatrixes.Beam;
using IcVibrations.Core.Calculator.MainMatrixes.BeamWithDva;
using IcVibrations.Core.DTO.Input;
using IcVibrations.Core.Mapper;
using IcVibrations.Core.Mapper.Profiles;
using IcVibrations.Core.Models.BeamWithDynamicVibrationAbsorber;
using IcVibrations.Core.NewmarkNumericalIntegration.BeamWithDva;
using IcVibrations.Core.Validators.Profiles;
using IcVibrations.DataContracts.CalculateVibration.BeamWithDynamicVibrationAbsorber;
using IcVibrations.Methods.AuxiliarOperations;
using IcVibrations.Models.Beam.Characteristics;
using System.Threading.Tasks;

namespace IcVibrations.Core.Operations.BeamWithDva
{
    /// <summary>
    /// It's responsible to calculate the vibration in a beam with dynamic vibration absorber.
    /// </summary>
    /// <typeparam name="TProfile"></typeparam>
    public abstract class CalculateBeamWithDvaVibration<TProfile> : CalculateVibration<CalculateBeamWithDvaVibrationRequest<TProfile>, BeamWithDvaRequestData<TProfile>, TProfile, BeamWithDva<TProfile>>, ICalculateBeamWithDvaVibration<TProfile>
        where TProfile : Profile, new()
    {
        private readonly IMappingResolver _mappingResolver;
        private readonly IAuxiliarOperation _auxiliarOperation;
        private readonly IProfileMapper<TProfile> _profileMapper;
        private readonly IBeamWithDvaMainMatrix _mainMatrix;
        private readonly IBeamMainMatrix<TProfile> _beamMainMatrix;
        private readonly IArrayOperation _arrayOperation;

        /// <summary>
        /// Class contructor.
        /// </summary>
        /// <param name="newmarkMethod"></param>
        /// <param name="mappingResolver"></param>
        /// <param name="profileValidator"></param>
        /// <param name="auxiliarOperation"></param>
        /// <param name="profileMapper"></param>
        /// <param name="mainMatrix"></param>
        /// <param name="beamMainMatrix"></param>
        /// <param name="arrayOperation"></param>
        public CalculateBeamWithDvaVibration(
            IBeamWithDvaNewmarkMethod newmarkMethod,
            IMappingResolver mappingResolver,
            IProfileValidator<TProfile> profileValidator,
            IAuxiliarOperation auxiliarOperation,
            IProfileMapper<TProfile> profileMapper,
            IBeamWithDvaMainMatrix mainMatrix,
            IBeamMainMatrix<TProfile> beamMainMatrix,
            IArrayOperation arrayOperation)
            : base(newmarkMethod, mappingResolver, profileValidator, auxiliarOperation)
        {
            this._mappingResolver = mappingResolver;
            this._auxiliarOperation = auxiliarOperation;
            this._profileMapper = profileMapper;
            this._mainMatrix = mainMatrix;
            this._beamMainMatrix = beamMainMatrix;
            this._arrayOperation = arrayOperation;
        }

        public async override Task<BeamWithDva<TProfile>> BuildBeam(CalculateBeamWithDvaVibrationRequest<TProfile> request, uint degreesFreedomMaximum)
        {
            if (request == null)
            {
                return null;
            }

            int i = 0;

            double[] dvaMasses = new double[request.BeamData.Dvas.Count];
            double[] dvaHardnesses = new double[request.BeamData.Dvas.Count];
            uint[] dvaNodePositions = new uint[request.BeamData.Dvas.Count];

            foreach (DynamicVibrationAbsorber dva in request.BeamData.Dvas)
            {
                dvaMasses[i] = dva.DvaMass;
                dvaHardnesses[i] = dva.DvaHardness;
                dvaNodePositions[i] = dva.DvaNodePosition;
                i += 1;
            }

            GeometricProperty geometricProperty = new GeometricProperty();

            if (request.BeamData.Profile.Area != default && request.BeamData.Profile.MomentOfInertia != default)
            {
                geometricProperty.Area = await this._arrayOperation.Create(request.BeamData.Profile.Area.Value, request.BeamData.NumberOfElements, nameof(request.BeamData.Profile.Area));
                geometricProperty.MomentOfInertia = await this._arrayOperation.Create(request.BeamData.Profile.MomentOfInertia.Value, request.BeamData.NumberOfElements, nameof(request.BeamData.Profile.MomentOfInertia));
            }
            else
            {
                geometricProperty = await this._profileMapper.Execute(request.BeamData.Profile, degreesFreedomMaximum);
            }

            return new BeamWithDva<TProfile>()
            {
                FirstFastening = FasteningFactory.Create(request.BeamData.FirstFastening),
                Forces = await this._mappingResolver.BuildFrom(request.BeamData.Forces, degreesFreedomMaximum),
                GeometricProperty = geometricProperty,
                LastFastening = FasteningFactory.Create(request.BeamData.LastFastening),
                Length = request.BeamData.Length,
                Material = MaterialFactory.Create(request.BeamData.Material),
                NumberOfElements = request.BeamData.NumberOfElements,
                Profile = request.BeamData.Profile,
                DvaHardnesses = dvaHardnesses,
                DvaMasses = dvaMasses,
                DvaNodePositions = dvaNodePositions
            };
        }

        public async override Task<NewmarkMethodInput> CreateInput(BeamWithDva<TProfile> beam, NewmarkMethodParameter newmarkMethodParameter, uint degreesFreedomMaximum)
        {
            bool[] bondaryCondition = await this._mainMatrix.CalculateBondaryCondition(beam.FirstFastening, beam.LastFastening, degreesFreedomMaximum, (uint)beam.DvaNodePositions.Length);
            uint numberOfTrueBoundaryConditions = 0;

            for (int i = 0; i < degreesFreedomMaximum; i++)
            {
                if (bondaryCondition[i] == true)
                {
                    numberOfTrueBoundaryConditions += 1;
                }
            }

            // Main matrixes to create input.
            double[,] mass = await this._beamMainMatrix.CalculateMass(beam, degreesFreedomMaximum);

            double[,] hardness = await this._beamMainMatrix.CalculateHardness(beam, degreesFreedomMaximum);

            double[,] massWithDva = await this._mainMatrix.CalculateMassWithDva(mass, beam.DvaMasses, beam.DvaNodePositions);

            double[,] hardnessWithDva = await this._mainMatrix.CalculateHardnessWithDva(hardness, beam.DvaHardnesses, beam.DvaNodePositions);

            double[,] dampingWithDva = await this._mainMatrix.CalculateDamping(massWithDva, hardnessWithDva);

            double[] forces = beam.Forces;

            // Creating input.
            NewmarkMethodInput input = new NewmarkMethodInput
            {
                Mass = this._auxiliarOperation.ApplyBondaryConditions(massWithDva, bondaryCondition, numberOfTrueBoundaryConditions + (uint)beam.DvaNodePositions.Length),

                Hardness = this._auxiliarOperation.ApplyBondaryConditions(hardnessWithDva, bondaryCondition, numberOfTrueBoundaryConditions + (uint)beam.DvaNodePositions.Length),

                Damping = this._auxiliarOperation.ApplyBondaryConditions(dampingWithDva, bondaryCondition, numberOfTrueBoundaryConditions + (uint)beam.DvaNodePositions.Length),

                Force = this._auxiliarOperation.ApplyBondaryConditions(forces, bondaryCondition, numberOfTrueBoundaryConditions),

                NumberOfTrueBoundaryConditions = numberOfTrueBoundaryConditions,

                NumberOfDvas = (uint)beam.DvaNodePositions.Length,

                Parameter = newmarkMethodParameter
            };

            return input;
        }
    }
}
