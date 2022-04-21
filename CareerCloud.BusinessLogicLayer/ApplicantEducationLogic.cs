using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;

namespace CareerCloud.BusinessLogicLayer
{
    public class ApplicantEducationLogic : BaseLogic<ApplicantEducationPoco>
    {
        public ApplicantEducationLogic(IDataRepository<ApplicantEducationPoco> repo)
            : base(repo)
        {
        }

        public override void Update(ApplicantEducationPoco[] pocos)
        {
            Verify(pocos);
            base.Update(pocos);
        }

        public override void Add(ApplicantEducationPoco[] pocos)
        {
            Verify(pocos);
            base.Add(pocos);
        }

        protected override void Verify(ApplicantEducationPoco[] pocos)
        {
            List<ValidationException> errors =
                 new List<ValidationException>();
            foreach (ApplicantEducationPoco poco in pocos)
            {
                if (string.IsNullOrEmpty(poco.Major))
                {
                    errors.Add(
                        new ValidationException(107,
                        "Cannot be empty or less than 3 characters"));
                }
                else if (poco.Major.Length < 3)
                {
                    errors.Add(
                       new ValidationException(107,
                       "Cannot be empty or less than 3 characters"));
                }
                if (poco.StartDate > DateTime.Now)
                {
                    errors.Add(
                        new ValidationException(108,
                        "StartDate Cannot be greater than today"));
                }
                if (poco.StartDate > poco.CompletionDate)
                {
                    errors.Add(
                        new ValidationException(109,
                        "CompletionDate cannot be earlier than StartDate")
                        );
                }
                if (errors.Count > 0)
                {
                    throw new AggregateException(errors);
                }
            }
        }
    }
}
