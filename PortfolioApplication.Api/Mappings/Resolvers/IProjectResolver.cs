using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PortfolioApplication.Api.Mappings.Resolvers
{
    public interface IProjectResolver<in TSource, in TDestination, TDestMember>
    {
        Task<IList<TDestMember>> Resolve(TSource source, TDestination destination, TDestMember destMember, ResolutionContext context);
    }
}
