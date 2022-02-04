using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebAppEjemplo
{
    public partial class pagina2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection("Server=(localdb)\\ProjectModels;Database=Pruebas;Trusted_Connection=True;");
            connection.Open();
            SqlCommand command = new SqlCommand("Select Color, Estado From Coches", connection);
            SqlDataReader reader = command.ExecuteReader();
            SqlDataReader sqlDataReader = reader;
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("Color");
            dataTable.Columns.Add("Estado");
            while (sqlDataReader.Read())
            {
                DataRow row = dataTable.NewRow();
                row["Color"] = sqlDataReader["Color"];
                row["Estado"] = sqlDataReader["Estado"];
                dataTable.Rows.Add(row);
            }
            GridView1.DataSource = dataTable;
            GridView1.DataBind();
            reader.Close();
            connection.Close();

            TextBox3.Visible = false;
            TextBox4.Visible = false;
            Button3.Visible = false;
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string texto = ((TextBox)TextBox1.FindControl("TextBox1")).Text;
            Label1.Text = texto;

            Response.Redirect("~/pagina3.aspx?val=texto");

            Server.Transfer("~/pagina3.aspx", true);

        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            TextBox3.Visible = true;
            TextBox4.Visible = true;
            Button3.Visible = true;

        }

        //botón para añadir un nuevo registro.
        protected void Button3_Click(object sender, EventArgs e)
        {
            //Coger TextBox3 y TextBox4 e insertarlo en la BBDD
            string txtColor = ((TextBox)TextBox1.FindControl("TextBox3")).Text;
            string txtEstado = ((TextBox)TextBox1.FindControl("TextBox4")).Text;


            SqlConnection connection = new SqlConnection("Server=(localdb)\\ProjectModels;Database=Pruebas;Trusted_Connection=True;");
            connection.Open();

            var sql = "INSERT INTO Coches(Color, Estado) VALUES(@color, @estado)";
            SqlCommand command = new SqlCommand(sql, connection);

            command.Parameters.AddWithValue("@color", txtColor.ToString());
            command.Parameters.AddWithValue("@estado", txtEstado.ToString());

            try
            {
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Algo salió mal");
            }
            finally
            {
                connection.Close();
                connection.Dispose();
                Server.Transfer("~/pagina2.aspx");
            }


        }
        protected void GridView1_RowDelete(object sender, GridViewDeleteEventArgs e)
        {
            string color = "";
            string estado = "";
            foreach(DictionaryEntry dict in e.Values)
            {
                if (dict.Key == "Color")
                {
                    color = dict.Value.ToString();
                }
                else
                {
                    estado = dict.Value.ToString();
                }
                
            }

            SqlConnection connection = new SqlConnection("Server=(localdb)\\ProjectModels;Database=Pruebas;Trusted_Connection=True;");
            connection.Open();

            var sql = "DELETE FROM Coches where Color = @color and Estado = @estado";
            SqlCommand command = new SqlCommand(sql, connection);

            command.Parameters.AddWithValue("@color", color);
            command.Parameters.AddWithValue("@estado", estado);

            try
            {
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Algo salió mal");
            }
            finally
            {
                connection.Close();
                connection.Dispose();
                Server.Transfer("~/pagina2.aspx");
            }
        }

        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridView1.EditIndex = e.NewEditIndex;
        }

        private string GetIdFromDb(string color, string estado)
        {
            string id = "";

            SqlConnection connection = new SqlConnection("Server=(localdb)\\ProjectModels;Database=Pruebas;Trusted_Connection=True;");
            connection.Open();

            SqlCommand command = new SqlCommand("Select Id, Color, Estado FROM Coches where Color = @color and Estado = @estado", connection);
            command.Parameters.AddWithValue("@color", color);
            command.Parameters.AddWithValue("@estado", estado);
            SqlDataReader reader = command.ExecuteReader();
            id = reader.GetValue(0).ToString();
            
            reader.Close();
            connection.Close();

            return id;
        }

        protected void grdview_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            
            string colorNuevo = "";
            string estadoNuevo = "";
      
            foreach (DictionaryEntry dict in e.NewValues)
            {
                if (dict.Key == "Color")
                {
                    colorNuevo = dict.Value.ToString();
                }
                else
                {
                    estadoNuevo = dict.Value.ToString();
                }

            }

            SqlConnection connection = new SqlConnection("Server=(localdb)\\ProjectModels;Database=Pruebas;Trusted_Connection=True;");
            connection.Open();

            GetIdFromDb(colorNuevo, estadoNuevo);

            //var sql = "UPDATE Coches SET Color = @color and Estado = @estado where Id = @id";
            //SqlCommand command = new SqlCommand(sql, connection);

            //command.Parameters.AddWithValue("@color", colorNuevo);
            //command.Parameters.AddWithValue("@estado", estadoNuevo);

            //try
            //{
            //    command.ExecuteNonQuery();
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine("Algo salió mal");
            //}
            //finally
            //{
            //    connection.Close();
            //    connection.Dispose();
            //    Server.Transfer("~/pagina2.aspx");
            //}
        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            //GridView1.EditIndex = gvue.NewEditIndex;
            
        }
    }
}