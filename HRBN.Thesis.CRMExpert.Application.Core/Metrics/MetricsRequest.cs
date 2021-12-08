using System;

namespace HRBN.Thesis.CRMExpert.Application.Core.Metrics
{
    public class MetricsRequest
    {
        public Guid CorrelationId { get; set; }
        public long ElapsedMilliseconds { get; set; }

        public MetricsStage End(string lastStageName)
        {
            throw new NotImplementedException();
        }

        public MetricsStage AddStage(string name, long elapsedMilliseconds)
        {
            throw new NotImplementedException();
        }

        public void SetCheckpoint()
        {
            
        }

        public MetricsStage EndCheckpoint(string name)
        {
            throw new NotImplementedException();
        }
    }
}