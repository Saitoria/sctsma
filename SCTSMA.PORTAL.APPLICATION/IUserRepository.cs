using SCTSMA.PORTAL.DOMAIN.User;

namespace SCTSMA.PORTAL.APPLICATION
{
    public interface IUserRepository
    {
        Task<LoginResponseModel> LoginUser(LoginRequestModel loginRequest);
        Task<ActivateUserResponseModel> ActivateUser(ActivateUserRequestModel activateUserRequest);
        Task<GetUserModel> GetUser();
    }
}
