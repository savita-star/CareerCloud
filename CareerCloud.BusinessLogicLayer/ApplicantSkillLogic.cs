using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;


namespace CareerCloud.BusinessLogicLayer
{
    public class ApplicantSkillLogic : BaseLogic<ApplicantSkillPoco>
    {
        public ApplicantSkillLogic(IDataRepository<ApplicantSkillPoco> repo)
            : base(repo)
        {
        }

        public override void Update(ApplicantSkillPoco[] pocos)
        {
            Verify(pocos);
            base.Update(pocos);
        }

        public override void Add(ApplicantSkillPoco[] pocos)
        {
            Verify(pocos);
            base.Add(pocos);
        }

        protected override void Verify(ApplicantSkillPoco[] pocos)
        {
            List<ValidationException> errors =
                 new List<ValidationException>();
            foreach (ApplicantSkillPoco poco in pocos)
            {
                if (poco.StartMonth > 12)
                {
                    errors.Add(
                        new ValidationException(101,
                        "StartMonth Cannot be greater than 12"));
                }
                if (poco.EndMonth > 12)
                {
                    errors.Add(
                        new ValidationException(102,
                        "EndMonth Cannot be greater than 12")
                        );
                }
                if (poco.StartYear < 1900)
                {
                    errors.Add(
                        new ValidationException(103,
                        "StartYear Cannot be less then 1900")
                        );
                }
                if (poco.EndYear < poco.StartYear)
                {
                    errors.Add(
                        new ValidationException(104,
                        "EndYear Cannot be less then StartYear")
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

