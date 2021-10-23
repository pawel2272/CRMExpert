using System;
using System.Diagnostics;
using Microsoft.Extensions.Logging;

namespace HRBN.Thesis.CRMExpert.Application.Core.Metrics
{
    public class MetricsService : IMetricsService
    {
        private readonly ILogger<MetricsService> _logger;
        private readonly ICacheService _cache;

        public MetricsService(ILogger<MetricsService> logger, ICacheService cache)
        {
            _logger = logger;
            _cache = cache;
        }
        
        public void SetContext(Guid context)
        {
            throw new NotImplementedException();
        }

        public void StartRequest(Guid correlatioinId)
        {
            throw new NotImplementedException();
        }

        public void SetCheckpoint(Guid correlationId)
        {
            var record = GetRecord(correlationId);
            record.SetCheckpoint();
            SaveRecord(record);
        }

        public void EndCheckpoint(Guid correlationId, string name)
        {
            var record = GetRecord(correlationId);
            var stage = record.EndCheckpoint(name);
            SaveRecord(record);

            LogStage(correlationId, stage);
        }

        public void SaveTime(Guid correlationId, string name, Action action)
        {
            var watch = new Stopwatch();
            watch.Start();

            action();
            
            watch.Stop();
            var elapsedMilliseconds = watch.ElapsedMilliseconds;

            var record = GetRecord(correlationId);
            var stage = record.AddStage(name, elapsedMilliseconds);
            SaveRecord(record);

            LogStage(correlationId, stage);
        }

        public MetricsRequest EndRequest(Guid correlationId, string lastStageName)
        {
            var record = GetRecord(correlationId);
            var lastStage = record.End(lastStageName);
            DeleteRecord(correlationId);

            LogStage(correlationId, lastStage);

            _logger.LogInformation(
                "[{LogType}]-[{MetricsType}] {ElapsedMilliseconds} ms ({CorrelationId})",
                "Metrics",
                "Request",
                record.ElapsedMilliseconds,
                correlationId);

            return record;
        }

        private void SaveRecord(MetricsRequest request) =>
            _cache.Set($"metrics-{request.CorrelationId}", request, TimeSpan.FromMinutes(5));

        private MetricsService GetRecord(Guid correlationId) => _cache.Get<MetricsRequest>($"metrics-{correlationId}");

        private void DeleteRecord(Guid correlationId) => _cache.Remove($"metrics-{correlationId}");

        private void LogStage(Guid correlationId, MetricsStage stage) => _logger.LogInformation(
            "[{LogType}]-[{MetricsType}] {StageName} {ElapsedMilliseconds} ms ({CorrelationId})",
            "Metrics",
            "Stage",
            stage.Name,
            stage.ElapsedMilliseconds,
            correlationId);
    }
}