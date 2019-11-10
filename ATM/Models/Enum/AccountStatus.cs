using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Enum
{
    public class AccountStatus
    {
        private AccountStatus(AccountStatusEnum @enum)
        {
            Id = (int)@enum;
            Name = @enum.ToString();
            Description = @enum.GetEnumDescription();
        }

        protected AccountStatus() { } //For EF

        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(100)]
        public string Description { get; set; }

        public static implicit operator AccountStatus(AccountStatusEnum @enum) => new AccountStatus(@enum);

        public static implicit operator AccountStatusEnum(AccountStatus st) => (AccountStatusEnum)st.Id;

    }
}