using DAL.Helper;
using Model;
using Helper;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System;

namespace DAL
{
    public partial class TinTucRepository : ITinTucRepository
    {
        private IDatabaseHelper _dbHelper;
        public TinTucRepository(IDatabaseHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }

        public TinTucModel GetDatabyID(string matintuc)
        {
            string msgError = "";
            try
            {
                var dt = _dbHelper.ExecuteSProcedureReturnDataTable(out msgError, "sp_blog_get_by_id",
                     "@matintuc", matintuc);
                if (!string.IsNullOrEmpty(msgError))
                    throw new Exception(msgError);
                return dt.ConvertTo<TinTucModel>().FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Create(TinTucModel model)
        {
            string msgError = "";
            try
            {
                var result = _dbHelper.ExecuteScalarSProcedureWithTransaction(out msgError, "sp_blog_create",
                "@matintuc", model.MaTinTuc,
                "@tieude", model.TieuDe,
                "@mota", model.MoTa,
                "@noidung", model.NoiDung,
                 "@manguoidung", model.MaNguoiDung,
                "@ngaydang", model.NgayDang,
                "@anh", model.Anh);
                if ((result != null && !string.IsNullOrEmpty(result.ToString())) || !string.IsNullOrEmpty(msgError))
                {
                    throw new Exception(Convert.ToString(result) + msgError);
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Delete(string matintuc)
        {
            string msgError = "";
            try
            {
                var result = _dbHelper.ExecuteScalarSProcedureWithTransaction(out msgError, "sp_blog_delete",
                "@matintuc", matintuc);
                if ((result != null && !string.IsNullOrEmpty(result.ToString())) || !string.IsNullOrEmpty(msgError))
                {
                    throw new Exception(Convert.ToString(result) + msgError);
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool Update(TinTucModel model)
        {
            string msgError = "";
            try
            {
                var result = _dbHelper.ExecuteScalarSProcedureWithTransaction(out msgError, "sp_blog_update",
                 "@matintuc", model.MaTinTuc,
                "@tieude", model.TieuDe,
                "@mota", model.MoTa,
                "@noidung", model.NoiDung,
                 "@manguoidung", model.MaNguoiDung,
                "@ngaydang", model.NgayDang,
                "@anh", model.Anh);
                if ((result != null && !string.IsNullOrEmpty(result.ToString())) || !string.IsNullOrEmpty(msgError))
                {
                    throw new Exception(Convert.ToString(result) + msgError);
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public List<TinTucModel> Search(int pageIndex, int pageSize, out long total, string tieuDe)
        {
            string msgError = "";
            total = 0;
            try
            {
                var dt = _dbHelper.ExecuteSProcedureReturnDataTable(out msgError, "sp_blog_search",
                    "@page_index", pageIndex,
                    "@page_size", pageSize,
                    "@tieuDe", tieuDe);
                if (!string.IsNullOrEmpty(msgError))
                    throw new Exception(msgError);
                if (dt.Rows.Count > 0) total = (long)dt.Rows[0]["RecordCount"];
                return dt.ConvertTo<TinTucModel>().ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
