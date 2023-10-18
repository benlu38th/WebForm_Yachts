using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Yachts
{
    public partial class dealers : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string country_id = Request.QueryString["country_id"];
            string country_name = Request.QueryString["country_name"];
            string nowPage = Request.QueryString["nowPage"];
            if (country_id == null || country_name == null || nowPage == null || !IsNumber(nowPage) || Convert.ToInt32(nowPage) > 3)
            {
                Response.Redirect("Dealers.aspx?country_id=2&country_name=USA&nowPage=1");
            }
            else if (country_id == null || country_name == null)
            {
                Response.Redirect("Dealers.aspx?country_id=2&country_name=USA&nowPage=" + nowPage);
            }
            else
            {
                lit_country.Text = country_name;
                lit_country2.Text = country_name;
            }

            if (!IsPostBack)
            {
                Show();
                Show2();
            }
        }
        private void Show()
        {

            //1.連線資料庫(合在一起寫)
            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["YachtsConnectionString"].ConnectionString);

            //2.sql語法
            string sql = "SELECT * FROM countries ";

            //3.創建command物件
            SqlCommand command = new SqlCommand(sql, connection);

            //4.資料庫連線開啟
            connection.Open();

            //5.執行sql (連線的作法-需自行關閉)
            SqlDataReader reader = command.ExecuteReader();
            //DataReader速度快只能逐筆單向有上往下而且不能計算，適合用來抓單筆資料
            //控制器資料來源

            // 繫結到Repeater的數據源
            Repeater1.DataSource = reader;
            Repeater1.DataBind();

            //6.資料庫關閉
            connection.Close();
        }
        private void Show2()
        {

            //現在在第幾頁
            string nowPage = Request.QueryString["nowPage"];
            //一頁幾個
            int itemsInAPage = 2;//可編輯
            //開頭項目編號
            int itemStart = (Convert.ToInt32(nowPage) - 1) * itemsInAPage + 1;
            //結束項目編號
            int itemEnd = Convert.ToInt32(nowPage) * itemsInAPage;

            string country_id = Request.QueryString["country_id"];

            //1.連線資料庫(合在一起寫)
            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["YachtsConnectionString"].ConnectionString);

            //2.sql語法
            string sql = $"WITH dealersCTE as (SELECT ROW_NUMBER() OVER (ORDER BY dealer_id DESC) AS rowindex, *  FROM dealers WHERE area_id IN ( SELECT area_id FROM areas WHERE country_id = (SELECT country_id FROM countries WHERE country_id = {country_id})))SELECT* FROM  dealersCTE WHERE rowindex >= {itemStart} AND rowindex <={itemEnd}; ";

            //3.創建command物件
            SqlCommand command = new SqlCommand(sql, connection);

            //4.創建適配器 (適配器會自己開關)-非連線作法
            SqlDataAdapter adapter = new SqlDataAdapter(command);

            //5.創建一張表 (空的) 
            DataTable table = new DataTable();
            // DataTable缺點：記憶體佔用大，如果一口氣塞很大筆會掛，可再做運算(購物車應用)					

            //6.數據填充表(已自動開關)
            adapter.Fill(table);

            //拿資料到畫面上
            //控制器資料來源
            Repeater3.DataSource = table;

            //控制器綁定
            Repeater3.DataBind();


            //以下獲取總列數
            string countSql = $"SELECT COUNT(*) FROM dealers WHERE area_id IN (SELECT area_id FROM areas WHERE country_id = {country_id})";

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
            Repeater2.DataSource = dataSource;
            Repeater2.DataBind();
        }
        protected void lbtn_chose_Click(object sender, EventArgs e)
        {
            string country_name = "";

            //取得country_id
            LinkButton lbtn = (LinkButton)sender;
            string country_id = lbtn.CommandArgument;

            //1.連線資料庫(合在一起寫)
            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["YachtsConnectionString"].ConnectionString);

            //2.sql語法
            string sql = "SELECT * FROM countries WHERE country_id = @country_id";

            //3.創建command物件
            SqlCommand command = new SqlCommand(sql, connection);

            //3.5.參數化，Parameters:為了防止SQL Injection 程式碼攻擊，所以寫入SQL時，要使用參數「@參數名稱」來代替直接給的值，而給予參數的資料型態要使用Add
            command.Parameters.AddWithValue("@country_id", Convert.ToInt32(country_id));

            //4.資料庫連線開啟
            connection.Open();

            //5.執行sql (連線的作法-需自行關閉)
            SqlDataReader reader = command.ExecuteReader();
            //DataReader速度快只能逐筆單向有上往下而且不能計算，適合用來抓單筆資料
            //控制器資料來源

            if (reader.Read())
            {
                country_id = reader["country_id"].ToString();
                country_name = reader["country_name"].ToString();
            }

            //6.資料庫關閉
            connection.Close();

            if (country_name != "")
            {
                Response.Redirect("Dealers.aspx?country_id=" + country_id + "&country_name=" + country_name + "&nowPage=1");
            }
            else
            {
                Response.Redirect("Dealers.aspx?country_id=2&country_name=USA&nowPage=1");
            }
        }

        protected void Repeater3_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                // 获取当前数据行
                DataRowView dataItem = (DataRowView)e.Item.DataItem;

                // 尋找 Literal 控制項
                Literal lit_dealer_address = (Literal)e.Item.FindControl("lit_dealer_address");

                // 設定 Literal 的文字值
                lit_dealer_address.Text = GetShortAddress(dataItem["dealer_address"].ToString());
            }
        }
        private static string GetShortAddress(string a)
        {
            string ans = a;
            //取得 ~/fileUpload/ 以後
            if (a.Length >= 60)
            {
                ans = a.Insert(60, "<br>");
            }
            return ans;
        }
        // 用正規式判斷是否為數字
        bool IsNumber(string inputData)
        {
            //條件開頭是數字，結尾是數字，數字1個或以上，即回傳true
            return System.Text.RegularExpressions.Regex.IsMatch(inputData, "^[0-9]+$");
        }

        protected void Repeater2_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                string nowPage = Request.QueryString["nowPage"];

                // 尋找 LinkButton 控制項
                LinkButton linkButton1 = (LinkButton)e.Item.FindControl("LinkButton1");

                if (nowPage == linkButton1.Text)
                {
                    linkButton1.Style["color"] = "#009DB3";
                }
            }
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            string country_id = Request.QueryString["country_id"];
            string country_name = Request.QueryString["country_name"];
            LinkButton linkButton = (LinkButton)sender;
            string nowPage = linkButton.Text;

            Response.Redirect("Dealers.aspx?country_id=" + country_id + "&country_name=" + country_name + "&nowPage=" + nowPage);
        }

        protected void lbtn_firstPage_Click(object sender, EventArgs e)
        {
            string country_id = Request.QueryString["country_id"];
            string country_name = Request.QueryString["country_name"];
            Response.Redirect("Dealers.aspx?country_id=" + country_id + "&country_name=" + country_name + "&nowPage=1");

        }

        protected void lbtn_lastPage_Click(object sender, EventArgs e)
        {
            string country_id = Request.QueryString["country_id"];
            string country_name = Request.QueryString["country_name"];

            //以下獲取總列數
            //1.連線資料庫(合在一起寫)
            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["YachtsConnectionString"].ConnectionString);

            string countSql = $"SELECT COUNT(*) FROM dealers WHERE area_id IN (SELECT area_id FROM areas WHERE country_id = {country_id})";

            // 创建用于获取总行数的命令对象
            SqlCommand countCommand = new SqlCommand(countSql, connection);

            // 打开数据库连接
            connection.Open();

            // 执行查询，获取总数
            int totalRows = (int)countCommand.ExecuteScalar();

            // 关闭数据库连接
            connection.Close();
            //以上獲取總列數

            //一頁幾個
            int itemsInAPage = 2;//可編輯

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

            Response.Redirect("Dealers.aspx?country_id=" + country_id + "&country_name=" + country_name + "&nowPage=" + totalPage);
        }

    }
}