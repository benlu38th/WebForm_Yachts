using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Yachts
{
    public partial class company : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string title_name = Request.QueryString["title_name"];
                if(title_name == null)
                {
                    Response.Redirect("Company.aspx?title_name="+ "About Us");
                }
                Show();
            }
        }
        private void Show()
        {
            string title_name = Request.QueryString["title_name"];

            //1.連線資料庫(合在一起寫)
            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["YachtsConnectionString"].ConnectionString);

            //2.sql語法
            string sql = "SELECT * FROM companyTitles Where title_name = @title_name";

            //3.創建command物件
            SqlCommand command = new SqlCommand(sql, connection);

            //3.5.參數化，Parameters:為了防止SQL Injection 程式碼攻擊，所以寫入SQL時，要使用參數「@參數名稱」來代替直接給的值，而給予參數的資料型態要使用Add
            command.Parameters.AddWithValue("@title_name", title_name);

            //4.資料庫連線開啟
            connection.Open();

            //5.執行sql (連線的作法-需自行關閉)
            SqlDataReader reader = command.ExecuteReader();
            //DataReader速度快只能逐筆單向有上往下而且不能計算，適合用來抓單筆資料
            //控制器資料來源
            if (reader.Read())
            {
                title_des.Text = reader["title_des"].ToString();
            }

            //6.資料庫關閉
            connection.Close();
        }

        protected void lbtn_aboutus_Click(object sender, EventArgs e)
        {
            Response.Redirect("company.aspx?title_name=" + "About Us");
        }

        protected void lbtn_certificat_Click(object sender, EventArgs e)
        {
            Response.Redirect("company.aspx?title_name=" + "Certificat");
        }
    }
}