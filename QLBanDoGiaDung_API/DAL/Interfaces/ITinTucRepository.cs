using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    public interface ITinTucRepository
    {
        TinTucModel GetDatabyID(string matintuc);
        bool Create(TinTucModel model);
        bool Update(TinTucModel model);
        bool Delete(string matintuc);
        List<TinTucModel> Search(int pageIndex, int pageSize, out long total, string tieuDe);
    }
}
