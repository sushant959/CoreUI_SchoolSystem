﻿using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using UpdatedScholSystem.Models;

namespace UpdatedScholSystem.Service
{
    public static class Utilities
    {
        public static string ExportToPdf(DataTable data,string title)
        {
            DirectoryInfo di = new DirectoryInfo(HttpContext.Current.Server.MapPath("~/UploadedFiles/student/Report/"));
            if (!di.Exists) di.Create();

            string filename = Path.Combine(di.FullName + "_studentreport.pdf");

            _ExportToPdf(data, filename,title);
            return "/UploadedFiles/student/Report/_studentreport.pdf";
        }

        private static void _ExportToPdf(DataTable dt, string filename,string title)
        {
            Document document = new Document();
            // Set the page size
            document.SetPageSize(PageSize.A4.Rotate());
            PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(filename, FileMode.Create));
            document.Open();
            //Font font5 = FontFactory.GetFont(FontFactory.HELVETICA, BaseFont.IDENTITY_H, BaseFont.EMBEDDED, 5);


            int fontSize = 8;
            int cols = dt.Columns.Count;
            if (cols < 8) fontSize = 10;


            BaseFont bf = BaseFont.CreateFont(Environment.GetEnvironmentVariable("windir") + @"\fonts\ARIALUNI.TTF",
                BaseFont.IDENTITY_H, true);
            Font font5 = new Font(bf, fontSize);
            Font fontTitle = new Font(bf, 14, 1);
            PdfPTable table = new PdfPTable(cols);
            //PdfPRow row = null;

            //float[] widths = new float[] { 4f, 4f, 4f, 4f };
            //table.SetWidths(widths);

            table.WidthPercentage = 100;

            foreach (DataColumn c in dt.Columns)
            {
                Font fontHeader = new iTextSharp.text.Font(bf, fontSize, iTextSharp.text.Font.NORMAL, new BaseColor(System.Drawing.Color.White));

                iTextSharp.text.pdf.PdfPCell h_cell = new iTextSharp.text.pdf.PdfPCell(new Phrase(fontSize, c.ColumnName, fontHeader));
                h_cell.BackgroundColor = new BaseColor(150, 150, 150);

                table.AddCell(h_cell);
            }
            int count = 0;
            foreach (DataRow r in dt.Rows)
            {
                if (dt.Rows.Count > 0)
                {
                    count++;
                    for (int i = 0; i < dt.Columns.Count; i++)
                    {
                        PdfPCell cell = new PdfPCell((new Phrase(r[i].ToString(), font5)));
                        if (count % 2 == 0) cell.BackgroundColor = new BaseColor(240, 240, 240);
                        table.AddCell(cell);
                    }
                }
            }
            iTextSharp.text.Image pic = iTextSharp.text.Image.GetInstance(HttpContext.Current.Server.MapPath("~/Content/School.jpg"));
            pic.ScalePercent(30);
            pic.SetAbsolutePosition(40, 480);
            document.Add(pic);
            var time = DateTime.Now;
            Paragraph date = new Paragraph(time.ToString());
            date.Alignment = Element.ALIGN_RIGHT;
            document.Add(date);
            Paragraph header = new Paragraph("GEMS Public School \n Hattiban Kathmandu-110055");
            header.Alignment = Element.ALIGN_CENTER;
            document.Add(header);
            
            Paragraph p = new Paragraph("\nReport of " + title + "\n", fontTitle);
            p.Alignment = Element.ALIGN_CENTER;
            document.Add(p);
            document.Add(new Paragraph(" "));

            document.Add(table);
            document.Close();
        }
        public static List<string> getUserAccessFeatures(int c, string role)
        {
            List<string> features = new List<string>();
            var roleId = BaseDbServices.Instance.GetData("select * from tblgroup where Company_ID='" + c + "' and UserRole='" + role + "'", null);
            var id = roleId.Rows[0]["ID"];

            var featureId = BaseDbServices.Instance.GetData("select * from tblusercontrol where Company_ID='" + c + "' and Group_ID='" + id + "'", null);
            List<UserControl> lstUserControl = new List<UserControl>();

            if (featureId.Rows.Count > 0)
            {
                for (int i = 0; i < featureId.Rows.Count; i++)
                {
                    UserControl userControl = new UserControl();
                    userControl.ID = Convert.ToInt32(featureId.Rows[i]["ID"]);
                    userControl.Group_ID = Convert.ToInt32(featureId.Rows[i]["Group_ID"]);
                    userControl.Feature_ID = Convert.ToInt32(featureId.Rows[i]["Feature_ID"]);
                    userControl.Action_ID = Convert.ToInt32(featureId.Rows[i]["Action_ID"]);
                    userControl.Company_ID = Convert.ToInt32(featureId.Rows[i]["Company_ID"]);
                    lstUserControl.Add(userControl);
                }
               
            }
            //var featureId = Repos.Instance.UserControlRepo.GetAll().Where(x => x.Company_ID == c && x.Group_ID == id).ToList();
            var result = lstUserControl.GroupBy(test => test.Feature_ID)
                                    .Select(grp => grp.First())
                                    .ToList();
            for (int k = 0; k < result.Count; k++)
            {
                var feId = result[k].Feature_ID;
                var featureName = BaseDbServices.Instance.GetData("select * frm tblfeature where Company_ID='" + c + "' and ID='" + feId + "'", null);
                features.Add(featureName.Rows[0]["Name"].ToString());
            }
            return features;
        }
        public static List<string> getUserFeatureActionAccess(int c, string role, string feature)
        {
            List<string> actions = new List<string>();
            var roleId = BaseDbServices.Instance.GetData("select * from tblgroup where Company_ID='" + c + "' and UserRole='" + role + "'", null);
            var id = roleId.Rows[0]["ID"];
           
            var featureDetail = BaseDbServices.Instance.GetData("select * from tblfeature where Company_ID='" + c + "' and Name='" + feature + "'", null);
            var featureId = featureDetail.Rows[0]["ID"];
 //var result = Repos.Instance.UserControlRepo.GetAll().Where(x => x.Company_ID == c && x.Group_ID == id && x.Feature_ID == featureId).ToList();
            var result = BaseDbServices.Instance.GetData("select * from tblusercontrol where Company_ID='" + c + "' and Group_ID='" + id + "' and Feature_ID='" + featureId + "'", null);
            for (int k = 0; k < result.Rows.Count; k++)
            {
                var actionId = result.Rows[k]["Action_ID"];
                var featureName = BaseDbServices.Instance.GetData("select * from tblfeatureaction where Company_ID='" + c + "' and ID='" + actionId + "'", null);
               // var featureName = Repos.Instance.FeatureActionRepo.SearchFor(x => x.Company_ID == c && x.ID == actionId).ToList();
                actions.Add(featureName.Rows[0]["Name"].ToString());
            }
            return actions;
        }

    }
}