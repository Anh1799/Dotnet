// tạo interface IThanhToanService
public interface IThanhToanService
{
    // phương thức thanh toán
    void ThanhToan(double soTien);
}

// IOTP

// tien mat
public class ThanhToanTienMat : IThanhToanService
{
    public void ThanhToan(double soTien)
    {
        Console.WriteLine($"Thanh toán tiền mặt thành công, số tiền: {soTien}");
    }
}
// the tin dung
public class ThanhToanTheTinDung : IThanhToanService
{
    public void ThanhToan(double soTien)
    {
        // mở rộng cho nhập thêm cvv , số thẻ , hạn sử dụng ...
        Console.WriteLine($"Thanh toán thẻ tín dụng thành công, số tiền: {soTien}");
    }
}
// vi dien tu
public class ThanhToanViDienTu : IThanhToanService
{
    public void ThanhToan(double soTien)
    {
        // otp , mã xác nhận ...
        Console.WriteLine($"Thanh toán ví điện tử thành công, số tiền: {soTien}");
    }
}