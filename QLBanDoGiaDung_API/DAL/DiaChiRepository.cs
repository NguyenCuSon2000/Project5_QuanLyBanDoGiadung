using DAL.Helper;
using Model;
using Helper;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System;

namespace DAL
{
    public partial class DiaChiRepository : IDiaChiRepository
    {
        private IDatabaseHelper _dbHelper;
        public DiaChiRepository(IDatabaseHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }

        public List<TinhThanhPho> GetTinhThanhPho()
        {
            string msgError = "";
            try
            {
                var dt = _dbHelper.ExecuteSProcedureReturnDataTable(out msgError, "sp_province_all");
                if (!string.IsNullOrEmpty(msgError))
                    throw new Exception(msgError);
                return dt.ConvertTo<TinhThanhPho>().ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<QuanHuyen> GetQuanHuyen()
        {
            string msgError = "";
            try
            {
                var dt = _dbHelper.ExecuteSProcedureReturnDataTable(out msgError, "sp_district_all");
                if (!string.IsNullOrEmpty(msgError))
                    throw new Exception(msgError);
                return dt.ConvertTo<QuanHuyen>().ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<QuanHuyen> GetQHbyMaTinh(string maTP)
        {
            string msgError = "";
            try
            {
                var dt = _dbHelper.ExecuteSProcedureReturnDataTable(out msgError, "sp_getQH_by_MaTP",
                    "@maTP", maTP);
                if (!string.IsNullOrEmpty(msgError))
                    throw new Exception(msgError);
                return dt.ConvertTo<QuanHuyen>().ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<XaPhuong> GetXaPhuong()
        {
            string msgError = "";
            try
            {
                var dt = _dbHelper.ExecuteSProcedureReturnDataTable(out msgError, "sp_ward_all");
                if (!string.IsNullOrEmpty(msgError))
                    throw new Exception(msgError);
                return dt.ConvertTo<XaPhuong>().ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<XaPhuong> GetXPbyMaQH(string maQH)
        {
            string msgError = "";
            try
            {
                var dt = _dbHelper.ExecuteSProcedureReturnDataTable(out msgError, "sp_getXP_by_MaQH",
                    "@maQH", maQH);
                if (!string.IsNullOrEmpty(msgError))
                    throw new Exception(msgError);
                return dt.ConvertTo<XaPhuong>().ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
