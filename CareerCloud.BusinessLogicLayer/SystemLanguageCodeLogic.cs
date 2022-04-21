using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;


namespace CareerCloud.BusinessLogicLayer
{

    public class SystemLanguageCodeLogic : SystemLanguageCodeBase<SystemLanguageCodePoco>
    {
        public SystemLanguageCodeLogic(IDataRepository<SystemLanguageCodePoco> repo)
            : base(repo)
        {
        }

        public override void Update(SystemLanguageCodePoco[] pocos)
        {
            Verify(pocos);
            base.Update(pocos);
        }

        public override void Add(SystemLanguageCodePoco[] pocos)
        {
            Verify(pocos);
            base.Add(pocos);
        }

        protected override void Verify(SystemLanguageCodePoco[] pocos)
        {
            List<ValidationException> errors =
                 new List<ValidationException>();
            foreach (SystemLanguageCodePoco poco in pocos)
            {
                if (string.IsNullOrEmpty(poco.LanguageID))
                {
                    errors.Add(
                        new ValidationException(1000,
                        " Code Cannot be empty"));
                }
                if (string.IsNullOrEmpty(poco.Name))
                {
                    errors.Add(
                        new ValidationException(1001,
                        "Name Cannot be empty"));
                }
                if (string.IsNullOrEmpty(poco.NativeName))
                {
                    errors.Add(
                        new ValidationException(1002,
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

