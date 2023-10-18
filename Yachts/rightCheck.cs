using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Security;

namespace Yachts
{
    public class rightCheck
    {
        public static void HaveWebRight(string nowWebPageRight_id, HttpResponse response)
        {
            //以下判斷網頁權限
            //取得使用者識別
            string ticketUserID = ((FormsIdentity)(HttpContext.Current.User.Identity)).Ticket.Name;

            //1.連線資料庫
            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["YachtsConnectionString"].ConnectionString);

            //2.sql語法，VALUES先使用@參數來取代直接給值，以防SQL Injection 程式碼
            //刪除
            string sql = "SELECT * FROM rightUserHas WHERE user_id = @user_id; ";

            //3.創建command物件
            SqlCommand command = new SqlCommand(sql, connection);

            //4.參數化，Parameters:為了防止SQL Injection 程式碼攻擊，所以寫入SQL時，要使用參數「@參數名稱」來代替直接給的值，而給予參數的資料型態要使用Add
            command.Parameters.AddWithValue("@user_id", Convert.ToInt32(ticketUserID));

            //5.資料庫連線開啟
            connection.Open();

            //6.執行sql (連線的作法-需自行關閉)
            SqlDataReader reader = command.ExecuteReader();

            bool haveRight = false;

            while (reader.Read())
            {
                //找到代表有權限
                if (reader["right_id"].ToString() == nowWebPageRight_id)
                {
                    haveRight = true;
                }
            }
            //7.資料庫關閉
            connection.Close();

            //沒權限跳回登入頁
            if (!haveRight)
            {
                response.Redirect("login.aspx");
            }
            //以上判斷網頁權限
        }
    }
}