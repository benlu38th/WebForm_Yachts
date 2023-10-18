using CKFinder;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Yachts.backend
{
    public partial class yatchs_specs : System.Web.UI.Page
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

            //CKEditor設定
            FileBrowser fileBrowser = new FileBrowser();
            fileBrowser.BasePath = "/ckfinder";
            fileBrowser.SetupCKEditor(cke_spec_content);


        }
        private void Show()
        {
            string yatch_id = Request.QueryString["yatch_id"];

            //1.連線資料庫(合在一起寫)
            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["YachtsConnectionString"].ConnectionString);

            //2.sql語法
            string sql = "SELECT * FROM specs WHERE yatch_id = @yatch_id";

            //3.創建command物件
            SqlCommand command = new SqlCommand(sql, connection);

            //3.5.參數化，Parameters:為了防止SQL Injection 程式碼攻擊，所以寫入SQL時，要使用參數「@參數名稱」來代替直接給的值，而給予參數的資料型態要使用Add
            command.Parameters.AddWithValue("@yatch_id", Convert.ToInt32(yatch_id));

            //4.資料庫連線開啟
            connection.Open();

            //5.執行sql (連線的作法-需自行關閉)
            SqlDataReader reader = command.ExecuteReader();
            //DataReader速度快只能逐筆單向有上往下而且不能計算，適合用來抓單筆資料
            //控制器資料來源
            if (reader.Read())
            {
                lbl_spec_content.Text = reader["spec_content"].ToString();
                cke_spec_content.Text = reader["spec_content"].ToString();
            }

            //6.資料庫關閉
            connection.Close();
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

        protected void editSpecContent_Click(object sender, EventArgs e)
        {
            if(editSpecContent.Text == "開始編輯")
            {
                editSpecContent.Text = "確認送出";

                cancelSpecContent.Visible = true;

                lbl_spec_content.Visible = false;
                cke_spec_content.Visible = true;

            }
            else
            {
                editSpecContent.Text = "開始編輯";

                cancelSpecContent.Visible = false;

                lbl_spec_content.Visible = true;
                cke_spec_content.Visible = false;

                //SQL是否已經有資料，有資料=true，沒資料=false
                Boolean sameYatch_idInSQL = false;

                //以下1~6判斷SQL是否已經有資料
                string yatch_id = Request.QueryString["yatch_id"];

                //1.連線資料庫(合在一起寫)
                SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["YachtsConnectionString"].ConnectionString);

                //2.sql語法
                string sql = "SELECT * FROM specs ";

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
                    if(reader["yatch_id"].ToString() == yatch_id)
                    {
                        sameYatch_idInSQL = true;
                        break;
                    }
                }

                //6.資料庫關閉
                connection.Close();

                //資料庫已經有相同資料，執行Update
                if (sameYatch_idInSQL)
                {
                    string spec_content = cke_spec_content.Text;

                    //2.sql語法，VALUES先使用@參數來取代直接給值，以防SQL Injection 程式碼
                    //修改
                    string sqlUpd = "UPDATE specs SET spec_content=@spec_content WHERE yatch_id = @yatch_id ";

                    //3.創建command物件
                    SqlCommand commandUpd = new SqlCommand(sqlUpd, connection);

                    //4.參數化，Parameters:為了防止SQL Injection 程式碼攻擊，所以寫入SQL時，要使用參數「@參數名稱」來代替直接給的值，而給予參數的資料型態要使用Add
                    commandUpd.Parameters.AddWithValue("@spec_content", spec_content);
                    commandUpd.Parameters.AddWithValue("@yatch_id", Convert.ToInt32(yatch_id));

                    //5.資料庫連線開啟
                    connection.Open();

                    //6.執行sql (新增刪除修改)
                    commandUpd.ExecuteNonQuery(); //無回傳值

                    //7.資料庫關閉
                    connection.Close();

                    //畫面渲染
                    Show();
                }
                //資料庫尚未有資料，執行Insert
                else
                {
                    string spec_content = cke_spec_content.Text;

                    //2.sql語法，VALUES先使用@參數來取代直接給值，以防SQL Injection 程式碼
                    //新增
                    string sqlIns = "INSERT INTO specs (spec_content,yatch_id) VALUES(@spec_content, @yatch_id)";


                    //3.創建command物件
                    SqlCommand commandIns = new SqlCommand(sqlIns, connection);

                    //4.參數化，Parameters:為了防止SQL Injection 程式碼攻擊，所以寫入SQL時，要使用參數「@參數名稱」來代替直接給的值，而給予參數的資料型態要使用Add
                    commandIns.Parameters.AddWithValue("@spec_content", spec_content);
                    commandIns.Parameters.AddWithValue("@yatch_id", Convert.ToInt32(yatch_id));

                    //5.資料庫連線開啟
                    connection.Open();

                    //6.執行sql (新增刪除修改)
                    commandIns.ExecuteNonQuery(); //無回傳值

                    //7.資料庫關閉
                    connection.Close();

                    //畫面渲染
                    Show();
                }
            }
        }

        protected void cancelSpecContent_Click(object sender, EventArgs e)
        {
            editSpecContent.Text = "開始編輯";

            cancelSpecContent.Visible = false;

            lbl_spec_content.Visible = true;
            cke_spec_content.Visible = false;
        }
        protected void lbtn_goYatchs_Click(object sender, EventArgs e)
        {
            Response.Redirect("yatchs.aspx");
        }
    }
}
