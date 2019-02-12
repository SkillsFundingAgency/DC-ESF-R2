﻿using System.Reflection;

namespace ESFA.DC.ESF.R2.Models.Generation
{
    public sealed class ModelProperty
    {
        public ModelProperty(string[] names, PropertyInfo methodInfo)
        {
            Names = names;
            MethodInfo = methodInfo;
        }

        public string[] Names { get; }

        public PropertyInfo MethodInfo { get; }
    }
}
