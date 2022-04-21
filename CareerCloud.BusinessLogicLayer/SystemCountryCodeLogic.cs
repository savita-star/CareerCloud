using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;


namespace CareerCloud.BusinessLogicLayer
{

    public class SystemCountryCodeLogic : SystemCountryCodeBase<SystemCountryCodePoco>
    {
        public SystemCountryCodeLogic(IDataRepository<SystemCountryCodePoco> repo)
            : base(repo)
        {
        }

        public override void Update(SystemCountryCodePoco[] pocos)
        {
            Verify(pocos);
            base.Update(pocos);
        }

        public override void Add(SystemCountryCodePoco[] pocos)
        {
            Verify(pocos);
            base.Add(pocos);
        }

        protected override void Verify(SystemCountryCodePoco[] pocos)
        {
            List<ValidationException> errors =
                 new List<ValidationException>();
            foreach (SystemCountryCodePoco poco in pocos)
            {
                if (string.IsNullOrEmpty(poco.Code))
                {
                    errors.Add(
                        new ValidationException(900,
                        " Code Cannot be empty"));
                }
                if (string.IsNullOrEmpty(poco.Name))
                {
                    errors.Add(
                        new ValidationException(901,
                        "Name Cannot be empty"));
                }


                if (errors.Count > 0)
                {
                    throw new AggregateException(errors);
                }
            }
        }
    }
}
