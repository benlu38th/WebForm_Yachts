using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Yachts.pages
{
    public partial class MasterPage : System.Web.UI.MasterPage
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ////清除Cache，避免登出後按上一頁還會顯示Cache頁面
            Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1));
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetNoStore();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //檢查用戶是否已驗證
                if (HttpContext.Current.User.Identity.IsAuthenticated)
                {
                    //做法二
                    //取得使用者資料
                    string ticketUserData = ((FormsIdentity)(HttpContext.Current.User.Identity)).Ticket.UserData;
                    string[] ticketUserDataArr = ticketUserData.Split(';');

                    lit_user_account.Text = ticketUserDataArr[0];
                }
                //else
                //{
                //    Response.Redirect("B_login.aspx");
                //}

            }


            //以下判斷左邊列表的顏色
            Literal active = head.FindControl("active") as Literal;

            if (active.Text == "User_List")
            {
                User_List.Attributes["class"] = "active";
            }
            else if (active.Text == "Dealers")
            {
                Dealers.Attributes["class"] = "active";
            }
            else if (active.Text == "DealersDetail")
            {
                DealersDetail.Attributes["class"] = "active";
            }
            else if (active.Text == "Company")
            {
                Company.Attributes["class"] = "active";
            }
            else if (active.Text == "News")
            {
                News.Attributes["class"] = "active";
            }
            else if (active.Text == "Yatchs")
            {
                Yatchs.Attributes["class"] = "active";
            }
            else if (active.Text == "Client")
            {
                Client.Attributes["class"] = "active";
            }
            //以上判斷左邊列表的顏色

            //以下判斷左邊列表的權限
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


            while (reader.Read())
            {
                for (int i = 1; i <= 5; i++)
                {
                    string a = reader["right_id"].ToString();
                    if (reader["right_id"].ToString() == i.ToString())
                    {

                        if (i == 1)
                        {
                            Dealers.Visible = true;
                            DealersDetail.Visible = true;
                            break;
                        }
                        else if (i == 2)
                        {
                            Company.Visible = true;
                            break;
                        }
                        else if (i == 3)
                        {
                            News.Visible = true;
                            break;
                        }
                        else if (i == 4)
                        {
                            Yatchs.Visible = true;
                            break;
                        }
                        else if (i == 5)
                        {
                            Client.Visible = true;
                            break;
                        }
                        
                    }
                }
            }


            //7.資料庫關閉
            connection.Close();
            //以上判斷左邊列表的顏色

        }
        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            //清除cookie
            Response.Cookies[".ASPXAUTH"].Expires = DateTime.Now.AddYears(-1);
            //登出驗證表單
            FormsAuthentication.SignOut();

            Response.Redirect("login.aspx");
        }
    }
}