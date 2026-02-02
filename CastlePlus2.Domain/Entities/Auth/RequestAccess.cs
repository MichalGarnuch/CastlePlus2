namespace CastlePlus2.Domain.Entities.Auth
{
    public enum RequestAccessStatus
    {
        Pending = 0,
        Approved = 1,
        Rejected = 2
    }

    public sealed class RequestAccess
    {
        public int IdRequestAccess { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? Login { get; set; }
        public string? Phone { get; set; }
        public string Department { get; set; } = string.Empty;
        public string Justification { get; set; } = string.Empty;
        public RequestAccessStatus Status { get; set; }
        public string RequestedBy { get; set; } = string.Empty;
        public DateTime CreatedAtUtc { get; set; }
        public DateTime UpdatedAtUtc { get; set; }
        public string? ApprovedBy { get; set; }
        public DateTime? ApprovedAtUtc { get; set; }
        public string? ApprovedLogin { get; set; }
        public string? ApprovedEmail { get; set; }
        public string? ApprovedRoleCodes { get; set; }
        public string? RejectedBy { get; set; }
        public DateTime? RejectedAtUtc { get; set; }
        public string? RejectionReason { get; set; }
    }
}