class Menu{
    public List<MonAn> DanhSachMon{get;set;} = new List<MonAn>();
    public int chon {get;set;}
    public void ThemMonAn(MonAn MonAnMoi){
        DanhSachMon.Add(MonAnMoi);
    }

    public void HienThiChucNang(){
        Console.WriteLine($@"
        1/ Them mon an
        2/ Hien thi menu
        3/ Xoa mon an
        4/ Thoat
        ");
    }

    public int ChonChucNang(){
        Console.Write($"Hay chon chuc nang:");
        chon = Convert.ToInt32(Console.ReadLine());
        return chon;
    }

    public void Thoat(){
        chon = 4;
    }
}