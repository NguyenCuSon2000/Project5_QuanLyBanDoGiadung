using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    public partial interface IDonHangBussiness
    {
        List<DonHangModel> GetDataAll();
        List<DonHangModel> GetDataAllPaginate(int pageIndex, int pageSize, out long total);
        bool Create(DonHangModel model);
        bool Update(DonHangModel model);
        DonHangModel GetDatabyID(string id);
        bool Delete(string id);
        List<DonHangModel> Search(int pageIndex, int pageSize, out long total, string hoten, string diachi, string email);

    }
}
