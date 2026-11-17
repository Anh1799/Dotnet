// S - Single Responsibility Principle
/*
- Mỗi class chỉ nên có một lý do để thay đổi, tức là chỉ nên có một trách nhiệm duy nhất.
- Điều này giúp tăng tính dễ bảo trì, dễ hiểu và giảm sự phụ thuộc giữa các
- class độc lập chỉ làm làm 1 nhiệm vụ cũ thể

Facade Pattern: gom các class có tránh nhiệm khác nhau nhưng cùng 1 nhóm vào 1 class để dễ sử dụng

Design Pattern: Mẫu thiết kế , giải pháp tổnge quả đã được kiểm chứng , 
- VD: Mẫu nhà cấp 4 , Mẫu nhà 1 tầng , 2 tầng 3 tầng ...
*/
public class ThanhToan
{
    // chỉ chứa các phương thức liên quan đến việc thanh toán
    public void ThanhToanOnline()
    {
        Console.WriteLine("Thanh Toán thành công");
    }
}
public class BaoCao
{
    // chỉ chứa các phương thức liên quan đến việc tạo báo cáo
    public void TaoBaoCao()
    {
        Console.WriteLine("Báo cáo đã được tạo");
    }
}
// gio Hang
public class GioHang
{
    // chỉ chứa các phương thức liên quan đến việc quản lý giỏ hàng
    public void ThemSanPham()
    {
        Console.WriteLine("Sản phẩm đã được thêm vào giỏ hàng");
    }
    // xoá sản phẩm ra khỏi giỏ hàng
    // tăng số lương...
}

// đặt hàng
public class DatHang
{
    // chỉ chứa các phương thức liên quan đến việc đặt hàng
    public void DatHangOnline()
    {
        Console.WriteLine("Đặt hàng thành công");
    }
}

// Facade pattern : tạo một lớp trung gian để kết nối các lớp có trách nhiệm khác nhau, giúp giảm sự phụ thuộc giữa chúng và làm cho mã dễ bảo trì hơn.
public class MuaSamFacade
{
    private GioHang _gioHang;
    private DatHang _datHang;
    private ThanhToan _thanhToan;

// khởi tạo trong constructor
    public MuaSamFacade()
    {
        _gioHang = new GioHang();
        _datHang = new DatHang();
        _thanhToan = new ThanhToan();
    }
    public void MuaSamOnline()
    {
        _gioHang.ThemSanPham();
        _datHang.DatHangOnline();
        _thanhToan.ThanhToanOnline();
    }
}