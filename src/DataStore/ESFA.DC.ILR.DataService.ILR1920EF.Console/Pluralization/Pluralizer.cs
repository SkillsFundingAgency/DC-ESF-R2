﻿using Microsoft.EntityFrameworkCore.Design;

namespace ESFA.DC.ILR.DataService.ILR1920EF.Console.Pluralization
{
    public class Pluralizer : IPluralizer
    {
        public string Pluralize(string name)
        {
            return name.Pluralize() ?? name;
        }

        public string Singularize(string name)
        {
            return name.Singularize() ?? name;
        }
    }
}
