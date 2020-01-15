using Xunit;

namespace IcVibrationsTest.Validators.BeamRequest.TestDatas
{
    // TheoryData<diameter, thickness, errorCount>
    public class ValidateRetangularInputData : TheoryData<double, double, int>
    {
        public ValidateRetangularInputData()
        {
            Add(0, 0, 1);
            Add(double.MinValue, double.MaxValue, 2);
            Add(-0.002, -0.0009, 2);
            Add(double.MaxValue, double.MaxValue, 1);
            Add(double.MinValue, double.MinValue, 1);
            Add(-0.002, -0.002, 1);
            Add(double.MaxValue, double.MinValue, 0);
            Add(0.002, 0.001, 0);
        }
    }
}