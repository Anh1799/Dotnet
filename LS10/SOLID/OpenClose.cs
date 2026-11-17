/* OPEN CLOSE PRINCIPLE (OCP) 

Close: khong thay doi ma nguon cua class da ton tai

Open: cho phep mo rong chuc nang ma khong. cansua doi ma nguon


*/

public class GiamGia
{
    // method giam gia theo phan loai khach hang
    // Khach bth 1%, Thanh vien 5%

    public double PhanTramGiamGia(string loaiKH)
    {
        // DUa vao loai khach de giam % tuong ung
        if (loaiKH == "bt") return 0.01;
        else if (loaiKH == "tv") return 0.05;
        else if (loaiKH == "vip") return 0.1;
        else if (loaiKH == "vvip") return 0.15;
        else return 0;
    }
}

// Moi khach hang la mot class rieng, de mo rong tinh nang giam gia cho khach hang moi, ta chi can tao them class moi ma khong can sua doi ma nguon cua class GiamGia   
public interface IGiamGia
{
    double PhanTramGiamGia();
}

public class KHVip : IGiamGia
{
    public double PhanTramGiamGia()
    {
        return 0.1;
    }
}

public class KHVVip : IGiamGia
{
    public double PhanTramGiamGia()
    {
        return 0.15;
    }
}


public class KHThanhVien : IGiamGia
{
    public double PhanTramGiamGia()
    {
        return 0.05;
    }
}

public class KHBinhdong : IGiamGia
{
    public double PhanTramGiamGia()
    {
        return 0.01;
    }
}

