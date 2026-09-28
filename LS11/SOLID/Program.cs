
using Microsoft.Extensions.DependencyInjection;

public class Program
{
    public static void Main(string[] args)
    {
        // S - Single Responsibility Principle

        // muốn thêm giỏ hàng , đặt hàng, thanh toán  

        // var gioHang = new GioHang();
        // var datHang = new DatHang();
        // var thanhToan = new ThanhToan();
        // gioHang.ThemSanPham();
        // datHang.DatHangOnline();
        // thanhToan.ThanhToanOnline();


        // var muasam = new MuaSamFacade();
        // muasam.MuaSamOnline();


        // L
        // CanhCuc canhCuc = new CanhCuc();

        // canhCuc.Name = "Cánh cụt";
        // canhCuc.Bay(); //

        var zl = new ZaloOA("Hello");
        GuiThongBao guiThongBao = new GuiThongBao(zl);
        guiThongBao.Gui();

        var sms = new SMS();
        GuiThongBao guiThongBao2 = new GuiThongBao(sms);
        guiThongBao2.Gui();





        // SOLID TÔNG HỢP
        // phương thức thanh toán :
        // ThanhToanTienMat thanhToanTienMat = new ThanhToanTienMat();
        // ThanhToanViDienTu thanhToanViDienTu = new ThanhToanViDienTu();
        // DatHangService datHangService = new DatHangService(thanhToanViDienTu);
        DonHang donHang = new DonHang();
        donHang.ThemSanPham(new SanPham("Áo thun", 100000));
        donHang.ThemSanPham(new SanPham("Quần jean", 200000));
        // mua hàng và thanh toán
        // datHangService.MuaHang(donHang);



        var service  = new ServiceCollection();

        // đăng ký dịch vụ
        // khi gặp IThanhToanService thì sẽ trả về ThanhToanViDienTu

        service.AddTransient<IThanhToanService, ThanhToanTienMat>();

        // có thể đăng ký nhiều dịch vụ khác nhau
        service.AddTransient<DatHangService>(); // khởi tạo DatHangService mà không cần truyền tham số vào constructor

        
        // build service provider
        var serviceProvider = service.BuildServiceProvider();


        var datHangServiceFromDI = serviceProvider.GetService<DatHangService>();
        datHangServiceFromDI.MuaHang(donHang);



        // SỬ DỤNG DI BẰNG TAY
        var container = new DIContainer();
        // đăng kí dịch vụ
        container.Register<IThanhToanService, ThanhToanTheTinDung>();

        // DatHangService datHangServiceFromContainer = new DatHangService(container.Resolve<IThanhToanService>()); 
        DatHangService datHangServiceFromContainer =container.Resolve<DatHangService>();
        
        datHangServiceFromContainer.MuaHang(donHang);



        // BT buooir truowsc
        QuanLyNhanVien quanLyNhanVien = new QuanLyNhanVien();
        quanLyNhanVien.HienThiMenu();

    }
}

/*
TỔNG HỢP
- SOLID là một tập hợp các nguyên tắc thiết kế phần mềm giúp tạo ra mã nguồn dễ bảo trì, dễ hiểu và linh hoạt hơn.
- S : Mỗi class chỉ làm 1 việc duy nhất
- O : đóng khi hoàn thiện và mở khi cần mở rộng
- L : các class con có thể thay thế class cha mà không làm thay đổi hành vi của chương trình (sai logic)
- I : tách interface thành nhiều interface nhỏ , để class không phải implement những phương thức mà nó không sử dụng
- D : Đảo ngược phụ thuộc : code cấp cao không nên phụ thuộc vào code cấp thấp , mà cả 2 nên phụ thuộc vào abstraction (interface) , code cấp thấp phải implement interface để có thể hoán đổi dễ dàng





*/