using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;

namespace CareerCloud.BusinessLogicLayer
{
    public class ApplicantJobApplicationLogic : BaseLogic<ApplicantJobApplicationPoco>
    {
        public ApplicantJobApplicationLogic(IDataRepository<ApplicantJobApplicationPoco> repo)
            : base(repo)
        {
        }

        public override void Update(ApplicantJobApplicationPoco[] pocos)
        {
            Verify(pocos);
            base.Update(pocos);
        }

        public override void Add(ApplicantJobApplicationPoco[] pocos)
        {
            Verify(pocos);
            base.Add(pocos);
        }

        protected override void Verify(ApplicantJobApplicationPoco[] pocos)
        {
            List<ValidationException> errors =
                 new List<ValidationException>();
            foreach (ApplicantJobApplicationPoco poco in pocos)
            {
                if (poco.ApplicationDate > DateTime.Now)
                {
                    errors.Add(
                        new ValidationException(110,
                        "ApplicationDate cannot be greater than today"));
                }
                if (errors.Count > 0)
                {
                    throw new AggregateException(errors);
                }
            }
        }
    }
}
