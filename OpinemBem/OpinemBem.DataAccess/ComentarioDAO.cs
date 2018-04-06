using OpinemBem.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace OpinemBem.DataAccess
{
    public class ComentarioDAO
    {
        public void Inserir(Comentario obj) 
        {
            //Criando uma conexão com o banco de dados
            using (SqlConnection conn = new SqlConnection(@"Initial Catalog=OpinemBem; Data Source=localhost; Integrated Security=SSPI"))
            {
                //Criando instrução sql para inserir na tabela de categorias
                string strSQL = "INSERT INTO comentario (id_usuario, id_projeto, data_comentario, mensagem) VALUES (@id_usuario, @id_projeto, @data_comentario, @mensagem;";
                {
                    //Criando um comando sql que será executado na base de dados
                    using (SqlCommand cmd = new SqlCommand(strSQL))
                    {
                        cmd.Connection = conn;
                        //Preenchendo os parâmetros da instrução sql
                        cmd.Parameters.Add("@id_usuario", SqlDbType.VarChar).Value = obj.Usuario.Id;
                        cmd.Parameters.Add("@id_projeto", SqlDbType.VarChar).Value = obj.ProjetoDeLei.Id;
                        cmd.Parameters.Add("@data_comentario", SqlDbType.DateTime).Value = obj.DataHora;
                        cmd.Parameters.Add("@mensagem", SqlDbType.VarChar).Value = obj.Mensagem;
                        

                        //Abrindo conexão com o banco de dados
                        conn.Open();
                        //Executando instrução sql
                        cmd.ExecuteNonQuery();
                        //Fechando conexão com o banco de dados
                        conn.Close();
                    }
                }
            }
        }           

        public void Atualizar(Comentario obj)
        {
            using (SqlConnection conn = new SqlConnection("@Iniatial Catalog= OpinemBem; Data Source=localhost; Integrated Security=SSPI;"))
            {
                string strSQL = @"UPDATE comentario set comentario = @comentario where id_comentario = @id_comentario;";
                {
                    using (SqlCommand cmd = new SqlCommand(strSQL))
                    {
                        cmd.Connection = conn;

                        cmd.Parameters.Add("@id_usuario", SqlDbType.VarChar).Value = obj.Usuario.Id;
                        cmd.Parameters.Add("@id_projeto", SqlDbType.VarChar).Value = obj.ProjetoDeLei.Id;
                        cmd.Parameters.Add("@data_comentario", SqlDbType.DateTime).Value = obj.DataHora;
                        cmd.Parameters.Add("@mensagem", SqlDbType.VarChar).Value = obj.Mensagem;

                        conn.Open();
                        cmd.ExecuteNonQuery();
                        conn.Close();

                    }
                }
            }
        }

        public void Deletar(Comentario obj)
        {
            using (SqlConnection conn = new SqlConnection(@"Initial Catalog=OpinemBem; Data Source=localhost; Integrated Security=SSPI;"))
            {
                string strSQL = @"DELETE FROM comentario where id_comentario = @id_comentario;";
                {
                    using (SqlCommand cmd = new SqlCommand(strSQL))
                    {
                        cmd.Connection = conn;
                        cmd.Parameters.Add("@id_comentario", SqlDbType.VarChar).Value = obj.Id;

                        conn.Open();
                        cmd.ExecuteNonQuery();
                        conn.Close();
                    }
                }
            }
        }

        public Comentario BuscarPorId(int id)
        {
            using (SqlConnection conn = new SqlConnection(@"Initial Catalog=OpinemBem; Data Source=localhost; Integrated Security=SSPI;"))
            {
                string strSQL = @"SELECT * FROM comentario where id_comentario = @id_comentario;";
                {
                    using (SqlCommand cmd = new SqlCommand(strSQL))
                    {
                        conn.Open();
                        cmd.Connection = conn;
                        cmd.CommandText = strSQL;

                        var dataReader = cmd.ExecuteReader();
                        var dt = new DataTable();
                        dt.Load(dataReader);

                        conn.Close();

                        if (!(dt != null && dt.Rows.Count > 0))
                            return null;

                        var row = dt.Rows[0];
                        var comentario = new Comentario()
                        {
                            Id = Convert.ToInt32(row["id_comentario"]),
                            Usuario = new Usuario() { Id = Convert.ToInt32(row["id_usuario"]) },
                            ProjetoDeLei = new ProjetoDeLei() { Id = Convert.ToInt32(row["id_projeto"]) },
                            DataHora = Convert.ToDateTime(row["data_comentario"]),
                            Mensagem = row["mensagem"].ToString()
                        };

                        return comentario;
                    }
                }
            }
        }

        public List<Comentario> BuscarTodos()
        {
            var lst = new List<Comentario>();
            {
                using (SqlConnection conn = new SqlConnection(@"Initial Catalog=OpinemBem; Data Source=localhost; Integrated Security=SSPI;"))
                {
                    string strSQL = @"SELECT * FROM comentario;";

                    using (SqlCommand cmd = new SqlCommand(strSQL))
                    {
                        conn.Open();
                        cmd.Connection = conn;
                        cmd.CommandText = strSQL;

                        var dataReader = cmd.ExecuteReader();
                        var dt = new DataTable();
                        dt.Load(dataReader);

                        conn.Close();

                        foreach (DataRow row in dt.Rows)
                        {
                            var Comentario = new Comentario()
                            {
                                Id = Convert.ToInt32(row["id_comentario"]),
                                Usuario = new Usuario() { Id = Convert.ToInt32(row["id_usuario"]) },
                                ProjetoDeLei = new ProjetoDeLei() { Id = Convert.ToInt32(row["id_projeto"])},
                                DataHora = Convert.ToDateTime(row["data_comentario"]),
                                Mensagem = row["mensagem"].ToString()                            
                            };
                            lst.Add(Comentario);
                        }
                    }
                }
                return lst;
            }
        }
    }
}
