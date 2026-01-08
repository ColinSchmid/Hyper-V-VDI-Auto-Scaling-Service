using HyperV.VDIAutoScaling.Core.Configuration;
using HyperV.VDIAutoScaling.Core.Models;
using System.Text.Json;

namespace HyperV.VDIAutoScaling.Service.Configuration
{
    public class FileScalingConfigProvider : IScalingConfigProvider
    {
        private readonly string _configPath;
        private ScalingConfig? _cachedConfig;
        public FileScalingConfigProvider(string ConfigPath)
        {
            _configPath = ConfigPath;
        }
        public ScalingConfig GetConfig()
        {
            if(_cachedConfig != null)
                return _cachedConfig;

            if (!File.Exists(_configPath))
                throw new FileNotFoundException(
                    $"ScaliingConfig not found at {_configPath}"
                );

            var json = File.ReadAllText(_configPath);

            _cachedConfig = JsonSerializer.Deserialize<ScalingConfig>(json)
                ?? throw new InvalidOperationException("Invalid scaling config");

            return _cachedConfig;
        }
    }
}
