using Lmss.Models.Configuration;
using Microsoft.Extensions.Logging;
namespace Lmss.Hosting;

/// <summary>
///     Factory for creating LmssService instances.
/// </summary>
public static class ServiceFactory {
    /// <summary>
    ///     Creates a new LmssService with default settings.
    /// </summary>
    public static LmssService Create(ILogger? logger = null) {
        var client = ClientFactory.Create();
        return new LmssService( client, logger );
    }

    /// <summary>
    ///     Creates a new LmssService with custom settings.
    /// </summary>
    public static LmssService Create(LmssSettings settings, ILogger? logger = null) {
        var client = ClientFactory.Create( settings );
        return new LmssService( client, logger );
    }

    /// <summary>
    ///     Creates a new LmssService with settings configuration.
    /// </summary>
    public static LmssService Create(Action<LmssSettings> configure, ILogger? logger = null) {
        var settings = new LmssSettings();
        configure( settings );
        var client = ClientFactory.Create( settings );
        return new LmssService( client, logger );
    }
}