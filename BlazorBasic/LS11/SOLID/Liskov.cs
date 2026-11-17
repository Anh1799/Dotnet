/*
Liskov Substitution Principle (LSP) - Nguyên tắc thay thế Liskov
 - class con kế thừa class cha không được lỗi logic
 - Nếu một class con kế thừa từ class cha, thì có thể sử dụng đối tượng của class con thay cho class cha mà không làm sai logic chương trình.

- Tập trung vào:Kế thừa (Inheritance) và khả năng thay thế (Substitution)
    
    → Đảm bảo các class con thực hiện đúng các cam kết của class cha.
    - 





*/

// người dùng đăng nhập vào hệ thống : tên , tuổi , giới tính , địa chỉ , số điện thoại , email
// nghiệp vụ: 



// class LoaiChim
public class Bird
{
    public string Name { get; set; }
    // phương thức bay, ăn , ngủ

    // public virtual void Bay()
    // {
    //     Console.WriteLine($"{Name} đang bay");
    // }
    public virtual void An()
    {
        Console.WriteLine($"{Name} đang ăn");
    }
    public virtual void Ngu()
    {
        Console.WriteLine($"{Name} đang ngủ");
    } 

}
// interface IBay 
public interface IBay
{
    void Bay();
}
// tạo class con
public class ChimSe : Bird, IBay
{
    public void Bay()
    {
        Console.WriteLine($"{Name} đang bay chậm");
    }
}
// đại bàng
public class DaiBang : Bird, IBay
{
    public void Bay()
    {
        Console.WriteLine($"{Name} đang bay nhanh");
    }
}

public class CanhCuc : Bird // cánh cụt không bay được , có method bay -> lỗi logic
{
   
}