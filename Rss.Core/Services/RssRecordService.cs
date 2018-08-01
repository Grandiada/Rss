using Microsoft.EntityFrameworkCore;
using Rss.Core.Models;
using Rss.Core.Models.Context;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Rss.Core
{
    public sealed class RssRecordService
    {
        private readonly Func<RssDbContext> _contextFactory;

        public RssRecordService(Func<RssDbContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<IReadOnlyList<RssRecord>> GetAsync(CancellationToken cancellationToken)
        {
            using (var context = _contextFactory())
            {
                var records = await context.RssRecords.ToListAsync(cancellationToken);

                return records;
            }
        }

        public async Task<RssRecord> AddAsync(RssRecord record, CancellationToken cancellationToken)
        {
            if (record == null)
                throw new ArgumentNullException(nameof(record));
            if (record.Id > 0)
                throw new ArgumentOutOfRangeException(nameof(record));

            using (var context = _contextFactory())
            {
                await context.RssRecords.AddAsync(record, cancellationToken);
                await context.SaveChangesAsync(cancellationToken);
                return record;
            }
        }

        public async Task UpdateAsync(
            RssRecord record, IReadOnlyList<string> propertyNames, CancellationToken cancellationToken)
        {
            if (record == null)
                throw new ArgumentNullException(nameof(record));
            if (record.Id < 1)
                throw new ArgumentOutOfRangeException(nameof(record));
            if (propertyNames == null)
                throw new ArgumentNullException(nameof(propertyNames));
            if (propertyNames.Count == 0)
                throw new ArgumentOutOfRangeException(nameof(propertyNames));

            using (var context = _contextFactory())
            {
                var entity = context.RssRecords.Attach(record);

                foreach (var propertyName in propertyNames)
                {
                    var property = entity.Property(propertyName);

                    if (property == null)
                        throw new InvalidOperationException();

                    property.IsModified = true;
                }

                await context.SaveChangesAsync(cancellationToken);
            }
        }

    }
}
