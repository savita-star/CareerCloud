using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;


namespace CareerCloud.BusinessLogicLayer
{
    public class ApplicantWorkHistoryLogic : BaseLogic<ApplicantWorkHistoryPoco>
    {
        public ApplicantWorkHistoryLogic(IDataRepository<ApplicantWorkHistoryPoco> repo)
            : base(repo)
        {
        }

        public override void Update(ApplicantWorkHistoryPoco[] pocos)
        {
            Verify(pocos);
            base.Update(pocos);
        }

        public override void Add(ApplicantWorkHistoryPoco[] pocos)
        {
            Verify(pocos);
            base.Add(pocos);
        }

        protected override void Verify(ApplicantWorkHistoryPoco[] pocos)
        {
            List<ValidationException> errors =
                 new List<ValidationException>();
            foreach (ApplicantWorkHistoryPoco poco in pocos)
            {

                if ((poco?.CompanyName?.Length ?? 0) <= 2)
                {
                    errors.Add(
                        new ValidationException(105,
                        "Must be greater then 2 characters"));
                }
                if (errors.Count > 0)
                {
                    throw new AggregateException(errors);
                }
            }
        }
    }
}
