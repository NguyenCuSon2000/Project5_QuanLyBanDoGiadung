using DAL.Helper;
using Model;
using Helper;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System;

namespace DAL
{
  public partial class ThongKeRepository : IThongKeRepository
  {
    private IDatabaseHelper _dbHelper;
    public ThongKeRepository(IDatabaseHelper dbHelper)
    {
      _dbHelper = dbHelper;
    }

    public List<ChiTietDonHangModel> GetSanPhamBanChay(int pageIndex, int pageSize, out long total, string tenSanPham)
    {
      string msgError = "";
      total = 0;
      try
      {
        var dt = _dbHelper.ExecuteSProcedureReturnDataTable(out msgError, "sp_product_ban_chay",
            "@page_index", pageIndex,
            "@page_size", pageSize,
            "@tenSanPham", tenSanPham);
        if (!string.IsNullOrEmpty(msgError))
          throw new Exception(msgError);
        if (dt.Rows.Count > 0)
        {
          total = (long)dt.Rows[0]["RecordCount"];
        }

        return dt.ConvertTo<ChiTietDonHangModel>().ToList();
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public string GetSoLuongSanPham()
    {
      string msgError = "";
      try
      {
        var dt = _dbHelper.ExecuteSProcedureReturnDataTable(out msgError, "sp_thongke_SLSP");
        if (!string.IsNullOrEmpty(msgError))
          throw new Exception(msgError);
        return dt.Rows[0]["SLSP"].ToString();
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }
    public string GetSoLuongLoaiSP()
    {
      string msgError = "";
      try
      {
        var dt = _dbHelper.ExecuteSProcedureReturnDataTable(out msgError, "sp_thongke_SLLSP");
        if (!string.IsNullOrEmpty(msgError))
          throw new Exception(msgError);
        return dt.Rows[0]["SLLSP"].ToString();
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }
    public string GetSoLuongNhomSP()
    {
      string msgError = "";
      try
      {
        var dt = _dbHelper.ExecuteSProcedureReturnDataTable(out msgError, "sp_thongke_SLNSP");
        if (!string.IsNullOrEmpty(msgError))
          throw new Exception(msgError);
        return dt.Rows[0]["SLNSP"].ToString();
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }
    public string GetSoLuongHangSP()
    {
      string msgError = "";
      try
      {
        var dt = _dbHelper.ExecuteSProcedureReturnDataTable(out msgError, "sp_thongke_SLHSP");
        if (!string.IsNullOrEmpty(msgError))
          throw new Exception(msgError);
        return dt.Rows[0]["SLHSP"].ToString();
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }
    public string GetSoLuongDonHang()
    {
      string msgError = "";
      try
      {
        var dt = _dbHelper.ExecuteSProcedureReturnDataTable(out msgError, "sp_thongke_SLDH");
        if (!string.IsNullOrEmpty(msgError))
          throw new Exception(msgError);
        return dt.Rows[0]["SLDH"].ToString();
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }
    public string GetSoLuongNguoiDung()
    {
      string msgError = "";
      try
      {
        var dt = _dbHelper.ExecuteSProcedureReturnDataTable(out msgError, "sp_thongke_SLND");
        if (!string.IsNullOrEmpty(msgError))
          throw new Exception(msgError);
        return dt.Rows[0]["SLND"].ToString();
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }
    public string GetSoLuongTinTuc()
    {
      string msgError = "";
      try
      {
        var dt = _dbHelper.ExecuteSProcedureReturnDataTable(out msgError, "sp_thongke_SLTT");
        if (!string.IsNullOrEmpty(msgError))
          throw new Exception(msgError);
        return dt.Rows[0]["SLTT"].ToString();
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }
  }
}
