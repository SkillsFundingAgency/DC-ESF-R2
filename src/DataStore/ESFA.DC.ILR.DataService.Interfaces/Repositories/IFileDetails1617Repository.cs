using System.Threading;
using System.Threading.Tasks;
using ESFA.DC.ILR.DataService.Models;

namespace ESFA.DC.ILR.DataService.Interfaces.Repositories
{
    public interface IFileDetails1617Repository
    {
        Task<ILRFileDetails> GetLatest1617FileDetailsPerUkPrn(
            int ukPrn,
            CancellationToken cancellationToken);
    }
}