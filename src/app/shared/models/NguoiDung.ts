import { Role } from "./Role.enum";
export class NguoiDung {
    MaNguoiDung: number;
    HoTen: string;
    NgaySinh: any;
    DiaChi: string;
    GioiTinh: string;
    Email: string;
    TaiKhoan: string;
    MatKhau: string;
    role: Role;
    anh: string;
    token?: string;
}