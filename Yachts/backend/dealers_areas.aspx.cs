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
    public partial class B_dealers_areas : System.Web.UI.Page
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
            if (!IsPostBack)
            {

                Show_ddl_country_name();

                string co = Request.QueryString["co"];
                string co_id = Request.QueryString["co_id"];
                if (!string.IsNullOrEmpty(co) && !string.IsNullOrEmpty(co_id))
                {
                    ddl_country_name.Items.FindByValue(co_id).Selected = true;
                }
                Show();
            }
        }
        protected void ddl_country_name_SelectedIndexChanged(object sender, EventArgs e)
        {
            string co = ddl_country_name.SelectedItem.Text;
            string co_id = ddl_country_name.SelectedValue;
            Response.Redirect("dealers_areas.aspx?co=" + co + "&co_id=" + co_id);
        }
        protected void btn_addArea_Click(object sender, EventArgs e)
        {
            string co_id = Request.QueryString["co_id"];

            if (string.IsNullOrEmpty(co_id))
            {
                // 輸出JavaScript警告對話框
                string message = "請先選擇一個國家!";
                string encodedMessage = HttpUtility.HtmlEncode(message);
                Response.Write("<script>alert('" + encodedMessage + "');</script>");
            }
            else
            {
                string area = txt_input_area.Text;

                if (string.IsNullOrEmpty(area))
                {
                    // 輸出JavaScript警告對話框
                    string message = "地區欄位不得為空值!";
                    string encodedMessage = HttpUtility.HtmlEncode(message);
                    Response.Write("<script>alert('" + encodedMessage + "');</script>");
                }
                else
                {
                    //以下將所填寫之地區新增至SQL資料庫
                    //1.連線資料庫
                    SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["YachtsConnectionString"].ConnectionString);

                    //2.sql語法，VALUES先使用@參數來取代直接給值，以防SQL Injection 程式碼
                    //新增
                    string sql = "INSERT INTO areas (area_name, country_id) VALUES(@area_name, @country_id) ";


                    //3.創建command物件
                    SqlCommand command = new SqlCommand(sql, connection);

                    //4.參數化，Parameters:為了防止SQL Injection 程式碼攻擊，所以寫入SQL時，要使用參數「@參數名稱」來代替直接給的值，而給予參數的資料型態要使用Add
                    command.Parameters.AddWithValue("@area_name", area);
                    command.Parameters.AddWithValue("@country_id", Convert.ToInt32(co_id));

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
        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.Redirect("dealers_countries.aspx");
        }
        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            // 啟用編輯模式
            GridView1.EditIndex = e.NewEditIndex;

            // 重新繫結GridView，顯示編輯模式
            Show();
        }

        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            // 取消編輯模式
            GridView1.EditIndex = -1;



            // 重新繫結GridView，顯示取消編輯後的資料
            Show();
        }

        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            // 取得 GridView 編輯列的索引值
            int index = GridView1.EditIndex;

            //取得該列的主索引欄位的值(須在gridview 加入 DataKeyNames = "id")
            GridViewRow row = GridView1.Rows[index];

            TextBox txt_area_name = (TextBox)row.FindControl("txt_area_name");
            string keyId = GridView1.DataKeys[row.RowIndex]["area_id"].ToString();

            //1.連線資料庫
            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["YachtsConnectionString"].ConnectionString);

            //2.sql語法，VALUES先使用@參數來取代直接給值，以防SQL Injection 程式碼
            //修改
            string sql = "UPDATE areas SET area_name=@area_name  WHERE area_id = @area_id ";

            //3.創建command物件
            SqlCommand command = new SqlCommand(sql, connection);

            //4.參數化，Parameters:為了防止SQL Injection 程式碼攻擊，所以寫入SQL時，要使用參數「@參數名稱」來代替直接給的值，而給予參數的資料型態要使用Add
            command.Parameters.AddWithValue("@area_name", txt_area_name.Text);
            command.Parameters.AddWithValue("@area_id", Convert.ToInt32(keyId));

            //5.資料庫連線開啟
            connection.Open();

            //6.執行sql (新增刪除修改)
            command.ExecuteNonQuery(); //無回傳值

            //7.資料庫關閉
            connection.Close();

            // 取消編輯模式
            GridView1.EditIndex = -1;

            //畫面渲染
            Show();
        }
        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            //取得被點擊的 GridView 的資料列索引
            int rowIndex = e.RowIndex;

            //取得 GridView 控制項中指定索引的資料列
            GridViewRow row = GridView1.Rows[rowIndex];

            //取得該列的主索引欄位的值(須在gridview 加入 DataKeyNames = "id")
            string keyId = GridView1.DataKeys[row.RowIndex]["area_id"].ToString();

            //1.連線資料庫
            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["YachtsConnectionString"].ConnectionString);

            //2.sql語法，VALUES先使用@參數來取代直接給值，以防SQL Injection 程式碼
            //修改
            string sql = "DELETE FROM areas WHERE area_id = @area_id ";

            //3.創建command物件
            SqlCommand command = new SqlCommand(sql, connection);

            //4.參數化，Parameters:為了防止SQL Injection 程式碼攻擊，所以寫入SQL時，要使用參數「@參數名稱」來代替直接給的值，而給予參數的資料型態要使用Add
            command.Parameters.AddWithValue("@area_id", Convert.ToInt32(keyId));

            //5.資料庫連線開啟
            connection.Open();

            //6.執行sql (新增刪除修改)
            command.ExecuteNonQuery(); //無回傳值

            //7.資料庫關閉
            connection.Close();

            //畫面渲染
            Show();
        }
        private void Show()
        {
            string co_id = Request.QueryString["co_id"];

            //1.連線資料庫(合在一起寫)
            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["YachtsConnectionString"].ConnectionString);

            //2.sql語法
            string sql = "SELECT * FROM areas WHERE country_id = @country_id";

            //3.創建command物件
            SqlCommand command = new SqlCommand(sql, connection);

            //3.5參數化，Parameters:為了防止SQL Injection 程式碼攻擊，所以寫入SQL時，要使用參數「@參數名稱」來代替直接給的值，而給予參數的資料型態要使用Add
            command.Parameters.AddWithValue("@country_id", Convert.ToInt32(co_id));

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

            txt_input_area.Text = "";
        }
        private void Show_ddl_country_name()
        {
            // 連線資料庫
            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["YachtsConnectionString"].ConnectionString);

            // SQL 語法
            string sql = "SELECT * FROM countries";

            // 創建 SqlCommand 物件
            SqlCommand command = new SqlCommand(sql, connection);

            // 資料庫連線開啟
            connection.Open();

            // 執行 SQL 查詢，取得資料
            SqlDataReader reader = command.ExecuteReader();

            ddl_country_name.Items.Clear();
            ddl_country_name.Items.Add(new ListItem("--請選擇--", "0"));

            // 將資料加入 DropDownList
            while (reader.Read())
            {
                // 假設 DropDownList 控制項名稱為 "ddl_country_name
                ddl_country_name.Items.Add(new ListItem(reader["country_name"].ToString(), reader["country_id"].ToString()));
            }

            // 資料庫關閉
            connection.Close();
        }

        //protected void lbtn_addDealer_Click(object sender, EventArgs e)
        //{
        //    string co = Request.QueryString["co"];
        //    string co_id = Request.QueryString["co_id"];
        //    // 取得點擊的 LinkButton
        //    LinkButton lb = (LinkButton)sender;

        //    // 取得所在的 GridViewRow
        //    GridViewRow row = (GridViewRow)lb.NamingContainer;

        //    //這一列的控制項必須是 TemplateField控制項 才能使用
        //    Label lbl_area_name = (Label)row.FindControl("lbl_area_name");

        //    //取得該列的主索引欄位的值(須在gridview 加入 DataKeyNames = "id")
        //    string area_id = GridView1.DataKeys[row.RowIndex]["area_id"].ToString();

        //    Response.Redirect("dealers_add.aspx?co=" + co + "&co_id=" + co_id + "&area=" + lbl_area_name.Text + "&area_id=" + area_id);
        //}

        //protected void goDealers_areas_Click(object sender, EventArgs e)
        //{
        //    Response.Redirect("dealers_areas.aspx");
        //}
        //protected void goDealers_countries_Click(object sender, EventArgs e)
        //{
        //    Response.Redirect("dealers_countries.aspx");
        //}

        //protected void goDealers_Click(object sender, EventArgs e)
        //{
        //    string co = Request.QueryString["co"];
        //    string co_id = Request.QueryString["co_id"];

        //    if (!string.IsNullOrEmpty(co))
        //    {
        //        Response.Redirect("dealers.aspx?co_id="+co_id+"&co="+co);
        //    }
        //    else
        //    {
        //        Response.Redirect("dealers.aspx" );
        //    }
        //}

        protected void lbtn_goCountries_Click(object sender, EventArgs e)
        {
            Response.Redirect("dealers_countries.aspx");
        }

        protected void lbtn_goAreas_Click(object sender, EventArgs e)
        {
            string co = Request.QueryString["co"];
            string co_id = Request.QueryString["co_id"];

            if(string.IsNullOrEmpty(co) || string.IsNullOrEmpty(co_id))
            {
                Response.Redirect("dealers_areas.aspx");
            }
            else{
                Response.Redirect("dealers_areas.aspx?co=" + co + "&co_id=" + co_id);
            }
        }
    }
}