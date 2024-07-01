using SCTSMA.PORTAL.DOMAIN.Dispute;

namespace SCTSMA.PORTAL.APPLICATION
{
    public interface IDisputeRepository
    {
        Task<List<GetDisputesResponseModel>> GetAllDisputes();
        Task<CreateDisputeResponseModel> CreateDispute(CreateDisputeRequestModel disputePost);
    }
}
