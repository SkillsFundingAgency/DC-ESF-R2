﻿using System.Collections.Generic;
using ESFA.DC.ESF.R2.Models;

namespace ESFA.DC.ESF.R2.Interfaces.Validation
{
    public interface ILooseValidatorCommand
    {
        IList<ValidationErrorModel> Errors { get; }

        bool RejectFile { get; }

        int Priority { get; }

        bool Execute(SupplementaryDataLooseModel model);
    }
}