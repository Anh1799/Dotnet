

public class CartService{
    public List<ProductCartViewModel> lstProd {get;set;} = new List<ProductCartViewModel>(){
            new ProductCartViewModel
            {
                Id = 2,
                image = "https://dummyimage.com/300x300/000/fff&text=Samsung+S24",
                name = "Samsung Galaxy S24",
                price = 1499,
                quantity = 1
            }
    };

    public async Task AddItem(ProductCartViewModel itemClick){
        //Kiểm tra giỏ hàng có item đó chưa nếu có thì tăng số lượng nếu chưa có thì thêm vào lstProd
        ProductCartViewModel itemCart = lstProd.SingleOrDefault(item => item.Id == itemClick.Id);
        if (itemCart != null){
            itemCart.quantity += 1;
        }
        else{
            lstProd.Add(itemClick);
        }
        
        //Cập nhật lại giao diện 
        await StateHasChange();

    }


    public event Action OnChange;

    public async Task StateHasChange(){
        OnChange?.Invoke();
    }
}