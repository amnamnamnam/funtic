using System.ComponentModel.DataAnnotations;

namespace FUNTIK.Models
{
    #region snippet1
    public class Contact
    {
        public int ContactId { get; set; }

        // user ID from AspNetUser table.
        public string? OwnerID { get; set; }

        public string? Name { get; set; }

        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }

        public ContactStatus Status { get; set; }
    }

    public enum ContactStatus
    {
        Submitted,
        Approved,
        Rejected
    }
    #endregion
}
