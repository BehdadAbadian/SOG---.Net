using Catalog.Domain.Categories;
using Catalog.Infrastructure.Patterns;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Catalog.Application.CategoryCommandQuery.Query;

public class GetAllCategoryQuery : IRequest<List<GetAllCategoryQueryRespond>>
{

}
public class GetAllCategoryQueryRespond
{
    public Guid Id { get; set; }
    public string ?Title { get; set; }
    public string ?Description { get; set; }
    public string ?CreationDate { get; set; }
}

public class GetAllCategoryQueryHandler : IRequestHandler<GetAllCategoryQuery, List<GetAllCategoryQueryRespond>>
{
    private readonly ILogger<GetAllCategoryQueryHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICategoryRepository _categoryRepository;

    public GetAllCategoryQueryHandler(ILogger<GetAllCategoryQueryHandler> logger, ICategoryRepository categoryRepository)
    {
        _logger = logger;
        _categoryRepository = categoryRepository;
    }

    public async Task<List<GetAllCategoryQueryRespond>> Handle(GetAllCategoryQuery request, CancellationToken cancellationToken)
    {
        var categories = await _categoryRepository.GetAll();
        List<GetAllCategoryQueryRespond> getAllCategoryQueryResponds = new List<GetAllCategoryQueryRespond>();
        categories.ForEach(item =>
        {
            var i = new GetAllCategoryQueryRespond
            {
                Id = item.Id.Value,
                Title = item.Title,
                Description = item.Description,
                CreationDate = item.CreationDate.ToString()
            };
            getAllCategoryQueryResponds.Add(i);
        });
        _logger.LogInformation("Create list of All Categories");
        return getAllCategoryQueryResponds;
    }
}
