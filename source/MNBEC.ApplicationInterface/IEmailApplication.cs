using System.Collections.Generic;
using System.Threading.Tasks;
using MNBEC.Domain;

namespace MNBEC.ApplicationInterface
{
    /// <summary>
    /// IEmailApplication provide interface for Email related Application.
    /// </summary>
    public interface IEmailApplication
    {


        Task<bool> SendEmployeeCreationEmail(ApplicationUser employee, bool useLanguage);

        Task<bool> EmployeeSendForgotPasswordEmail(ApplicationUser employee, bool useLanguage);

        Task<bool> DealerSendForgotPasswordEmail(ApplicationUser dealer, bool useLanguage);

        Task<bool> SendDealerCreationEmail(ApplicationUser dealer, bool useLanguage);



        Task<List<EmailTemplate>> GetAllEmailTemplate();

    }
}
