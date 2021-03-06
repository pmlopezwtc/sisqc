﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Mainsite.xsd;

namespace Mainsite.Reporte
{
    public partial class ResumenProductoTerminado_juntos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ReportDocument rptDoc = new ReportDocument();
            ProductoTerminado_prueba2 ds = new ProductoTerminado_prueba2(); // .xsd file name
            DataTable dt = new DataTable();
            dt.TableName = "Producto Terminado";
            dt = ObtenerProductoTerminado(); //This function is located below this function
            ds.Tables[0].Merge(dt);

            // Your .rpt file path will be below
            
            rptDoc.Load(Server.MapPath("../RPT/ReporteTodoJunto_3.rpt"));
            // rptDoc.Load(Server.MapPath("../RPT/ProductoTerminado_juntos.rpt"));

            //set dataset to the report viewer.
            rptDoc.SetDataSource(ds);
            

            CrystalReportViewer1.ReportSource = rptDoc;
            
        }

        public DataTable ObtenerProductoTerminado()
        {

            string final = Session["final"].ToString();
            //Connection string replace 'databaseservername' with your db server name
            System.Configuration.Configuration rootWebConfig = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("/sisqc");
            System.Configuration.ConnectionStringSettings connStringLM;
            connStringLM = rootWebConfig.ConnectionStrings.ConnectionStrings["CONTROLPTConnectionString"];
            SqlConnection Con = new SqlConnection(connStringLM.ToString());

            SqlCommand cmd = new SqlCommand();
            DataSet ds = null;
            SqlDataAdapter adapter;
            //try
            //{
            Con.Open();
            //Stored procedure calling. It is already in sample db.
            cmd.CommandText = final;
            cmd.CommandType = CommandType.Text;
            cmd.Connection = Con;
            ds = new DataSet();
            adapter = new SqlDataAdapter(cmd);

            adapter.Fill(ds, "Users");
            return ds.Tables[0];
            
        }

     


    }
}