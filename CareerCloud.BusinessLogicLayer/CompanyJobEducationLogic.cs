using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;

namespace CareerCloud.BusinessLogicLayer
{
    public class CompanyJobEducationLogic : BaseLogic<CompanyJobEducationPoco>
    {
        public CompanyJobEducationLogic(IDataRepository<CompanyJobEducationPoco> repo)
            : base(repo)
        {
        }

        public override void Update(CompanyJobEducationPoco[] pocos)
        {
            Verify(pocos);
            base.Update(pocos);
        }

        public override void Add(CompanyJobEducationPoco[] pocos)
        {
            Verify(pocos);
            base.Add(pocos);
        }

        protected override void Verify(CompanyJobEducationPoco[] pocos)
        {
            List<ValidationException> errors =
                 new List<ValidationException>();
            foreach (CompanyJobEducationPoco poco in pocos)
            {

                if ((poco?.Major?.Length ?? 0) < 3)
                {
                    errors.Add(
                       new ValidationException(200,
                       "Major must be at least 2 characters"));
                }
                if ((poco?.Importance ?? -1) < 0)
                {
                    errors.Add(
                        new ValidationException(201,
                        "Importance cannot be empty"));
                }
                if (errors.Count > 0)
                {
                    throw new AggregateException(errors);
                }
            }
        }
    }
}
