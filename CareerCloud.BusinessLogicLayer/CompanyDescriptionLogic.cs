using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;

namespace CareerCloud.BusinessLogicLayer
{
    public class CompanyDescriptionLogic : BaseLogic<CompanyDescriptionPoco>
    {
        public CompanyDescriptionLogic(IDataRepository<CompanyDescriptionPoco> repo)
            : base(repo)
        {
        }

        public override void Update(CompanyDescriptionPoco[] pocos)
        {
            Verify(pocos);
            base.Update(pocos);
        }

        public override void Add(CompanyDescriptionPoco[] pocos)
        {
            Verify(pocos);
            base.Add(pocos);
        }

        protected override void Verify(CompanyDescriptionPoco[] pocos)
        {
            List<ValidationException> errors =
                 new List<ValidationException>();
            foreach (CompanyDescriptionPoco poco in pocos)
            {
                if ((poco?.CompanyDescription?.Length ?? 0) <= 2)
                {
                    errors.Add(
                        new ValidationException(107,
                        "CompanyDescription must be greater than 2 characters"));
                }
                if ((poco?.CompanyName?.Length ?? 0) <= 2)
                {
                    errors.Add(
                        new ValidationException(106,
                        "CompanyName must be greater than 2 characters"));
                }
                if (errors.Count > 0)
                {
                    throw new AggregateException(errors);
                }
            }
        }
    }
}