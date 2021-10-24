using HRBN.Thesis.CRMExpert.Domain;

namespace HRBN.Thesis.CRMExpert.Infrastructure.Repositories
{
    public sealed class UnitOfWork : IUnitOfWork
    {
        private readonly CRMContext _context;

        public UnitOfWork(CRMContext context)
        {
            _context = context;
            SampleRepository = new SampleRepository(context);
        }
        
        public void Dispose()
        {
            _context.Dispose();
        }

        public ISampleRepository SampleRepository { get; }
        
        public void Commit()
        {
            _context.SaveChanges();
        }
    }
}