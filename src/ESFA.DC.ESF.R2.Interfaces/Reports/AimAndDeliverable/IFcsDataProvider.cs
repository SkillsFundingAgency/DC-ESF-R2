﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ESFA.DC.ESF.R2.Models.AimAndDeliverable;

namespace ESFA.DC.ESF.R2.Interfaces.Reports.AimAndDeliverable
{
    public interface IFcsDataProvider
    {
        Task<ICollection<FCSDeliverableCodeMapping>> GetFcsDeliverableCodeMappingsAsync(
            CancellationToken cancellationToken);
    }
}
