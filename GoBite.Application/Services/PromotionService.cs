using GoBite.Application.Interfaces.Service;
using GoBite.Application.UnitOfWork;

namespace GoBite.Application.Services;

public class PromotionService:IPromotionService
{
    private readonly IUnitOfWork unitOfWork;
    public PromotionService(IUnitOfWork unitOfWork)
    {
        this.unitOfWork = unitOfWork;
    }
}
