using System;

namespace PremePayBooks.API.Models
{
    public abstract class BaseClass
    {
        public Guid Id { get; set; }

        protected BaseClass()
        {
            Id = Guid.NewGuid();
        }
    }
}