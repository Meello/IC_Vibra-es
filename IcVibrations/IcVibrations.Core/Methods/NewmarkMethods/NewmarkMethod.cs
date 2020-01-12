using IcVibrations.Core.DTO;
using IcVibrations.DataContracts;
using IcVibrations.Models.Beam;
using System;
using System.Collections.Generic;
using System.Text;

namespace IcVibrations.Methods.NewmarkMethod
{
    public class NewmarkMethod : INewmarkMethod
    {
        public NewmarkMethodOutput Execute(Beam beam, int elementCount, int degreesFeedromMaximum, OperationResponseBase response)
        {
            NewmarkMethodOutput newmarkMethodOutput = new NewmarkMethodOutput();


            return newmarkMethodOutput;
        }
    }
}
