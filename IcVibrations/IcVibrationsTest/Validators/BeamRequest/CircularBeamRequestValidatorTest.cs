using FluentAssertions;
using IcVibrations.Core.Validators.BeamRequest;
using IcVibrations.DataContracts;
using IcVibrations.DataContracts.Beam;
using IcVibrationsTest.Validators.BeamRequest.TestDatas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Xunit;

namespace IcVibrationsTest.Validators.BeamRequest
{
    public class CircularBeamRequestValidatorTest : CircularBeamRequestValidator
    {
        [Theory]
        [ClassData(typeof(ValidateRetangularInputData))]
        public void ValidateShapeInput_Should_AddError_When_ReceiveZero_And_DoNothing_Otherwise(double diameter, double thickness, int errorCount)
        {
            // Arrange
            OperationResponseBase responseBase = new OperationResponseBase();
            CircularBeamRequestData requestData = new CircularBeamRequestData
            {
                Diameter = diameter,
                Thickness = thickness
            };

            // Act
            this.ValidateShapeInput(requestData, responseBase);

            // Assert
            responseBase.Errors.Count
                .Should()
                .Be(errorCount);
        }
    }
}
