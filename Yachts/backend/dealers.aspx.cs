using CKFinder;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Yachts.pages
{

    public partial class B_dealers : System.Web.UI.Page
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            if (!HttpContext.Current.User.Identity.IsAuthenticated)
            {
                Response.Redirect("login.aspx");
            }
            //目前網頁{nowWebPageRight_id}手動輸入，每頁不同
            string nowWebPageRight_id = "1";
            //判斷是否有該頁全限，沒權限跳轉回login頁面
            rightCheck.HaveWebRight(nowWebPageRight_id, Response);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            string co_id = Request.QueryString["co_id"];
            string co = Request.QueryString["co"];

            if (!IsPostBack)
            {
                Show_ddl_country_name();
                Show_ddl_area_name();
                Show();
            }
        }
        protected void ddl_country_name_SelectedIndexChanged(object sender, EventArgs e)
        {
            string co = ddl_country_name.SelectedItem.Text;
            string co_id = ddl_country_name.SelectedValue;
            Response.Redirect("dealers.aspx?co=" + co + "&co_id=" + co_id);
        }
        protected void ddl_area_name_SelectedIndexChanged(object sender, EventArgs e)
        {
            string co = Request.QueryString["co"];
            string co_id = Request.QueryString["co_id"];
            string area = ddl_area_name.SelectedItem.Text;
            string area_id = ddl_area_name.SelectedValue;
            Response.Redirect("dealers.aspx?co=" + co + "&co_id=" + co_id + "&area=" + area + "&area_id=" + area_id);
        }
        private void Show_ddl_country_name()
        {
            // 連線資料庫
            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["YachtsConnectionString"].ConnectionString);

            // SQL 語法
            string sql = "SELECT * FROM [countries]";

            // 創建 SqlCommand 物件
            SqlCommand command = new SqlCommand(sql, connection);

            // 資料庫連線開啟
            connection.Open();

            // 執行 SQL 查詢，取得資料
            SqlDataReader reader = command.ExecuteReader();

            ddl_country_name.Items.Clear();
            ddl_country_name.Items.Add(new ListItem("--請選擇一個國家--", "0"));

            // 將資料加入 DropDownList
            while (reader.Read())
            {
                // 假設 DropDownList 控制項名稱為 "ddl_country_name"
                ddl_country_name.Items.Add(new ListItem(reader["country_name"].ToString(), reader["country_id"].ToString()));
            }

            // 資料庫關閉
            connection.Close();

            string co_id = Request.QueryString["co_id"];
            if (!string.IsNullOrEmpty(co_id))
            {
                ddl_country_name.Items.FindByValue(co_id).Selected = true;
            }

        }
        private void Show_ddl_area_name()
        {
            string country_id = Request.QueryString["co_id"];
            if (!string.IsNullOrEmpty(country_id))
            {
                // 連線資料庫
                SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["YachtsConnectionString"].ConnectionString);

                // SQL 語法
                string sql = "SELECT * FROM areas WHERE country_id = @country_id";

                // 創建 SqlCommand 物件
                SqlCommand command = new SqlCommand(sql, connection);

                //參數化，Parameters:為了防止SQL Injection 程式碼攻擊，所以寫入SQL時，要使用參數「@參數名稱」來代替直接給的值，而給予參數的資料型態要使用Add
                command.Parameters.AddWithValue("@country_id", Convert.ToInt32(country_id));

                // 資料庫連線開啟
                connection.Open();

                // 執行 SQL 查詢，取得資料
                SqlDataReader reader = command.ExecuteReader();

                ddl_area_name.Items.Clear();
                ddl_area_name.Items.Add(new ListItem("--請選擇一個地區--", "0"));

                // 將資料加入 DropDownList
                while (reader.Read())
                {
                    // 假設 DropDownList 控制項名稱為 "ddl_area_name"
                    ddl_area_name.Items.Add(new ListItem(reader["area_name"].ToString(), reader["area_id"].ToString()));
                }

                // 資料庫關閉
                connection.Close();

                string area_id = Request.QueryString["area_id"];
                if (!string.IsNullOrEmpty(area_id))
                {
                    ddl_area_name.Items.FindByValue(area_id).Selected = true;
                }
            }
        }
        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            //取得被點擊的 GridView 的資料列索引
            int rowIndex = e.RowIndex;

            //取得 GridView 控制項中指定索引的資料列
            GridViewRow row = GridView1.Rows[rowIndex];

            //取得該列的主索引欄位的值(須在gridview 加入 DataKeyNames = "id")
            string dealer_id = GridView1.DataKeys[row.RowIndex]["dealer_id"].ToString();

            //1.連線資料庫
            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["YachtsConnectionString"].ConnectionString);

            //2.sql語法，VALUES先使用@參數來取代直接給值，以防SQL Injection 程式碼
            //刪除
            string sql = "DELETE FROM dealers WHERE dealer_id = @dealer_id; ";

            //3.創建command物件
            SqlCommand command = new SqlCommand(sql, connection);

            //4.參數化，Parameters:為了防止SQL Injection 程式碼攻擊，所以寫入SQL時，要使用參數「@參數名稱」來代替直接給的值，而給予參數的資料型態要使用Add
            command.Parameters.AddWithValue("@dealer_id", dealer_id);

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
            string country_id = Request.QueryString["co_id"];
            string area_id = Request.QueryString["area_id"];
            try
            {
                if ((!string.IsNullOrEmpty(country_id) && string.IsNullOrEmpty(area_id)) || (!string.IsNullOrEmpty(country_id) && !string.IsNullOrEmpty(area_id) && ddl_area_name.SelectedValue=="0"))
                {
                    //1.連線資料庫(合在一起寫)
                    SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["YachtsConnectionString"].ConnectionString);

                    //2.sql語法
                    string sql = $"SELECT * FROM dealers d WHERE area_id IN (SELECT area_id FROM areas WHERE country_id = (SELECT country_id FROM countries WHERE country_id = {country_id})) ORDER BY d.initdate DESC";

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
                else if (!string.IsNullOrEmpty(country_id) && !string.IsNullOrEmpty(area_id))
                {
                    //1.連線資料庫(合在一起寫)
                    SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["YachtsConnectionString"].ConnectionString);

                    //2.sql語法
                    string sql = $"SELECT * FROM dealers WHERE area_id = {area_id}";

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
            }
            catch
            {
            }
        }

        protected void lbtn_edit_Click(object sender, EventArgs e)
        {
            string co = Request.QueryString["co"];
            string co_id = Request.QueryString["co_id"];
            // 取得點擊的 LinkButton
            LinkButton lb = (LinkButton)sender;

            // 取得所在的 GridViewRow
            GridViewRow row = (GridViewRow)lb.NamingContainer;


            //取得該列的主索引欄位的值(須在gridview 加入 DataKeyNames = "id")
            string dealer_id = GridView1.DataKeys[row.RowIndex]["dealer_id"].ToString();

            Response.Redirect("dealers_edit.aspx?co=" + co + "&co_id=" + co_id + "&dealer_id=" + dealer_id);
        }
        protected void Button5_Click(object sender, EventArgs e)
        {
            string co = Request.QueryString["co"];
            string co_id = Request.QueryString["co_id"];

            Response.Redirect("dealers_areas.aspx?co=" + co + "&co_id=" + co_id);
        }

        protected void Button4_Click(object sender, EventArgs e)
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

            // 取得編輯列中所輸入的值，用txtValue1.Text、txtValue2.Text及cbValue1.Checked
            GridViewRow row = GridView1.Rows[index];

            //取得該列的主索引欄位的值(須在gridview 加入 DataKeyNames = "id")
            string dealer_id = GridView1.DataKeys[row.RowIndex]["dealer_id"].ToString();

            TextBox txt_dealer_name = (TextBox)row.FindControl("txt_dealer_name");
            TextBox txt_dealer_contact = (TextBox)row.FindControl("txt_dealer_contact");
            TextBox txt_dealer_address = (TextBox)row.FindControl("txt_dealer_address");
            TextBox txt_dealer_tel = (TextBox)row.FindControl("txt_dealer_tel");
            TextBox txt_dealer_fax = (TextBox)row.FindControl("txt_dealer_fax");
            TextBox txt_dealer_cell = (TextBox)row.FindControl("txt_dealer_cell");
            TextBox txt_dealer_mail = (TextBox)row.FindControl("txt_dealer_mail");
            TextBox txt_dealer_website = (TextBox)row.FindControl("txt_dealer_website");

            FileUpload ful_dealer_photoPath = (FileUpload)row.FindControl("ful_dealer_photoPath");

            //取得上傳檔案名稱(包含.jpg)
            string fileName = ful_dealer_photoPath.FileName;

            //取得上傳檔案尾標(即 【.jpg】)
            string fileExtension = Path.GetExtension(fileName).ToLower();

            //建立上傳檔案許可之尾標
            string[] allowExtensions = { ".jpg", ".jepg", ".png", ".gif", ".jfif" };

            //建立圖片存檔之路徑
            string savePath = @"C:\Users\盧致宇\source\repos\Yachts\Yachts\photos\dealerPhoto\";

            //建立存在資料庫之取出圖片路徑
            string photo_path = "~/photos/dealerPhoto/" + fileName;

            //建立檔案格式OK的布林值
            Boolean fileOK = false;

            //建立檔案上傳OK的布林值
            Boolean uploadOK = false;

            //如果有選擇檔案即執行
            if (ful_dealer_photoPath.HasFile)
            {

                //測試上傳檔案尾標是否符合格式
                for (int i = 0; i < allowExtensions.Length; i++)
                {
                    if (fileExtension == allowExtensions[i])
                    {
                        fileOK = true;
                    }
                }
                //上傳檔案尾標符合格式即執行
                if (fileOK)
                {
                    try
                    {
                        //建立存放檔案路徑
                        string saveResult = Path.Combine(savePath, fileName);
                        //存檔案到路徑
                        ful_dealer_photoPath.SaveAs(saveResult);
                        uploadOK = true;
                    }
                    catch
                    {
                        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Message", "<script>alert('發生例外錯誤，上傳失敗！');</script>");
                    }
                }
                else
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Message", "<script>alert('上傳的檔案，附檔名只能是.jpg , .jepg , .png , .gif , .jfif ！');</script>");
                }
                //上傳檔案成功即執行
                if (uploadOK)
                {
                    //1.連線資料庫
                    SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["YachtsConnectionString"].ConnectionString);

                    //2.sql語法，VALUES先使用@參數來取代直接給值，以防SQL Injection 程式碼
                    //修改
                    string sql = "UPDATE dealers SET dealer_name=@dealer_name, dealer_contact=@dealer_contact, dealer_address=@dealer_address, dealer_tel=@dealer_tel, dealer_fax=@dealer_fax, dealer_cell=@dealer_cell, dealer_mail=@dealer_mail, dealer_website=@dealer_website, dealer_photoPath=@dealer_photoPath  WHERE dealer_id = @dealer_id ";

                    //3.創建command物件
                    SqlCommand command = new SqlCommand(sql, connection);

                    //4.參數化，Parameters:為了防止SQL Injection 程式碼攻擊，所以寫入SQL時，要使用參數「@參數名稱」來代替直接給的值，而給予參數的資料型態要使用Add
                    command.Parameters.AddWithValue("@dealer_id", Convert.ToInt32(dealer_id));
                    command.Parameters.AddWithValue("@dealer_name", txt_dealer_name.Text);
                    command.Parameters.AddWithValue("@dealer_contact", txt_dealer_contact.Text);
                    command.Parameters.AddWithValue("@dealer_address", txt_dealer_address.Text);
                    command.Parameters.AddWithValue("@dealer_tel", txt_dealer_tel.Text);
                    command.Parameters.AddWithValue("@dealer_fax", txt_dealer_fax.Text);
                    command.Parameters.AddWithValue("@dealer_cell", txt_dealer_cell.Text);
                    command.Parameters.AddWithValue("@dealer_mail", txt_dealer_mail.Text);
                    command.Parameters.AddWithValue("@dealer_website", txt_dealer_website.Text);
                    command.Parameters.AddWithValue("@dealer_photoPath", photo_path);


                    //5.資料庫連線開啟
                    connection.Open();

                    //6.執行sql (新增刪除修改)
                    command.ExecuteNonQuery(); //無回傳值

                    //7.資料庫關閉
                    connection.Close();

                    // 取消編輯模式
                    GridView1.EditIndex = -1;

                    // 重新繫結GridView，顯示取消編輯後的資料
                    Show();
                }
            }
            else
            {
                //1.連線資料庫
                SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["YachtsConnectionString"].ConnectionString);

                //2.sql語法，VALUES先使用@參數來取代直接給值，以防SQL Injection 程式碼
                //修改
                string sql = "UPDATE dealers SET dealer_name=@dealer_name, dealer_contact=@dealer_contact, dealer_address=@dealer_address, dealer_tel=@dealer_tel, dealer_fax=@dealer_fax, dealer_cell=@dealer_cell, dealer_mail=@dealer_mail, dealer_website=@dealer_website WHERE dealer_id = @dealer_id ";

                //3.創建command物件
                SqlCommand command = new SqlCommand(sql, connection);

                //4.參數化，Parameters:為了防止SQL Injection 程式碼攻擊，所以寫入SQL時，要使用參數「@參數名稱」來代替直接給的值，而給予參數的資料型態要使用Add
                command.Parameters.AddWithValue("@dealer_id", Convert.ToInt32(dealer_id));
                command.Parameters.AddWithValue("@dealer_name", txt_dealer_name.Text);
                command.Parameters.AddWithValue("@dealer_contact", txt_dealer_contact.Text);
                command.Parameters.AddWithValue("@dealer_address", txt_dealer_address.Text);
                command.Parameters.AddWithValue("@dealer_tel", txt_dealer_tel.Text);
                command.Parameters.AddWithValue("@dealer_fax", txt_dealer_fax.Text);
                command.Parameters.AddWithValue("@dealer_cell", txt_dealer_cell.Text);
                command.Parameters.AddWithValue("@dealer_mail", txt_dealer_mail.Text);
                command.Parameters.AddWithValue("@dealer_website", txt_dealer_website.Text);


                //5.資料庫連線開啟
                connection.Open();

                //6.執行sql (新增刪除修改)
                command.ExecuteNonQuery(); //無回傳值

                //7.資料庫關閉
                connection.Close();

                // 取消編輯模式
                GridView1.EditIndex = -1;

                // 重新繫結GridView，顯示取消編輯後的資料
                Show();
            }
        }
        //protected void goDealers_areas_Click(object sender, EventArgs e)
        //{
        //    string co = Request.QueryString["co"];
        //    string co_id = Request.QueryString["co_id"];
        //    if (!string.IsNullOrEmpty(co))
        //    {
        //        Response.Redirect("dealers_areas.aspx?co_id=" + co_id + "&co=" + co);
        //    }
        //    else
        //    {
        //        Response.Redirect("dealers_areas.aspx");
        //    }
        //}
        //protected void goDealers_countries_Click(object sender, EventArgs e)
        //{
        //        Response.Redirect("dealers_countries.aspx");
        //}

        //protected void goDealers_Click(object sender, EventArgs e)
        //{
        //    string co = Request.QueryString["co"];
        //    string co_id = Request.QueryString["co_id"];

        //    if (!string.IsNullOrEmpty(co))
        //    {
        //        Response.Redirect("dealers.aspx?co_id=" + co_id + "&co=" + co);
        //    }
        //    else
        //    {
        //        Response.Redirect("dealers.aspx");
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

            if (string.IsNullOrEmpty(co) || string.IsNullOrEmpty(co_id))
            {
                Response.Redirect("dealers_areas.aspx");
            }
            else
            {
                Response.Redirect("dealers_areas.aspx?co=" + co + "&co_id=" + co_id);
            }
        }
        protected void lbtn_goDealers_Click(object sender, EventArgs e)
        {

        }

        protected void btn_addDealer_Click(object sender, EventArgs e)
        {
            string co = Request.QueryString["co"];
            string co_id = Request.QueryString["co_id"];
            string area = Request.QueryString["area"];
            string area_id = Request.QueryString["area_id"];

            if (!string.IsNullOrEmpty(co) && !string.IsNullOrEmpty(co_id) && !string.IsNullOrEmpty(area) && !string.IsNullOrEmpty(area_id) && ddl_area_name.SelectedValue != "0" && ddl_country_name.SelectedValue != "0")
            {
                Response.Redirect("dealers_add.aspx?co=" + co + "&co_id=" + co_id + "&area=" + area + "&area_id=" + area_id);
            }
            else
            {
                // 輸出JavaScript警告對話框
                string message = "先選擇一個國家及地區才能新增代理商!!!";
                string encodedMessage = HttpUtility.HtmlEncode(message);
                Response.Write("<script>alert('" + encodedMessage + "');</script>");
            }

        }
    }
}