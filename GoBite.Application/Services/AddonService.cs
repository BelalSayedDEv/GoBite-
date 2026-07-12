using GoBite.Application.Interfaces.Service;
using GoBite.Application.UnitOfWork;

namespace GoBite.Application.Services;

public class AddonService: IAddonService
{
    private readonly IUnitOfWork unitOfWork;

    public AddonService(IUnitOfWork unitOfWork)
    {
        this.unitOfWork = unitOfWork;
    }
}
