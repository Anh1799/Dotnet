public class NhanVien
{
    private static int nextId = 1;
    public int MaNhanVien {get; set;}
    public string? Ten {get; set;}
    public double Luong1H {get; set;}
    public int SoGioLam {get; set;}

    public NhanVien()
    {
        
    }

    public double TinhLuong()
    {
        return Luong1H * SoGioLam;
    }

    public void HienThiThongTin()
    {
        Console.WriteLine($"Nhap ma nhan vien:");
        MaNhanVien = int.Parse(Console.ReadLine());
        Console.WriteLine($"Nhap ten nhan vien:");
        Ten = Console.ReadLine();
        Console.WriteLine($"Nhap luong 1 gio:");
        Luong1H = double.Parse(Console.ReadLine());
        Console.WriteLine($"Nhap so gio da lam:");
        SoGioLam = int.Parse(Console.ReadLine());
    }

     

    

}