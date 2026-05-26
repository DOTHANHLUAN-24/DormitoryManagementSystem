namespace DormitoryManagement.Domain.Enums
{
    /// <summary>
    /// Trạng thái của hóa đơn thanh toán.
    /// </summary>
    public enum InvoiceStatus 
    { 
        /// <summary>Hóa đơn chưa được thanh toán.</summary>
        Unpaid, 
        
        /// <summary>Hóa đơn đã được thanh toán đầy đủ.</summary>
        Paid, 
        
        /// <summary>Hóa đơn đã quá hạn thanh toán.</summary>
        Overdue, 
        
        /// <summary>Hóa đơn mới được thanh toán một phần.</summary>
        PartiallyPaid 
    }
}
