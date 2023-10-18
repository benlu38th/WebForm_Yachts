using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Yachts.newpages
{
    public partial class B_Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            string input_account = txt_user_account.Text;
            string input_PWD = txt_user_password.Text;

            //string input_account = txt_user_account.Text;
            //string input_PWD = txt_user_password.Text;

            //資料庫沒有一樣的帳號 = true
            bool noSameAccountInSQL = true;

            //1.連線資料庫
            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["YachtsConnectionString"].ConnectionString);

            //2.sql語法
            string sqlCheck = "SELECT user_account, user_password, user_salt FROM users";


            //3.創建command物件
            SqlCommand cmSqlCheck = new SqlCommand(sqlCheck, connection);

            //4.資料庫連線開啟
            connection.Open();

            //5.執行sql (連線的作法-需自行關閉)
            SqlDataReader reader = cmSqlCheck.ExecuteReader();
            //DataReader速度快只能逐筆單向有上往下而且不能計算，適合用來抓單筆資料

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    if (reader[0].ToString() == input_account)
                    {
                        noSameAccountInSQL = false;
                        break;
                    }
                }
                //如果資料庫有一樣的帳號才執行
                if (!noSameAccountInSQL)
                {
                    string salt = reader[2].ToString();
                    string inputPasswordHash = CreatePasswordHash(input_PWD, salt);

                    //如果密碼一樣才執行
                    if (inputPasswordHash == reader[1].ToString())
                    {
                        Response.Redirect("B_User_List.aspx");
                    }
                    else
                    {
                        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Message", "<script>alert('密碼錯誤!');</script>");
                    }
                }
                else
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Message", "<script>alert('資料庫沒有此帳號，請設新帳號!');</script>");
                }
            }
            //6.資料庫關閉
            connection.Close();
        }

        // 返回加密後的字串(密碼 , salt)
        public string CreatePasswordHash(string pwd, string strSalt)
        {
            //把密碼和Salt連起來
            string saltAndPwd = String.Concat(pwd, strSalt);
            //對密碼進行雜湊
            string hashenPwd = FormsAuthentication.HashPasswordForStoringInConfigFile(saltAndPwd, "sha1");
            //轉為小寫字元並擷取前16個字串
            hashenPwd = hashenPwd.ToLower().Substring(0, 16);
            //返回雜湊後的值
            return hashenPwd;
        }
    }
}