/*
Open Close Principle (OCP): Nguyên tắc đóng mở

Đóng : không thay đổi mã nguồn của class đã tồn tại

Mở: cho phép mở rộng chức năng mà không cần sửa đổi mã nguồn hiện có


*/
public class GiamGia
{
    // method giảm giá theo phân loại khách hàng
    // khách hàng bth thì giảm 1% , Thành viên 5%, VIP 10 
    public double PhanTramGiamGia(string loaiKH)
    {
        // dựa vào loai KH để trả % tương ứng
        if (loaiKH == "bt") return 0.01;
        else if (loaiKH == "tv") return 0.05;
        else if (loaiKH == "vip") return 0.1;
        else return 0;
    }
}
// thêm Svip
// VIết lại phù hợp với nguyên tắc OCP -> strategy pattern : tách riêng từng chiến lược giảm giá thành 1 class riêng
public interface IGiamGia
{
    double PhanTramGiamGia();
}

// khách hàng bth 
// khách hàng thành viên
// khách hàng vip
// khách hàng svip
// mỗi khách hàng là 1 class riêng -> mở rộng 
public class KHVipGiamGia : IGiamGia
{
    public double PhanTramGiamGia()
    {
        return 0.1;
    }
}
public class KHTVgiamGia : IGiamGia
{
    public double PhanTramGiamGia()
    {
        return 0.05;
    }
}