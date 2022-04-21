using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;

namespace CareerCloud.BusinessLogicLayer
{
    public class SecurityRoleLogic : BaseLogic<SecurityRolePoco>
    {
        public SecurityRoleLogic(IDataRepository<SecurityRolePoco> repo)
            : base(repo)
        {
        }

        public override void Update(SecurityRolePoco[] pocos)
        {
            Verify(pocos);
            base.Update(pocos);
        }

        public override void Add(SecurityRolePoco[] pocos)
        {
            Verify(pocos);
            base.Add(pocos);
        }

        protected override void Verify(SecurityRolePoco[] pocos)
        {
            List<ValidationException> errors =
                 new List<ValidationException>();
            foreach (SecurityRolePoco poco in pocos)
            {
                if (string.IsNullOrEmpty(poco.Role))
                {
                    errors.Add(
                        new ValidationException(800,
                        "Role Cannot be empty"));
                }
                if (errors.Count > 0)
                {
                    throw new AggregateException(errors);
                }
            }
        }
    }
}


