using GoBite.Application.Interfaces.Service;
using GoBite.Application.UnitOfWork;

namespace GoBite.Application.Services;

public class ProductPromotionService:IProductPromotionService
{
    private readonly IUnitOfWork unitOfWork;
    public ProductPromotionService(IUnitOfWork unitOfWork)
    {
        this.unitOfWork = unitOfWork;
    }

}
