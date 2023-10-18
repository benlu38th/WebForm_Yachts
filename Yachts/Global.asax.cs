using Microsoft.AspNet.FriendlyUrls;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.UI;
using System.Windows;
using System.Windows.Forms;

namespace Yachts
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            // 設定不顯示副檔名 (如果只想隱藏副檔名做到此區塊就好)
            var routes = RouteTable.Routes;
            var settings = new FriendlyUrlSettings();
            settings.AutoRedirectMode = RedirectMode.Permanent;
            //routes.EnableFriendlyUrls(settings);

            //修改避免 CKFinder 上傳功能錯誤
            routes.EnableFriendlyUrls(settings, new Microsoft.AspNet.FriendlyUrls.Resolvers.IFriendlyUrlResolver[] { new MyWebFormsFriendlyUrlResolver() });

            // 應用程式啟動時執行的程式碼
            //RouteConfig.RegisterRoutes(RouteTable.Routes);
            //BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
        public class MyWebFormsFriendlyUrlResolver : Microsoft.AspNet.FriendlyUrls.Resolvers.WebFormsFriendlyUrlResolver
        {
            public override string ConvertToFriendlyUrl(string path)
            {
                //字串為 ckfinder 固定內容
                if (!string.IsNullOrEmpty(path) && path.ToLower().Contains("/ckfinder/core/connector/aspx/connector.aspx"))
                {
                    return path;
                }
                return base.ConvertToFriendlyUrl(path);
            }
        }
        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            HttpRuntimeSection section = (HttpRuntimeSection)ConfigurationManager.GetSection("system.web/httpRuntime");
            int maxFileSize = section.MaxRequestLength * 1024;
            if (Request.ContentLength > maxFileSize)
            {
                try
                {
                    System.Windows.Forms.MessageBox.Show("檔案太大囉!");

                    string news_id = Request.QueryString["news_id"];
                    string news_title = Request.QueryString["news_title"];
                    if(!string.IsNullOrEmpty(news_id) && !string.IsNullOrEmpty(news_title))
                    {
                        Response.Redirect("~/backend/news_detail.aspx?news_id=" + news_id + "&news_title=" + news_title);
                    }
                    else
                    {
                        Response.Redirect("~/backend/mySizeError.aspx");
                    }

                }
                catch (Exception ex)
                {
                }
            }
        }
    }
}