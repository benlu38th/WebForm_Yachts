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
    public partial class Yatchs_Overview : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                string yatch_id = Request.QueryString["yatch_id"];
                string yatch_name = Request.QueryString["yatch_name"];
                if (string.IsNullOrEmpty(yatch_id))
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
                    if (reader.Read())
                    {
                        yatch_id = reader["yatch_id"].ToString();
                        yatch_name = reader["yatch_name"].ToString();
                    }

                    //6.資料庫關閉
                    connection.Close();

                    Response.Redirect("Yachts_Overview.aspx?yatch_id=" + yatch_id + "&yatch_name=" + yatch_name);
                }

                ShowLeftListAndCrumb();
                ShowRepeaterPhotos();
                ShowContentAndDimeAndDimePhoto();
                ShowOverviewsDownload();

            }
        }
        private void ShowLeftListAndCrumb()
        {
            string yatch_name = Request.QueryString["yatch_name"];

            //ShowLeftList
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
            RepeaterLeftList.DataSource = reader; //(拿資料)
                                                  //控制器綁定 (真的把資料放進去)
            RepeaterLeftList.DataBind();

            //6.資料庫關閉
            connection.Close();

            //Show Crumb
            little_yatch_name.Text = yatch_name;
            big_yatch_name.Text = yatch_name;
        }
        private void ShowRepeaterPhotos()
        {
            string yatch_id = Request.QueryString["yatch_id"];

            //1.連線資料庫(合在一起寫)
            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["YachtsConnectionString"].ConnectionString);

            //2.sql語法
            string sql = "SELECT * FROM yatchPhotos WHERE yatch_id = @yatch_id";

            //3.創建command物件
            SqlCommand command = new SqlCommand(sql, connection);

            //3.5.參數化，Parameters:為了防止SQL Injection 程式碼攻擊，所以寫入SQL時，要使用參數「@參數名稱」來代替直接給的值，而給予參數的資料型態要使用Add
            command.Parameters.AddWithValue("@yatch_id", Convert.ToInt32(yatch_id));

            //4.資料庫連線開啟
            connection.Open();

            //5.執行sql (連線的作法-需自行關閉)
            SqlDataReader reader = command.ExecuteReader();
            //DataReader速度快只能逐筆單向有上往下而且不能計算，適合用來抓單筆資料

            RepeaterPhotos.DataSource = reader;
            RepeaterPhotos.DataBind();

            //6.資料庫關閉
            connection.Close();
        }
        private void ShowContentAndDimeAndDimePhoto()
        {
            string yatch_id = Request.QueryString["yatch_id"];

            //1.連線資料庫(合在一起寫)
            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["YachtsConnectionString"].ConnectionString);

            //2.sql語法
            string sql = "SELECT * FROM overviews WHERE yatch_id = @yatch_id";

            //3.創建command物件
            SqlCommand command = new SqlCommand(sql, connection);

            //3.5.參數化，Parameters:為了防止SQL Injection 程式碼攻擊，所以寫入SQL時，要使用參數「@參數名稱」來代替直接給的值，而給予參數的資料型態要使用Add
            command.Parameters.AddWithValue("@yatch_id", Convert.ToInt32(yatch_id));

            //4.資料庫連線開啟
            connection.Open();

            //5.執行sql (連線的作法-需自行關閉)
            SqlDataReader reader = command.ExecuteReader();
            //DataReader速度快只能逐筆單向有上往下而且不能計算，適合用來抓單筆資料

            if (reader.Read())
            {
                lit_overview_content.Text = reader["overview_content"].ToString();
                lit_overview_dime.Text = reader["overview_dime"].ToString();
                img_overview_dimePhoto.ImageUrl = reader["overview_dimePhoto"].ToString();
            }

            //6.資料庫關閉
            connection.Close();
        }
        private void ShowOverviewsDownload()
        {
            string yatch_id = Request.QueryString["yatch_id"];

            //1.連線資料庫(合在一起寫)
            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["YachtsConnectionString"].ConnectionString);

            //2.sql語法
            string sql = "SELECT * FROM overviewsDL WHERE yatch_id = @yatch_id";

            //3.創建command物件
            SqlCommand command = new SqlCommand(sql, connection);

            //3.5.參數化，Parameters:為了防止SQL Injection 程式碼攻擊，所以寫入SQL時，要使用參數「@參數名稱」來代替直接給的值，而給予參數的資料型態要使用Add
            command.Parameters.AddWithValue("@yatch_id", Convert.ToInt32(yatch_id));

            //4.資料庫連線開啟
            connection.Open();

            //5.執行sql (連線的作法-需自行關閉)
            SqlDataReader reader = command.ExecuteReader();
            //DataReader速度快只能逐筆單向有上往下而且不能計算，適合用來抓單筆資料

            RepeaterOverviewsDL.DataSource = reader;
            RepeaterOverviewsDL.DataBind();

            //6.資料庫關閉
            connection.Close();
        }

        private static string GetFileName(string a)
        {
            string ans = a;
            //取得 ~/fileUpload/ 以後
            if (a.IndexOf("~/overviewsUpload/") >= 0)
            {
                ans = a.Substring(a.IndexOf("~/overviewsUpload/") + 18);
            }
            return ans;
        }
        private static string GetHyperLinkName(string a)
        {
            string ans = a;
            //取得 ~/ 以後
            if (a.IndexOf("~/") >= 0)
            {
                ans = a.Substring(a.IndexOf("~/") + 2);
            }
            return ans;
        }
        protected void lbtn_yatch_name_Click(object sender, EventArgs e)
        {
            LinkButton linkButton = (LinkButton)sender;
            RepeaterItem repeaterItem = (RepeaterItem)linkButton.NamingContainer;
            Literal lit_yatch_name = (Literal)repeaterItem.FindControl("lit_yatch_name");
            Literal lit_yatch_id = (Literal)repeaterItem.FindControl("lit_yatch_id");

            string yatch_id = lit_yatch_id.Text;
            string yatch_name = lit_yatch_name.Text;

            Response.Redirect("Yachts_Overview.aspx?yatch_id=" + yatch_id + "&yatch_name=" + yatch_name);

        }

        protected void lbtn_overview_Click(object sender, EventArgs e)
        {
            string yatch_id = Request.QueryString["yatch_id"];
            string yatch_name = Request.QueryString["yatch_name"];
            Response.Redirect("Yachts_Overview.aspx?yatch_id=" + yatch_id + "&yatch_name=" + yatch_name);
        }

        protected void lbtn_layout_Click(object sender, EventArgs e)
        {
            string yatch_id = Request.QueryString["yatch_id"];
            string yatch_name = Request.QueryString["yatch_name"];
            Response.Redirect("Yachts_Layout.aspx?yatch_id=" + yatch_id + "&yatch_name=" + yatch_name);
        }

        protected void lbtn_spec_Click(object sender, EventArgs e)
        {
            string yatch_id = Request.QueryString["yatch_id"];
            string yatch_name = Request.QueryString["yatch_name"];
            Response.Redirect("Yachts_Specification.aspx?yatch_id=" + yatch_id + "&yatch_name=" + yatch_name);
        }

        protected void RepeaterOverviewsDL_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            //使用Reader時使用
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                // 获取当前数据行
                IDataRecord dataItem = (IDataRecord)e.Item.DataItem;

                // 查找 Label/LinkButton 控件
                //Label Label1 = (Label)e.Item.FindControl("Label1");
                //Literal lit_dealer_address = (Literal)e.Item.FindControl("lit_dealer_address");
                LinkButton lbtn_overviewsDL_downloadPath = (LinkButton)e.Item.FindControl("lbtn_overviewsDL_downloadPath");

                // 设置 Label/LinkButton 的文本值
                //Label1.Text = dataItem["newsDL_path"].ToString().Replace("/", "");
                //lit_dealer_address.Text = GetShortAddress(dataItem["dealer_address"].ToString());
                lbtn_overviewsDL_downloadPath.Text = GetFileName(dataItem["overviewsDL_downloadPath"].ToString());
            }
        }

        protected void lbtn_overviewsDL_downloadPath_Click(object sender, EventArgs e)
        {
            // 取得點擊的 LinkButton
            LinkButton lbtn_overviewsDL_downloadPath = (LinkButton)sender;

            string filePath = lbtn_overviewsDL_downloadPath.CommandArgument;

            // 设置下载文件的名称
            string fileName = Path.GetFileName(filePath);

            // 设置响应头信息
            Response.Clear();
            Response.ContentType = "application/octet-stream";
            Response.AddHeader("Content-Disposition", "attachment; filename=" + fileName);
            Response.WriteFile(filePath);
            Response.End();
        }

        protected void RepeaterPhotos_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            //使用Reader時使用
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                // 获取当前数据行
                IDataRecord dataItem = (IDataRecord)e.Item.DataItem;

                // 查找 Label/LinkButton 控件
                //Label Label1 = (Label)e.Item.FindControl("Label1");
                //Literal lit_dealer_address = (Literal)e.Item.FindControl("lit_dealer_address");
                HyperLink link_yatchPhoto_photoPath = (HyperLink)e.Item.FindControl("link_yatchPhoto_photoPath");

                // 设置 Label/LinkButton 的文本值
                //Label1.Text = dataItem["newsDL_path"].ToString().Replace("/", "");
                //lit_dealer_address.Text = GetShortAddress(dataItem["dealer_address"].ToString());
                link_yatchPhoto_photoPath.NavigateUrl = GetHyperLinkName (dataItem["yatchPhoto_photoPath"].ToString());
            }
        }
    }
}