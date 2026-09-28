public class SanPham
{
    public string TenSP { get; set; }
    public double Gia { get; set; }

    public SanPham()
    {

    }
    public SanPham(string tenSP, double gia)
    {
        TenSP = tenSP;
        Gia = gia;
    }

}