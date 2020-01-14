using IcVibrations.Core.Validators.BeamRequest;
using IcVibrations.DataContracts;
using IcVibrations.DataContracts.Beam;
using IcVibrationsTest.Validators.BeamRequest.TestDatas;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace IcVibrationsTest.Validators.BeamRequest
{
    public class CircularBeamRequestValidatorTest
    {
        private readonly CircularBeamRequestValidator _validator;

        public CircularBeamRequestValidatorTest()
        {
            this._validator = new CircularBeamRequestValidator();
        }

        [Theory]
        [ClassData(typeof(ValidateRetangularInputData))]
        public void ValidateShapeInput_Should_AddError_When_ReceiveZero_And_DoNothing_Otherwise(double diameter, double thickness, int errorCount)
        {
            // Arrange
            OperationResponseBase response = new OperationResponseBase();
            CircularBeamRequestData requestData = new CircularBeamRequestData
            {
                Diameter = diameter,
                Thickness = thickness
            };

            //http://anthonygiretti.com/2018/08/26/how-to-unit-test-private-methods-in-net-core-applications-even-if-its-bad/

            // Act

            // Assert
        }
    }
}
