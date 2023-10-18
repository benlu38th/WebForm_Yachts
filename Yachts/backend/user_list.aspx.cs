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
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            if (!HttpContext.Current.User.Identity.IsAuthenticated)
            {
                Response.Redirect("login.aspx");
            }

            

        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                Show();

            }

        }
        private void Show()
        {
            //1.連線資料庫(合在一起寫)
            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["YachtsConnectionString"].ConnectionString);

            //2.sql語法
            string sql = "SELECT * FROM users";

            //3.創建command物件
            SqlCommand command = new SqlCommand(sql, connection);

            //4.資料庫連線開啟
            connection.Open();

            //5.執行sql (連線的作法-需自行關閉)
            SqlDataReader reader = command.ExecuteReader();
            //DataReader速度快只能逐筆單向有上往下而且不能計算，適合用來抓單筆資料
            //控制器資料來源
            GridView1.DataSource = reader; //(拿資料)
                                           //控制器綁定 (真的把資料放進去)
            GridView1.DataBind();

            //6.資料庫關閉
            connection.Close();
        }

        protected void lbtn_user_management_Click(object sender, EventArgs e)
        {
            // 取得點擊的 LinkButton
            LinkButton lb = (LinkButton)sender;

            // 取得所在的 GridViewRow
            GridViewRow row = (GridViewRow)lb.NamingContainer;

            //取得該列的主索引欄位的值(須在gridview 加入 DataKeyNames = "user_id")
            string user_id = GridView1.DataKeys[row.RowIndex]["user_id"].ToString();

            Response.Redirect("user_management.aspx?user_id=" + user_id);
        }
        //選擇刪除按鈕及管理按鈕是否出現
        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //取得使用者User_Rank
            string ticketUserData = ((FormsIdentity)(HttpContext.Current.User.Identity)).Ticket.UserData;
            string[] ticketUserDataArr = ticketUserData.Split(';');
            string ticketUser_Rank = ticketUserDataArr[1];

            //取得那一列的的user_rank
            int user_rank = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "user_rank"));

            // 檢查使用者User_Rank是否小於每一列的user_rank，若結果為是隱藏該按鈕
            if (Convert.ToInt32(ticketUser_Rank) <= user_rank)
            {
                // 取得編輯按鈕控制項
                LinkButton lbtn_user_management = (LinkButton)e.Row.FindControl("lbtn_user_management");
                // 將編輯按鈕控制項的 Visible 屬性設為 false，隱藏該按鈕
                lbtn_user_management.Visible = false;

                // 取得刪除按鈕控制項
                LinkButton lbtn_delete = (LinkButton)e.Row.FindControl("lbtn_delete");
                // 將編輯按鈕控制項的 Visible 屬性設為 false，隱藏該按鈕
                lbtn_delete.Visible = false;
            }
        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            //取得被點擊的 GridView 的資料列索引
            int rowIndex = e.RowIndex;

            //取得 GridView 控制項中指定索引的資料列
            GridViewRow row = GridView1.Rows[rowIndex];

            //取得該列的主索引欄位的值(須在gridview 加入 DataKeyNames = "user_id")
            string user_id = GridView1.DataKeys[row.RowIndex]["user_id"].ToString();

            //1.連線資料庫
            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["YachtsConnectionString"].ConnectionString);

            //2.sql語法，VALUES先使用@參數來取代直接給值，以防SQL Injection 程式碼
            //刪除
            string sql = "DELETE FROM users WHERE user_id = @user_id; ";

            //3.創建command物件
            SqlCommand command = new SqlCommand(sql, connection);

            //4.參數化，Parameters:為了防止SQL Injection 程式碼攻擊，所以寫入SQL時，要使用參數「@參數名稱」來代替直接給的值，而給予參數的資料型態要使用Add
            command.Parameters.AddWithValue("@user_id", Convert.ToInt32(user_id));

            //5.資料庫連線開啟
            connection.Open();

            //6.執行sql (新增刪除修改)
            command.ExecuteNonQuery(); //無回傳值

            //7.資料庫關閉
            connection.Close();

            //畫面渲染
            Show();
        }
    }
}