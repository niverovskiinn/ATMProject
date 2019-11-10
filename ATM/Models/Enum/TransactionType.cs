using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Enum
{
    public class TransactionType
    {
        private TransactionType(TransactionTypeEnum @enum)
        {
            Id = (int)@enum;
            Name = @enum.ToString();
            Description = @enum.GetEnumDescription();
        }

        protected TransactionType() { } //For EF

        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(100)]
        public string Description { get; set; }

        public static implicit operator TransactionType(TransactionTypeEnum @enum) => new TransactionType(@enum);

        public static implicit operator TransactionTypeEnum(TransactionType type) => (TransactionTypeEnum)type.Id;

    }
}