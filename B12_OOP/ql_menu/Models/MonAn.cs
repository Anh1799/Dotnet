class MonAn{
    public static int IDMon = 1;
    public int MaMonAn{get;set;}
    public string? TenMonAn{get;set;}
    public double Gia {get;set;}

    public MonAn(){
        NhapMon();
        MaMonAn = IDMon;
        MonAn.IDMon++;
    }

    public void NhapMon(){
        Console.Write("Nhap vao ten mon:");
        TenMonAn = Console.ReadLine();
        Console.Write("Nhap vao gia: ");
        Gia = Convert.ToDouble(Console.ReadLine());

    }

    public void XuatMonAn(){
        Console.WriteLine($@"
            {MaMonAn}/ {TenMonAn} - {Gia}$
        ");
    }
}