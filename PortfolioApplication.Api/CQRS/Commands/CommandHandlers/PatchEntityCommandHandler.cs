using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using PortfolioApplication.Entities.Entities;
using PortfolioApplication.Services;
using PortfolioApplication.Services.DatabaseContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;

namespace PortfolioApplication.Api.CQRS.Commands.CommandHandlers
{
    public abstract class PatchEntityCommandHandler<TCommand, TEntity> : ICommandHandler<TCommand, TEntity>
        where TCommand : ICommand
        where TEntity : BaseEntity
    {
        private IUnitOfWork _unitOfWork;
        private IDatabaseSet _databaseSet;
        private IDistributedCache _redisCache;
        private DbSet<TEntity> _entitySet;
        private IMapper _mapper;

        public PatchEntityCommandHandler(IDatabaseSet databaseSet, IUnitOfWork unitOfWork, IDistributedCache redisCache, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _databaseSet = databaseSet;
            _entitySet = _databaseSet.Set<TEntity>();
            _redisCache = redisCache;
            _mapper = mapper;
        }

        public void Handle(TCommand command, Expression<Func<TEntity, bool>> retrievalFunc)
        {
            throw new NotImplementedException();
        }

        public async Task HandleAsync(TCommand command, Expression<Func<TEntity, bool>> retrievalFunc)
        {
            var property = command.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public).Single(prop => prop.Name.Contains("Dto"));
            var dto = property.GetValue(command);
            TEntity entity = _mapper.Map<TEntity>(dto);

            _unitOfWork.TrackEntity(entity);
            await _unitOfWork.SaveAsync();

            string redisKey = RedisHelper.ComposeRedisKey(typeof(TEntity).Name, entity.Id.ToString());

            await _redisCache.RemoveAsync(redisKey);
            await _redisCache.RemoveAsync(RedisHelper.ComposeRedisKey(typeof(TEntity).Name, "*"));
        }
    }
}
