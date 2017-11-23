namespace PortfolioApplication.Services
{
    public static class RedisHelpers
    {
        public static string ComposeRedisKey(string entityTypeName, string id)
        {
            return entityTypeName + ":" + id;
        }
    }
}
