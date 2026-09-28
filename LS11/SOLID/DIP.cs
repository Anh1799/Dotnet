/*
    DIP - Dependency Inversion Principle: Nguyên tắc đảo ngược phụ thuộc
- Code cấp cao (ví dụ như business logic, controller...) không nên phụ thuộc trực tiếp vào class cụ thể (chi tiết triển khai), mà nên phụ thuộc vào interface hoặc abstraction.
- Code cấp thấp (class cụ thể) phải implement interface để có thể hoán đổi, test, thay đổi dễ dàng.
Khởi tạo bên trong class : GuiThongBao sẽ phụ thuộc vào ZaloOA, nếu muốn thay đổi dịch vụ gửi thông báo khác như Email, SMS thì phải sửa mã nguồn của GuiThongBao, vi phạm nguyên tắc DIP.

*/


// gửi thông báo 

// IMessage
public interface IMessage
{
    void SendMessage();
}
public class ZaloOA : IMessage // service gửi thông báo qua Zalo
{
    public ZaloOA(string mess)
    {
        
    }
    public void SendMessage()
    {
        Console.WriteLine("đã gửi thông báo Zalo");
    }
}
// SMS
public class SMS : IMessage
{
    public void SendMessage()
    {
        Console.WriteLine("đã gửi thông báo SMS");
    }
}


public class GuiThongBao // 
{
    // public ZaloOA _zaloOa = new ZaloOA(); // sử dụng thì khởi tạo bên trong class 
    public IMessage _message;
    public GuiThongBao(IMessage message) // khởi tạo bên ngoài class , có thể truyền vào ZaloOA hoặc SMS , không phụ thuộc vào 1 dịch vụ cụ thể nào
    {
        _message = message;
    }
    public void Gui()
    {
        _message.SendMessage();
    }
}