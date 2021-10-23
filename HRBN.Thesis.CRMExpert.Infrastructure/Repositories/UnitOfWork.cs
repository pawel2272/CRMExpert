namespace HRBN.Thesis.CRMExpert.Infrastructure.Repositories
{
    public sealed class UnitOfWork : IUnitOfWork
    {
        private readonly DatabaseContext _context;

        public UnitOfWork(DatabaseContext context)
        {
            _context = context;
            SampleRepository = new SampleRepository(context);
        }
        
        public void Dispose()
        {
            _context.Dispose();
        }

        public ISampleRepository SampleRepository { get; }
        
        public async Commit()
        {
            _context.SaveChanges();
        }
    }
}