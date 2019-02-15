using System;
using ESFA.DC.ESF.R2.Database.EF;
using ESFA.DC.ESF.R2.Interfaces.DataAccessLayer;
using ESFA.DC.ESF.R2.Models;

namespace ESFA.DC.ESF.R2.DataAccessLayer.Mappers
{
    public class SupplementaryDataModelMapper : ISupplementaryDataModelMapper
    {
        public SupplementaryDataModel GetModelFromEntity(SupplementaryData entity)
        {
            return new SupplementaryDataModel
            {
                DeliverableCode = entity.DeliverableCode,
                ConRefNumber = entity.ConRefNumber,
                ULN = entity.Uln,
                CostType = entity.CostType,
                CalendarYear = entity.CalendarYear,
                CalendarMonth = entity.CalendarMonth,
                ReferenceType = entity.ReferenceType,
                Reference = entity.Reference,
                ProviderSpecifiedReference = entity.ProviderSpecifiedReference,
                StaffName = entity.StaffName,
                LearnAimRef = entity.LearnAimRef,
                SupplementaryDataPanelDate = entity.SupplementaryDataPanelDate,
                Value = entity.Value
            };
        }

        public SupplementaryDataModel GetSupplementaryDataModelFromLooseModel(SupplementaryDataLooseModel looseModel)
        {
            return new SupplementaryDataModel
            {
                ConRefNumber = looseModel.ConRefNumber,
                ULN = ConvertToNullableLong(looseModel.ULN),
                DeliverableCode = looseModel.DeliverableCode,
                CostType = looseModel.CostType,
                ReferenceType = looseModel.ReferenceType,
                Reference = looseModel.Reference,
                ProviderSpecifiedReference = looseModel.ProviderSpecifiedReference,
                CalendarMonth = ConvertToNullableInt(looseModel.CalendarMonth),
                CalendarYear = ConvertToNullableInt(looseModel.CalendarYear),
                StaffName = looseModel.StaffName,
                LearnAimRef = looseModel.LearnAimRef,
                SupplementaryDataPanelDate = ConvertToNullableDateTime(looseModel.SupplementaryDataPanelDate),
                Value = ConvertToNullableDecimal(looseModel.Value)
            };
        }

        private long? ConvertToNullableLong(string value)
        {
            return string.IsNullOrEmpty(value?.Trim()) ? (long?)null : long.Parse(value);
        }

        private int? ConvertToNullableInt(string value)
        {
            return string.IsNullOrEmpty(value?.Trim()) ? (int?)null : int.Parse(value);
        }

        private decimal? ConvertToNullableDecimal(string value)
        {
            return string.IsNullOrEmpty(value?.Trim()) ? (decimal?)null : decimal.Parse(value);
        }

        private DateTime? ConvertToNullableDateTime(string value)
        {
            return string.IsNullOrEmpty(value?.Trim()) ? (DateTime?)null : DateTime.Parse(value);
        }
    }
}