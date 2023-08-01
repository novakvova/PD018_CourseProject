namespace Microsoft.Extensions.Configuration;

public static class ConfigurationExtensions {
    public static string? GetStorage(this IConfiguration configuration, string name) {
        return configuration?.GetSection("Storage")[name];
    }
}
