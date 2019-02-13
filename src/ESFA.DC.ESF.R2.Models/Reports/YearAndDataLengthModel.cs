namespace ESFA.DC.ESF.R2.Models.Reports
{
    public sealed class YearAndDataLengthModel
    {
        public YearAndDataLengthModel(int year, int dataLength)
        {
            Year = year;
            DataLength = dataLength;
        }

        public int Year { get; }

        public int DataLength { get; }
    }
}
