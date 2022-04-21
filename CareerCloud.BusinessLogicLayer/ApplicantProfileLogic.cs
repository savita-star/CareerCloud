using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;

namespace CareerCloud.BusinessLogicLayer
{
    public class ApplicantProfileLogic : BaseLogic<ApplicantProfilePoco>
    {
        public ApplicantProfileLogic(IDataRepository<ApplicantProfilePoco> repo)
            : base(repo)
        {
        }

        public override void Update(ApplicantProfilePoco[] pocos)
        {
            Verify(pocos);
            base.Update(pocos);
        }

        public override void Add(ApplicantProfilePoco[] pocos)
        {
            Verify(pocos);
            base.Add(pocos);
        }

        protected override void Verify(ApplicantProfilePoco[] pocos)
        {
            List<ValidationException> errors =
                 new List<ValidationException>();
            foreach (ApplicantProfilePoco poco in pocos)
            {

                if (poco.CurrentSalary < 0)
                {
                    errors.Add(
                       new ValidationException(111,
                       "CurrentSalary cannot be negative"));
                }
                if (poco.CurrentRate < 0)
                {
                    errors.Add(
                        new ValidationException(112,
                        "CurrentRate cannot be negative"));
                }
                if (errors.Count > 0)
                {
                    throw new AggregateException(errors);
                }
            }
        }
    }
}