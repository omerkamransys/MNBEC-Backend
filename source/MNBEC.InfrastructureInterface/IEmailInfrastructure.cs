using System.Collections.Generic;
using System.Threading.Tasks;
using MNBEC.Domain;

namespace MNBEC.InfrastructureInterface
{
    /// <summary>
    /// IEmailInfrastructure provides the interface for Email operations in Databasse.
    /// </summary>
    public interface IEmailInfrastructure
    {
        Task<EmailTemplate> Get(EmailTemplate emailRequest);
        Task<List<EmailTemplate>> GetAllEmailTemplate();
    }
}