using System.Threading;
using System.Threading.Tasks;
using ESFA.DC.ILR.DataService.Models;

namespace ESFA.DC.ILR.DataService.Interfaces.Repositories
{
    public interface IFileDetails1819Repository
    {
        Task<ILRFileDetails> GetLatest1819FileDetailsPerUkPrn(
            int ukPrn,
            CancellationToken cancellationToken);
    }
}