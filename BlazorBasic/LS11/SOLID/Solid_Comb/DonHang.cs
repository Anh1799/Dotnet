public class DonHang
{ 
    // ds sản phẩm trong đơn hàng
    public List<SanPham> SanPhams { get; set; } = new List<SanPham>();

    // them SP
    public void ThemSanPham(SanPham sp)
    {
        SanPhams.Add(sp);
    }

    // Tinh Tong Tien

    public double TinhTongTien()
    {
        //c1: chạy vòng lặp
        // double tongTien = 0;
        // foreach (var sp in SanPhams)
        // {
        //     tongTien += sp.Gia;
        // }
        // return tongTien;
        // c2: sử dụng LINQ
        return SanPhams.Sum(sp => sp.Gia); // tính tổng giá của tất cả sản phẩm trong đơn hàng
    }

}