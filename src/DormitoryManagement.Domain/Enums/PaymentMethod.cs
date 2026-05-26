namespace DormitoryManagement.Domain.Enums
{
    /// <summary>
    /// Phương thức thanh toán hóa đơn.
    /// </summary>
    public enum PaymentMethod 
    { 
        /// <summary>Thanh toán bằng tiền mặt.</summary>
        Cash, 
        
        /// <summary>Chuyển khoản ngân hàng.</summary>
        BankTransfer, 
        
        /// <summary>Thanh toán qua thẻ tín dụng.</summary>
        CreditCard, 
        
        /// <summary>Thanh toán qua ví điện tử.</summary>
        EWallet 
    }
}
