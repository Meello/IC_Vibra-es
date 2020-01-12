using IcVibrations.Core.DTO;
using IcVibrations.DataContracts;
using IcVibrations.Models.Beam;
using System;
using System.Collections.Generic;
using System.Text;

namespace IcVibrations.Methods.NewmarkMethod
{
    public interface INewmarkMethod
    {
        NewmarkMethodOutput Execute(NewmarkMethodInput input, OperationResponseBase response);
    }
}
