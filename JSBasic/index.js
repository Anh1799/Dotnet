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

//Viết hàm lấy danh sách sản phẩm từ api getAllProduct

let getAllProductApi = async () => {

    let response = await fetch("https://apistore.cybersoft.edu.vn/api/Product");
    let data = await response.json();
    console.log(data);
    //JS DOM 
    let strHTML = "";
    for(let item of data.content){
        strHTML += `<div class="col-md-3 mt-2">
            <div class="card">
                <img src="${item.image}" alt="${item.alias}" />
                <div class="card-body">
                    <h3 class="card-title">${item.name} </h3>
                    <p class="card-text">${item.price}</div>
                    <button class="btn btn-dark">Detail</button>
                </div>
            </div>
        </div>
        `;
    }
    document.querySelector('#content').innerHTML += strHTML;
}
// getAllProductApi();


//Application js 

/*
    localstorage: Lưu được 5mb tại client và lưu dạng key value
    .getItem('key'): lấy ra dữ liệu value từ localstorage dựa vào key
    .setItem('key'): Gán giá trị vào localstorage dựa vào key
    .removeItem('key'): Xoá giá trị localstorage dựa vào key
*/



window.setLocalstorage = () => {
    localStorage.setItem("data","data 123");

    let prod =  {
        id:1,
        name:'Iphone',
        price: 1000
    }

    //primitive value: string, boolean,number 
    localStorage.setItem('data object',JSON.stringify(prod));
    /*
        json -> string : JSON.stringify <=>  JsonSerializer.Serialize()
        string -> json: JSON.parse <=> JsonSerializer.Deserialize<T>    
    */
}




window.getStorage = () => {
    let sData = localStorage.getItem('data');
    console.log(sData)

    //Lấy json data 
    let sJSON = localStorage.getItem('data object');
    console.log(sJSON);
    let json = JSON.parse(sJSON);
    console.log(json);
    console.log(json.id);
    console.log(json.name);
    console.log(json.price);
}

window.removeStorage = () => {
    localStorage.removeItem('data');
}




//Tạo các hàm get set remove cookie
export function setCookie(name, value, days) {
    const expires = new Date(Date.now() + days * 24 * 60 * 60 * 1000).toUTCString();
    document.cookie = `${name}=${value}; expires=${expires}; path=/`;
}

export function getCookie(name) {
    const cookies = document.cookie.split(';');
    for (let cookie of cookies) {
        const [cookieName, cookieValue] = cookie.trim().split('=');
        if (cookieName === name) {
            return cookieValue;
        }
    }
    return null;
}

export function removeCookie(name) {
    setCookie(name, '', -1);
}

setCookie('accessToken','ABCCybersoft123',30);