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
    public partial class yatchs : System.Web.UI.Page
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
            if (!IsPostBack)
            {
                Show();
            }
            
        }
        public static string GetFileName(string a)
        {
            string output = a;
            if (a.IndexOf("~/photos/yatchsCoverPhoto/") >= 0)
            {
                output = output.Substring(output.IndexOf("~/photos/yatchsCoverPhoto/") + 26);
            }
            return output;
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            if (Button1.Text == "新增船型")
            {
                lbl_input_name.Visible = true;
                txt_input_modelName.Visible = true;
                txt_input_num.Visible = true;
                txt_input_note.Visible = true;

                lbl_input_newModel.Visible = true;
                chk_input_newModel.Visible = true;

                lbl_input_coverPhoto.Visible = true;
                ful_input_coverPhoto.Visible = true;

                Button1.Text = "確認送出";

                Button2.Visible = true;
            }
            else
            {
                lbl_input_name.Visible = false;
                txt_input_modelName.Visible = false;
                txt_input_num.Visible = false;
                txt_input_note.Visible = false;

                lbl_input_newModel.Visible = false;
                chk_input_newModel.Visible = false;

                lbl_input_coverPhoto.Visible = false;
                ful_input_coverPhoto.Visible = false;

                Button1.Text = "新增船型";

                Button2.Visible = false;

                string input_modelName = txt_input_modelName.Text;
                string input_num = txt_input_num.Text;
                string input_note = txt_input_note.Text;
                bool input_newModel = chk_input_newModel.Checked;


                //取得上傳檔案名稱(包含.jpg)
                string fileName = ful_input_coverPhoto.FileName;

                //取得上傳檔案尾標(即 【.jpg】)
                string fileExtension = Path.GetExtension(fileName).ToLower();

                //建立上傳檔案許可之尾標
                string[] allowExtensions = { ".jpg", ".jepg", ".png", ".gif", ".jfif" };

                //建立圖片存檔之路徑
                string savePath = @"C:\Users\盧致宇\source\repos\Yachts\Yachts\photos\yatchsCoverPhoto\";

                //建立存在資料庫之取出圖片路徑
                string photo_path = "~/photos/yatchsCoverPhoto/" + fileName;

                //建立檔案格式OK的布林值
                Boolean fileOK = false;

                //建立檔案上傳OK的布林值
                Boolean uploadOK = false;

                //資料庫是否有相同檔案
                Boolean samePhotoInSQL = false;

                //如果有選擇檔案即執行
                if (ful_input_coverPhoto.HasFile)
                {
                    //1.連線資料庫(合在一起寫)
                    SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["YachtsConnectionString"].ConnectionString);

                    //2.sql語法
                    string sql = "SELECT * FROM yatchs";

                    //3.創建command物件
                    SqlCommand command = new SqlCommand(sql, connection);

                    //4.資料庫連線開啟
                    connection.Open();

                    //5.執行sql (連線的作法-需自行關閉)
                    SqlDataReader reader = command.ExecuteReader();
                    //DataReader速度快只能逐筆單向有上往下而且不能計算，適合用來抓單筆資料

                    while (reader.Read())
                    {
                        if (photo_path == reader["yatch_coverPhoto"].ToString())
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
                                ful_input_coverPhoto.SaveAs(saveResult);
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
                        if (uploadOK && input_modelName != "" && input_num != "")
                        {
                            //1.連線資料庫
                            //已存在

                            //2.sql語法，VALUES先使用@參數來取代直接給值，以防SQL Injection 程式碼
                            string sql2 = "INSERT INTO yatchs (yatch_name, yatch_modelName, yatch_num, yatch_note, yatch_coverPhoto, yatch_newModel) VALUES(@yatch_name, @yatch_modelName, @yatch_num, @yatch_note, @yatch_coverPhoto, @yatch_newModel)";

                            //3.創建command物件
                            SqlCommand command2 = new SqlCommand(sql2, connection);

                            //4.參數化，Parameters:為了防止SQL Injection 程式碼攻擊，所以寫入SQL時，要使用參數「@參數名稱」來代替直接給的值，而給予參數的資料型態要使用Add
                            command2.Parameters.AddWithValue("@yatch_name", input_modelName +" "+ input_num);
                            command2.Parameters.AddWithValue("@yatch_modelName", input_modelName);
                            command2.Parameters.AddWithValue("@yatch_num", input_num);
                            command2.Parameters.AddWithValue("@yatch_note", input_note);
                            command2.Parameters.AddWithValue("@yatch_coverPhoto", photo_path);
                            command2.Parameters.AddWithValue("@yatch_newModel", input_newModel);

                            //5.資料庫連線開啟
                            connection.Open();

                            //6.執行sql (新增刪除修改)
                            command2.ExecuteNonQuery(); //無回傳值

                            //7.資料庫關閉
                            connection.Close();
                        }
                        else
                        {
                            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Message", "<script>alert('請填寫船名及船號！');</script>");
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
            txt_input_modelName.Text = "";
            txt_input_num.Text="";
            chk_input_newModel.Checked = false;

            Show();
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            lbl_input_name.Visible = false;
            txt_input_modelName.Visible = false;
            txt_input_num.Visible = false;
            txt_input_note.Visible = false;

            lbl_input_newModel.Visible = false;
            chk_input_newModel.Visible = false;

            lbl_input_coverPhoto.Visible = false;
            ful_input_coverPhoto.Visible = false;

            Button1.Text = "新增船型";

            Button2.Visible = false;

            Show();
        }
        private void Show()
        {
            //1.連線資料庫(合在一起寫)
            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["YachtsConnectionString"].ConnectionString);

            //2.sql語法
            string sql = "SELECT * FROM yatchs ORDER BY initdate DESC";

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

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            // 取得點擊的 LinkButton
            LinkButton lb = (LinkButton)sender;

            // 取得所在的 GridViewRow
            GridViewRow row = (GridViewRow)lb.NamingContainer;

            //取得該列的主索引欄位的值(須在gridview 加入 DataKeyNames = "id")
            string yatch_id = GridView1.DataKeys[row.RowIndex]["yatch_id"].ToString();

            LinkButton lbtn_yatch_name = (LinkButton)row.FindControl("lbtn_yatch_name");

            Response.Redirect("yatchs_overviews.aspx?yatch_id="+ yatch_id+"&yatch_name="+ lbtn_yatch_name.Text);
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
            string yatch_id = GridView1.DataKeys[row.RowIndex]["yatch_id"].ToString();

            TextBox txt_yatch_modelName = (TextBox)row.FindControl("txt_yatch_modelName");
            TextBox txt_yatch_num = (TextBox)row.FindControl("txt_yatch_num");
            TextBox txt_yatch_note = (TextBox)row.FindControl("txt_yatch_note");
            CheckBox edit_chk_yatch_newModel = (CheckBox)row.FindControl("edit_chk_yatch_newModel");
            FileUpload ful_yatch_coverPhoto = (FileUpload)row.FindControl("ful_yatch_coverPhoto");

            //取得上傳檔案名稱(包含.jpg)
            string fileName = ful_yatch_coverPhoto.FileName;

            //取得上傳檔案尾標(即 【.jpg】)
            string fileExtension = Path.GetExtension(fileName).ToLower();

            //建立上傳檔案許可之尾標
            string[] allowExtensions = { ".jpg", ".jepg", ".png", ".gif", ".jfif" };

            //建立圖片存檔之路徑
            string savePath = @"C:\Users\盧致宇\source\repos\Yachts\Yachts\photos\yatchsCoverPhoto\";

            //建立存在資料庫之取出圖片路徑
            string photo_path = "~/photos/yatchsCoverPhoto/" + fileName;

            //建立檔案格式OK的布林值
            Boolean fileOK = false;

            //建立檔案上傳OK的布林值
            Boolean uploadOK = false;

            //資料庫是否有相同檔案
            Boolean samePhotoInSQL = false;

            //取得一開始的路徑
            Image edit_img_yatch_coverPhoto = (Image)row.FindControl("edit_img_yatch_coverPhoto");
            string originalPhotoPath = edit_img_yatch_coverPhoto.ImageUrl;
            string originalPhotoSavePath = Path.Combine(savePath, GetFileName(originalPhotoPath));

            //如果有選擇檔案即執行
            if (ful_yatch_coverPhoto.HasFile)
            {
                //1.連線資料庫(合在一起寫)
                SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["YachtsConnectionString"].ConnectionString);

                //2.sql語法
                string sql = "SELECT * FROM yatchs";

                //3.創建command物件
                SqlCommand command = new SqlCommand(sql, connection);

                //4.資料庫連線開啟
                connection.Open();

                //5.執行sql (連線的作法-需自行關閉)
                SqlDataReader reader = command.ExecuteReader();
                //DataReader速度快只能逐筆單向有上往下而且不能計算，適合用來抓單筆資料

                while (reader.Read())
                {
                    if (photo_path == reader["yatch_coverPhoto"].ToString())
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

                            //存新檔案到路徑
                            ful_yatch_coverPhoto.SaveAs(saveResult);

                            //刪除原本檔案
                            if (File.Exists(originalPhotoSavePath))
                            {
                                File.Delete(originalPhotoSavePath);
                            }

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
                        //取得該列的主索引欄位的值(須在gridview 加入 DataKeyNames = "id")

                        //1.連線資料庫

                        //2.sql語法，VALUES先使用@參數來取代直接給值，以防SQL Injection 程式碼
                        //修改
                        string sql2 = "UPDATE yatchs SET yatch_name=@yatch_name, yatch_modelName=@yatch_modelName, yatch_num=@yatch_num, yatch_note=@yatch_note, yatch_coverPhoto=@yatch_coverPhoto, yatch_newModel=@yatch_newModel  WHERE yatch_id = @yatch_id ";

                        //3.創建command物件
                        SqlCommand command2 = new SqlCommand(sql2, connection);

                        //4.參數化，Parameters:為了防止SQL Injection 程式碼攻擊，所以寫入SQL時，要使用參數「@參數名稱」來代替直接給的值，而給予參數的資料型態要使用Add
                        command2.Parameters.AddWithValue("@yatch_name", txt_yatch_modelName.Text+" "+ txt_yatch_num.Text);
                        command2.Parameters.AddWithValue("@yatch_modelName", txt_yatch_modelName.Text);
                        command2.Parameters.AddWithValue("@yatch_num", txt_yatch_num.Text);
                        command2.Parameters.AddWithValue("@yatch_note", txt_yatch_note.Text);
                        command2.Parameters.AddWithValue("@yatch_coverPhoto", photo_path);
                        command2.Parameters.AddWithValue("@yatch_newModel", edit_chk_yatch_newModel.Checked);
                        command2.Parameters.AddWithValue("@yatch_id",Convert.ToInt32( yatch_id));

                        //5.資料庫連線開啟
                        connection.Open();

                        //6.執行sql (新增刪除修改)
                        command2.ExecuteNonQuery(); //無回傳值

                        //7.資料庫關閉
                        connection.Close();
                    }
                    else
                    {
                        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Message", "<script>alert('請填寫船名及船號！');</script>");
                    }
                }
                else
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Message", "<script>alert('檔名重複！請更改檔名！');</script>");
                }
            }
            else
            {
                //取得該列的主索引欄位的值(須在gridview 加入 DataKeyNames = "id")

                //1.連線資料庫(合在一起寫)
                SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["YachtsConnectionString"].ConnectionString);

                //2.sql語法，VALUES先使用@參數來取代直接給值，以防SQL Injection 程式碼
                //修改
                string sql2 = "UPDATE yatchs SET yatch_name=@yatch_name, yatch_modelName=@yatch_modelName, yatch_num=@yatch_num, yatch_note=@yatch_note, yatch_newModel=@yatch_newModel  WHERE yatch_id = @yatch_id ";

                //3.創建command物件
                SqlCommand command2 = new SqlCommand(sql2, connection);

                //4.參數化，Parameters:為了防止SQL Injection 程式碼攻擊，所以寫入SQL時，要使用參數「@參數名稱」來代替直接給的值，而給予參數的資料型態要使用Add
                command2.Parameters.AddWithValue("@yatch_name", txt_yatch_modelName.Text + " " + txt_yatch_num.Text);
                command2.Parameters.AddWithValue("@yatch_modelName", txt_yatch_modelName.Text);
                command2.Parameters.AddWithValue("@yatch_num", txt_yatch_num.Text);
                command2.Parameters.AddWithValue("@yatch_note", txt_yatch_note.Text);
                command2.Parameters.AddWithValue("@yatch_newModel", edit_chk_yatch_newModel.Checked);
                command2.Parameters.AddWithValue("@yatch_id", Convert.ToInt32(yatch_id));

                //5.資料庫連線開啟
                connection.Open();

                //6.執行sql (新增刪除修改)
                command2.ExecuteNonQuery(); //無回傳值

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
            string yatch_id = GridView1.DataKeys[row.RowIndex]["yatch_id"].ToString();

            //取得資料列中 ID 為 img_yatch_coverPhoto 的 Image 控制項
            Image img_yatch_coverPhoto = (Image)row.FindControl("img_yatch_coverPhoto");

            //取得該列的照片欄位的值(檔案路徑)
            string deleteFile = img_yatch_coverPhoto.ImageUrl;

            string deleteFilePath = @"C:\Users\盧致宇\source\repos\Yachts\Yachts\photos\yatchsCoverPhoto\" + GetFileName(deleteFile);

            //如果檔案路徑存在才執行
            if (File.Exists(deleteFilePath))
            {
                File.Delete(deleteFilePath);
            }

            //執行刪除資料庫內容
            //1.連線資料庫(合在一起寫)
            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["YachtsConnectionString"].ConnectionString);

            //2.sql語法，VALUES先使用@參數來取代直接給值，以防SQL Injection 程式碼
            //刪除
            string sql = "DELETE FROM yatchs WHERE yatch_id = @yatch_id; ";

            //3.創建command物件
            SqlCommand command = new SqlCommand(sql, connection);

            //4.參數化，Parameters:為了防止SQL Injection 程式碼攻擊，所以寫入SQL時，要使用參數「@參數名稱」來代替直接給的值，而給予參數的資料型態要使用Add
            command.Parameters.AddWithValue("@yatch_id", Convert.ToInt32( yatch_id ));

            //5.資料庫連線開啟
            connection.Open();

            //6.執行sql (新增刪除修改)
            command.ExecuteNonQuery(); //無回傳值

            //7.資料庫關閉
            connection.Close();

            Show();
        }
    }
}