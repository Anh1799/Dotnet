namespace blazor_buoi24.Services;

public class CarService{
    public string color = "red";

    public event Action OnChange;

    public async Task changeColor(string colorParam){
        color = colorParam;
        await StateHasChange();
    }

    public async Task StateHasChange(){
        OnChange?.Invoke();
    }


}