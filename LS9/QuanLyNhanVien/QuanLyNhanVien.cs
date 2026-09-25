using Newtonsoft.Json;

public class QuanLyNhanVien<T> where T : NhanVien
{
    public List<T> Items = new List<T>();
    public static string duongdan = "nhanvien.json";
    public QuanLyNhanVien()
    {

    }

    public void ThemNv(T nv)
    {
        Items.Add(nv);

    }

    public List<T> TimKiemTheoTen(string ten)
    {
        return Items.Where(nv => nv.Ten.Contains(ten)).ToList();
    }

    public void Xoa(int maNhanVien)
    {
        var nv = Items.FirstOrDefault(nv => nv.MaNhanVien == maNhanVien);
        if (nv != null)
        {
            Items.Remove(nv);
        }
        else
        {
            Console.WriteLine($"Khong tim thay nhan vien co ma {maNhanVien}");
        }
    }

    public void HienThiChucNang()
    {
        while (true)
        {
            Console.WriteLine("1. Them nhan vien");
            Console.WriteLine("2. Tim kiem nhan vien theo ten");
            Console.WriteLine("3. Xoa nhan vien theo ma");
            Console.WriteLine("4. Sua ten nhan vien");
            Console.WriteLine("5. Hien thi tat ca nhan vien");
            Console.WriteLine("6. Luu file");
            Console.WriteLine("7. Doc file");
            Console.WriteLine("0. Thoat");

            int chon;
            if (!int.TryParse(Console.ReadLine(), out chon))
            {
                Console.WriteLine("Vui long nhap so:");
                continue;
            }
            switch (chon)
            {
                case 1:
                    {
                        T nv = Activator.CreateInstance<T>();
                        nv.HienThiThongTin();
                        ThemNv(nv);
                        break;
                    }
                case 2:
                    {
                        Console.WriteLine("Nhap vao ten nhan vien can tim:");
                        string ten = Console.ReadLine();
                        var kq = TimKiemTheoTen(ten);
                        foreach (var nv in kq)
                        {
                            Console.WriteLine($"Ma nhan vien: {nv.MaNhanVien}, Ten: {nv.Ten}, Luong 1 gio: {nv.Luong1H}, So gio lam: {nv.SoGioLam}, Tong luong: {nv.TinhLuong()}");
                        }
                        break;
                    }
                case 3:
                    {
                        Console.WriteLine("Nhap vao ma nhan vien can xoa:");
                        int index;
                        if (!int.TryParse(Console.ReadLine(), out index))
                        {
                            Console.WriteLine("Vui long nhap so:");
                            continue;
                        }
                        Xoa(index);
                        break;
                    }
                case 4:
                    {
                        Console.WriteLine("Nhap vao ma nhan vien can sua:");
                        int index;
                        if (!int.TryParse(Console.ReadLine(), out index))
                        {
                            Console.WriteLine("Vui long nhap so:");
                            continue;
                        }
                        var nv = Items.FirstOrDefault(nv => nv.MaNhanVien == index);
                        if (nv != null)
                        {
                            Console.WriteLine($"Nhap ten moi cho nhan vien {nv.Ten}:");
                            nv.Ten = Console.ReadLine();
                        }
                        else
                        {
                            Console.WriteLine($"Khong tim thay nhan vien co ma {index}");
                        }
                        break;
                    }
                case 5:
                    {
                        foreach (var nv in Items)
                        {
                            Console.WriteLine($"Ma nhan vien: {nv.MaNhanVien}, Ten: {nv.Ten}, Luong 1 gio: {nv.Luong1H}, So gio lam: {nv.SoGioLam}, Tong luong: {nv.TinhLuong()}");
                        }
                        break;
                    }
                case 6:
                    {
                        LuuMenu();
                        break;
                    }
                case 7:
                    {
                        DocMenu();
                        break;
                    }
                case 0:
                    {
                        return;
                    }
                default:
                    {
                        Console.WriteLine("Vui long chon chuc nang tu 0 den 7");
                        break;
                    }
            }

        }

    }

    public void LuuMenu()
    {
        var json = JsonConvert.SerializeObject(Items);
        File.WriteAllText(duongdan, json);

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("---------------------");
        Console.WriteLine("Luu file thanh cong");
        Console.WriteLine("---------------------");
        Console.ResetColor();
    }

    public void DocMenu()
    {
        if (File.Exists(duongdan))
        {
            var json = File.ReadAllText(duongdan);
            Items = JsonConvert.DeserializeObject<List<T>>(json) ?? new List<T>();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("---------------------");
            Console.WriteLine("Doc file thanh cong");
            Console.WriteLine("---------------------");
            Console.ResetColor();

        }
    }
}