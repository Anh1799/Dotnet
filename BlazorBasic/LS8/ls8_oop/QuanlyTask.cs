public class Task
{
    public string TenTask { get; set; }
    public bool TrangThai { get; set; }
    public Task(string name)
    {
        TenTask = name;
        TrangThai = false;
    }

    public void HoanThanh()
    {
        TrangThai = true;
    }

    public void HienThi(int index)
    {
        string _trangThai = TrangThai ? "Hoan thanh" : "Chua hoan thanh";
        Console.WriteLine($"{index}, Cong viec: {TenTask}, Trang thai: {_trangThai}");
    }

}

public class QuanLyTask
{
    public List<Task> DanhSachTask { get; set; } = new();

    public QuanLyTask()
    {
        //constructor
    }

    // Phuong thu: them task(), xoa task(), hien thi tat ca task()      

    public void ThemTask(Task task)
    {
        DanhSachTask.Add(task);
        Console.WriteLine($"Da them task thanh cong: {task.TenTask}");
    }

    public void HoanThanh(int index)
    {
        var checkIndex = index - 1;
        if (checkIndex < 0 || checkIndex >= DanhSachTask.Count)
        {
            Console.WriteLine("Chi so cong viec khong hop le.");
            return;
        }
        Console.WriteLine($"Hoan Thanh task");
    }

    public void HienThiTatCaTask()
    {
        Console.WriteLine("Danh sach cong viec:");
        for (int i = 0; i < DanhSachTask.Count; i++)
        {
            DanhSachTask[i].HienThi(i + 1);
        }
    }

    public void HienThiChucNang()
    {
        int chon = 0;
        do
        {
            Console.WriteLine("Chức năng quản lý công việc:");
            Console.WriteLine("1. Thêm công việc");
            Console.WriteLine("2. Hoàn thành công việc");
            Console.WriteLine("3. Hiển thị tất cả công việc");
            Console.WriteLine("4. Thoát");
            var checkInput = int.TryParse(Console.ReadLine(), out chon);
            if (!checkInput)
            {
                Console.WriteLine("Vui lòng nhập số từ 1 đến 4.");
                continue;
            }
            if (chon == 4)
            {
                Console.WriteLine("Da thoat chuong trinh.");
                break;
            }
            switch (chon)
            {
                case 1:
                    Console.WriteLine("Them task");
                    Console.WriteLine("Nhap ten cong viec:");
                    string tenTask = Console.ReadLine();
                    Task cv = new Task(tenTask);
                    ThemTask(cv);
                    break;

                case 2:
                    Console.WriteLine("Hoan thanh task!");
                    HienThiTatCaTask();
                    int index;
                    var checkIndex = int.TryParse(Console.ReadLine(), out index);
                    HoanThanh(index);
                    break;

                case 3:
                    Console.WriteLine("Show Thong Tin");
                    HienThiTatCaTask();
                    break;
            }


        }
        while (chon != 4);
    }
}
