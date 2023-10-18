using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Yachts
{
    public partial class News1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ///以下獲取總列數
            //1.連線資料庫(合在一起寫)
            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["YachtsConnectionString"].ConnectionString);

            string countSql = $"SELECT COUNT(*) FROM news";

            // 创建用于获取总行数的命令对象
            SqlCommand countCommand = new SqlCommand(countSql, connection);

            // 打开数据库连接
            connection.Open();

            // 执行查询，获取总数
            int totalRows = (int)countCommand.ExecuteScalar();

            // 关闭数据库连接
            connection.Close();
            ///以上獲取總列數

            ///以下找出一頁幾個

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
            string itemsInAPageStr = "1";
            if (reader.Read())
            {
                itemsInAPageStr = reader[1].ToString();
            }
            int itemsInAPage = Convert.ToInt32(itemsInAPageStr);
            //6.資料庫關閉
            connection.Close();
            ///以上找出一頁幾個

            //總共幾頁
            int totalPage = 0;
            if (totalRows % itemsInAPage == 0)
            {
                totalPage = totalRows / itemsInAPage;
            }
            else
            {
                totalPage = totalRows / itemsInAPage + 1;
            }

            string nowPage = Request.QueryString["nowPage"];
            if (nowPage == null )
            {
                Response.Redirect("News.aspx?nowPage=1");
            }
            else if(Convert.ToInt32( totalPage )<Convert.ToInt32(nowPage))
            {
                Response.Redirect("News.aspx?nowPage=1");
            }
            if (!IsPostBack)
            {
                ShowPages();
            }
        }
        private void ShowPages() {

            //現在在第幾頁
            string nowPage = Request.QueryString["nowPage"];

            ///以下找出一頁幾個
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
            string itemsInAPageStr = "1";
            if (reader.Read())
            {
                itemsInAPageStr = reader[1].ToString();
            }
            int itemsInAPage = Convert.ToInt32(itemsInAPageStr);
            //6.資料庫關閉
            connection.Close();
            ///以上找出一頁幾個

            //開頭項目編號
            int itemStart = (Convert.ToInt32(nowPage) - 1) * itemsInAPage + 1;
            //結束項目編號
            int itemEnd = Convert.ToInt32(nowPage) * itemsInAPage;


            //2.sql語法
            string sqlShowPage = $"WITH newsCTE AS( SELECT ROW_NUMBER() OVER(ORDER BY  news_top DESC, news_launchDate DESC) AS rowindex, * FROM news ) SELECT* FROM  newsCTE WHERE rowindex >= {itemStart} AND rowindex <={itemEnd}";

            //3.創建command物件
            SqlCommand commandShowPage = new SqlCommand(sqlShowPage, connection);

            //4.創建適配器 (適配器會自己開關)-非連線作法
            SqlDataAdapter adapter = new SqlDataAdapter(commandShowPage);

            //5.創建一張表 (空的) 
            DataTable table = new DataTable();
            // DataTable缺點：記憶體佔用大，如果一口氣塞很大筆會掛，可再做運算(購物車應用)					

            //6.數據填充表(已自動開關)
            adapter.Fill(table);

            //拿資料到畫面上
            //控制器資料來源
            RepeaterNews.DataSource = table;

            //控制器綁定
            RepeaterNews.DataBind();


            //以下獲取總列數
            string countSql = $"SELECT COUNT(*) FROM news";

            // 创建用于获取总行数的命令对象
            SqlCommand countCommand = new SqlCommand(countSql, connection);

            // 打开数据库连接
            connection.Open();

            // 执行查询，获取总数
            int totalRows = (int)countCommand.ExecuteScalar();

            // 关闭数据库连接
            connection.Close();
            //以上獲取總列數

            //決定下方分頁鈕要有幾個
            //總共幾頁
            int totalPage = 0;
            if (totalRows % itemsInAPage == 0)
            {
                totalPage = totalRows / itemsInAPage;
            }
            else
            {
                totalPage = totalRows / itemsInAPage + 1;
            }
            //建立總頁數個長度的陣列dataSource
            int[] dataSource = new int[totalPage];
            //將dataSource加入Repeater2
            RepeaterPages.DataSource = dataSource;
            RepeaterPages.DataBind();
        }
        protected void lbtn_page_Click(object sender, EventArgs e)
        {
            LinkButton linkButton = (LinkButton)sender;
            string nowPage = linkButton.Text;

            Response.Redirect("News.aspx?nowPage=" + nowPage);
        }

        protected void lbtn_firstPage_Click(object sender, EventArgs e)
        {
            Response.Redirect("News.aspx?nowPage=1");
        }

        protected void lbtn_lastPage_Click(object sender, EventArgs e)
        {
            ///以下獲取總列數
            //1.連線資料庫(合在一起寫)
            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["YachtsConnectionString"].ConnectionString);

            string countSql = $"SELECT COUNT(*) FROM news";

            // 创建用于获取总行数的命令对象
            SqlCommand countCommand = new SqlCommand(countSql, connection);

            // 打开数据库连接
            connection.Open();

            // 执行查询，获取总数
            int totalRows = (int)countCommand.ExecuteScalar();

            // 关闭数据库连接
            connection.Close();
            ///以上獲取總列數

            ///以下找出一頁幾個

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
            string itemsInAPageStr = "1";
            if (reader.Read())
            {
                itemsInAPageStr = reader[1].ToString();
            }
            int itemsInAPage = Convert.ToInt32(itemsInAPageStr);
            //6.資料庫關閉
            connection.Close();
            ///以上找出一頁幾個

            //總共幾頁
            int totalPage = 0;
            if (totalRows % itemsInAPage == 0)
            {
                totalPage = totalRows / itemsInAPage;
            }
            else
            {
                totalPage = totalRows / itemsInAPage + 1;
            }
            Response.Redirect("News.aspx?nowPage=" + totalPage);
        }

        protected void RepeaterPages_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                string nowPage = Request.QueryString["nowPage"];

                // 尋找 LinkButton 控制項
                LinkButton lbtn_page = (LinkButton)e.Item.FindControl("lbtn_page");

                if (nowPage == lbtn_page.Text)
                {
                    lbtn_page.Style["color"] = "#009DB3";
                }
            }
        }
        protected void lbtn_news_title_Click(object sender, EventArgs e)
        {
            LinkButton linkButton = (LinkButton)sender;
            RepeaterItem repeaterItem = (RepeaterItem)linkButton.NamingContainer;
            Literal litNewsId = (Literal)repeaterItem.FindControl("lit_news_id");
            string news_id = litNewsId.Text;

            Response.Redirect("NewsContent.aspx?news_id=" + news_id);
        }
    }
}