using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Yachts.pages
{
    public partial class login : System.Web.UI.Page
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            //清除Cache，避免登出後按上一頁還會顯示Cache頁面
            Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1));
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetNoStore();
        }
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            string input_account = txt_user_account.Text;
            string input_PWD = txt_user_password.Text;

            //資料庫沒有一樣的帳號 = true
            bool noSameAccountInSQL = true;

            //1.連線資料庫
            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["YachtsConnectionString"].ConnectionString);

            //2.sql語法
            string sqlCheck = "SELECT user_account, user_password, user_salt, user_id, user_rank, rightUserHas_id FROM users";


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
                        //給予驗證票
                        //宣告驗證票要夾帶的資料 (用;區隔)
                        string userData = reader["user_account"].ToString() + ";" + reader["user_rank"].ToString() + ";" + reader["rightUserHas_id"].ToString();

                        //設定驗證票(夾帶資料，cookie 命名) // 需額外引入using System.Web.Configuration;
                        SetAuthenTicket(userData, reader["user_id"].ToString());

                        //6.資料庫關閉
                        connection.Close();

                        txt_user_account.Text = "";
                        txt_user_password.Text = "";

                        //跳轉至使用者後台管理頁
                        Response.Redirect("user_list.aspx");
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
        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            Response.Redirect("B_register.aspx");
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
        private void SetAuthenTicket(string userData, string userId)
        {
            //宣告一個驗證票 //需額外引入 using System.Web.Security;
            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, userId, DateTime.Now, DateTime.Now.AddMinutes(3), false, userData);
            //加密驗證票  
            string encryptedTicket = FormsAuthentication.Encrypt(ticket);
            //建立 Cookie  //說明詳下
            HttpCookie authenticationCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
            //將 Cookie 寫入回應
            Response.Cookies.Add(authenticationCookie);
        }
    }
}