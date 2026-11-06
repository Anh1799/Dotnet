console.log("abc");

/*
 Toan TU : + - * / %

 dynamic type: kieu du lieu se phu thuoc va gia tri
 var, let, const: Tu khoa khai bao bien

*/

// window.a = 5;
// var a = 5;
// var a = 10;

let a = 5;
a = "hello";

console.log(typeof a);

let strHello = "abcd ${a} xyz";

console.log(strHello);

// '1' + '1' => '11'
// 1 + 1 => 2

// Number('1') + Number('1');

console.log(`Ket qua: ${"10" / "2"}`);

// Ham - Function:

/* 

+ Doi voi C#
    void : khong co gia tri tra ve (khong return)
    outputType: return ve dung type

+ Doi voi js
    co gia tri tra ve thi co lenh return

*/

// expression function
let tinhTong = async function (a, b) {
  console.log(a + b);
};

// declarartion function
// window.tinhTong = function() {}
// function tinhTong(a,b){
//     return a - b;
// }

// function tinhTong(a,b){
//     return a + b;
// }

console.log(tinhTong(1, 3));

// Viết hàm lấy danh sách sản phẩm từ api getAllProduct

let getAllProductApi = async () => {
  let response = await fetch("https://apistore.cybersoft.edu.vn/api/Product");

  let data = await response.json();

  console.log(data);
}

// getAllProductApi();


/*
 localstorage: Luu duoc 5mb tai client va luu dang key value
 .getItem('key'): lay ra du lieu value tu localstorage dua vao key
 .setItem('key'): gan gai tri vao localstorage dua vao key
 .removeItem('key'): xoa gia tri localstorage dua vao key


*/


window.setLocalstorage = () => {
  localStorage.setItem("data", "data 123");

  let prod = {
    id:1,
    name:'Iphone',
    price: 1000
  }

  // primitive value: string, boolean, number
  localStorage.setItem('data object', JSON.stringify(prod));


  /* 
  
    json -> string : JSON.stringify <=> JsonSerializer.Serialize()
    string -> json : JSON.parse <=> JsonSerializer.Dererialize<T>
  
  
  */



}
