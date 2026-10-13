public class Vehicle{
    public string Brand{get;set;}
    public string Model{get;set;}
    public int Year{get;set;}

    public void Start(){
        Console.WriteLine("Vehicle is staring");
    }

    public void Stop(){
        Console.WriteLine("Vehicle is stopping");
    }

    
}