using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warden.Core.Extensions
{
    public static class IEnumerableExtensions
    {
        public static IEnumerable<IEnumerable<TSource>> Batch<TSource>(this IEnumerable<TSource> items, int batchSize)
        {
            TSource[] bucket = null;
            int count = 0;

            foreach(var item in items)
            {
                if (bucket == null)
                    bucket = new TSource[batchSize];

                bucket[count++] = item;

                if (count < batchSize)
                    continue;

                yield return bucket.Select(x => x);

                bucket = null;
                count = 0;
            }

            if (bucket != null && count > 0)
                yield return bucket.Take(count);
        }
    }
}
