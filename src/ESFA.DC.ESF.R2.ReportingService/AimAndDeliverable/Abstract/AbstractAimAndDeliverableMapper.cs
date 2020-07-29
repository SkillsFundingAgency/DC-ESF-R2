using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper.Configuration;
using ESFA.DC.ESF.R2.Interfaces;
using ESFA.DC.ESF.R2.ReportingService.AimAndDeliverable.Model;

namespace ESFA.DC.ESF.R2.ReportingService.AimAndDeliverable.Abstract
{
    public abstract class AbstractAimAndDeliverableMapper : ClassMap<AimAndDeliverableReportRow>, IClassMapper
    {
    }
}
