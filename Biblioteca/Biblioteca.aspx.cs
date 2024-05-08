using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Npgsql;

namespace Biblioteca
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGrid();
            }
        }

        protected void BindGrid()
        {
            string constr = ConfigurationManager.ConnectionStrings["BibliotecaConnectionString"].ConnectionString;
            using (NpgsqlConnection con = new NpgsqlConnection(constr))
            {
                using (NpgsqlCommand cmd = new NpgsqlCommand("SELECT * FROM \"Carti\"", con))
                {
                    using (NpgsqlDataAdapter sda = new NpgsqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        gvBooks.DataSource = dt;
                        gvBooks.DataBind();
                    }
                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            string constr = ConfigurationManager.ConnectionStrings["BibliotecaConnectionString"].ConnectionString;
            using (NpgsqlConnection con = new NpgsqlConnection(constr))
            {
                using (NpgsqlCommand cmd = new NpgsqlCommand("INSERT INTO \"Carti\" (title, author, year_pub) VALUES (@Titlu, @Autor, @AnPublicare)", con))
                {
                    cmd.Parameters.AddWithValue("@Titlu", txtTitlu.Text);
                    cmd.Parameters.AddWithValue("@Autor", txtAutor.Text);
                    cmd.Parameters.AddWithValue("@AnPublicare", int.Parse(txtAnPublicare.Text));
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            BindGrid();
        }

        protected void gvBooks_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvBooks.EditIndex = e.NewEditIndex;
            BindGrid();
        }

        protected void gvBooks_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = gvBooks.Rows[e.RowIndex];
            int id = Convert.ToInt32(gvBooks.DataKeys[e.RowIndex].Value);
            string titlu = (row.Cells[1].Controls[0] as TextBox).Text;
            string autor = (row.Cells[2].Controls[0] as TextBox).Text;
            int anPublicare = int.Parse((row.Cells[3].Controls[0] as TextBox).Text);

            string constr = ConfigurationManager.ConnectionStrings["BibliotecaConnectionString"].ConnectionString;
            using (NpgsqlConnection con = new NpgsqlConnection(constr))
            {
                using (NpgsqlCommand cmd = new NpgsqlCommand("UPDATE \"Carti\" SET title = @Titlu, author = @Autor, year_pub = @AnPublicare WHERE id = @Id", con))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    cmd.Parameters.AddWithValue("@Titlu", titlu);
                    cmd.Parameters.AddWithValue("@Autor", autor);
                    cmd.Parameters.AddWithValue("@AnPublicare", anPublicare);
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }

            gvBooks.EditIndex = -1;
            BindGrid();
        }

        protected void gvBooks_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvBooks.EditIndex = -1;
            BindGrid();
        }

        protected void gvBooks_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int id = Convert.ToInt32(gvBooks.DataKeys[e.RowIndex].Value);

            string constr = ConfigurationManager.ConnectionStrings["BibliotecaConnectionString"].ConnectionString;
            using (NpgsqlConnection con = new NpgsqlConnection(constr))
            {
                using (NpgsqlCommand cmd = new NpgsqlCommand("DELETE FROM \"Carti\" WHERE Id = @Id", con))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }

            BindGrid();
        }
    }
}