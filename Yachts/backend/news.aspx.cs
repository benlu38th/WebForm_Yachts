using System;
using System.Data.SqlClient;
using System.IO;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Yachts.pages
{
    public partial class B_news : System.Web.UI.Page
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            if (!HttpContext.Current.User.Identity.IsAuthenticated)
            {
                Response.Redirect("login.aspx");
            }
            //目前網頁[nowWebPageRight_id}手動輸入，每頁不同
            string nowWebPageRight_id = "3";
            //判斷是否有該頁全限，沒權限跳轉回login頁面
            rightCheck.HaveWebRight(nowWebPageRight_id, Response);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Show();
                ShowPage();
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (Button1.Text == "新增新聞")
            {
                txt_input_des.Visible = true;
                lbl_input_des.Visible = true;

                txt_input_launchDate.Visible = true;
                lbl_input_launchDate.Visible = true;

                txt_input_title.Visible = true;
                lbl_input_title.Visible = true;

                chk_input_top.Visible = true;
                lbl_input_top.Visible = true;

                ful_input_photo.Visible = true;
                lbl_input_photo.Visible = true;

                Button1.Text = "確認送出";

                Button2.Visible = true;
            }
            else
            {
                txt_input_title.Visible = false;
                lbl_input_title.Visible = false;

                txt_input_des.Visible = false;
                lbl_input_des.Visible = false;

                chk_input_top.Visible = false;
                lbl_input_top.Visible = false;

                txt_input_launchDate.Visible = false;
                lbl_input_launchDate.Visible = false;

                ful_input_photo.Visible = false;
                lbl_input_photo.Visible = false;

                Button1.Text = "新增新聞";

                Button2.Visible = false;

                string intput_title = txt_input_title.Text;
                string intput_des = txt_input_des.Text;
                bool intput_top = chk_input_top.Checked;
                DateTime input_launchDate;
                if (txt_input_launchDate.Text == "")
                {
                    input_launchDate = DateTime.Now;
                }
                else
                {
                    input_launchDate = DateTime.Parse(txt_input_launchDate.Text);
                }


                //取得上傳檔案名稱(包含.jpg)
                string fileName = ful_input_photo.FileName;

                //取得上傳檔案尾標(即 【.jpg】)
                string fileExtension = Path.GetExtension(fileName).ToLower();

                //建立上傳檔案許可之尾標
                string[] allowExtensions = { ".jpg", ".jepg", ".png", ".gif", ".jfif" };

                //建立圖片存檔之路徑
                string savePath = @"C:\Users\盧致宇\source\repos\Yachts\Yachts\photos\newsCoverPhoto\";

                //建立存在資料庫之取出圖片路徑
                string photo_path = "~/photos/newsCoverPhoto/" + fileName;

                //建立檔案格式OK的布林值
                Boolean fileOK = false;

                //建立檔案上傳OK的布林值
                Boolean uploadOK = false;

                //如果有選擇檔案即執行
                if (ful_input_photo.HasFile)
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
                            ful_input_photo.SaveAs(saveResult);
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
                    if (uploadOK && intput_title != "" && intput_des != "" && txt_input_launchDate.Text != "")
                    {
                        //1.連線資料庫
                        SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["YachtsConnectionString"].ConnectionString);

                        //2.sql語法，VALUES先使用@參數來取代直接給值，以防SQL Injection 程式碼
                        string sql = "INSERT INTO news (news_title, news_des, news_launchDate, news_photo, news_top) VALUES(@news_title, @news_des, @news_launchDate, @news_photo, @news_top)";

                        //3.創建command物件
                        SqlCommand command = new SqlCommand(sql, connection);

                        //4.參數化，Parameters:為了防止SQL Injection 程式碼攻擊，所以寫入SQL時，要使用參數「@參數名稱」來代替直接給的值，而給予參數的資料型態要使用Add
                        command.Parameters.AddWithValue("@news_title", intput_title);
                        command.Parameters.AddWithValue("@news_des", intput_des);
                        command.Parameters.AddWithValue("@news_launchDate", input_launchDate);
                        command.Parameters.AddWithValue("@news_photo", photo_path);
                        command.Parameters.AddWithValue("@news_top", intput_top);

                        //5.資料庫連線開啟
                        connection.Open();

                        //6.執行sql (新增刪除修改)
                        command.ExecuteNonQuery(); //無回傳值

                        //7.資料庫關閉
                        connection.Close();

                        Response.Redirect("news.aspx");
                    }
                    else
                    {
                        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Message", "<script>alert('請填些新聞標題、內容、及發布時間！');</script>");
                    }
                }
                else
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Message", "<script>alert('請選擇一張圖片！');</script>");
                }
            }
        }
        protected void Button2_Click(object sender, EventArgs e)
        {
            txt_input_title.Visible = false;
            lbl_input_title.Visible = false;

            txt_input_des.Visible = false;
            lbl_input_des.Visible = false;

            chk_input_top.Visible = false;
            lbl_input_top.Visible = false;

            txt_input_launchDate.Visible = false;
            lbl_input_launchDate.Visible = false;

            ful_input_photo.Visible = false;
            lbl_input_photo.Visible = false;

            Button1.Text = "新增新聞";

            Button2.Visible = false;
        }
        private void Show()
        {
            //1.連線資料庫(合在一起寫)
            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["YachtsConnectionString"].ConnectionString);

            //2.sql語法
            string sql = "SELECT * FROM news ORDER BY news_top DESC, news_launchDate DESC";

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
            string news_id = GridView1.DataKeys[row.RowIndex]["news_id"].ToString();

            TextBox txt_news_title = (TextBox)row.FindControl("txt_news_title");
            TextBox txt_news_des = (TextBox)row.FindControl("txt_news_des");
            TextBox txt_launchDate = (TextBox)row.FindControl("txt_launchDate");
            DateTime input_launchDate = DateTime.Parse(txt_launchDate.Text);
            CheckBox edit_chk_news_top = (CheckBox)row.FindControl("edit_chk_news_top");
            FileUpload GV1_ful_news_photo = (FileUpload)row.FindControl("GV1_ful_news_photo");

            //取得上傳檔案名稱(包含.jpg)
            string fileName = GV1_ful_news_photo.FileName;

            //取得上傳檔案尾標(即 【.jpg】)
            string fileExtension = Path.GetExtension(fileName).ToLower();

            //建立上傳檔案許可之尾標
            string[] allowExtensions = { ".jpg", ".jepg", ".png", ".gif", ".jfif" };

            //建立圖片存檔之路徑
            string savePath = @"C:\Users\盧致宇\source\repos\Yachts\Yachts\photos\newsCoverPhoto\";

            //建立存在資料庫之取出圖片路徑
            string photo_path = "~/photos/newsCoverPhoto/" + fileName;

            //建立檔案格式OK的布林值
            Boolean fileOK = false;

            //建立檔案上傳OK的布林值
            Boolean uploadOK = false;

            //如果有選擇檔案即執行
            if (GV1_ful_news_photo.HasFile)
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
                        GV1_ful_news_photo.SaveAs(saveResult);
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
                    string sql = "UPDATE news SET news_title=@news_title, news_des=@news_des, news_launchDate=@news_launchDate, news_photo=@news_photo, news_top=@news_top  WHERE news_id = @news_id ";

                    //3.創建command物件
                    SqlCommand command = new SqlCommand(sql, connection);

                    //4.參數化，Parameters:為了防止SQL Injection 程式碼攻擊，所以寫入SQL時，要使用參數「@參數名稱」來代替直接給的值，而給予參數的資料型態要使用Add
                    command.Parameters.AddWithValue("@news_id", Convert.ToInt32(news_id));
                    command.Parameters.AddWithValue("@news_title", txt_news_title.Text);
                    command.Parameters.AddWithValue("@news_des", txt_news_des.Text);
                    command.Parameters.AddWithValue("@news_launchDate", input_launchDate);
                    command.Parameters.AddWithValue("@news_photo", photo_path);
                    command.Parameters.AddWithValue("@news_top", edit_chk_news_top.Checked);

                    //5.資料庫連線開啟
                    connection.Open();

                    //6.執行sql (新增刪除修改)
                    command.ExecuteNonQuery(); //無回傳值

                    //7.資料庫關閉
                    connection.Close();

                }
            }
            else
            {
                //1.連線資料庫
                SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["YachtsConnectionString"].ConnectionString);

                //2.sql語法，VALUES先使用@參數來取代直接給值，以防SQL Injection 程式碼
                //修改
                string sql = "UPDATE news SET news_title=@news_title, news_des=@news_des, news_launchDate=@news_launchDate, news_top=@news_top  WHERE news_id = @news_id ";

                //3.創建command物件
                SqlCommand command = new SqlCommand(sql, connection);

                //4.參數化，Parameters:為了防止SQL Injection 程式碼攻擊，所以寫入SQL時，要使用參數「@參數名稱」來代替直接給的值，而給予參數的資料型態要使用Add
                command.Parameters.AddWithValue("@news_id", Convert.ToInt32(news_id));
                command.Parameters.AddWithValue("@news_title", txt_news_title.Text);
                command.Parameters.AddWithValue("@news_des", txt_news_des.Text);
                command.Parameters.AddWithValue("@news_launchDate", input_launchDate);
                command.Parameters.AddWithValue("@news_top", edit_chk_news_top.Checked);

                //5.資料庫連線開啟
                connection.Open();

                //6.執行sql (新增刪除修改)
                command.ExecuteNonQuery(); //無回傳值

                //7.資料庫關閉
                connection.Close();
            }

            // 取消編輯模式
            GridView1.EditIndex = -1;

            // 重新繫結GridView，顯示取消編輯後的資料
            Show();

        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            //取得被點擊的 GridView 的資料列索引
            int rowIndex = e.RowIndex;

            //取得 GridView 控制項中指定索引的資料列
            GridViewRow row = GridView1.Rows[rowIndex];

            //取得該列的主索引欄位的值(須在gridview 加入 DataKeyNames = "id")
            string news_id = GridView1.DataKeys[row.RowIndex]["news_id"].ToString();

            //1.連線資料庫
            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["YachtsConnectionString"].ConnectionString);

            //2.sql語法，VALUES先使用@參數來取代直接給值，以防SQL Injection 程式碼
            //刪除
            string sql = "DELETE FROM news WHERE news_id = @news_id; ";

            //3.創建command物件
            SqlCommand command = new SqlCommand(sql, connection);

            //4.參數化，Parameters:為了防止SQL Injection 程式碼攻擊，所以寫入SQL時，要使用參數「@參數名稱」來代替直接給的值，而給予參數的資料型態要使用Add
            command.Parameters.AddWithValue("@news_id", Convert.ToInt32(news_id));


            //5.資料庫連線開啟
            connection.Open();

            //6.執行sql (新增刪除修改)
            command.ExecuteNonQuery(); //無回傳值

            //7.資料庫關閉
            connection.Close();

            //畫面渲染
            Show();
        }
        protected void lbtn_news_title_Click(object sender, EventArgs e)
        {
            // 取得點擊的 LinkButton
            LinkButton lb = (LinkButton)sender;

            // 取得所在的 GridViewRow
            GridViewRow row = (GridViewRow)lb.NamingContainer;

            //取得該列的主索引欄位的值(須在gridview 加入 DataKeyNames = "id")
            string news_id = GridView1.DataKeys[row.RowIndex]["news_id"].ToString();

            //這一列的控制項必須是 TemplateField控制項 才能使用
            LinkButton lbtn_news_title = (LinkButton)row.FindControl("lbtn_news_title");

            Response.Redirect("news_detail.aspx?news_id=" + news_id + "&news_title=" + lbtn_news_title.Text);
        }

        protected void btn_checkPage_Click(object sender, EventArgs e)
        {
            if (btn_checkPage.Text == "更新") {
                btn_checkPage.Text = "確認";
                txt_news_pageInAPage.Visible = true;
                lbl_news_pageInAPage.Visible = false;
            }
            else {
                btn_checkPage.Text = "更新";
                txt_news_pageInAPage.Visible = false;
                lbl_news_pageInAPage.Visible = true;

                string newsPage_id = lit_overviewsDL_id.Text;
                string news_pageInAPage = txt_news_pageInAPage.Text;
                try
                {
                    if (Convert.ToInt32(news_pageInAPage) >= 1 && Convert.ToInt32(news_pageInAPage) <= 9)
                    {
                        //1.連線資料庫
                        SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["YachtsConnectionString"].ConnectionString);

                        //2.sql語法，VALUES先使用@參數來取代直接給值，以防SQL Injection 程式碼
                        //修改
                        string sql = "UPDATE newsPage SET newsPage_pagesInAPage=@newsPage_pagesInAPage WHERE newsPage_id = @newsPage_id ";

                        //3.創建command物件
                        SqlCommand command = new SqlCommand(sql, connection);

                        //4.參數化，Parameters:為了防止SQL Injection 程式碼攻擊，所以寫入SQL時，要使用參數「@參數名稱」來代替直接給的值，而給予參數的資料型態要使用Add
                        command.Parameters.AddWithValue("@newsPage_id", Convert.ToInt32(newsPage_id));
                        command.Parameters.AddWithValue("@newsPage_pagesInAPage", Convert.ToInt32(news_pageInAPage));

                        //5.資料庫連線開啟
                        connection.Open();

                        //6.執行sql (新增刪除修改)
                        command.ExecuteNonQuery(); //無回傳值

                        //7.資料庫關閉
                        connection.Close();

                        ShowPage();
                    }
                    else
                    {
                        ShowPage();
                        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Message", "<script>alert('請輸入1~9的整數!');</script>");
                    }
                }
                catch
                {
                    ShowPage();
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Message", "<script>alert('請輸入1~9的整數!');</script>");
                }
                
            }
        }
        private void ShowPage() {
            //1.連線資料庫(合在一起寫)
            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["YachtsConnectionString"].ConnectionString);

            //2.sql語法
            string sql = "SELECT * FROM newsPage ";

            //3.創建command物件
            SqlCommand command = new SqlCommand(sql, connection);

            //4.資料庫連線開啟
            connection.Open();

            //5.執行sql (連線的作法-需自行關閉)
            SqlDataReader reader = command.ExecuteReader();
            //DataReader速度快只能逐筆單向有上往下而且不能計算，適合用來抓單筆資料
            //控制器資料來源
            if (reader.Read())
            {
                lbl_news_pageInAPage.Text = reader[1].ToString();
                txt_news_pageInAPage.Text = reader[1].ToString();
                lit_overviewsDL_id.Text = reader[0].ToString();
            }

            //6.資料庫關閉
            connection.Close();
        }
    }
}