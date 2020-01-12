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
        public NewmarkMethodOutput Execute(NewmarkMethodInput input, OperationResponseBase response)
        {
            double a0 = 1.0 / (beta * dt * dt);
            double a1 = gama / (beta * dt);
            double a2 = 1.0 / (beta * dt);
            double a3 = gama / (beta);
            double a4 = 1 / (2 * beta);
            double a5 = dt * ((gama / (2 * beta)) - 1);
            double a6 = dt * (1 - gama);
            double a7 = gama * dt;

            throw new NotImplementedException();
        }
    }
}
