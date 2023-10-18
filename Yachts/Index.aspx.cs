using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Yachts
{
    public partial class index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowFirstLittlePhoto();
                ShowLittlePhoto();
                ShowFirstBigPhoto();
                ShowBigPhoto();
                ShowRepeaterNews();
            }

        }
        private void ShowFirstLittlePhoto()
        {
            //1.連線資料庫(合在一起寫)
            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["YachtsConnectionString"].ConnectionString);

            //2.sql語法
            string sql = "WITH yatchsCTE AS ( SELECT ROW_NUMBER() OVER (ORDER BY yatch_modelName ASC, yatch_num ASC) AS rowindex, * FROM yatchs) SELECT* FROM yatchsCTE WHERE rowindex = 1 ";

            //3.創建command物件
            SqlCommand command = new SqlCommand(sql, connection);

            //4.資料庫連線開啟
            connection.Open();

            //5.執行sql (連線的作法-需自行關閉)
            SqlDataReader reader = command.ExecuteReader();
            //DataReader速度快只能逐筆單向有上往下而且不能計算，適合用來抓單筆資料
            //控制器資料來源

            RepeaterFirstLittlePhoto.DataSource = reader;
            RepeaterFirstLittlePhoto.DataBind();

            //6.資料庫關閉
            connection.Close();
        }
        private void ShowLittlePhoto()
        {
            //1.連線資料庫(合在一起寫)
            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["YachtsConnectionString"].ConnectionString);

            //2.sql語法
            string sql = "WITH yatchsCTE AS ( SELECT ROW_NUMBER() OVER (ORDER BY yatch_modelName ASC, yatch_num ASC) AS rowindex, * FROM yatchs) SELECT* FROM yatchsCTE WHERE rowindex > 1 ";

            //3.創建command物件
            SqlCommand command = new SqlCommand(sql, connection);

            //4.資料庫連線開啟
            connection.Open();

            //5.執行sql (連線的作法-需自行關閉)
            SqlDataReader reader = command.ExecuteReader();
            //DataReader速度快只能逐筆單向有上往下而且不能計算，適合用來抓單筆資料
            //控制器資料來源
            
            RepeaterLittlePhoto.DataSource = reader;
            RepeaterLittlePhoto.DataBind();

            //6.資料庫關閉
            connection.Close();
        }
        private void ShowFirstBigPhoto()
        {
            //1.連線資料庫(合在一起寫)
            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["YachtsConnectionString"].ConnectionString);

            //2.sql語法
            string sql = "WITH yatchsCTE AS ( SELECT ROW_NUMBER() OVER (ORDER BY yatch_modelName ASC, yatch_num ASC) AS rowindex, * FROM yatchs) SELECT* FROM yatchsCTE WHERE rowindex = 1 ";

            //3.創建command物件
            SqlCommand command = new SqlCommand(sql, connection);

            //4.資料庫連線開啟
            connection.Open();

            //5.執行sql (連線的作法-需自行關閉)
            SqlDataReader reader = command.ExecuteReader();
            //DataReader速度快只能逐筆單向有上往下而且不能計算，適合用來抓單筆資料
            //控制器資料來源

            RepeaterFirsrBigPhoto.DataSource = reader;
            RepeaterFirsrBigPhoto.DataBind();

            //6.資料庫關閉
            connection.Close();
        }
        private void ShowBigPhoto()
        {
            //1.連線資料庫(合在一起寫)
            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["YachtsConnectionString"].ConnectionString);

            //2.sql語法
            string sql = "WITH yatchsCTE AS ( SELECT ROW_NUMBER() OVER (ORDER BY yatch_modelName ASC, yatch_num ASC) AS rowindex, * FROM yatchs) SELECT* FROM yatchsCTE WHERE rowindex > 1 ";

            //3.創建command物件
            SqlCommand command = new SqlCommand(sql, connection);

            //4.資料庫連線開啟
            connection.Open();

            //5.執行sql (連線的作法-需自行關閉)
            SqlDataReader reader = command.ExecuteReader();
            //DataReader速度快只能逐筆單向有上往下而且不能計算，適合用來抓單筆資料
            //控制器資料來源

            RepeaterBigPhoto.DataSource = reader;
            RepeaterBigPhoto.DataBind();

            //6.資料庫關閉
            connection.Close();
        }
        protected void RepeaterFirsrBigPhoto_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            //使用Reader時使用
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                // 获取当前数据行
                //IDataRecord dataItem = (IDataRecord)e.Item.DataItem;


                // 查找 Label/LinkButton 控件
                //Label Label1 = (Label)e.Item.FindControl("Label1");
                Literal lit_yatch_newModel = (Literal)e.Item.FindControl("lit_yatch_newModel");
                Image img_yatch_newModel = (Image)e.Item.FindControl("img_yatch_newModel");

                // 设置 Label/LinkButton 的文本值
                //Label1.Text = dataItem["newsDL_path"].ToString().Replace("/", "");
                //lit_dealer_address.Text = GetShortAddress(dataItem["dealer_address"].ToString());

                if (lit_yatch_newModel.Text == "True")
                {
                    img_yatch_newModel.Visible = true;
                }
            }
        }
        //如果newModel等於True，將照片Visible等於True
        protected void RepeaterBigPhoto_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            //使用Reader時使用
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                // 获取当前数据行
                IDataRecord dataItem = (IDataRecord)e.Item.DataItem;


                // 查找 Label/LinkButton 控件
                //Label Label1 = (Label)e.Item.FindControl("Label1");
                Literal lit_yatch_newModel = (Literal)e.Item.FindControl("lit_yatch_newModel");
                Image img_yatch_newModel = (Image)e.Item.FindControl("img_yatch_newModel");

                // 设置 Label/LinkButton 的文本值
                //Label1.Text = dataItem["newsDL_path"].ToString().Replace("/", "");
                //lit_dealer_address.Text = GetShortAddress(dataItem["dealer_address"].ToString());

                if (lit_yatch_newModel.Text == "True")
                {
                    img_yatch_newModel.Visible = true;
                }
            }
        }
        private void ShowRepeaterNews()
        {
            //1.連線資料庫(合在一起寫)
            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["YachtsConnectionString"].ConnectionString);

            //2.sql語法
            string sql = "WITH newsCTE AS ( SELECT ROW_NUMBER() OVER (ORDER BY news_top DESC, news_launchDate DESC) AS rowindex, * FROM news) SELECT * FROM newsCTE WHERE rowindex >= 1 AND rowindex <= 3";

            //3.創建command物件
            SqlCommand command = new SqlCommand(sql, connection);

            //4.資料庫連線開啟
            connection.Open();

            //5.執行sql (連線的作法-需自行關閉)
            SqlDataReader reader = command.ExecuteReader();
            //DataReader速度快只能逐筆單向有上往下而且不能計算，適合用來抓單筆資料
            //控制器資料來源

            RepeaterNews.DataSource = reader;
            RepeaterNews.DataBind();

            //6.資料庫關閉
            connection.Close();
        }

        protected void lbtn_goToNews_Click(object sender, EventArgs e)
        {
            LinkButton linkButton = (LinkButton)sender;
            RepeaterItem repeaterItem = (RepeaterItem)linkButton.NamingContainer;
            Literal litNewsId = (Literal)repeaterItem.FindControl("lit_news_id");
            string news_id = litNewsId.Text;

            Response.Redirect("NewsContent.aspx?news_id=" + news_id);
        }

        protected void img_little_yatch_coverPhoto_Click(object sender, ImageClickEventArgs e)
        {
            ImageButton imageButton = (ImageButton)sender;
            RepeaterItem repeaterItem = (RepeaterItem)imageButton.NamingContainer;
            Literal litYatchId = (Literal)repeaterItem.FindControl("lit_first_little_yatch_id");
            string yatch_id = litYatchId.Text;
            string yatch_name = "";

            //1.連線資料庫(合在一起寫)
            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["YachtsConnectionString"].ConnectionString);

            //2.sql語法
            string sql = " SELECT * FROM yatchs WHERE yatch_id = @yatch_id";

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
                yatch_name = reader["yatch_name"].ToString();
            }

            //6.資料庫關閉
            connection.Close();

            Response.Redirect("Yachts_Overview.aspx?yatch_id=" + yatch_id + "&yatch_name="+ yatch_name);
        }
    }
}