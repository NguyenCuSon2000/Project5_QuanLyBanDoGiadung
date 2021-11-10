using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    public partial interface INguoiDungBussiness
    {
        NguoiDungModel Authenticate(string username, string password);
        List<NguoiDungModel> GetDataAllPaginate(int pageIndex, int pageSize, out long total);
        NguoiDungModel GetDatabyID(string id);
        bool Create(NguoiDungModel model);
        bool Update(NguoiDungModel model);
        bool Delete(string user_id);
        List<NguoiDungModel> Search(int pageIndex, int pageSize, out long total, string hoTen, string taiKhoan);
    }
}
