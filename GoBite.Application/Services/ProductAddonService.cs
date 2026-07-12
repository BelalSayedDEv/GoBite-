using GoBite.Application.Interfaces.Service;
using GoBite.Application.UnitOfWork;

namespace GoBite.Application.Services;

public class ProductAddonService:IProductAddonService
{
    private readonly IUnitOfWork unitOfWork;

    public ProductAddonService(IUnitOfWork unitOfWork)
    {
        this.unitOfWork = unitOfWork;
    }
}
