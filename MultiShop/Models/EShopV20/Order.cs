    using System;
    using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
    
    public class Order
    {
        public int Id { get; set; }
        public string CustomerId { get; set; }
        public string ReceiverName { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }
        public double Amount { get; set; }
        public string Status { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }