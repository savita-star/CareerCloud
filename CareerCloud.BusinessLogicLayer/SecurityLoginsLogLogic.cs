using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;

namespace CareerCloud.BusinessLogicLayer
{
    public class SecurityLoginsLogLogic : BaseLogic<SecurityLoginsLogPoco>
    {
        private const int saltLengthLimit = 10;

        public SecurityLoginsLogLogic(IDataRepository<SecurityLoginsLogPoco> repository) : base(repository)
        {
        }
    }
}
