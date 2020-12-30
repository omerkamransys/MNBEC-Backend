using System.Threading.Tasks;
using MNBEC.Domain;
using MNBEC.Domain.Common;

namespace MNBEC.ApplicationInterface
{
    /// <summary>
    /// IQuestionnaireTemplateApplication inherits IBaseApplication interface to provide interface for QuestionnaireTemplate related Application.
    /// </summary>
    public interface IQuestionnaireTemplateApplication : IBaseApplication<QuestionnaireTemplate>
    {
    }
}
