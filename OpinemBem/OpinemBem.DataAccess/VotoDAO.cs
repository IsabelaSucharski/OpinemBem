using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using OpinemBem.Models;

namespace OpinemBem.DataAccess
{
    public class VotoDAO
    {
        public void Inserir(Voto obj)
        {
            using (SqlConnection conn = new SqlConnection(@"Initial Catalog=OpinemBem; Data Source=localhost; Integrated Security=SSPI"))
            {
                string strSQL = "INSERT INTO voto (id_usuario, id_projeto, data_voto) VALUES ( @id_usuario, @id_projeto, @data_voto);";
                {
                    using (SqlCommand cmd = new SqlCommand(strSQL))
                    {
                        cmd.Connection = conn;

                        cmd.Parameters.Add("@id_usuario", SqlDbType.VarChar).Value = obj.Usuario.Id;
                        cmd.Parameters.Add("@id_projeto", SqlDbType.VarChar).Value = obj.ProjetoDeLei.Id;
                        cmd.Parameters.Add("@data_voto", SqlDbType.VarChar).Value = obj.DataVoto;

                        conn.Open();
                        cmd.ExecuteNonQuery();
                        conn.Close();
                    }
                }
            }
        }

        public void Atualizar(Voto obj)
        {
            using (SqlConnection conn = new SqlConnection(@"Initial Catalog=OpinemBem; Data Source=localhost; Integrated Security=SSPI"))
            {
                string strSQL = "UPDATE voto set voto = @voto where id_projeto = @id_projeto;";
                {
                    using (SqlCommand cmd = new SqlCommand(strSQL))
                    {
                        cmd.Connection = conn;

                    }
}
