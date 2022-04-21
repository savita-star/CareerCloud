using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System.Text.RegularExpressions;

namespace CareerCloud.BusinessLogicLayer
{
    public class CompanyProfileLogic : BaseLogic<CompanyProfilePoco>
    {
        public CompanyProfileLogic(IDataRepository<CompanyProfilePoco> repo)
            : base(repo)
        {
        }

        public override void Update(CompanyProfilePoco[] pocos)
        {
            Verify(pocos);
            base.Update(pocos);
        }

        public override void Add(CompanyProfilePoco[] pocos)
        {
            Verify(pocos);
            base.Add(pocos);
        }

        protected override void Verify(CompanyProfilePoco[] pocos)
        {
            Regex phoneRegex = new Regex(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$");
            string[] endsWith = new string[] { ".ca", ".com", ".biz" };
            List<ValidationException> errors =
                 new List<ValidationException>();
            foreach (CompanyProfilePoco poco in pocos)
            {
                bool isEndsWith = !string.IsNullOrEmpty(poco.CompanyWebsite) && endsWith.Any(e => poco.CompanyWebsite.EndsWith(e));
                if (isEndsWith == false)
                {
                    errors.Add(
                        new ValidationException(600,
                        "Valid websites must end with the following extensions – + '.ca', '.com', '.biz'"));
                }
                if (phoneRegex.IsMatch(poco?.ContactPhone ?? "") == false)
                {
                    errors.Add(
                        new ValidationException(601,
                        "Must conform to a phone number pattern"));
                }
                if (errors.Count > 0)
                {
                    throw new AggregateException(errors);
                }
            }
        }
    }
}

