/*
    ISP (Interface Segregation Principle) - Nguyên tắc phân tách giao diện
    - Định nghĩa :  Một class không nên bị buộc phải implement những interface mà nó không dùng đến.
    - Lợi ích: Giúp giảm sự phụ thuộc giữa các class, tăng tính linh hoạt và dễ bảo trì của mã nguồn.
    - Tập trung vào: Giao diện (Interface) và sự phân tách (Segregation)
*/
/*

NHỚ NHANH L và I
- L: Nếu có hành vi không đúng với “tất cả con”, đừng nhét vào cha → tách ra interface riêng.
- I: Nếu có nhiều hành vi khác nhau trong một interface, tách thành nhiều interface nhỏ – ai dùng gì thì lấy dó

*/

// những thằng HS SV sẽ thực thi interface ISinhVien
public interface ISinhVien
{
    void DiHoc();
    // void ThiNghiem(); // lab
    // void GDQP(); 
}

// tách gdqp và thinghiem
public interface IThiNghiem
{
    void ThiNghiem();
}
public interface IGDQP
{
    void GDQP();
}
// 
public class TieuHoc : ISinhVien
{
    public void DiHoc()
    {
        Console.WriteLine("Đi học tiểu học");
    }

}
// cap hai
public class TrungHoc : ISinhVien , IThiNghiem
{
    public void DiHoc()
    {
        Console.WriteLine("Đi học trung học");
    }

    public void ThiNghiem()
    {
        Console.WriteLine("Thực hiện thí nghiệm ở trung học");
    }
   
}