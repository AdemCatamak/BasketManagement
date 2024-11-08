﻿using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BasketManagement.Shared.Domain;
using BasketManagement.Shared.Domain.DomainMessageBroker;

namespace BasketManagement.Shared.Infrastructure.Db
{
    public class EfAppDbContext : DbContext
    {
        private readonly IEnumerable<IEntityTypeConfigurationAssembly> _configurationAssemblies;
        private readonly IDomainMessageBroker _domainMessageBroker;

        public EfAppDbContext(DbContextOptions<EfAppDbContext> options,
                              IEnumerable<IEntityTypeConfigurationAssembly> configurationAssemblies,
                              IDomainMessageBroker domainMessageBroker
        )
            : base(options)
        {
            _configurationAssemblies = configurationAssemblies;
            _domainMessageBroker = domainMessageBroker;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (IEntityTypeConfigurationAssembly entityTypeConfigurationAssembly in _configurationAssemblies)
            {
                modelBuilder.ApplyConfigurationsFromAssembly(entityTypeConfigurationAssembly.Assembly);
            }

            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            PublishDomainEventsAsync(CancellationToken.None).ConfigureAwait(false)
                                                            .GetAwaiter().GetResult();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = new CancellationToken())
        {
            await PublishDomainEventsAsync(cancellationToken);
            int result = await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
            return result;
        }

        private async Task PublishDomainEventsAsync(CancellationToken cancellationToken)
        {
            while (TryRemoveDomainEvent(out IDomainEvent? domainEvent))
            {
                if (domainEvent == null) continue;
                await _domainMessageBroker.PublishAsync(domainEvent, cancellationToken);
            }
        }

        private bool TryRemoveDomainEvent(out IDomainEvent? domainEvent)
        {
            DomainEventHolder? domainEventHolder = ChangeTracker
                                                  .Entries()
                                                  .Where(entry => entry.Entity is DomainEventHolder)
                                                  .Select(entry =>
                                                          {
                                                              var model = (DomainEventHolder) entry.Entity;
                                                              return model;
                                                          })
                                                  .FirstOrDefault(x => x.DomainEvents.Any());

            if (domainEventHolder == null)
            {
                domainEvent = null;
                return false;
            }

            return domainEventHolder.TryRemoveDomainEvent(out domainEvent);
        }
    }
}