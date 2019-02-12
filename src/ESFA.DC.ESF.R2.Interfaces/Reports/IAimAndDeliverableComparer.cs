using ESFA.DC.ESF.R2.Models.Reports;

namespace ESFA.DC.ESF.R2.Interfaces.Reports
{
    public interface IAimAndDeliverableComparer
    {
        int Compare(AimAndDeliverableModel first, AimAndDeliverableModel second);
    }
}