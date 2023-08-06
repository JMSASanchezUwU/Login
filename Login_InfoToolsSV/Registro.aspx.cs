using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Login_InfoToolsSV
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        string patron = "Clave";
        string msj="";
        protected void BtnRegistrar_Click(object sender, EventArgs e)
        {
            string conectar = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;
            SqlConnection sqlConectar = new SqlConnection(conectar);
            SqlCommand cmd = new SqlCommand("SP_AgregarUsuario", sqlConectar)
            {
                CommandType = CommandType.StoredProcedure
            };
            cmd.Connection.Open();
            cmd.Parameters.Add("@Usuario", SqlDbType.VarChar, 50).Value = tbUsuario.Text;
            cmd.Parameters.Add("@Contrasenia", SqlDbType.VarChar, 50).Value = tbPassword.Text;
            cmd.Parameters.Add("@Patron", SqlDbType.VarChar, 50).Value = patron;
            try
            {
                cmd.ExecuteNonQuery();
                msj = "1";
            }
            catch (Exception ex)
            {
                msj = ex.Message;
            }

            if (msj=="1")
            {
                //Agregamos una sesion de usuario
                cmd.Connection.Close();
                string script = "<script>alert('Usuario creado con éxito');</script>";
                ClientScript.RegisterStartupScript(this.GetType(), "UsuarioCreadoExitoso", script);

                Response.Redirect("Login_InfoToolsSV.aspx");

            }


            cmd.Connection.Close();
        }

        protected void BtnRedireccionar_Click(Object sender, EventArgs e)
        {
            Response.Redirect("Login_InfoToolsSV.aspx");
        }
    }
}