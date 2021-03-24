using System.Threading.Tasks;
using DTOs;

namespace MobilePhoneCardiography.Services.DataStore
{
    public interface IControllerDatabase
    {
        public  Task<bool> ValidateLogin(IUser user);
        public  Task<bool> ValidatePatient(IPatient patient);

    }
}