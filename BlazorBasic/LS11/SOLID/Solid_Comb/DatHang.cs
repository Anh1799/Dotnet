public class DatHangService
{
    public IThanhToanService _thanhToanService { get; set; }

    public DatHangService(IThanhToanService thanhToanService)  // DIP : nguyên lý thiết kế trong SOLID
    {
        _thanhToanService = thanhToanService; // Dependency Injection DI : tiêm phụ thuộc - Design Pattern : giải pháp thiết kế đã được kiểm chứng
    }

    // mua hàng và thanh thoán
    public void MuaHang(DonHang donHang)
    {
        double tongTien = donHang.TinhTongTien();
        // gọi phương thức thanh toán
        _thanhToanService.ThanhToan(tongTien);
    }
}

/*
DIP : nguyên lý thiết kế trong SOLID
- Các module cấp cao không nên phụ thuộc vào các module cấp thấp. Cả hai nên phụ thuộc vào các abstraction/ interface
DI: Dependency Injection : tiêm phụ thuộc  :là kỹ thuật hiện thực hoá DIP 
- Design Pattern : giải pháp thiết kế đã được kiểm chứng , có rất nhiều mẫu thiết kế khác nhau , giúp giải quyết các vấn đề phổ biến trong thiết kế phần mềm
- VD: Singleton , Factory , Observer , Strategy , Facade ...


*/