using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Yachts.backend
{
    public partial class user_management : System.Web.UI.Page
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            if (!HttpContext.Current.User.Identity.IsAuthenticated)
            {
                Response.Redirect("login.aspx");
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string ticketUserID = ((FormsIdentity)(HttpContext.Current.User.Identity)).Ticket.Name;
                string ticketUserID_right = "0";

                string userViewed_id = Request.QueryString["user_id"];
                string userViewed_id_right = "0";

                //以下搜索ticketUserID及userViewed_id的層級
                //1.連線資料庫(合在一起寫)
                SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["YachtsConnectionString"].ConnectionString);

                //2.sql語法
                string sql = "SELECT * FROM users";

                //3.創建command物件
                SqlCommand command = new SqlCommand(sql, connection);

                //4.資料庫連線開啟
                connection.Open();

                //5.執行sql (連線的作法-需自行關閉)
                SqlDataReader reader = command.ExecuteReader();
                //DataReader速度快只能逐筆單向有上往下而且不能計算，適合用來抓單筆資料

                while (reader.Read())
                {
                    if (reader["user_id"].ToString() == ticketUserID)
                    {
                        ticketUserID_right = reader["user_rank"].ToString();
                    }
                    else if (reader["user_id"].ToString() == userViewed_id)
                    {
                        userViewed_id_right = reader["user_rank"].ToString();
                    }

                }

                //6.資料庫關閉
                connection.Close();
                //以上搜索ticketUserID及userViewed_id的層級

                //如果使用者層級小於等於被檢視者的層級，跳轉回使用者清單
                if (Convert.ToInt32(ticketUserID_right) <= Convert.ToInt32(userViewed_id_right))
                {
                    Response.Redirect("user_list.aspx");
                }
                else if (userViewed_id_right == "0")
                {
                    Response.Redirect("user_list.aspx");
                }
                Show();
                Show_chkls_right_name();
                ShowVorNot();
            }
        }
        private void Show()
        {
            string userViewed_id = Request.QueryString["user_id"];

            //1.連線資料庫(合在一起寫)
            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["YachtsConnectionString"].ConnectionString);

            //2.sql語法
            string sql = "SELECT * FROM users WHERE user_id = @userViewed_id";

            //3.創建command物件
            SqlCommand command = new SqlCommand(sql, connection);

            //3.5. 參數化，Parameters:為了防止SQL Injection 程式碼攻擊，所以寫入SQL時，要使用參數「@參數名稱」來代替直接給的值，而給予參數的資料型態要使用Add
            command.Parameters.AddWithValue("@userViewed_id", Convert.ToInt32(userViewed_id));

            //4.資料庫連線開啟
            connection.Open();

            //5.執行sql (連線的作法-需自行關閉)
            SqlDataReader reader = command.ExecuteReader();
            //DataReader速度快只能逐筆單向有上往下而且不能計算，適合用來抓單筆資料
            if (reader.Read())
            {
                lbl_user_account.Text = reader["user_account"].ToString();
                txt_user_account.Text = reader["user_account"].ToString();
                lbl_user_sex.Text = reader["user_sex"].ToString();
                txt_user_sex.Text = reader["user_sex"].ToString();
                lbl_user_mail.Text = reader["user_mail"].ToString();
                txt_user_mail.Text = reader["user_mail"].ToString();
                lbl_user_rank.Text = reader["user_rank"].ToString();
                txt_user_rank.Text = reader["user_rank"].ToString();
            }

            //6.資料庫關閉
            connection.Close();
        }

        protected void btn_goUserList_Click(object sender, EventArgs e)
        {
            Response.Redirect("user_list");
        }

        protected void btn_startEdit_Click(object sender, EventArgs e)
        {
            if (btn_startEdit.Text == "開始編輯")
            {
                btn_startEdit.Text = "確認送出";
                btn_cancelEdit.Visible = true;

                lbl_user_account.Visible = false;
                txt_user_account.Visible = true;
                lbl_user_sex.Visible = false;
                txt_user_sex.Visible = true;
                lbl_user_mail.Visible = false;
                txt_user_mail.Visible = true;
                lbl_user_rank.Visible = false;
                txt_user_rank.Visible = true;

                chkls_right_name.Enabled = true;
            }
            else
            {
                btn_startEdit.Text = "開始編輯";
                btn_cancelEdit.Visible = false;

                lbl_user_account.Visible = true;
                txt_user_account.Visible = false;
                lbl_user_sex.Visible = true;
                txt_user_sex.Visible = false;
                lbl_user_mail.Visible = true;
                txt_user_mail.Visible = false;
                lbl_user_rank.Visible = true;
                txt_user_rank.Visible = false;

                chkls_right_name.Enabled = false;

                string user_id = Request.QueryString["user_id"];

                string user_account = txt_user_account.Text;
                string user_sex = txt_user_sex.Text;
                string user_mail = txt_user_mail.Text;
                string user_rank = txt_user_rank.Text;

                if (user_account.Length <= 50)
                {
                    if (user_sex.Length <= 2)
                    {
                        if (user_mail.Length <= 50)
                        {
                            if (user_rank == "1" || user_rank == "2" || user_rank == "3" || user_rank == "4" || user_rank == "5")
                            {
                                //1.連線資料庫
                                SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["YachtsConnectionString"].ConnectionString);

                                //2.sql語法，VALUES先使用@參數來取代直接給值，以防SQL Injection 程式碼
                                //修改
                                string sql = "UPDATE users SET user_account=@user_account, user_sex=@user_sex, user_mail=@user_mail, user_rank=@user_rank WHERE user_id = @user_id ";

                                //3.創建command物件
                                SqlCommand command = new SqlCommand(sql, connection);

                                //4.參數化，Parameters:為了防止SQL Injection 程式碼攻擊，所以寫入SQL時，要使用參數「@參數名稱」來代替直接給的值，而給予參數的資料型態要使用Add
                                command.Parameters.AddWithValue("@user_account", user_account);
                                command.Parameters.AddWithValue("@user_sex", user_sex);
                                command.Parameters.AddWithValue("@user_mail", user_mail);
                                command.Parameters.AddWithValue("user_rank", user_rank);
                                command.Parameters.AddWithValue("user_id", Convert.ToInt32(user_id));

                                //5.資料庫連線開啟
                                connection.Open();

                                //6.執行sql (新增刪除修改)
                                command.ExecuteNonQuery(); //無回傳值

                                //7.資料庫關閉
                                connection.Close();

                                //以下獲取有幾列
                                string sqlSelectUserRightCount = "SELECT COUNT(*) FROM rightUserHas WHERE user_id = @user_id";
                                SqlCommand commandSelectUserRightCount = new SqlCommand(sqlSelectUserRightCount, connection);
                                commandSelectUserRightCount.Parameters.AddWithValue("@user_id", user_id);
                                connection.Open();
                                int totalCount = (int)commandSelectUserRightCount.ExecuteScalar();
                                connection.Close();
                                //以上獲取有幾列

                                //以下處理權限(增加)
                                foreach (ListItem item in chkls_right_name.Items)
                                {
                                    //有打勾
                                    if (item.Selected)
                                    {
                                        //2.sql語法
                                        string sqlSelectUserRight = "SELECT *  FROM rightUserHas WHERE user_id = @user_id";

                                        //3.創建command物件
                                        SqlCommand commandSelectUserRight = new SqlCommand(sqlSelectUserRight, connection);
                                        commandSelectUserRight.Parameters.AddWithValue("@user_id", user_id);

                                        //4.資料庫連線開啟
                                        connection.Open();

                                        //5.執行sql (連線的作法-需自行關閉)
                                        SqlDataReader reader = commandSelectUserRight.ExecuteReader();
                                        //DataReader速度快只能逐筆單向有上往下而且不能計算，適合用來抓單筆資料
                                        // 檢查 SqlDataReader 是否有資料列
                                        if (reader.HasRows)
                                        {
                                            int rowNumber = 0;
                                            while (reader.Read())
                                            {
                                                rowNumber++;

                                                string a = reader["right_id"].ToString();
                                                if (reader["right_id"].ToString() == item.Value)
                                                {
                                                    break;
                                                }

                                                if (rowNumber == totalCount)
                                                {
                                                    //6.資料庫關閉
                                                    connection.Close();

                                                    //4.資料庫連線開啟
                                                    connection.Open();

                                                    string sqlAddUserRight = "INSERT INTO rightUserHas (user_id, right_id) VALUES(@user_id, @right_id) ";

                                                    SqlCommand commandAddUserRight = new SqlCommand(sqlAddUserRight, connection);
                                                    commandAddUserRight.Parameters.AddWithValue("@user_id", Convert.ToInt32(user_id));
                                                    commandAddUserRight.Parameters.AddWithValue("@right_id", Convert.ToInt32(item.Value));

                                                    //6.執行sql (新增刪除修改)
                                                    commandAddUserRight.ExecuteNonQuery(); //無回傳值

                                                    totalCount++;

                                                    break;

                                                }
                                            }

                                        }
                                        else
                                        {
                                            //6.資料庫關閉
                                            connection.Close();

                                            //4.資料庫連線開啟
                                            connection.Open();

                                            string sqlAddUserRight = "INSERT INTO rightUserHas (user_id, right_id) VALUES(@user_id, @right_id) ";

                                            SqlCommand commandAddUserRight = new SqlCommand(sqlAddUserRight, connection);
                                            commandAddUserRight.Parameters.AddWithValue("@user_id", Convert.ToInt32(user_id));
                                            commandAddUserRight.Parameters.AddWithValue("@right_id", Convert.ToInt32(item.Value));

                                            //6.執行sql (新增刪除修改)
                                            commandAddUserRight.ExecuteNonQuery(); //無回傳值

                                            totalCount++;
                                        }

                                        //6.資料庫關閉
                                        connection.Close();
                                    }
                                }
                                //以上處理權限增加

                                //處理權限(刪除)
                                foreach (ListItem item in chkls_right_name.Items)
                                {
                                    //沒有打勾
                                    if (!item.Selected)
                                    {
                                        //2.sql語法
                                        string sqlSelectUserRight = "SELECT *  FROM rightUserHas WHERE user_id = @user_id";

                                        //3.創建command物件
                                        SqlCommand commandSelectUserRight = new SqlCommand(sqlSelectUserRight, connection);
                                        commandSelectUserRight.Parameters.AddWithValue("@user_id", user_id);

                                        //4.資料庫連線開啟
                                        connection.Open();

                                        //5.執行sql (連線的作法-需自行關閉)
                                        SqlDataReader reader = commandSelectUserRight.ExecuteReader();
                                        //DataReader速度快只能逐筆單向有上往下而且不能計算，適合用來抓單筆資料
                                        // 檢查 SqlDataReader 是否有資料列

                                        while (reader.Read())
                                        {
                                            if(reader["right_id"].ToString() == item.Value)
                                            {
                                                //6.資料庫關閉
                                                connection.Close();

                                                //4.資料庫連線開啟
                                                connection.Open();

                                                //刪除
                                                string sqlDeleteUserRight = "DELETE FROM rightUserHas WHERE user_id = @user_id AND right_id=@right_id; ";

                                                SqlCommand commandDeleteUserRight = new SqlCommand(sqlDeleteUserRight, connection);
                                                commandDeleteUserRight.Parameters.AddWithValue("@user_id", Convert.ToInt32(user_id));
                                                commandDeleteUserRight.Parameters.AddWithValue("@right_id", Convert.ToInt32(item.Value));

                                                //6.執行sql (新增刪除修改)
                                                commandDeleteUserRight.ExecuteNonQuery(); //無回傳值

                                                break;
                                            }
                                        }
                                        //6.資料庫關閉
                                        connection.Close();
                                    }
                                }



                                //畫面渲染
                                Show();
                                Show_chkls_right_name();
                                ShowVorNot();
                            }
                            else
                            {
                                // 輸出JavaScript警告對話框
                                string message = "權限請輸入1、2、3、4或5";
                                string encodedMessage = HttpUtility.HtmlEncode(message);
                                Response.Write("<script>alert('" + encodedMessage + "');</script>");
                                Show();
                            }
                        }
                        else
                        {
                            // 輸出JavaScript警告對話框
                            string message = "信箱長度物超過50個字元!";
                            string encodedMessage = HttpUtility.HtmlEncode(message);
                            Response.Write("<script>alert('" + encodedMessage + "');</script>");
                            Show();
                        }
                    }
                    else
                    {
                        // 輸出JavaScript警告對話框
                        string message = "性別長度物超過2個字元!";
                        string encodedMessage = HttpUtility.HtmlEncode(message);
                        Response.Write("<script>alert('" + encodedMessage + "');</script>");
                        Show();
                    }
                }
                else
                {
                    // 輸出JavaScript警告對話框
                    string message = "帳號長度物超過50個字元!";
                    string encodedMessage = HttpUtility.HtmlEncode(message);
                    Response.Write("<script>alert('" + encodedMessage + "');</script>");
                    Show();
                }
            }
        }

        protected void btn_cancelEdit_Click(object sender, EventArgs e)
        {
            btn_startEdit.Text = "開始編輯";
            btn_cancelEdit.Visible = false;

            lbl_user_account.Visible = true;
            txt_user_account.Visible = false;
            lbl_user_sex.Visible = true;
            txt_user_sex.Visible = false;
            lbl_user_mail.Visible = true;
            txt_user_mail.Visible = false;
            lbl_user_rank.Visible = true;
            txt_user_rank.Visible = false;

            chkls_right_name.Enabled = false;

            Show_chkls_right_name();

        }
        private void Show_chkls_right_name()
        {
            //string chkls_right_name_value;
            //string chkls_right_name_text;

            // 連線資料庫
            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["YachtsConnectionString"].ConnectionString);

            // SQL 語法
            string sql = "SELECT * FROM rights";

            // 創建 SqlCommand 物件
            SqlCommand command = new SqlCommand(sql, connection);

            // 資料庫連線開啟
            connection.Open();

            // 執行 SQL 查詢，取得資料
            SqlDataReader reader = command.ExecuteReader();

            //先清除既有選項，不然選項會越加越多
            chkls_right_name.Items.Clear();

            // 將資料加入 DropDownList
            while (reader.Read())
            {
                // 假設 DropDownList 控制項名稱為 "ddl_country_name
                chkls_right_name.Items.Add(new ListItem(reader["right_name"].ToString(), reader["right_id"].ToString()));
            }

            // 資料庫關閉
            connection.Close();

            //// 在頁面首次載入時，設置選項1為已勾選狀態
            //ListItem listItem1 = chkls_right_name.Items.FindByValue("2");
            //if (listItem1 != null)
            //{
            //    listItem1.Selected = true;
            //}

            //foreach (ListItem item in chkls_right_name.Items)
            //{
            //    if (item.Selected)
            //    {
            //        string text = item.Text;
            //        string value = item.Value;

            //        // 在這裡可以使用選項的 text 和 value 進行後續處理
            //    }
            //}
        }
        private void ShowVorNot()
        {
            string user_id = Request.QueryString["user_id"];

            // 連線資料庫
            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["YachtsConnectionString"].ConnectionString);

            // SQL 語法
            string sql = "SELECT * FROM rightUserHas WHERE user_id = @user_id";

            // 創建 SqlCommand 物件
            SqlCommand command = new SqlCommand(sql, connection);

            //參數化，Parameters:為了防止SQL Injection 程式碼攻擊，所以寫入SQL時，要使用參數「@參數名稱」來代替直接給的值，而給予參數的資料型態要使用Add
            command.Parameters.AddWithValue("@user_id", user_id);

            // 資料庫連線開啟
            connection.Open();

            // 執行 SQL 查詢，取得資料
            SqlDataReader reader = command.ExecuteReader();

            // 將資料加入 DropDownList
            while (reader.Read())
            {
                ListItem listItem1 = chkls_right_name.Items.FindByValue(reader["right_id"].ToString());
                if (listItem1 != null)
                {
                    listItem1.Selected = true;
                }
            }

            // 資料庫關閉
            connection.Close();
        }
    }
}