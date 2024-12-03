using Catalog.Domain.Categories;
using Catalog.Infrastructure.Patterns;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Catalog.Application.CategoryCommandQuery.Command;

public class DeleteCategoryCommand : IRequest<DeleteCategoryCommandRespond>
{
    public string? Title { get; set; }
}
public class DeleteCategoryCommandRespond
{
    public string? Message { get; set; }

}

public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, DeleteCategoryCommandRespond>
{
    private readonly ILogger<DeleteCategoryCommandHandler> _logger;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteCategoryCommandHandler(ILogger<DeleteCategoryCommandHandler> logger, ICategoryRepository categoryRepository, IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _categoryRepository = categoryRepository;
        _unitOfWork = unitOfWork;
    }
    public async Task<DeleteCategoryCommandRespond> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
    {
        if(!await _categoryRepository.Exists(request.Title))
        {
            _logger.LogWarning("Record With This Name Not Found!, Name : {0}", request.Title);
            return new DeleteCategoryCommandRespond { Message = "Record With This Name Not Found!" };
        }
        _categoryRepository.Delete(request.Title);
        await _unitOfWork.SaveChanges();
        _logger.LogInformation("Record with Name : {0} from Database deleted!", request.Title);
        return new DeleteCategoryCommandRespond { Message = "Done" };

    }
}
