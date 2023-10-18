using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Yachts.backend
{
    public partial class yatchPhotos : System.Web.UI.Page
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
                Show();
            }
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
        private static string GetFileName(string a)
        {
            string ans = "";
            //取得~/photos/yatchsPhoto/ 以後
            if (a.IndexOf("~/photos/yatchsPhoto/") >= 0)
            {
                ans = a.Substring(a.IndexOf("~/photos/yatchsPhoto/") + 21);
            }
            return ans;
        }
        private void Show()
        {
            string yatch_id = Request.QueryString["yatch_id"];

            //1.連線資料庫(合在一起寫)
            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["YachtsConnectionString"].ConnectionString);

            //2.sql語法
            string sql = "SELECT * FROM yatchPhotos WHERE yatch_id = @yatch_id";

            //3.創建command物件
            SqlCommand command = new SqlCommand(sql, connection);

            //4.參數化，Parameters:為了防止SQL Injection 程式碼攻擊，所以寫入SQL時，要使用參數「@參數名稱」來代替直接給的值，而給予參數的資料型態要使用Add
            command.Parameters.AddWithValue("@yatch_id", Convert.ToInt32( yatch_id ));

            //4.資料庫連線開啟
            connection.Open();

            //5.執行sql (連線的作法-需自行關閉)
            SqlDataReader reader = command.ExecuteReader();
            //DataReader速度快只能逐筆單向有上往下而且不能計算，適合用來抓單筆資料
            //控制器資料來源
            Repeater1.DataSource = reader; //(拿資料)
                                           //控制器綁定 (真的把資料放進去)
            Repeater1.DataBind();

            //6.資料庫關閉
            connection.Close();
        }
        protected void btn_uploadLayouts_Click(object sender, EventArgs e)
        {
            string yatch_id = Request.QueryString["yatch_id"];

            //取得上傳檔案名稱(包含副檔名)
            string fileName = ful_yatchPhoto_photoPath.FileName;

            //建立檔案儲存之路徑(未加檔名)
            string savePath = @"C:\Users\盧致宇\source\repos\Yachts\Yachts\photos\yatchsPhoto\";

            //建立存在資料庫SQL之取出檔案路徑
            string file_path = "~/photos/yatchsPhoto/" + fileName;

            //如果有選擇檔案即執行
            if (ful_yatchPhoto_photoPath.HasFile)
            {
                //以下判斷存檔路徑是否有一樣的檔案，若有則添加流水號
                // 儲存檔案完整路徑(有加檔名)
                string fileSavePath = Path.Combine(savePath, fileName);

                // 檢查檔案是否已存在，若存在則添加流水號
                string temptFileName = fileName;
                int counter = 0;
                while (File.Exists(fileSavePath))
                {
                    counter += 1;

                    // 更新檔案名稱
                    fileName = $"{counter}_" + temptFileName;

                    // 更新儲存檔案完整路徑(有加檔名)
                    fileSavePath = Path.Combine(savePath, fileName);
                }
                //以上判斷存檔路徑是否有一樣的檔案，若有則添加流水號

                // 現在可以使用 fileSavePath 來儲存檔案

                //更新存在資料庫SQL之取出檔案路徑
                file_path = "~/photos/yatchsPhoto/" + fileName;

                try
                {
                    //1.連線資料庫(合在一起寫)
                    SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["YachtsConnectionString"].ConnectionString);

                    //2.sql語法，VALUES先使用@參數來取代直接給值，以防SQL Injection 程式碼
                    string sql2 = "INSERT INTO yatchPhotos (yatchPhoto_photoPath, yatch_id) VALUES(@yatchPhoto_photoPath, @yatch_id)";

                    //3.創建command物件
                    SqlCommand command2 = new SqlCommand(sql2, connection);

                    //4.參數化，Parameters:為了防止SQL Injection 程式碼攻擊，所以寫入SQL時，要使用參數「@參數名稱」來代替直接給的值，而給予參數的資料型態要使用Add
                    command2.Parameters.AddWithValue("@yatchPhoto_photoPath", file_path);
                    command2.Parameters.AddWithValue("@yatch_id", Convert.ToInt32(yatch_id));

                    //5.資料庫連線開啟
                    connection.Open();

                    //6.執行sql (新增刪除修改)
                    command2.ExecuteNonQuery(); //無回傳值

                    //7.資料庫關閉
                    connection.Close();

                    //存檔案到路徑
                    ful_yatchPhoto_photoPath.SaveAs(fileSavePath);

                    Show();
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

        protected void deleteImage_Click(object sender, EventArgs e)
        {
            //取得newsDL_id
            Button button = (Button)sender;
            string yatchPhoto_id = button.CommandArgument;

            //1.連線資料庫
            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["YachtsConnectionString"].ConnectionString);

            //2.sql語法，VALUES先使用@參數來取代直接給值，以防SQL Injection 程式碼
            //刪除
            string sql = "DELETE FROM yatchPhotos WHERE yatchPhoto_id = @yatchPhoto_id; ";

            //3.創建command物件
            SqlCommand command = new SqlCommand(sql, connection);

            //4.參數化，Parameters:為了防止SQL Injection 程式碼攻擊，所以寫入SQL時，要使用參數「@參數名稱」來代替直接給的值，而給予參數的資料型態要使用Add
            command.Parameters.AddWithValue("@yatchPhoto_id", Convert.ToInt32(yatchPhoto_id));

            //5.資料庫連線開啟
            connection.Open();

            //6.執行sql (新增刪除修改)
            command.ExecuteNonQuery(); //無回傳值

            //7.資料庫關閉
            connection.Close();

            //畫面渲染
            Show();

        }
        protected void lbtn_goYatchs_Click(object sender, EventArgs e)
        {
            Response.Redirect("yatchs.aspx");
        }
    }
}