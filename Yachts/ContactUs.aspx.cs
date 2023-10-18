using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows;

namespace Yachts
{
    public partial class ContactUs : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ShowArea();
        }
        private void ShowArea()
        {
            string country_id = ddl_contact_country.SelectedValue;

            if (country_id != "0")
            {
                ddl_contact_area.Enabled = true;

                //1.連線資料庫(合在一起寫)
                SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["YachtsConnectionString"].ConnectionString);

                //2.sql語法
                string sql = "SELECT * FROM areas WHERE country_id = @country_id";

                //3.創建command物件
                SqlCommand command = new SqlCommand(sql, connection);

                //3.5.參數化，Parameters:為了防止SQL Injection 程式碼攻擊，所以寫入SQL時，要使用參數「@參數名稱」來代替直接給的值，而給予參數的資料型態要使用Add
                command.Parameters.AddWithValue("@country_id", Convert.ToInt32(country_id));

                //4.資料庫連線開啟
                connection.Open();

                //5.執行sql (連線的作法-需自行關閉)
                SqlDataReader reader = command.ExecuteReader();
                //DataReader速度快只能逐筆單向有上往下而且不能計算，適合用來抓單筆資料

                // 清空DropDownList中原有的項目
                ddl_contact_area.Items.Clear();

                // 使用資料讀取器(SqlDataReader)逐筆讀取資料
                while (reader.Read())
                {
                    // 讀取資料列中的特定欄位，例如使用索引或欄位名稱
                    string itemName = reader["area_name"].ToString(); // 將 "area_name" 替換為您要填入DropDownList的資料欄位名稱
                    string itemValue = reader["area_id"].ToString(); // 將 "area_id" 替換為您要填入DropDownList的值欄位名稱

                    // 建立新的ListItem並將其加入到DropDownList中
                    ListItem listItem = new ListItem();

                    listItem.Text = itemName;
                    listItem.Value = itemValue;

                    ddl_contact_area.Items.Add(listItem);
                }

                connection.Close();
            }
            else
            {
                ddl_contact_area.Enabled = false;

                // 清空DropDownList中原有的項目
                ddl_contact_area.Items.Clear();

                // 建立新的ListItem並將其加入到DropDownList中
                ListItem listItem = new ListItem();

                listItem.Text = "--請先選國家--";
                listItem.Value = "0";

                ddl_contact_area.Items.Add(listItem);
            }

        }
        public static string ChangeToHTML(string words)
        {
            words = words.Replace(">", "&gt;");
            words = words.Replace("<", "&lt;");
            words = words.Replace(@"\r", "<br>");
            words = words.Replace(@"\n", "<br>");
            words = words.Replace("|", "&brvbar");
            words = words.Replace(" ", "&nbsp;");
            return words;
        }
        protected void ImageButton_Click(object sender, ImageClickEventArgs e)
        {
            string input_name = txt_contact_name.Text;
            string input_mail = txt_contact_mail.Text;
            string input_phone = txt_contact_phone.Text;
            string input_country_value = ddl_contact_country.SelectedValue;
            string input_country_name = ddl_contact_country.SelectedItem.Text;
            string input_country_area = ddl_contact_area.SelectedItem.Text;
            string input_comments = txt_contact_comments.Text;
            string input_yatch = ddl_contact_yatch.SelectedItem.Text;

            if (!string.IsNullOrEmpty(input_name) && input_name.Length <= 50)
            {
                if (!string.IsNullOrEmpty(input_mail) && input_mail.Length <= 50)
                {
                    if (!string.IsNullOrEmpty(input_phone) && input_phone.Length <= 50)
                    {
                        if (input_country_value != "0")
                        {
                            if (input_comments.Length <= 200)
                            {
                                if (!string.IsNullOrEmpty(Recaptcha1.Response))
                                {
                                    var result = Recaptcha1.Verify();

                                    if (result.Success)
                                    {
                                        //1.連線資料庫
                                        SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["YachtsConnectionString"].ConnectionString);

                                        //2.sql語法，VALUES先使用@參數來取代直接給值，以防SQL Injection 程式碼
                                        //新增
                                        string sql = "INSERT INTO contact(contact_name, contact_mail, contact_phone, contact_country, contact_area, contact_yatch, contact_comments) VALUES(@contact_name, @contact_mail, @contact_phone, @contact_country, @contact_area, @contact_yatch, @contact_comments)";

                                        //3.創建command物件
                                        SqlCommand command = new SqlCommand(sql, connection);

                                        //4.參數化，Parameters:為了防止SQL Injection 程式碼攻擊，所以寫入SQL時，要使用參數「@參數名稱」來代替直接給的值，而給予參數的資料型態要使用Add
                                        command.Parameters.AddWithValue("@contact_name", input_name);
                                        command.Parameters.AddWithValue("@contact_mail", input_mail);
                                        command.Parameters.AddWithValue("@contact_phone", input_phone);
                                        command.Parameters.AddWithValue("@contact_country", input_country_name);
                                        command.Parameters.AddWithValue("@contact_area", input_country_area);
                                        command.Parameters.AddWithValue("@contact_yatch", input_yatch);
                                        command.Parameters.AddWithValue("@contact_comments", input_comments);

                                        //5.資料庫連線開啟
                                        connection.Open();

                                        //6.執行sql (新增刪除修改)
                                        command.ExecuteNonQuery(); //無回傳值

                                        //7.資料庫關閉
                                        connection.Close();

                                        txt_contact_name.Text = "";
                                        txt_contact_mail.Text = "";
                                        txt_contact_phone.Text = "";
                                        ddl_contact_country.SelectedValue = "0";
                                        txt_contact_comments.Text = "";

                                        // 清空DropDownList中原有的項目
                                        ddl_contact_area.Items.Clear();

                                        // 建立新的ListItem並將其加入到DropDownList中
                                        ListItem listItem = new ListItem();

                                        listItem.Text = "--請先選國家--";
                                        listItem.Value = "0";

                                        ddl_contact_area.Items.Add(listItem);

                                        ddl_contact_area.Enabled = false;

                                        //以下開始寄信
                                        // 取得收件者信箱
                                        string toMail = "xxx@gmail.com";

                                        // 設定寄件者信箱和應用程式密碼
                                        string fromMail = "xxx38th@gmail.com";
                                        string fromPassword = "password";

                                        // 建立郵件訊息物件
                                        MailMessage message = new MailMessage();
                                        message.From = new MailAddress(fromMail);
                                        message.Subject = "Yachts Poss" +
                                            "ible Buyer";
                                        message.To.Add(new MailAddress(toMail));
                                        message.Body = "<html><body> " +
                                            "<p>"+"Name：" + input_name+"</p>"+
                                            "<p>" + "Email：" +input_mail+ "</p>" +
                                            "<p>" + "Phone：" + input_phone+ "</p>" +
                                            "<p>" + "Country：" + input_country_name+ "</p>" +
                                            "<p>" + "Area：" + input_country_area+ "</p>" +
                                            "<p>" + "Yacht：" +input_yatch+ "</p>" +
                                             "Comments：" + ChangeToHTML(input_comments) +
                                            " </body></html>";
                                        message.IsBodyHtml = true;

                                        // 設定 SMTP 客戶端
                                        var smptClient = new SmtpClient("smtp.gmail.com") {
                                            Port = 587,
                                            Credentials = new NetworkCredential(fromMail, fromPassword),
                                            EnableSsl = true,
                                        };

                                        // 送出郵件
                                        smptClient.Send(message);
                                        //以上結束寄信

                                        MessageBox.Show("已收到您的訊息，請去email確認信件!", "恭喜!");
                                    }
                                }
                                else
                                {
                                    // 輸出JavaScript警告對話框
                                    string message = "請完成機器人驗證!";
                                    string encodedMessage = HttpUtility.HtmlEncode(message);
                                    Response.Write("<script>alert('" + encodedMessage + "');</script>");
                                }
                            }
                            else
                            {
                                // 輸出JavaScript警告對話框
                                string message = "意見回饋請勿超過200個字元";
                                string encodedMessage = HttpUtility.HtmlEncode(message);
                                Response.Write("<script>alert('" + encodedMessage + "');</script>");
                            }
                        }
                        else
                        {
                            // 輸出JavaScript警告對話框
                            string message = "請選擇你所在的國家及地區!";
                            string encodedMessage = HttpUtility.HtmlEncode(message);
                            Response.Write("<script>alert('" + encodedMessage + "');</script>");
                        }
                    }
                    else
                    {
                        // 輸出JavaScript警告對話框
                        string message = "請填入連絡電話，同時不要超過50個字元!";
                        string encodedMessage = HttpUtility.HtmlEncode(message);
                        Response.Write("<script>alert('" + encodedMessage + "');</script>");
                    }
                }
                else
                {
                    // 輸出JavaScript警告對話框
                    string message = "請填入信箱，同時不要超過50個字元!";
                    string encodedMessage = HttpUtility.HtmlEncode(message);
                    Response.Write("<script>alert('" + encodedMessage + "');</script>");
                }
            }
            else
            {
                // 輸出JavaScript警告對話框
                string message = "請填入名稱，同時不要超過50個字元!";
                string encodedMessage = HttpUtility.HtmlEncode(message);
                Response.Write("<script>alert('" + encodedMessage + "');</script>");
            }
        }
    }
}