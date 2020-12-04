using MNBEC.Domain;

namespace MNBEC.ApplicationInterface
{
    public interface IRoleApplication : IBaseApplication<ApplicationRole>
    {
        //TODO: Remove Comments
        //Task<IdentityResult> CreateAsync(ApplicationRole role, CancellationToken ct);
        //Task<IdentityResult> Update(ApplicationRole role, CancellationToken ct);
    }
}
