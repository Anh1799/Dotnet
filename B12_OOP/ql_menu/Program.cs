
Menu menuMonAn = new Menu();
menuMonAn.HienThiChucNang();
int chon = menuMonAn.ChonChucNang();
while(chon != 4){
    switch(menuMonAn.Chon){
        case 1 : {
            MonAn monAn = new MonAn();
            menuMonAn.ThemMonAn(monAn);
        }; break;
        default: {

        }; break;
    }

    // hien thi menu sau khi thuc hien xong 1 chuc nang
    menuMonAn.HienThiChucNang();
    menuMonAn.ChonChucNang();

}