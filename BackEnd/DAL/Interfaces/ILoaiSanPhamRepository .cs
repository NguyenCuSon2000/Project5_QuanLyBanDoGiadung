using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    public interface ILoaiSanPhamRepository
    {
        List<LoaiSanPhamModel> GetDataAll();
        List<LoaiSanPhamModel> GetDataAllPaginate(int pageIndex, int pageSize, out long total);
        LoaiSanPhamModel GetDatabyID(string maloaisp);
        bool Create(LoaiSanPhamModel model);
        bool Update(LoaiSanPhamModel model);
        bool Delete(string maloaisp);
        List<LoaiSanPhamModel> Search(int pageIndex, int pageSize, out long total, string maloaisp, string tenloai);
        List<LoaiSanPhamModel> GetCategoryByGroup(int pageIndex, int pageSize, out long total, string manhom);
    }
}
