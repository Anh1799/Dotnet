//api-controller-async
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;


namespace backend_netcore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        public static List<string> lstProduct = new List<string>() { "Product 1", "product 2", "product 3" };
        public static List<ProductDTO> lstProductDTO = new List<ProductDTO>()
        {
            new ProductDTO() { Id = 1, Name = "Product 1", Price = 10000 },
            new ProductDTO() { Id = 2, Name = "Product 2", Price = 20000 },
            new ProductDTO() { Id = 3, Name = "Product 3", Price = 15000 }
        };


        static ProductController()
        {
            foreach (var item in lstProductDTO)
            {
                item.Alias = HelperFunction.StringToSlug(item.Name);
            }
        }
        public ProductController()
        {

        }

        [HttpGet("GetAllProduct")]
        public async Task<IActionResult> GetAllProduct()
        {
            return Ok(lstProduct);
        }

        [HttpGet("GetAllProductDTO")]
        public async Task<IActionResult> GetAllProductDTO()
        {
            var respone = new ResponseTypeDTO<List<ProductDTO>>()
            {
                StatusCode = 200,
                Message = "Success",
                Content = lstProductDTO,
                DateTime = DateTime.Now
            };
            return Ok(respone);
        }

        // [HttpGet("GetAllProductDTO")]
        // public async Task<IActionResult> GetAllProductDTO()
        // {
        //     var respone = new ResponseTypeDTO<List<ProductDTO>>()
        //     {
        //         StatusCode = 200,
        //         Message = "Success",
        //         Content = lstProductDTO,
        //         DateTime = DateTime.Now
        //     };
        //     return StatusCode(StatusCodes.Status200OK, respone);
        // }

        [HttpGet("GetProductById/{id}")]
        public async Task<IActionResult> GetProductById([FromRoute] int id)
        {
            // Kiem tra xem id co ton tai trong danh sach hay khong
            ProductDTO? prodDetail = await Task.FromResult(lstProductDTO.SingleOrDefault(item => item.Id == id));

            var response = new ResponseTypeDTO<dynamic>()
            {
                StatusCode = 400,
                Message = "Khong tim thay san pham voi id: " + id,
                Content = null,
                DateTime = DateTime.Now
            };
            if (prodDetail == null)
            {
                response.Content = "Khong tim thay san pham id: " + id;
                return StatusCode(StatusCodes.Status400BadRequest, response); // status code 400
            }

            else
            {
                return Ok(prodDetail); // status code 200
            }
        }

        [HttpPost("AddProduct")] //FormBody là người dùng nhập liệu từ form 
        public async Task<IActionResult> AddProduct([FromBody] ProductDTO product)
        {
            // Kiem tra id co ton tai trong list chua? neu ton tai thi tra ve 400, neu chua ton tai thi them vao list va tra ve 200
            var existingProduct = lstProductDTO.SingleOrDefault(p => p.Id == product.Id);
            if (existingProduct != null)
            {
                var responseData = new ResponseTypeDTO<string>()
                {
                    StatusCode = 400,
                    Message = "San pham da ton tai voi id: " + product.Id,
                    Content = null,
                    DateTime = DateTime.Now
                };
                return StatusCode(StatusCodes.Status400BadRequest, responseData);
            }

            // Tạo alias cho sản phẩm
            product.Alias = HelperFunction.StringToSlug(product.Name);

            lstProductDTO.Add(product);
            var response = new ResponseTypeDTO<List<ProductDTO>>()
            {
                StatusCode = 200,
                Message = "Success",
                Content = lstProductDTO,
                DateTime = DateTime.Now
            };
            return Ok(response);
        }

        [HttpDelete("DeleteProduct/{id}")]
        public async Task<IActionResult> DeleteProduct([FromRoute] int id)
        {
            //Lấy ra phần tử trong list từ id
            var productToRemove = lstProductDTO.FirstOrDefault(p => p.Id == id);
            //Kiểm tra nếu có phần tử đó thì xoá
            if (productToRemove != null)
            {
                lstProductDTO.Remove(productToRemove);
                var respone = new ResponseTypeDTO<List<ProductDTO>>()
                {
                    StatusCode = 200,
                    Message = "Success",
                    Content = lstProductDTO,
                    DateTime = DateTime.Now
                };
                return Ok(respone);
            }
            var responseDAte = new ResponseTypeDTO<string>()
            {
                StatusCode = 400,
                Message = "Khong tim thay san pham voi id: " + id,
                Content = null,
                DateTime = DateTime.Now
            };
            return StatusCode(StatusCodes.Status400BadRequest, responseDAte);

        }

        // Assss BBBaaa => assss bbbaaa
        // JsoN => json
        [HttpGet("SearchProductByName")]
        public async Task<IActionResult> SearchProductByName([FromQuery] string keyword)
        {
            string name = HelperFunction.StringToSlug(keyword);
            var lstSearchProduct = lstProductDTO.Where(p => p.Alias.Contains(name)).ToList();
            if (lstSearchProduct.Count == 0)
            {
                var response = new ResponseTypeDTO<string>()
                {
                    StatusCode = 400,
                    Message = "Khong tim thay san pham voi ten: " + keyword,
                    Content = null,
                    DateTime = DateTime.Now
                };
                return StatusCode(StatusCodes.Status400BadRequest, response);
            }
            else
            {
                var response = new ResponseTypeDTO<List<ProductDTO>>()
                {
                    StatusCode = 200,
                    Message = @$"Found {lstSearchProduct.Count} products with name: {keyword}",
                    Content = lstSearchProduct,
                    DateTime = DateTime.Now
                };
                return Ok(response);
            }
        }

        [HttpPut("UpdateProduct")]
        public async Task<IActionResult> UpdateProduct([FromBody] ProductDTO updatedProduct)
        {
            var productToUpdate = lstProductDTO.SingleOrDefault(p => p.Id == updatedProduct.Id);
            if (productToUpdate != null)
            {
                productToUpdate.Name = updatedProduct.Name;
                productToUpdate.Price = updatedProduct.Price;
            }
            return lstProductDTO;
        }

        [HttpPatch("DiscountProduct/{id}")]
        public List<ProductDTO> DiscountProduct([FromRoute] int id, [FromBody] decimal discountPercentage)
        {
            //TÌm trong list có sản phẩm đó hay không
            var productToDiscount = lstProductDTO.SingleOrDefault(p => p.Id == id);
            if (productToDiscount != null)
            {                    //Tính giá sau khi giảm giá
                var discountedPrice = productToDiscount.Price * (1 - discountPercentage / 100);
                productToDiscount.Price = discountedPrice;
            }
            return lstProductDTO;

        }




    }
}
