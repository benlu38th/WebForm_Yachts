using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Yachts.pages
{
    public partial class B_dealers_list : System.Web.UI.Page
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            if (!HttpContext.Current.User.Identity.IsAuthenticated)
            {
                Response.Redirect("login.aspx");
            }
            //目前網頁[nowWebPageRight_id}手動輸入，每頁不同
            string nowWebPageRight_id = "1";
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

			//取得該列的主索引欄位的值(須在gridview 加入 DataKeyNames = "id")
			GridViewRow row = GridView1.Rows[index];
			TextBox txt_country_name = (TextBox)row.FindControl("txt_country_name");
			string keyId = GridView1.DataKeys[row.RowIndex]["country_id"].ToString();

			//1.連線資料庫
			SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["YachtsConnectionString"].ConnectionString);

			//2.sql語法，VALUES先使用@參數來取代直接給值，以防SQL Injection 程式碼
			//修改
			string sql = "UPDATE countries SET country_name=@country_name  WHERE country_id = @country_id ";


			//3.創建command物件
			SqlCommand command = new SqlCommand(sql, connection);

			//4.參數化，Parameters:為了防止SQL Injection 程式碼攻擊，所以寫入SQL時，要使用參數「@參數名稱」來代替直接給的值，而給予參數的資料型態要使用Add
			command.Parameters.AddWithValue("@country_name", txt_country_name.Text);
			command.Parameters.AddWithValue("@country_id", Convert.ToInt32(keyId));

			//5.資料庫連線開啟
			connection.Open();

			//6.執行sql (新增刪除修改)
			command.ExecuteNonQuery(); //無回傳值

			//7.資料庫關閉
			connection.Close();

			// 取消編輯模式
			GridView1.EditIndex = -1;

			//畫面渲染
			Show();
		}

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
			//取得被點擊的 GridView 的資料列索引
			int rowIndex = e.RowIndex;

			//取得 GridView 控制項中指定索引的資料列
			GridViewRow row = GridView1.Rows[rowIndex];

			//取得該列的主索引欄位的值(須在gridview 加入 DataKeyNames = "id")
			string keyId = GridView1.DataKeys[row.RowIndex]["country_id"].ToString();

			//1.連線資料庫
			SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["YachtsConnectionString"].ConnectionString);

			//2.sql語法，VALUES先使用@參數來取代直接給值，以防SQL Injection 程式碼
			//修改
			string sql = "DELETE FROM countries  WHERE country_id = @country_id ";


			//3.創建command物件
			SqlCommand command = new SqlCommand(sql, connection);

			//4.參數化，Parameters:為了防止SQL Injection 程式碼攻擊，所以寫入SQL時，要使用參數「@參數名稱」來代替直接給的值，而給予參數的資料型態要使用Add
			command.Parameters.AddWithValue("@country_id", Convert.ToInt32(keyId));

			//5.資料庫連線開啟
			connection.Open();

			//6.執行sql (新增刪除修改)
			command.ExecuteNonQuery(); //無回傳值

			//7.資料庫關閉
			connection.Close();

			//畫面渲染
			Show();

		}
		protected void Button1_Click(object sender, EventArgs e)
		{
			string country = txt_input_country.Text;

			if (string.IsNullOrEmpty(country))
			{
				// 輸出JavaScript警告對話框
				string message = "請填寫國家欄位!";
				string encodedMessage = HttpUtility.HtmlEncode(message);
				Response.Write("<script>alert('" + encodedMessage + "');</script>");
			}
			else
			{
				//1.連線資料庫
				SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["YachtsConnectionString"].ConnectionString);

				//2.sql語法，VALUES先使用@參數來取代直接給值，以防SQL Injection 程式碼
				//新增
				string sql = "INSERT INTO countries(country_name) VALUES(@country_name)";


				//3.創建command物件
				SqlCommand command = new SqlCommand(sql, connection);

				//4.參數化，Parameters:為了防止SQL Injection 程式碼攻擊，所以寫入SQL時，要使用參數「@參數名稱」來代替直接給的值，而給予參數的資料型態要使用Add
				command.Parameters.AddWithValue("@country_name", country);

				//5.資料庫連線開啟
				connection.Open();

				//6.執行sql (新增刪除修改)
				command.ExecuteNonQuery(); //無回傳值

				//7.資料庫關閉
				connection.Close();

				//畫面渲染
				Show();
			}
		}
		protected void lbtn_areaManagement_Click(object sender, EventArgs e)
		{
			// 取得點擊的 LinkButton
			LinkButton lb = (LinkButton)sender;

			// 取得所在的 GridViewRow
			GridViewRow row = (GridViewRow)lb.NamingContainer;

            //這一列的控制項必須是 TemplateField控制項 才能使用
            LinkButton lbtn_areaManagement = (LinkButton)row.FindControl("lbtn_areaManagement");

			//取得該列的主索引欄位的值(須在gridview 加入 DataKeyNames = "id")
			string keyId = GridView1.DataKeys[row.RowIndex]["country_id"].ToString();

			Response.Redirect("dealers_areas.aspx?co="+ lbtn_areaManagement.Text+"&co_id="+ keyId);
		}
		private void Show()
		{
			//1.連線資料庫(合在一起寫)
			SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["YachtsConnectionString"].ConnectionString);

			//2.sql語法
			string sql = "SELECT * FROM countries";

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

			txt_input_country.Text = "";
		}
        protected void lbtn_dealerManagement_Click(object sender, EventArgs e)
        {
            // 取得點擊的 LinkButton
            LinkButton lb = (LinkButton)sender;

            // 取得所在的 GridViewRow
            GridViewRow row = (GridViewRow)lb.NamingContainer;

            //取得該列的主索引欄位的值(須在gridview 加入 DataKeyNames = "id")
            string country_id = GridView1.DataKeys[row.RowIndex]["country_id"].ToString();

            //這一列的控制項必須是 TemplateField控制項 才能使用
            Label lbl_country_name = (Label)row.FindControl("lbl_country_name");

            Response.Redirect("dealers.aspx?co=" + lbl_country_name.Text + "&co_id=" + country_id);
        }
        protected void lbtn_goCountries_Click(object sender, EventArgs e)
        {
            Response.Redirect("dealers_countries.aspx");
        }
    }
}