using System.Collections.Generic;
using CsvHelper.Configuration;
using ESFA.DC.ESF.R2.Models.Generation;

namespace ESFA.DC.ESF.R2.Interfaces.Services
{
    public interface IValueProvider
    {
        void GetFormattedValue(List<object> values, object value, ClassMap mapper, ModelProperty modelProperty);
    }
}
