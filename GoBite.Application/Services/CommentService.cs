using GoBite.Application.Interfaces.Service;
using GoBite.Application.UnitOfWork;

namespace GoBite.Application.Services;

public class CommentService:ICommentService
{
    private readonly IUnitOfWork unitOfWork;

    public CommentService(IUnitOfWork unitOfWork)
    {
        this.unitOfWork = unitOfWork;
    }


}
