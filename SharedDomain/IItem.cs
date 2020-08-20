using System;
using System.ComponentModel.DataAnnotations;

namespace SharedDomain
{
    public interface IItem
    {
        [Key]
        public Guid Id { get; set; }
    }
}
