using ESFA.DC.ESF.Database.EF.Interfaces;

namespace ESFA.DC.ESF.R2.Database.EF
{
    public partial class ESF_R2Entities : IESF_R2Entities
    {
        public ESF_R2Entities(string connectionString)
            : base(connectionString)
        {
        }
    }
}