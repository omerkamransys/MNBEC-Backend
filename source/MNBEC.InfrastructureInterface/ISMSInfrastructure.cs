using System.Threading.Tasks;
using MNBEC.Domain;

namespace MNBEC.InfrastructureInterface
{
    /// <summary>
    /// IEmailInfrastructure provides the interface for Email operations in Databasse.
    /// </summary>
    public interface ISMSInfrastructure
    {
        Task<SMS> Get(SMS emailRequest);
    }
}