using SCTSMA.PORTAL.DOMAIN.User.ActivateUser;
using SCTSMA.PORTAL.DOMAIN.User.CreateUser;
using SCTSMA.PORTAL.DOMAIN.User.ListUser;
using SCTSMA.PORTAL.DOMAIN.User.LoginUser;

namespace SCTSMA.PORTAL.APPLICATION
{
    public interface IUserRepository
    {
        Task<LoginResponseModel> LoginUser(LoginRequestModel loginRequest);
        Task<ActivateUserResponseModel> ActivateUser(ActivateUserRequestModel activateUserRequest);
        Task<List<ListUserResponseModel>> GetAllUsers();
        Task<CreateUserResponseModel> CreateUser(CreateUserRequestModel createUserRequest);
    }
}
