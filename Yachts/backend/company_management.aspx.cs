using CKFinder;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Yachts.pages
{
    public partial class B_company_management : System.Web.UI.Page
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            if (!HttpContext.Current.User.Identity.IsAuthenticated)
            {
                Response.Redirect("login.aspx");
            }
            //目前網頁[nowWebPageRight_id}手動輸入，每頁不同
            string nowWebPageRight_id = "2";
            //判斷是否有該頁全限，沒權限跳轉回login頁面
            rightCheck.HaveWebRight(nowWebPageRight_id, Response);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string title_id = Request.QueryString["title_id"];
                string title_name = Request.QueryString["title_name"];

                if (title_id == null || title_name == null)
                {
                    Response.Redirect("company.aspx");
                }

                lbl_title_name.Text = title_name;

                //CKEditor設定
                FileBrowser fileBrowser = new FileBrowser();
                fileBrowser.BasePath = "/ckfinder";
                fileBrowser.SetupCKEditor(cke_title_des);

                //渲染
                Show();
            }

        }
        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.Redirect("company.aspx");
        }
        private void Show()
        {
            string title_id = Request.QueryString["title_id"];

            //1.連線資料庫(合在一起寫)
            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["YachtsConnectionString"].ConnectionString);

            //2.sql語法
            string sql = "SELECT title_des FROM companyTitles WHERE title_id = @title_id";

            //3.創建command物件
            SqlCommand command = new SqlCommand(sql, connection);

            //3.5.參數化，Parameters:為了防止SQL Injection 程式碼攻擊，所以寫入SQL時，要使用參數「@參數名稱」來代替直接給的值，而給予參數的資料型態要使用Add
            command.Parameters.AddWithValue("@title_id", title_id);

            //4.資料庫連線開啟
            connection.Open();

            //5.執行sql (連線的作法-需自行關閉)
            SqlDataReader reader = command.ExecuteReader();
            //DataReader速度快只能逐筆單向有上往下而且不能計算，適合用來抓單筆資料

            if (reader.Read())
            {
                cke_title_des.Text = reader[0].ToString();
                lbl_title_des.Text = HttpUtility.HtmlDecode(reader[0].ToString() );
            }

            //6.資料庫關閉
            connection.Close();
        }

        protected void Button1_Click1(object sender, EventArgs e)
        {
            if (Button1.Text == "開始編輯")
            {
                Button1.Text = "確認送出";
                lbl_title_des.Visible = false;
                cke_title_des.Visible = true;
                // 將焦點設定回按鈕
                Page.SetFocus(Button1);
            }
            else
            {
                string title_id = Request.QueryString["title_id"];
                string title_des = cke_title_des.Text;

                //1.連線資料庫
                SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["YachtsConnectionString"].ConnectionString);

                //2.sql語法，VALUES先使用@參數來取代直接給值，以防SQL Injection 程式碼
                //修改
                string sql = "UPDATE companyTitles SET title_des = @title_des  WHERE title_id = @title_id";

                //3.創建command物件
                SqlCommand command = new SqlCommand(sql, connection);

                //4.參數化，Parameters:為了防止SQL Injection 程式碼攻擊，所以寫入SQL時，要使用參數「@參數名稱」來代替直接給的值，而給予參數的資料型態要使用Add
                command.Parameters.AddWithValue("@title_id", title_id);
                command.Parameters.AddWithValue("@title_des", title_des);

                //5.資料庫連線開啟
                connection.Open();

                //6.執行sql (新增刪除修改)
                command.ExecuteNonQuery(); //無回傳值

                //7.資料庫關閉
                connection.Close();

                Show();

                Button1.Text = "開始編輯";
                lbl_title_des.Visible = true;
                cke_title_des.Visible = false;

                // 將焦點設定回按鈕
                Page.SetFocus(Button1);
            }
        }

        protected void lbtn_goCompany_Click(object sender, EventArgs e)
        {
            Response.Redirect("company.aspx");
        }
    }
}