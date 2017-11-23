namespace PortfolioApplication.Services
{
    public static class RedisHelper
    {
        public static string ComposeRedisKey(string entityTypeName, string id)
        {
            return entityTypeName + ":" + id;
        }
    }
}
