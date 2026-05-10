using AutoMapper;
using DormitoryManagement.Domain.Common;

namespace DormitoryManagement.Application.Mappings
{
    public static class MappingExtensions
    {
        public static PagedResult<TDestination> MapToPagedResult<TSource, TDestination>(
            this PagedResult<TSource> source, IMapper mapper)
        {
            var mappedItems = mapper.Map<IEnumerable<TDestination>>(source.Items);

            return new PagedResult<TDestination>(
                mappedItems,
                source.TotalCount,
                source.PageNumber,
                source.PageSize
            );
        }
    }
}