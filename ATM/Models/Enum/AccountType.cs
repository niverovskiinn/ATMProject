using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Enum
{
    public class AccountType
    {
        private AccountType(AccountTypeEnum @enum)
        {
            Id = (int) @enum;
            Name = @enum.ToString();
            Description = @enum.GetEnumDescription();
        }

        protected AccountType()
        {
        } //For EF

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [Required] [MaxLength(100)] public string Name { get; set; }

        [MaxLength(100)] public string Description { get; set; }

        public static implicit operator AccountType(AccountTypeEnum @enum)
        {
            return new AccountType(@enum);
        }

        public static implicit operator AccountTypeEnum(AccountType type)
        {
            return (AccountTypeEnum) type.Id;
        }
    }
}