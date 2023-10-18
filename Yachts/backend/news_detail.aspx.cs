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
    public partial class news_detail : System.Web.UI.Page
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
                string news_id = Request.QueryString["news_id"];
                string news_title = Request.QueryString["news_title"];
                if (!IsPostBack)
                {
                    if (news_id == null || news_title == null)
                    {
                        Response.Redirect("news.aspx");
                    }

                    //CKEditor設定
                    FileBrowser fileBrowser = new FileBrowser();
                    fileBrowser.BasePath = "/ckfinder";
                    fileBrowser.SetupCKEditor(cke_news_longDes);

                    Show();
                    Show2();
                }

            }
        }
        private void Show()
        {
            string news_id = Request.QueryString["news_id"];
            string news_title = Request.QueryString["news_title"];

            lbl_news_title.Text = news_title;

            //1.連線資料庫(合在一起寫)
            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["YachtsConnectionString"].ConnectionString);

            //2.sql語法
            string sql = "SELECT * FROM news WHERE news_id = @news_id";

            //3.創建command物件
            SqlCommand command = new SqlCommand(sql, connection);

            //3.5 參數化，Parameters:為了防止SQL Injection 程式碼攻擊，所以寫入SQL時，要使用參數「@參數名稱」來代替直接給的值，而給予參數的資料型態要使用Add
            command.Parameters.AddWithValue("@news_id", Convert.ToInt32(news_id));

            //4.資料庫連線開啟
            connection.Open();

            //5.執行sql (連線的作法-需自行關閉)
            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                lbl_news_longDes.Text = reader["news_longDes"].ToString();
                cke_news_longDes.Text = reader["news_longDes"].ToString();
            }

            //6.資料庫關閉
            connection.Close();
        }
        private void Show2()
        {
            string news_id = Request.QueryString["news_id"];

            //1.連線資料庫(合在一起寫)
            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["YachtsConnectionString"].ConnectionString);

            //2.sql語法
            string sql = "SELECT * FROM newsDL WHERE news_id = @news_id";

            //3.創建command物件
            SqlCommand command = new SqlCommand(sql, connection);

            //3.5 參數化，Parameters:為了防止SQL Injection 程式碼攻擊，所以寫入SQL時，要使用參數「@參數名稱」來代替直接給的值，而給予參數的資料型態要使用Add
            command.Parameters.AddWithValue("@news_id", Convert.ToInt32(news_id));

            //4.資料庫連線開啟
            connection.Open();

            //5.執行sql (連線的作法-需自行關閉)
            SqlDataReader reader = command.ExecuteReader();
            // 繫結到Repeater的數據源
            Repeater1.DataSource = reader;
            Repeater1.DataBind();

            //6.資料庫關閉
            connection.Close();
        }
        protected void btn_startEdit_Click(object sender, EventArgs e)
        {
            if (btn_startEdit.Text == "開始編輯")
            {
                lbl_news_longDes.Visible = false;
                cke_news_longDes.Visible = true;

                btn_startEdit.Text = "確認送出";
                btn_cancelEdit.Visible = true;
            }
            else
            {
                btn_cancelEdit.Visible = false;

                string news_id = Request.QueryString["news_id"];
                string news_longDes = cke_news_longDes.Text;

                //1.連線資料庫
                SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["YachtsConnectionString"].ConnectionString);

                //2.sql語法，VALUES先使用@參數來取代直接給值，以防SQL Injection 程式碼
                //修改
                string sql = "UPDATE news SET news_longDes=@news_longDes  WHERE news_id = @news_id ";


                //3.創建command物件
                SqlCommand command = new SqlCommand(sql, connection);

                //4.參數化，Parameters:為了防止SQL Injection 程式碼攻擊，所以寫入SQL時，要使用參數「@參數名稱」來代替直接給的值，而給予參數的資料型態要使用Add
                command.Parameters.AddWithValue("@news_longDes", news_longDes);
                command.Parameters.AddWithValue("@news_id", Convert.ToInt32(news_id));


                //5.資料庫連線開啟
                connection.Open();

                //6.執行sql (新增刪除修改)
                command.ExecuteNonQuery(); //無回傳值

                //7.資料庫關閉
                connection.Close();

                lbl_news_longDes.Visible = true;
                cke_news_longDes.Visible = false;

                btn_startEdit.Text = "開始編輯";

                //畫面渲染
                Show();
            }

        }
        protected void btn_cancelEdit_Click(object sender, EventArgs e)
        {
            if (btn_cancelEdit.Text == "取消編輯")
            {
                btn_startEdit.Text = "開始編輯";
                btn_cancelEdit.Visible = false;
                lbl_news_longDes.Visible = true;
                cke_news_longDes.Visible = false;
            }
        }
        private static string getFileName(string a)
        {
            string ans = "";
            //取得 ~/fileUpload/ 以後
            if (a.IndexOf("~/newsUpload/") >= 0)
            {
                ans = a.Substring(a.IndexOf("~/newsUpload/") + 13);
            }
            return ans;
        }
        protected void Button3_Click(object sender, EventArgs e)
        {


            string news_id = Request.QueryString["news_id"];

            //取得上傳檔案名稱(包含副檔名)
            string fileName = ful_newsDL_path.FileName;

            //建立檔案儲存之路徑
            string savePath = @"C:\Users\盧致宇\source\repos\Yachts\Yachts\newsUpload\";

            //建立存在資料庫之取出檔案路徑
            string file_path = "~/newsUpload/" + fileName;

            //資料庫有一樣的檔名
            Boolean sameNameInSQL = false;

            //建立檔案上傳OK的布林值
            Boolean uploadOK = false;

            //如果有選擇檔案即執行
            if (ful_newsDL_path.HasFile)
            {
                //1.連線資料庫(合在一起寫)
                SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["YachtsConnectionString"].ConnectionString);

                //2.sql語法
                string sql = "SELECT newsDL_path FROM newsDL WHERE news_id = @news_id";

                //3.創建command物件
                SqlCommand command = new SqlCommand(sql, connection);

                //3.5參數化，Parameters:為了防止SQL Injection 程式碼攻擊，所以寫入SQL時，要使用參數「@參數名稱」來代替直接給的值，而給予參數的資料型態要使用Add
                command.Parameters.AddWithValue("@news_id", news_id);

                //4.資料庫連線開啟
                connection.Open();

                //5.執行sql (連線的作法-需自行關閉)
                SqlDataReader reader = command.ExecuteReader();
                //DataReader速度快只能逐筆單向有上往下而且不能計算，適合用來抓單筆資料
                while (reader.Read())
                {
                    if (getFileName(reader["newsDL_path"].ToString()) == fileName)
                    {
                        sameNameInSQL = true;
                        break;
                    }
                }

                command.Cancel();
                reader.Close();

                //6.資料庫關閉
                connection.Close();

                //資料庫沒有相同檔名之檔案才執行
                if (!sameNameInSQL)
                {
                    int fileSize = ful_newsDL_path.PostedFile.ContentLength;  // 取得上傳檔案的大小（以位元組為單位）
                    int maxSizeInBytes = 1024 * 1024 * 5;  // 限制的最大檔案大小（5MB）

                    if (fileSize <= maxSizeInBytes)
                    {
                        try
                        {
                            //建立存放檔案路徑
                            string saveResult = Path.Combine(savePath, fileName);
                            //存檔案到路徑
                            ful_newsDL_path.SaveAs(saveResult);

                            //2.sql語法，VALUES先使用@參數來取代直接給值，以防SQL Injection 程式碼
                            string sql2 = "INSERT INTO newsDL (newsDL_path, news_id) VALUES(@newsDL_path, @news_id)";

                            //3.創建command物件
                            SqlCommand command2 = new SqlCommand(sql2, connection);

                            //4.參數化，Parameters:為了防止SQL Injection 程式碼攻擊，所以寫入SQL時，要使用參數「@參數名稱」來代替直接給的值，而給予參數的資料型態要使用Add
                            command2.Parameters.AddWithValue("@newsDL_path", file_path);
                            command2.Parameters.AddWithValue("@news_id", Convert.ToInt32(news_id));

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
                        // 檔案大小超過限制，顯示提示框
                        string message = "上傳的檔案大小超過限制（5MB）。請選擇更小的檔案。";
                        string script = $"<script>alert('{message}');</script>";
                        ClientScript.RegisterStartupScript(this.GetType(), "alert", script);
                    }
                }
                else
                {
                    // 輸出JavaScript警告對話框
                    string message = "資料庫已有相同檔名之檔案，請修改檔名！";
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

        protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                // 获取当前数据行
                IDataRecord dataItem = (IDataRecord)e.Item.DataItem;

                // 查找 Label/LinkButton 控件
                //Label Label1 = (Label)e.Item.FindControl("Label1");
                LinkButton lbtn_newsDL_path = (LinkButton)e.Item.FindControl("lbtn_newsDL_path");

                // 设置 Label/LinkButton 的文本值
                //Label1.Text = dataItem["newsDL_path"].ToString().Replace("/", "");
                lbtn_newsDL_path.Text = getFileName(dataItem["newsDL_path"].ToString());
            }
        }
        protected void Button4_Click(object sender, EventArgs e)
        {
            Show2();

            // 取得newsDL_path.text
            Button button4 = (Button)sender;
            RepeaterItem item = (RepeaterItem)button4.NamingContainer;
            LinkButton lbtn_newsDL_path = (LinkButton)item.FindControl("lbtn_newsDL_path");
            string newsDL_path = lbtn_newsDL_path.Text;

            string deleteFilePath = @"C:\Users\盧致宇\source\repos\Yachts\Yachts\newsUpload\" + newsDL_path;

            Boolean deleteOK = false;

            //如果檔案路徑存在才執行
            if (File.Exists(deleteFilePath))
            {
                File.Delete(deleteFilePath);

                deleteOK = true;
            }

            //如果檔案刪除了才執行
            if (deleteOK)
            {
                //取得newsDL_id
                Button button = (Button)sender;
                string newsDL_id = button.CommandArgument;

                //1.連線資料庫
                SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["YachtsConnectionString"].ConnectionString);

                //2.sql語法，VALUES先使用@參數來取代直接給值，以防SQL Injection 程式碼
                //刪除
                string sql = "DELETE FROM newsDL WHERE newsDL_id = @newsDL_id; ";

                //3.創建command物件
                SqlCommand command = new SqlCommand(sql, connection);

                //4.參數化，Parameters:為了防止SQL Injection 程式碼攻擊，所以寫入SQL時，要使用參數「@參數名稱」來代替直接給的值，而給予參數的資料型態要使用Add
                command.Parameters.AddWithValue("@newsDL_id", Convert.ToInt32(newsDL_id));


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
        }

        protected void goNews_Click(object sender, EventArgs e)
        {
            Response.Redirect("news.aspx");
        }
    }
}