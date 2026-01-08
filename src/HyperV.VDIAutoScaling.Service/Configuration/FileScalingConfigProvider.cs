using HyperV.VDIAutoScaling.Core.Configuration;
using HyperV.VDIAutoScaling.Core.Models;
using System.Text.Json;

namespace HyperV.VDIAutoScaling.Service.Configuration
{
    public class FileScalingConfigProvider : IScalingConfigProvider
    {
        private readonly string _configPath;
        private ScalingConfig? _cachedConfig;
        private DateTime _lastWriteTime;
        public FileScalingConfigProvider(string ConfigPath)
        {
            _configPath = ConfigPath;
        }
        public ScalingConfig GetConfig()
        {
            var fileInfo = new FileInfo(_configPath);

            if (!fileInfo.Exists)
                throw new FileNotFoundException(
                    $"ScaliingConfig not found at {_configPath}"
                );

            if (_cachedConfig == null || fileInfo.LastWriteTimeUtc > _lastWriteTime)
            {
                var json = File.ReadAllText(_configPath);

                _cachedConfig = JsonSerializer.Deserialize<ScalingConfig>(json)
                    ?? throw new InvalidOperationException("Invalid scaling config");

                _lastWriteTime = fileInfo.LastWriteTimeUtc;
            }


            return _cachedConfig;
        }
    }
}
