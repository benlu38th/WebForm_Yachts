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
    public partial class B_dealers_edit : System.Web.UI.Page
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            if (!HttpContext.Current.User.Identity.IsAuthenticated)
            {
                Response.Redirect("login.aspx");
            }
            //目前網頁[nowWebPageRight_id}手動輸入，每頁不同
            string nowWebPageRight_id = "1";
            //判斷是否有該頁全限，沒權限跳轉回login頁面
            rightCheck.HaveWebRight(nowWebPageRight_id, Response);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            FileBrowser fileBrowser = new FileBrowser();
            fileBrowser.BasePath = "/ckfinder";
            fileBrowser.SetupCKEditor(cke_dealer_des);

            string co = Request.QueryString["co"];
            string co_id = Request.QueryString["co_id"];
            int dealer_id = Convert.ToInt32( Request.QueryString["dealer_id"] );

            if (!IsPostBack)
            {
                if (!HttpContext.Current.User.Identity.IsAuthenticated)
                {
                    Response.Redirect("login.aspx");
                }

                if (co == null || co_id == null)
                {
                    Response.Redirect("dealers_countries.aspx");
                }
                else
                {
                    txt_countries.Text = co;

                    //1.連線資料庫(合在一起寫)
                    SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["YachtsConnectionString"].ConnectionString);


                    //2.sql語法
                    string sql = $"SELECT area_name FROM areas WHERE area_id = ( SELECT area_id FROM dealers WHERE dealer_id = {dealer_id}) ";

                    //3.創建command物件
                    SqlCommand command = new SqlCommand(sql, connection);

                    //4.資料庫連線開啟
                    connection.Open();

                    //5.執行sql (連線的作法-需自行關閉)
                    SqlDataReader reader = command.ExecuteReader();
                    //DataReader速度快只能逐筆單向有上往下而且不能計算，適合用來抓單筆資料
                    if (reader.Read())
                    {
                        txt_area.Text = reader[0].ToString();
                    }
                    //6.資料庫關閉
                    connection.Close();
                }

                Show();
            }

        }
        private void Show()
        {
            int dealer_id = Convert.ToInt32(Request.QueryString["dealer_id"]);

            //1.連線資料庫(合在一起寫)
            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["YachtsConnectionString"].ConnectionString);


            //2.sql語法
            string sql = $"SELECT dealer_des FROM dealers WHERE dealer_id = {dealer_id}";

            //3.創建command物件
            SqlCommand command = new SqlCommand(sql, connection);

            //4.資料庫連線開啟
            connection.Open();

            //5.執行sql (連線的作法-需自行關閉)
            SqlDataReader reader = command.ExecuteReader();
            //DataReader速度快只能逐筆單向有上往下而且不能計算，適合用來抓單筆資料
            if (reader.Read())
            {
                cke_dealer_des.Text = reader[0].ToString();
            }

            //6.資料庫關閉
            connection.Close();
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            string co = Request.QueryString["co"];
            string co_id = Request.QueryString["co_id"];

            Response.Redirect("dealers.aspx?co=" + co + "&co_id=" + co_id );
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string dealer_id = Request.QueryString["dealer_id"];
            string dealer_des = cke_dealer_des.Text;

            //1.連線資料庫
            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["YachtsConnectionString"].ConnectionString);

            //2.sql語法，VALUES先使用@參數來取代直接給值，以防SQL Injection 程式碼
            //修改
            string sql = "UPDATE dealers SET dealer_des = @dealer_des  WHERE dealer_id = @dealer_id ";

            //3.創建command物件
            SqlCommand command = new SqlCommand(sql, connection);

            //4.參數化，Parameters:為了防止SQL Injection 程式碼攻擊，所以寫入SQL時，要使用參數「@參數名稱」來代替直接給的值，而給予參數的資料型態要使用Add
            command.Parameters.AddWithValue("@dealer_des", HttpUtility.HtmlEncode(dealer_des) );
            command.Parameters.AddWithValue("@dealer_id", Convert.ToInt32(dealer_id) );

            //5.資料庫連線開啟
            connection.Open();

            //6.執行sql (新增刪除修改)
            command.ExecuteNonQuery(); //無回傳值

            //7.資料庫關閉
            connection.Close();

            string co = Request.QueryString["co"];
            string co_id = Request.QueryString["co_id"];

            Response.Redirect("dealers.aspx?co=" + co + "&co_id=" + co_id);
        }
    }
}