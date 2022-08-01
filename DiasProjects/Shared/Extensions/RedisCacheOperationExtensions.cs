using StackExchange.Redis;

namespace DiasWebApi.Shared.Extensions
{
    public class RedisCacheOperationExtensions : IDisposable
    {
        private const string ClearCacheLuaScript =
         "for _,k in ipairs(redis.call('KEYS', ARGV[1])) do\n" +
         "    redis.call('DEL', k)\n" +
         "end";

        private const string GetKeysLuaScript = "return redis.call('keys', ARGV[1])";
        private readonly string _redisConString;
        private readonly string _redisKeyHeader;
        private ConnectionMultiplexer connection;
        private IDatabase cache;       
        private bool isDisposed;


        public RedisCacheOperationExtensions(string redisConString, string keyHeader)
        {
            this._redisConString = redisConString;
            this._redisKeyHeader = keyHeader;
        }

        ~RedisCacheOperationExtensions()
        {
            this.Dispose(false);
        }
        protected async Task EnsureInitialized()
        {
            if (connection == null)
            {
                this.connection = await ConnectionMultiplexer.ConnectAsync(this._redisConString);
                this.cache = this.connection.GetDatabase();
            }
        }

        public async Task ClearAsync()
        {
            this.ThrowIfDisposed();
            await this.EnsureInitialized();

            await this.cache.ScriptEvaluateAsync(ClearCacheLuaScript,
                            values: new RedisValue[]
                            {
                                 this._redisKeyHeader
                            });

        }

        public async Task<string[]> GetKeysAsync()
        {
            this.ThrowIfDisposed();
            await this.EnsureInitialized();

            var result = await this.cache.ScriptEvaluateAsync(GetKeysLuaScript,
                            values: new RedisValue[]
                            {
                                 this._redisKeyHeader
                            });      
            
            return ((RedisResult[])result).Select(x => x.ToString()).ToArray();
        }


        public async Task RemoveAsync(string[] keys)
        {
            this.ThrowIfDisposed();
            if (keys == null) { throw new ArgumentNullException(nameof(keys)); }
            await this.EnsureInitialized();
            var keysArray = keys.ToArray();

            foreach (var key in keysArray)
            {
                await this.cache.KeyDeleteAsync(key);
            }

        }

        #region Dispose
        private void Dispose(bool disposing)
        {
            if (!this.isDisposed)
            {
                if (disposing && this.connection != null)
                {
                    this.connection.Close();
                }

                this.isDisposed = true;
            }
        }

        private void ThrowIfDisposed()
        {
            if (this.isDisposed)
            {
                throw new ObjectDisposedException(nameof(RedisCacheOperationExtensions));
            }
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion Dispose

    }
}
