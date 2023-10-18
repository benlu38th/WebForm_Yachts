using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Configuration;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Yachts.pages
{
    public partial class B_register : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            string user_account = txt_user_account.Text;
            string user_sex = ddl_user_sex.SelectedValue;
            string user_mail = txt_user_mail.Text;
            string user_password = txt_user_password.Text;
            string user_passwordCheck = txt_user_passwordCheck.Text;

            //確認次輸入的密碼不同
            if (user_password != user_passwordCheck)
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Message", "<script>alert('兩次輸入的密碼不同!');</script>");
            }

            //確認是否都填入資料
            if (string.IsNullOrEmpty(user_account) || string.IsNullOrEmpty(user_mail) || string.IsNullOrEmpty(user_password))
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Message", "<script>alert('請填入帳號、姓名、信箱及密碼!');</script>");
            }
            else
            {
                //資料庫沒有一樣的帳號 = true
                bool noSameAccountInSQL = true;

                //1.連線資料庫
                SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["YachtsConnectionString"].ConnectionString);

                //2.sql語法
                string sqlCheck = "SELECT user_account FROM users";


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
                        if(reader[0].ToString() == user_account)
                        {
                            noSameAccountInSQL = false;
                            break;
                        }
                    }
                }

                //6.資料庫關閉
                connection.Close();

                //資料庫沒有一樣的帳號才執行
                if (noSameAccountInSQL)
                {
                    //產生鹽
                    string salt = CreateSalt(32);

                    //產生加密後的密碼
                    string encryptionedPWD = CreatePasswordHash(user_password, salt);

                    //2.sql語法
                    string sqlAdd = "INSERT INTO users(user_account, user_sex, user_mail, user_password, user_salt) VALUES(@user_account, @user_sex, @user_mail, @user_password, @user_salt)";

                    //3.創建command物件
                    SqlCommand cmSqlAdd = new SqlCommand(sqlAdd, connection);



                    //4. 參數化，Parameters:為了防止SQL Injection 程式碼攻擊，所以寫入SQL時，要使用參數「@參數名稱」來代替直接給的值，而給予參數的資料型態要使用Add
                    cmSqlAdd.Parameters.AddWithValue("@user_account", user_account);
                    cmSqlAdd.Parameters.AddWithValue("@user_sex", user_sex);
                    cmSqlAdd.Parameters.AddWithValue("@user_mail", user_mail);
                    cmSqlAdd.Parameters.AddWithValue("@user_password", encryptionedPWD);
                    cmSqlAdd.Parameters.AddWithValue("@user_salt", salt);

                    //5.資料庫連線開啟
                    connection.Open();

                    //6.執行sql (新增刪除修改)
                    cmSqlAdd.ExecuteNonQuery(); //無回傳值

                    //7.資料庫關閉
                    connection.Close();

                    Response.Redirect("B_login.aspx");
                }
                else
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Message", "<script>alert('資料庫有相同帳號，請重設帳號!');</script>");
                }
            }
        }
        // 建立一個指定長度的隨機salt值
        public string CreateSalt(int saltLenght)
        {
            //生成一個加密的隨機數
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            byte[] buff = new byte[saltLenght];
            rng.GetBytes(buff);

            //返回一個Base64隨機數的字串
            return Convert.ToBase64String(buff);
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