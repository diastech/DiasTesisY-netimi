using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiasWebApi.Shared.Constants
{
    public static class RedisCacheConstants
    {
        public static DistributedCacheEntryOptions LocationV2RedisCacheEntryOptions = new DistributedCacheEntryOptions()
                                { SlidingExpiration = TimeSpan.FromDays(7), AbsoluteExpiration = DateTime.Now.AddMonths(1) };

        public static DistributedCacheEntryOptions CombinedUserAndAssignmentGroupRedisCacheEntryOptions = new DistributedCacheEntryOptions()
                                { SlidingExpiration = TimeSpan.FromDays(7), AbsoluteExpiration = DateTime.Now.AddMonths(1) };

        public static DistributedCacheEntryOptions userRedisCacheEntryOptions = new DistributedCacheEntryOptions()
                                { SlidingExpiration = TimeSpan.FromDays(7), AbsoluteExpiration = DateTime.Now.AddMonths(1) };

        public static DistributedCacheEntryOptions ticketReasonRedisCacheEntryOptions = new DistributedCacheEntryOptions()
                                { SlidingExpiration = TimeSpan.FromDays(7), AbsoluteExpiration = DateTime.Now.AddMonths(1) };

        public static DistributedCacheEntryOptions customTicketReasonRedisCacheEntryOptions = new DistributedCacheEntryOptions()
                                { SlidingExpiration = TimeSpan.FromDays(7), AbsoluteExpiration = DateTime.Now.AddMonths(1) };

        public static DistributedCacheEntryOptions customLocationV2LastNodeFindRedisCacheEntryOptions = new DistributedCacheEntryOptions()
                                { SlidingExpiration = TimeSpan.FromDays(1000), AbsoluteExpiration = DateTime.Now.AddYears(4) };

        public static DistributedCacheEntryOptions customTicketReasonLastNodeFindRedisCacheEntryOptions = new DistributedCacheEntryOptions()
        { SlidingExpiration = TimeSpan.FromDays(7), AbsoluteExpiration = DateTime.Now.AddMonths(1) };
    }
}
