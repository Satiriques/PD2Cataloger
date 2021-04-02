using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Linq
{
    public static class LinqExtensions
    {
        public static IEnumerable<TUnderlyingValue> GetValueOrEmpty<TKey, TUnderlyingValue, TValue>
              (this IDictionary<TKey, TValue> source, TKey key) where TValue : IEnumerable<TUnderlyingValue>
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            if(key == null)
            {
                return Enumerable.Empty<TUnderlyingValue>();
            }

            return source.TryGetValue(key, out TValue retVal) ? retVal : Enumerable.Empty<TUnderlyingValue>();
        }
    }
}
