using CMCS.Models;
using System.Collections.ObjectModel;

namespace CMCS.Services
{
    public interface IClaimService
    {
        void AddClaim(Claim claim);
        ObservableCollection<Claim> GetClaimsByStatus(ClaimStatus status);
        bool VerifyClaim(string staffNumber, UserRole verifierRole);
        bool RejectClaim(string staffNumber, UserRole rejecterRole);
        ObservableCollection<Claim> GetAllClaims();
        int GetClaimsCount();
        decimal CalculateTotalAmount(Claim claim);
    }
}