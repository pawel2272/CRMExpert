using System;

namespace HRBN.Thesis.CRMExpert.Application.Core.Metrics
{
    public interface IMetricsService
    {
        void SetContext(Guid context);

        void StartRequest(Guid correlatioinId);

        void SetCheckpoint(Guid correlationId);

        void EndCheckpoint(Guid correlationId, string name);

        void SaveTime(Guid correlationId, string name, Action action);

        MetricsRequest EndRequest(Guid correlationId, string lastStageName);
    }
}