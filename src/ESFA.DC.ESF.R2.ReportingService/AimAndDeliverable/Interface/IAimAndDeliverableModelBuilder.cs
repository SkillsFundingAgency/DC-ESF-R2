using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ESFA.DC.ESF.R2.Models;
using ESFA.DC.ESF.R2.ReportingService.AimAndDeliverable.Model;

namespace ESFA.DC.ESF.R2.ReportingService.AimAndDeliverable.Interface
{
    public interface IAimAndDeliverableModelBuilder
    {
        IEnumerable<AimAndDeliverableReportRow> Build();
    }
}
