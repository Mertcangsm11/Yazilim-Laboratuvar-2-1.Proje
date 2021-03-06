using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Configuration;
using System.Net.Mail;
using System.Net;
using System.Security.Cryptography;
using System.IO;
using System.Data.Common;
using System.Data.OleDb;
using System.Drawing;
using System.Text;
using ClosedXML.Excel;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using CarProje.Models;

namespace CarProje
{
    public class _main
    {
        public static string str_connection    = string.Empty;
        public static string s_AdminUser     = string.Empty;
        public static LoginUserModel loginUser = new LoginUserModel();

        internal static int komutcalistir(string sql_str)
        {
            int ret = 0;
            SqlConnection baglanti = new SqlConnection(str_connection);
            try
            {
                SqlCommand komut = new SqlCommand(sql_str, baglanti);
                baglanti.Open();
                ret = komut.ExecuteNonQuery();
                baglanti.Close();
                return ret;
            }
            catch (Exception ex)
            {
                if (baglanti.State == ConnectionState.Open)
                    baglanti.Close();
                return ret;
            }
        }

        internal static int komutcalistir_int(string sql_str)
        {
            object ret;
            int i_ret = 0;
            SqlConnection baglanti = new SqlConnection(str_connection);
            try
            {
                SqlCommand komut = new SqlCommand(sql_str, baglanti);
                baglanti.Open();
                ret = komut.ExecuteScalar();
                baglanti.Close();
                if (ret == null)
                {
                    i_ret = 0;
                }
                else
                {
                    if (!int.TryParse(ret.ToString(), out i_ret))
                        i_ret = 0;
                }
                return i_ret;
            }
            catch (Exception ex)
            {
                if (baglanti.State == ConnectionState.Open)
                    baglanti.Close();
                return i_ret;
            }
        }

        internal static bool komutcalistir_bool(string sql_str)
        {
            object ret;
            bool b_ret = false;
            SqlConnection baglanti = new SqlConnection(str_connection);
            try
            {
                SqlCommand komut = new SqlCommand(sql_str, baglanti);
                baglanti.Open();
                ret = komut.ExecuteScalar();
                baglanti.Close();
                if (ret == null)
                {
                    b_ret = false;
                }
                else
                {
                    if (!bool.TryParse(ret.ToString(), out b_ret))
                        b_ret = false;
                }
                return b_ret;
            }
            catch (Exception ex)
            {
                if (baglanti.State == ConnectionState.Open)
                    baglanti.Close();
                return b_ret;
            }
        }

        internal static string komutcalistir_str(string sql_str)
        {
            Object ret;
            SqlConnection baglanti = new SqlConnection(str_connection);
            try
            {
                SqlCommand komut = new SqlCommand(sql_str, baglanti);
                baglanti.Open();
                ret = komut.ExecuteScalar();
                baglanti.Close();
                if (ret == null)
                {
                    ret = "";
                }
                return ret.ToString();
            }
            catch (Exception ex)
            {
                if (baglanti.State == ConnectionState.Open)
                    baglanti.Close();
                ret = "";
                return ret.ToString();
            }
        }

        internal static DataTable komutcalistir_dt(string sql_str)
        {
            SqlConnection baglanti = new SqlConnection(str_connection);
            DataTable dt = new DataTable();
            try
            {
                SqlCommand komut = new SqlCommand(sql_str, baglanti);
                baglanti.Open();
                SqlDataReader dr = komut.ExecuteReader();
                dt.Load(dr);
                baglanti.Close();
                return dt;
            }
            catch (Exception ex)
            {
                if (baglanti.State == ConnectionState.Open)
                    baglanti.Close();
                return dt;
            }
        }

        internal static string Decrypt(string cipherText, string Password)
        {
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            PasswordDeriveBytes pdb = new PasswordDeriveBytes(Password,
                new byte[] {0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 
            0x64, 0x76, 0x65, 0x64, 0x65, 0x76});
            byte[] decryptedData = Decrypt(cipherBytes,
                pdb.GetBytes(32), pdb.GetBytes(16));
            return System.Text.Encoding.Unicode.GetString(decryptedData);
        }

        internal static string Encrypt(string clearText, string Password)
        {
            byte[] clearBytes =
              System.Text.Encoding.Unicode.GetBytes(clearText);
            PasswordDeriveBytes pdb = new PasswordDeriveBytes(Password,
                new byte[] {0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 
            0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76});
            byte[] encryptedData = Encrypt(clearBytes,
                     pdb.GetBytes(32), pdb.GetBytes(16));
            return Convert.ToBase64String(encryptedData);
        }

        internal static byte[] Encrypt(byte[] clearData, byte[] Key, byte[] IV)
        {
            MemoryStream ms = new MemoryStream();
            Rijndael alg = Rijndael.Create();
            alg.Key = Key;
            alg.IV = IV;
            CryptoStream cs = new CryptoStream(ms,
               alg.CreateEncryptor(), CryptoStreamMode.Write);
            cs.Write(clearData, 0, clearData.Length);
            cs.Close();
            byte[] encryptedData = ms.ToArray();
            return encryptedData;
        }
        
        internal static byte[] Decrypt(byte[] cipherData, byte[] Key, byte[] IV)
        {
            MemoryStream ms = new MemoryStream();
            Rijndael alg = Rijndael.Create();
            alg.Key = Key;
            alg.IV = IV;
            CryptoStream cs = new CryptoStream(ms,
                alg.CreateDecryptor(), CryptoStreamMode.Write);
            cs.Write(cipherData, 0, cipherData.Length);
            cs.Close();
            byte[] decryptedData = ms.ToArray();
            return decryptedData;
        }

        internal static void tbl_control()
        {
            string tbl_users = @"IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 'BASE TABLE' AND TABLE_NAME = 'USERS')
            BEGIN
            CREATE TABLE [dbo].[USERS](
	            [ID] [int] IDENTITY(1,1) NOT NULL,
	            [GROUPID] [int] NOT NULL,
	            [EMAIL] [nvarchar](500) NULL,
	            [PASSWORD] [nvarchar](max) NULL,
	            [FULLNAME] [nvarchar](500) NULL,
                [LOGIN] [nvarchar](500) NULL,
                [EXIT] [nvarchar](500) NULL,
             CONSTRAINT [PK_USERS] PRIMARY KEY CLUSTERED 
            (
	            [ID] ASC
            )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
            ) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
            END";
            komutcalistir(tbl_users);
        }

    }
}