using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;

namespace CareerCloud.BusinessLogicLayer
{
    public abstract class SystemCountryCodeBase<TPoco>
        where TPoco : ICountry
    {
        protected IDataRepository<TPoco> _repository;
        public SystemCountryCodeBase(IDataRepository<TPoco> repository)
        {
            _repository = repository;
        }

        protected virtual void Verify(TPoco[] pocos)
        {
            return;
        }

        public virtual TPoco Get(string Code)
        {
            return _repository.GetSingle(c => c.Code == Code);
        }

        public virtual List<TPoco> GetAll()
        {
            return _repository.GetAll().ToList();
        }

        public virtual void Add(TPoco[] pocos)
        {
            foreach (TPoco poco in pocos)
            {
                if (poco.Code == string.Empty)
                {
                    poco.Code = Guid.NewGuid().ToString();
                }
            }

            _repository.Add(pocos);
        }

        public virtual void Update(TPoco[] pocos)
        {
            _repository.Update(pocos);
        }

        public void Delete(TPoco[] pocos)
        {
            _repository.Remove(pocos);
        }
    }
}