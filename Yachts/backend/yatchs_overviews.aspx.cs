using CKFinder;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Yachts.backend
{
    public partial class yatchs_detail : System.Web.UI.Page
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            if (!HttpContext.Current.User.Identity.IsAuthenticated)
            {
                Response.Redirect("login.aspx");
            }
            //目前網頁[nowWebPageRight_id}手動輸入，每頁不同
            string nowWebPageRight_id = "4";
            //判斷是否有該頁全限，沒權限跳轉回login頁面
            rightCheck.HaveWebRight(nowWebPageRight_id, Response);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            string yatch_id = Request.QueryString["yatch_id"];
            string yatch_name = Request.QueryString["yatch_name"];
            if (yatch_id == null || yatch_name == null)
            {
                Response.Redirect("yatchs.aspx");
            }
            else
            {
                lbl_yatch_name.Text = yatch_name;
            }

            if (!IsPostBack)
            {
                //CKEditor設定
                FileBrowser fileBrowser = new FileBrowser();
                fileBrowser.BasePath = "/ckfinder";
                fileBrowser.SetupCKEditor(cke_overview_content);

                Show();
                Show2();
            }
        }
        private void Show()
        {
            string yatch_id = Request.QueryString["yatch_id"];


            //1.連線資料庫(合在一起寫)
            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["YachtsConnectionString"].ConnectionString);

            //2.sql語法
            string sql = "SELECT * FROM overviews WHERE yatch_id = @yatch_id";

            //3.創建command物件
            SqlCommand command = new SqlCommand(sql, connection);

            //3.5 參數化，Parameters:為了防止SQL Injection 程式碼攻擊，所以寫入SQL時，要使用參數「@參數名稱」來代替直接給的值，而給予參數的資料型態要使用Add
            command.Parameters.AddWithValue("@yatch_id", Convert.ToInt32(yatch_id));

            //4.資料庫連線開啟
            connection.Open();

            //5.執行sql (連線的作法-需自行關閉)
            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                lbl_overview_content.Text = reader["overview_content"].ToString();
                cke_overview_content.Text = reader["overview_content"].ToString();
                lbl_overview_dime.Text = reader["overview_dime"].ToString();
                cke_overview_dime.Text = reader["overview_dime"].ToString();
                img_overview_dimePhoto.ImageUrl = reader["overview_dimePhoto"].ToString();
            }

            //6.資料庫關閉
            connection.Close();
        }
        protected void editExceptDL_Click(object sender, EventArgs e)
        {
            if (editExceptDL.Text != "確認送出")
            {
                editExceptDL.Text = "確認送出";

                cke_overview_content.Visible = true;
                lbl_overview_content.Visible = false;
                cke_overview_dime.Visible = true;
                lbl_overview_dime.Visible = false;
                ful_overview_dimePhoto.Visible = true;
                img_overview_dimePhoto.Visible = false;
                cancelExcelDL.Visible = true;

            }
            else
            {
                editExceptDL.Text = "開始編輯";

                cke_overview_content.Visible = false;
                lbl_overview_content.Visible = true;
                cke_overview_dime.Visible = false;
                lbl_overview_dime.Visible = true;
                ful_overview_dimePhoto.Visible = false;
                img_overview_dimePhoto.Visible = true;
                cancelExcelDL.Visible = false;


                //是否有資料，有資料修改，沒資料新增
                Boolean sameYatch_idInSQL = false;

                //目前的yatch_id
                string yatch_id = Request.QueryString["yatch_id"];

                //1.連線資料庫(合在一起寫)
                SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["YachtsConnectionString"].ConnectionString);

                //2.sql語法
                string sql = "SELECT * FROM overviews";

                //3.創建command物件
                SqlCommand command = new SqlCommand(sql, connection);

                //4.資料庫連線開啟
                connection.Open();

                //5.執行sql (連線的作法-需自行關閉)
                SqlDataReader reader = command.ExecuteReader();
                //DataReader速度快只能逐筆單向有上往下而且不能計算，適合用來抓單筆資料
                //控制器資料來源
                while (reader.Read())
                {
                    if (yatch_id == reader["yatch_id"].ToString())
                    {
                        //有相同資校，改成true
                        sameYatch_idInSQL = true;
                        break;
                    }
                }

                //6.資料庫關閉
                connection.Close();

                //SQL已經有資料，修改
                if (sameYatch_idInSQL)
                {
                    //取得要新增的資料
                    string overview_content = cke_overview_content.Text;
                    string overview_dime = cke_overview_dime.Text;

                    //取得上傳檔案名稱(包含.jpg)
                    string fileName = ful_overview_dimePhoto.FileName;

                    //取得上傳檔案尾標(即 【.jpg】)
                    string fileExtension = Path.GetExtension(fileName).ToLower();

                    //建立上傳檔案許可之尾標
                    string[] allowExtensions = { ".jpg", ".jepg", ".png", ".gif", ".jfif" };

                    //建立圖片存檔之路徑
                    string savePath = @"C:\Users\盧致宇\source\repos\Yachts\Yachts\photos\overviewDimePhoto\";

                    //建立存在資料庫之取出圖片路徑
                    string photo_path = "~/photos/overviewDimePhoto/" + fileName;

                    //檔案格式OK的布林值
                    Boolean fileOK = false;

                    //檔案上傳OK的布林值
                    Boolean uploadOK = false;

                    //資料庫是否有相同檔案
                    Boolean samePhotoInSQL = false;

                    //如果有選擇檔案即執行
                    if (ful_overview_dimePhoto.HasFile)
                    {
                        //1.連線資料庫(合在一起寫)

                        //2.sql語法
                        string sqlCheck = "SELECT * FROM yatchs";

                        //3.創建command物件
                        SqlCommand commandCheckFile = new SqlCommand(sqlCheck, connection);

                        //4.資料庫連線開啟
                        connection.Open();

                        //5.執行sql (連線的作法-需自行關閉)
                        SqlDataReader readerCheckFile = commandCheckFile.ExecuteReader();
                        //DataReader速度快只能逐筆單向有上往下而且不能計算，適合用來抓單筆資料

                        while (readerCheckFile.Read())
                        {
                            if (photo_path == readerCheckFile["yatch_coverPhoto"].ToString())
                            {
                                samePhotoInSQL = true;
                                break;
                            }
                        }

                        //6.資料庫關閉
                        connection.Close();

                        //如果資料庫沒有一樣的上傳檔案才執行
                        if (!samePhotoInSQL)
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
                                    ful_overview_dimePhoto.SaveAs(saveResult);
                                    uploadOK = true;
                                }
                                catch
                                {
                                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Message", "<script>alert('發生例外錯誤，上傳失敗！');</script>");
                                }
                            }
                            else
                            {
                                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Message", "<script>alert('上傳的檔案，附檔名只能是.jpg , .jepg , .png , .gif , .jfif, ！');</script>");
                            }
                            //上傳檔案成功即執行
                            if (uploadOK)
                            {
                                //1.連線資料庫

                                //2.sql語法，VALUES先使用@參數來取代直接給值，以防SQL Injection 程式碼
                                //新增
                                string sqlUpdate = "UPDATE overviews SET overview_content=@overview_content, overview_dime=@overview_dime,overview_dimePhoto=@overview_dimePhoto   WHERE yatch_id = @yatch_id ";

                                //3.創建command物件
                                SqlCommand commandUpdate = new SqlCommand(sqlUpdate, connection);

                                //4.參數化，Parameters:為了防止SQL Injection 程式碼攻擊，所以寫入SQL時，要使用參數「@參數名稱」來代替直接給的值，而給予參數的資料型態要使用Add
                                commandUpdate.Parameters.AddWithValue("@overview_content", overview_content);
                                commandUpdate.Parameters.AddWithValue("@overview_dime", overview_dime);
                                commandUpdate.Parameters.AddWithValue("@overview_dimePhoto", photo_path);
                                commandUpdate.Parameters.AddWithValue("@yatch_id", Convert.ToInt32(yatch_id));

                                //5.資料庫連線開啟
                                connection.Open();

                                //6.執行sql (新增刪除修改)
                                commandUpdate.ExecuteNonQuery(); //無回傳值

                                //7.資料庫關閉
                                connection.Close();

                                //畫面渲染
                                Show();
                            }
                            else
                            {
                            }
                        }
                        else
                        {
                            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Message", "<script>alert('檔名重複！請更改檔名！');</script>");
                        }
                    }
                    //如果沒選擇檔案即執行
                    else
                    {
                        //1.連線資料庫

                        //2.sql語法，VALUES先使用@參數來取代直接給值，以防SQL Injection 程式碼
                        //新增
                        string sqlUpdate = "UPDATE overviews SET overview_content=@overview_content, overview_dime=@overview_dime WHERE yatch_id = @yatch_id ";

                        //3.創建command物件
                        SqlCommand commandUpdate = new SqlCommand(sqlUpdate, connection);

                        //4.參數化，Parameters:為了防止SQL Injection 程式碼攻擊，所以寫入SQL時，要使用參數「@參數名稱」來代替直接給的值，而給予參數的資料型態要使用Add
                        commandUpdate.Parameters.AddWithValue("@overview_content", overview_content);
                        commandUpdate.Parameters.AddWithValue("@overview_dime", overview_dime);
                        commandUpdate.Parameters.AddWithValue("@yatch_id", Convert.ToInt32(yatch_id));

                        //5.資料庫連線開啟
                        connection.Open();

                        //6.執行sql (新增刪除修改)
                        commandUpdate.ExecuteNonQuery(); //無回傳值

                        //7.資料庫關閉
                        connection.Close();

                        //畫面渲染
                        Show();
                    }
                }
                //SQL沒有資料，新增
                else
                {
                    //取得要新增的資料
                    string overview_content = cke_overview_content.Text;
                    string overview_dime = cke_overview_dime.Text;

                    //取得上傳檔案名稱(包含.jpg)
                    string fileName = ful_overview_dimePhoto.FileName;

                    //取得上傳檔案尾標(即 【.jpg】)
                    string fileExtension = Path.GetExtension(fileName).ToLower();

                    //建立上傳檔案許可之尾標
                    string[] allowExtensions = { ".jpg", ".jepg", ".png", ".gif", ".jfif" };

                    //建立圖片存檔之路徑
                    string savePath = @"C:\Users\盧致宇\source\repos\Yachts\Yachts\photos\overviewDimePhoto\";

                    //建立存在資料庫之取出圖片路徑
                    string photo_path = "~/photos/overviewDimePhoto/" + fileName;

                    //檔案格式OK的布林值
                    Boolean fileOK = false;

                    //檔案上傳OK的布林值
                    Boolean uploadOK = false;

                    //資料庫是否有相同檔案
                    Boolean samePhotoInSQL = false;

                    //如果有選擇檔案即執行
                    if (ful_overview_dimePhoto.HasFile)
                    {
                        //1.連線資料庫(合在一起寫)

                        //2.sql語法
                        string sqlCheck = "SELECT * FROM yatchs";

                        //3.創建command物件
                        SqlCommand commandCheckFile = new SqlCommand(sqlCheck, connection);

                        //4.資料庫連線開啟
                        connection.Open();

                        //5.執行sql (連線的作法-需自行關閉)
                        SqlDataReader readerCheckFile = commandCheckFile.ExecuteReader();
                        //DataReader速度快只能逐筆單向有上往下而且不能計算，適合用來抓單筆資料

                        while (readerCheckFile.Read())
                        {
                            if (photo_path == readerCheckFile["yatch_coverPhoto"].ToString())
                            {
                                samePhotoInSQL = true;
                                break;
                            }
                        }

                        //6.資料庫關閉
                        connection.Close();

                        //如果資料庫沒有一樣的上傳檔案才執行
                        if (!samePhotoInSQL)
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
                                    ful_overview_dimePhoto.SaveAs(saveResult);
                                    uploadOK = true;
                                }
                                catch
                                {
                                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Message", "<script>alert('發生例外錯誤，上傳失敗！');</script>");
                                }
                            }
                            else
                            {
                                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Message", "<script>alert('上傳的檔案，附檔名只能是.jpg , .jepg , .png , .gif , .jfif, ！');</script>");
                            }
                            //上傳檔案成功即執行
                            if (uploadOK)
                            {
                                //1.連線資料庫

                                //2.sql語法，VALUES先使用@參數來取代直接給值，以防SQL Injection 程式碼
                                //新增
                                string sqlAdd = "INSERT INTO overviews (overview_content,overview_dime,overview_dimePhoto,yatch_id) VALUES(@overview_content,@overview_dime,@overview_dimePhoto,@yatch_id)";


                                //3.創建command物件
                                SqlCommand commandAdd = new SqlCommand(sqlAdd, connection);

                                //4.參數化，Parameters:為了防止SQL Injection 程式碼攻擊，所以寫入SQL時，要使用參數「@參數名稱」來代替直接給的值，而給予參數的資料型態要使用Add
                                commandAdd.Parameters.AddWithValue("@overview_content", overview_content);
                                commandAdd.Parameters.AddWithValue("@overview_dime", overview_dime);
                                commandAdd.Parameters.AddWithValue("@overview_dimePhoto", photo_path);
                                commandAdd.Parameters.AddWithValue("@yatch_id", Convert.ToInt32(yatch_id));

                                //5.資料庫連線開啟
                                connection.Open();

                                //6.執行sql (新增刪除修改)
                                commandAdd.ExecuteNonQuery(); //無回傳值

                                //7.資料庫關閉
                                connection.Close();

                                //畫面渲染
                                Show();
                            }
                            else
                            {

                            }
                        }
                        else
                        {
                            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Message", "<script>alert('檔名重複！請更改檔名！');</script>");
                        }
                    }
                    else
                    {
                        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Message", "<script>alert('請選擇一張圖片！');</script>");
                    }
                }
            }

        }
        protected void cancelExcelDL_Click(object sender, EventArgs e)
        {
            editExceptDL.Text = "開始編輯";

            cke_overview_content.Visible = false;
            lbl_overview_content.Visible = true;
            cke_overview_dime.Visible = false;
            lbl_overview_dime.Visible = true;
            ful_overview_dimePhoto.Visible = false;
            img_overview_dimePhoto.Visible = true;
            cancelExcelDL.Visible = false;
        }
        private void Show2()
        {
            string yatch_id = Request.QueryString["yatch_id"];

            //1.連線資料庫(合在一起寫)
            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["YachtsConnectionString"].ConnectionString);

            //2.sql語法
            string sql = "SELECT * FROM overviewsDL WHERE yatch_id = @yatch_id";

            //3.創建command物件
            SqlCommand command = new SqlCommand(sql, connection);

            //3.5 參數化，Parameters:為了防止SQL Injection 程式碼攻擊，所以寫入SQL時，要使用參數「@參數名稱」來代替直接給的值，而給予參數的資料型態要使用Add
            command.Parameters.AddWithValue("@yatch_id", Convert.ToInt32(yatch_id));

            //4.資料庫連線開啟
            connection.Open();

            //5.執行sql (連線的作法-需自行關閉)
            SqlDataReader reader = command.ExecuteReader();
            // 繫結到Repeater的數據源
            Repeater2.DataSource = reader;
            Repeater2.DataBind();

            //6.資料庫關閉
            connection.Close();
        }
        private static string GetFileName(string a)
        {
            string ans = a;
            //取得 ~/overviewsUpload/ 以後
            if (a.IndexOf("~/overviewsUpload/") >= 0)
            {
                ans = a.Substring(a.IndexOf("~/overviewsUpload/") + 18);
            }
            return ans;
        }
        protected void btn_Upload_Click(object sender, EventArgs e)
        {

            string yatch_id = Request.QueryString["yatch_id"];

            //取得上傳檔案名稱(包含副檔名)
            string fileName = ful_overviewsDL_path.FileName;

            //建立檔案儲存之路徑
            string savePath = @"C:\Users\盧致宇\source\repos\Yachts\Yachts\overviewsUpload\";

            //建立存在資料庫SQL之取出檔案路徑
            string file_path = "~/overviewsUpload/" + fileName;

            //如果有選擇檔案即執行
            if (ful_overviewsDL_path.HasFile)
            {
                //以下判斷存檔路徑是否有一樣的檔案，若有則添加流水號
                // 儲存檔案完整路徑
                string fileSavePath = Path.Combine(savePath, fileName);

                // 檢查檔案是否已存在，若存在則添加流水號
                string temptFileName = fileName;
                int counter = 0;
                while (File.Exists(fileSavePath))
                {
                    counter += 1;

                    // 新的檔案名稱
                    fileName = $"{counter}_" + temptFileName;

                    // 更新儲存檔案完整路徑
                    fileSavePath = Path.Combine(savePath, fileName);
                }
                //以上判斷存檔路徑是否有一樣的檔案，若有則添加流水號

                // 現在可以使用 fileSavePath 來儲存檔案

                //更新存在資料庫SQL之取出檔案路徑
                file_path = "~/overviewsUpload/" + fileName;

                try
                {
                    //存檔案到路徑
                    ful_overviewsDL_path.SaveAs(fileSavePath);

                    //1.連線資料庫(合在一起寫)
                    SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["YachtsConnectionString"].ConnectionString);

                    //2.sql語法，VALUES先使用@參數來取代直接給值，以防SQL Injection 程式碼
                    string sql2 = "INSERT INTO overviewsDL (overviewsDL_downloadPath, yatch_id) VALUES(@overviewsDL_downloadPath, @yatch_id)";

                    //3.創建command物件
                    SqlCommand command2 = new SqlCommand(sql2, connection);

                    //4.參數化，Parameters:為了防止SQL Injection 程式碼攻擊，所以寫入SQL時，要使用參數「@參數名稱」來代替直接給的值，而給予參數的資料型態要使用Add
                    command2.Parameters.AddWithValue("@overviewsDL_downloadPath", file_path);
                    command2.Parameters.AddWithValue("@yatch_id", Convert.ToInt32(yatch_id));

                    //5.資料庫連線開啟
                    connection.Open();

                    //6.執行sql (新增刪除修改)
                    command2.ExecuteNonQuery(); //無回傳值

                    //7.資料庫關閉
                    connection.Close();

                    Show();
                    Show2();
                }
                catch
                {
                    // 輸出JavaScript警告對話框
                    string message = "發生例外錯誤，上傳失敗！";
                    string encodedMessage = HttpUtility.HtmlEncode(message);
                    Response.Write("<script>alert('" + encodedMessage + "');</script>");
                }
            }
            else
            {
                // 輸出JavaScript警告對話框
                string message = "必需選擇一個檔案！";
                string encodedMessage = HttpUtility.HtmlEncode(message);
                Response.Write("<script>alert('" + encodedMessage + "');</script>");
            }
        }

        protected void Repeater2_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                // 获取当前数据行
                IDataRecord dataItem = (IDataRecord)e.Item.DataItem;

                // 查找 Label/LinkButton 控件
                //Label Label1 = (Label)e.Item.FindControl("Label1");
                LinkButton lbtn_overviewsDL_path = (LinkButton)e.Item.FindControl("lbtn_overviewsDL_path");

                // 设置 Label/LinkButton 的文本值
                //Label1.Text = dataItem["newsDL_path"].ToString().Replace("/", "");
                lbtn_overviewsDL_path.Text = GetFileName(dataItem["overviewsDL_downloadPath"].ToString());
            }
        }

        protected void btn_deleteDL_Click(object sender, EventArgs e)
        {
            // 取得lbtn_overviewsDL_path.text
            Button btn_deleteDL = (Button)sender;
            RepeaterItem item = (RepeaterItem)btn_deleteDL.NamingContainer;
            LinkButton lbtn_overviewsDL_path = (LinkButton)item.FindControl("lbtn_overviewsDL_path");
            string overviewsDL_path = lbtn_overviewsDL_path.Text;

            string deleteFilePath = @"C:\Users\盧致宇\source\repos\Yachts\Yachts\overviewsUpload\" + overviewsDL_path;

            //取得overviewsDL_id
            Button button = (Button)sender;
            string overviewsDL_id = button.CommandArgument;

            //1.連線資料庫
            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["YachtsConnectionString"].ConnectionString);

            //2.sql語法，VALUES先使用@參數來取代直接給值，以防SQL Injection 程式碼
            //刪除
            string sql = "DELETE FROM overviewsDL WHERE overviewsDL_id = @overviewsDL_id; ";

            //3.創建command物件
            SqlCommand command = new SqlCommand(sql, connection);

            //4.參數化，Parameters:為了防止SQL Injection 程式碼攻擊，所以寫入SQL時，要使用參數「@參數名稱」來代替直接給的值，而給予參數的資料型態要使用Add
            command.Parameters.AddWithValue("@overviewsDL_id", Convert.ToInt32(overviewsDL_id));


            //5.資料庫連線開啟
            connection.Open();

            //6.執行sql (新增刪除修改)
            command.ExecuteNonQuery(); //無回傳值

            //7.資料庫關閉
            connection.Close();

            //畫面渲染
            Show();
            Show2();

        }

        protected void goYatchOverview_Click(object sender, EventArgs e)
        {
            string yatch_id = Request.QueryString["yatch_id"].ToString();
            string yatch_Name = Request.QueryString["yatch_name"].ToString();

            Response.Redirect("yatchs_overviews.aspx?yatch_id=" + yatch_id + "&yatch_name=" + yatch_Name);
        }

        protected void backYatch_Click(object sender, EventArgs e)
        {
            Response.Redirect("yatchs.aspx");
        }
        protected void goYatchSpecs_Click(object sender, EventArgs e)
        {
            string yatch_id = Request.QueryString["yatch_id"].ToString();
            string yatch_Name = Request.QueryString["yatch_name"].ToString();

            Response.Redirect("yatchs_specs.aspx?yatch_id=" + yatch_id + "&yatch_name=" + yatch_Name);
        }
        protected void goYatchLayouts_Click(object sender, EventArgs e)
        {
            string yatch_id = Request.QueryString["yatch_id"].ToString();
            string yatch_Name = Request.QueryString["yatch_name"].ToString();

            Response.Redirect("yatchs_layouts.aspx?yatch_id=" + yatch_id + "&yatch_name=" + yatch_Name);
        }
        protected void goYatchPhotos_Click(object sender, EventArgs e)
        {
            string yatch_id = Request.QueryString["yatch_id"].ToString();
            string yatch_Name = Request.QueryString["yatch_name"].ToString();

            Response.Redirect("yatch_yatchPhotos.aspx?yatch_id=" + yatch_id + "&yatch_name=" + yatch_Name);
        }

        protected void lbtn_goYatchs_Click(object sender, EventArgs e)
        {
            Response.Redirect("yatchs.aspx");
        }
    }
}
