namespace ESFA.DC.ESF.R2.Interfaces.Validation
{
    public interface IBaseValidator
    {
        string ErrorName { get; }

        bool IsWarning { get; }

        string ErrorMessage { get; }
    }
}