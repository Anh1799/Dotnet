

public class CartService
{
    public List<ProductCartViewModel> lstProd { get; set; } = new List<ProductCartViewModel>();

    public async Task AddItem(ProductCartViewModel itemClick)
    {
        //Kiểm tra giỏ hàng có item đó chưa nếu có thì tăng số lượng nếu chưa có thì thêm vào lstProd
        ProductCartViewModel itemCart = lstProd.SingleOrDefault(item => item.Id == itemClick.Id);
        if (itemCart != null)
        {
            itemCart.quantity += 1;
        }
        else
        {

            // Tao moi san pham click
            ProductCartViewModel newProduct = new ProductCartViewModel(){
                Id = itemClick.Id,
                name = itemClick.name,
                price = itemClick.price,
                image = itemClick.image,
                quantity = 1,
            }; 

            lstProd.Add(newProduct);
        }

        //Cập nhật lại giao diện 
        await StateHasChange();

    }

    public async Task RemoveItem(int id)
    {
        ProductCartViewModel? itemCart = lstProd.SingleOrDefault(item => item.Id == id);
        if (itemCart != null)
        {
            lstProd.Remove(itemCart);
        }
        await StateHasChange();
    }

    public async Task ChangeQuantity(int idClick, int quantity)
    {
        ProductCartViewModel? itemCart = lstProd.SingleOrDefault(item => item.Id == idClick);
        if (itemCart != null)
        {
            itemCart.quantity += quantity;
            if (itemCart.quantity == 0)
            {
                itemCart.quantity += 1;
            }
        }
        await StateHasChange();
    }


    public event Action OnChange;

    public async Task StateHasChange()
    {
        OnChange?.Invoke();
    }
}