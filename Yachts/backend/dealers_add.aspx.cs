using CKFinder;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Yachts.pages
{
    public partial class B_dealers_add_country : System.Web.UI.Page
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

            string co = Request.QueryString["co"];
            string area = Request.QueryString["area"];
            if (!IsPostBack)
            {
                if (co == null || area == null)
                {
                    Response.Redirect("dealers_countries.aspx");
                }
                else
                {
                    txt_countries.Text = co;
                    txt_area.Text = area;
                }
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string co = Request.QueryString["co"];
            string co_id = Request.QueryString["co_id"];
            string area = Request.QueryString["area"];
            string area_id = Request.QueryString["area_id"];

            //取得上傳檔案名稱(包含.jpg)
            string fileName = FileUpload1.FileName;

            //取得上傳檔案尾標(即 【.jpg】)
            string fileExtension = Path.GetExtension(fileName).ToLower();

            //建立上傳檔案許可之尾標
            string[] allowExtensions = { ".jpg", ".jepg", ".png", ".gif", ".jfif" };

            //建立圖片存檔之路徑
            string savePath = @"C:\Users\盧致宇\source\repos\Yachts\Yachts\photos\dealerPhoto\";

            //建立存在資料庫之取出圖片路徑
            string photo_path = "~/photos/dealerPhoto/" + fileName;

            //建立檔案格式OK的布林值
            Boolean fileOK = false;

            //建立檔案上傳OK的布林值
            Boolean uploadOK = false;

            //如果有選擇檔案即執行
            if (FileUpload1.HasFile)
            {

                //測試上傳檔案尾標是否符合格式
                for (int i = 0; i < allowExtensions.Length; i++)
                {
                    if (fileExtension == allowExtensions[i])
                    {
                        fileOK = true;
                    }
                }
                //上傳檔案尾標符合格式即執行
                if (fileOK)
                {
                    try
                    {
                        //建立存放檔案路徑
                        string saveResult = Path.Combine(savePath, fileName);
                        //存檔案到路徑
                        FileUpload1.SaveAs(saveResult);
                        uploadOK = true;
                    }
                    catch
                    {
                        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Message", "<script>alert('發生例外錯誤，上傳失敗！');</script>");
                    }
                }
                else
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Message", "<script>alert('上傳的檔案，附檔名只能是.jpg , .jepg , .png , .gif , .jfif ！');</script>");
                }
                //上傳檔案成功即執行
                if (uploadOK)
                {
                    //1.連線資料庫
                    SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["YachtsConnectionString"].ConnectionString);

                    //2.sql語法，VALUES先使用@參數來取代直接給值，以防SQL Injection 程式碼
                    //新增
                    string sql = "INSERT INTO dealers(area_id, area_name, dealer_name, dealer_contact, dealer_address, dealer_tel, dealer_fax, dealer_cell, dealer_mail, dealer_website, dealer_photoPath) VALUES(@area_id, @area_name, @dealer_name, @dealer_contact, @dealer_address, @dealer_tel, @dealer_fax, @dealer_cell, @dealer_mail, @dealer_website, @dealer_photoPath) ";


                    //3.創建command物件
                    SqlCommand command = new SqlCommand(sql, connection);

                    //4.參數化，Parameters:為了防止SQL Injection 程式碼攻擊，所以寫入SQL時，要使用參數「@參數名稱」來代替直接給的值，而給予參數的資料型態要使用Add
                    command.Parameters.AddWithValue("@area_id", Convert.ToInt32(area_id));
                    command.Parameters.AddWithValue("@area_name", area);
                    command.Parameters.AddWithValue("@dealer_name", txt_dealer_name.Text);
                    command.Parameters.AddWithValue("@dealer_contact", txt_dealer_contact.Text);
                    command.Parameters.AddWithValue("@dealer_address", txt_dealer_address.Text);
                    command.Parameters.AddWithValue("@dealer_tel", txt_dealer_tel.Text);
                    command.Parameters.AddWithValue("@dealer_fax", txt_dealer_fax.Text);
                    command.Parameters.AddWithValue("@dealer_cell", txt_dealer_cell.Text);
                    command.Parameters.AddWithValue("@dealer_mail", txt_dealer_mail.Text);
                    command.Parameters.AddWithValue("@dealer_website", txt_dealer_website.Text);
                    command.Parameters.AddWithValue("@dealer_photoPath", photo_path);

                    //5.資料庫連線開啟
                    connection.Open();

                    //6.執行sql (新增刪除修改)
                    command.ExecuteNonQuery(); //無回傳值

                    //7.資料庫關閉
                    connection.Close();

                    Response.Redirect("dealers.aspx?co=" + co + "&co_id=" + co_id);
                }
            }
            else
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Message", "<script>alert('請選擇一張照片！');</script>");
            }
        }
        protected void btn_returnToDealers_Click(object sender, EventArgs e)
        {
            string co = Request.QueryString["co"];
            string co_id = Request.QueryString["co_id"];
            string area = Request.QueryString["area"];
            string area_id = Request.QueryString["area_id"];

            Response.Redirect("dealers.aspx?co=" + co + "&co_id=" + co_id + "&area=" + area + "&area_id=" + area_id);
        }
        protected void lbtn_goCountries_Click(object sender, EventArgs e)
        {
            Response.Redirect("dealers_countries.aspx");
        }

        protected void lbtn_goAreas_Click(object sender, EventArgs e)
        {
            string co = Request.QueryString["co"];
            string co_id = Request.QueryString["co_id"];

            if (string.IsNullOrEmpty(co) || string.IsNullOrEmpty(co_id))
            {
                Response.Redirect("dealers_areas.aspx");
            }
            else
            {
                Response.Redirect("dealers_areas.aspx?co=" + co + "&co_id=" + co_id);
            }
        }
        protected void lbtn_goDealers_Click(object sender, EventArgs e)
        {
            string co = Request.QueryString["co"];
            string co_id = Request.QueryString["co_id"];
            string area = Request.QueryString["area"];
            string area_id = Request.QueryString["area_id"];

            Response.Redirect("dealers.aspx?co=" + co + "&co_id=" + co_id + "&area=" + area_id + "&area_id=" + area_id);
        }
    }
}