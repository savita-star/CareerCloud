using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;


namespace CareerCloud.BusinessLogicLayer
{
    public class CompanyJobSkillLogic : BaseLogic<CompanyJobSkillPoco>
    {
        public CompanyJobSkillLogic(IDataRepository<CompanyJobSkillPoco> repo)
            : base(repo)
        {
        }

        public override void Update(CompanyJobSkillPoco[] pocos)
        {
            Verify(pocos);
            base.Update(pocos);
        }

        public override void Add(CompanyJobSkillPoco[] pocos)
        {
            Verify(pocos);
            base.Add(pocos);
        }

        protected override void Verify(CompanyJobSkillPoco[] pocos)
        {
            List<ValidationException> errors =
                 new List<ValidationException>();
            foreach (CompanyJobSkillPoco poco in pocos)
            {
                if (poco.Importance < 0)
                {
                    errors.Add(
                        new ValidationException(400,
                        "Importance cannot be less than 0"));
                }
                if (errors.Count > 0)
                {
                    throw new AggregateException(errors);
                }
            }
        }
    }
}

