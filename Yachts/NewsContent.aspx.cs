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

namespace Yachts
{
    public partial class News : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string news_id = Request.QueryString["news_id"];
            if (string.IsNullOrEmpty(news_id))
            {
                Response.Redirect("News.aspx");
            }
            if (!IsPostBack)
            {
                ShowNewsDL();
                ShowNewsContent();
            }
        }
        private void ShowNewsContent()
        {
            string news_id = Request.QueryString["news_id"];

            //1.連線資料庫(合在一起寫)
            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["YachtsConnectionString"].ConnectionString);

            //2.sql語法
            string sql = "SELECT * FROM news WHERE news_id = @news_id";

            //3.創建command物件
            SqlCommand command = new SqlCommand(sql, connection);

            //3.5.參數化，Parameters:為了防止SQL Injection 程式碼攻擊，所以寫入SQL時，要使用參數「@參數名稱」來代替直接給的值，而給予參數的資料型態要使用Add
            command.Parameters.AddWithValue("@news_id", Convert.ToInt32(news_id));

            //4.資料庫連線開啟
            connection.Open();

            //5.執行sql (連線的作法-需自行關閉)
            SqlDataReader reader = command.ExecuteReader();
            //DataReader速度快只能逐筆單向有上往下而且不能計算，適合用來抓單筆資料
            if (reader.Read())
            {
                lit_news_title.Text = reader["news_title"].ToString();
                lit_news_longDes.Text = reader["news_longDes"].ToString();
            }


            //6.資料庫關閉
            connection.Close();
        }
        private void ShowNewsDL()
        {
            string news_id = Request.QueryString["news_id"];

            //1.連線資料庫(合在一起寫)
            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["YachtsConnectionString"].ConnectionString);

            //2.sql語法
            string sql = "SELECT * FROM newsDL WHERE news_id = @news_id";

            //3.創建command物件
            SqlCommand command = new SqlCommand(sql, connection);

            //3.5.參數化，Parameters:為了防止SQL Injection 程式碼攻擊，所以寫入SQL時，要使用參數「@參數名稱」來代替直接給的值，而給予參數的資料型態要使用Add
            command.Parameters.AddWithValue("@news_id", Convert.ToInt32(news_id));

            //4.資料庫連線開啟
            connection.Open();

            //5.執行sql (連線的作法-需自行關閉)
            SqlDataReader reader = command.ExecuteReader();
            //DataReader速度快只能逐筆單向有上往下而且不能計算，適合用來抓單筆資料
            //控制器資料來源
            RepeaterNewsDL.DataSource = reader; //(拿資料)
                                                //控制器綁定 (真的把資料放進去)
            RepeaterNewsDL.DataBind();

            //6.資料庫關閉
            connection.Close();
        }

        protected void lbtn_newsDL_Click(object sender, EventArgs e)
        {
            LinkButton linkButton = (LinkButton)sender;
            RepeaterItem repeaterItem = (RepeaterItem)linkButton.NamingContainer;
            Literal litNewsId = (Literal)repeaterItem.FindControl("lit_newsDL");

            string downloadFile = litNewsId.Text;

            Response.ContentType = ContentType;
            Response.AppendHeader("Content-Disposition", $"attachment; filename= {downloadFile}");

            //下載較小的檔案時使用
            Response.WriteFile(downloadFile);

            //下載較大的檔案時使用
            //Response.TransmitFile(downloadFile);

            Response.End();
        }

        protected void RepeaterNewsDL_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            //使用Reader時使用
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                // 获取当前数据行
                IDataRecord dataItem = (IDataRecord)e.Item.DataItem;


                // 查找 Label/LinkButton 控件
                //Label Label1 = (Label)e.Item.FindControl("Label1");
                LinkButton lbtn_newsDL = (LinkButton)e.Item.FindControl("lbtn_newsDL");

                // 设置 Label/LinkButton 的文本值
                //Label1.Text = dataItem["newsDL_path"].ToString().Replace("/", "");
                lbtn_newsDL.Text = GetFileName(dataItem["newsDL_path"].ToString());
            }
        }
        private static string GetFileName(string a)
        {
            string ans = a;
            //取得 ~/newsUpload/ 以後
            if (a.IndexOf("~/newsUpload/") >= 0)
            {
                ans = a.Substring(a.IndexOf("~/newsUpload/") + 13);
            }
            return ans;
        }
    }
}