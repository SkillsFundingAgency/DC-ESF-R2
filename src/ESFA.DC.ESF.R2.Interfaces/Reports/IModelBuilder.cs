namespace ESFA.DC.ESF.R2.Interfaces.Reports
{
    public interface IModelBuilder<out T>
    {
        T Build(IEsfJobContext esfJobContext);
    }
}
