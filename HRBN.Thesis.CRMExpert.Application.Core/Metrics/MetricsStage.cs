namespace HRBN.Thesis.CRMExpert.Application.Core.Metrics
{
    public class MetricsStage
    {
        public MetricsStage(string name, long elapsedMilliseconds)
        {
            Name = name;
            ElapsedMilliseconds = elapsedMilliseconds;
        }

        public string Name { get; }
        public long ElapsedMilliseconds { get; }
    }
}