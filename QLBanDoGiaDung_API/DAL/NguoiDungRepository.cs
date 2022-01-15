using DAL.Helper;
using Model;
using Helper;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System;
using System.Security.Cryptography;

namespace DAL
{
    public partial class NguoiDungRepository : INguoiDungRepository
    {
        private IDatabaseHelper _dbHelper;
        public NguoiDungRepository(IDatabaseHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }
        public List<NguoiDungModel> GetDataAllPaginate(int pageIndex, int pageSize, out long total)
        {
            string msgError = "";
            total = 0;
            try
            {
                var dt = _dbHelper.ExecuteSProcedureReturnDataTable(out msgError, "sp_user_all_paginate",
                    "@page_index", pageIndex,
                    "@page_size", pageSize);
                if (!string.IsNullOrEmpty(msgError))
                    throw new Exception(msgError);
                if (dt.Rows.Count > 0) total = (long)dt.Rows[0]["RecordCount"];
                return dt.ConvertTo<NguoiDungModel>().ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool Create(NguoiDungModel model)
        {
            string msgError = "";
            try
            {
                var result = _dbHelper.ExecuteScalarSProcedureWithTransaction(out msgError, "sp_user_create",
                "@manguoidung", model.MaNguoiDung,
                "@hoten", model.HoTen,
                "@ngaysinh", model.NgaySinh,
                "@diachi", model.DiaChi,
                "@gioitinh", model.GioiTinh,
                "@email", model.Email,
                "@taikhoan", model.TaiKhoan,
                "@matkhau", model.MatKhau,
                "@role", model.role);
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

        public bool Delete(string id)
        {
            string msgError = "";
            try
            {
                var result = _dbHelper.ExecuteScalarSProcedureWithTransaction(out msgError, "sp_user_delete",
                "@user_id", id);
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
        public bool Update(NguoiDungModel model)
        {
            string msgError = "";
            try
            {
                var result = _dbHelper.ExecuteScalarSProcedureWithTransaction(out msgError, "sp_user_update",
                "@manguoidung", model.MaNguoiDung,
                "@hoten", model.HoTen,
                "@ngaysinh", model.NgaySinh,
                "@diachi", model.DiaChi,
                "@gioitinh", model.GioiTinh,
                "@email", model.Email,
                "@taikhoan", model.TaiKhoan,
                "@matkhau", model.MatKhau,
                "@role", model.role);
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


        public NguoiDungModel GetUser(string username, string password)
        {
            string msgError = "";
            try
            {
                var dt = _dbHelper.ExecuteSProcedureReturnDataTable(out msgError, "sp_user_get_by_username_password",
                     "@taikhoan", username,
                     "@matkhau", GetMD5(password));
                if (!string.IsNullOrEmpty(msgError))
                    throw new Exception(msgError);
                return dt.ConvertTo<NguoiDungModel>().FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public NguoiDungModel GetDatabyID(string id)
        {
            string msgError = "";
            try
            {
                var dt = _dbHelper.ExecuteSProcedureReturnDataTable(out msgError, "sp_user_get_by_id",
                     "@manguoidung", id);
                if (!string.IsNullOrEmpty(msgError))
                    throw new Exception(msgError);
                return dt.ConvertTo<NguoiDungModel>().FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<NguoiDungModel> Search(int pageIndex, int pageSize, out long total, string hoten, string taikhoan)
        {
            string msgError = "";
            total = 0;
            try
            {
                var dt = _dbHelper.ExecuteSProcedureReturnDataTable(out msgError, "sp_user_search",
                    "@page_index", pageIndex,
                    "@page_size", pageSize,
                    "@hoTen", hoten,
                    "@taiKhoan", taikhoan);
                if (!string.IsNullOrEmpty(msgError))
                    throw new Exception(msgError);
                if (dt.Rows.Count > 0) total = (long)dt.Rows[0]["RecordCount"];
                return dt.ConvertTo<NguoiDungModel>().ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static string GetMD5(string str)
        {
          MD5 md5 = new MD5CryptoServiceProvider();
          byte[] fromData = Encoding.UTF8.GetBytes(str);
          byte[] targetData = md5.ComputeHash(fromData);
          string byte2String = null;

          for (int i = 0; i < targetData.Length; i++)
          {
            byte2String += targetData[i].ToString("x2");

          }
          return byte2String;
        }

    }
}
