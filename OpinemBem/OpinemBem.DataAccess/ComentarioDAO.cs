using OpinemBem.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace OpinemBem.DataAccess
{
    public class ComentarioDAO
    {
        public void Inserir(Comentario obj)
        {
            //Criando uma conexão com o banco de dados
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Db"].ConnectionString))
            {
                //Criando instrução sql para inserir na tabela de categorias
                string strSQL = "INSERT INTO comentario (id_usuario, id_projeto, data_comentario, mensagem) VALUES (@id_usuario, @id_projeto, @data_comentario, @mensagem);";
                {
                    //Criando um comando sql que será executado na base de dados
                    using (SqlCommand cmd = new SqlCommand(strSQL))
                    {
                        cmd.Connection = conn;
                        //Preenchendo os parâmetros da instrução sql
                        cmd.Parameters.Add("@id_usuario", SqlDbType.Int).Value = obj.Usuario.Id;
                        cmd.Parameters.Add("@id_projeto", SqlDbType.Int).Value = obj.ProjetoDeLei.Id;
                        cmd.Parameters.Add("@data_comentario", SqlDbType.DateTime).Value = obj.DataHora;
                        cmd.Parameters.Add("@mensagem", SqlDbType.VarChar).Value = obj.Mensagem;

                        foreach (SqlParameter parameter in cmd.Parameters)
                        {
                            if (parameter.Value == null)
                            {
                                parameter.Value = DBNull.Value;
                            }
                        }

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
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Db"].ConnectionString))
            {
                string strSQL = @"UPDATE comentario set comentario = @comentario where id_comentario = @id_comentario;";
                {
                    using (SqlCommand cmd = new SqlCommand(strSQL))
                    {
                        cmd.Connection = conn;

                        cmd.Parameters.Add("@id_usuario", SqlDbType.Int).Value = obj.Usuario.Id;
                        cmd.Parameters.Add("@id_projeto", SqlDbType.Int).Value = obj.ProjetoDeLei.Id;
                        cmd.Parameters.Add("@data_comentario", SqlDbType.DateTime).Value = obj.DataHora;
                        cmd.Parameters.Add("@mensagem", SqlDbType.VarChar).Value = obj.Mensagem;

                        foreach (SqlParameter parameter in cmd.Parameters)
                        {
                            if (parameter.Value == null)
                            {
                                parameter.Value = DBNull.Value;
                            }
                        }

                        conn.Open();
                        cmd.ExecuteNonQuery();
                        conn.Close();

                    }
                }
            }
        }

        public void Deletar(Comentario obj)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Db"].ConnectionString))
            {
                string strSQL = @"DELETE FROM comentario where id_comentario = @id_comentario;";
                {
                    using (SqlCommand cmd = new SqlCommand(strSQL))
                    {
                        cmd.Connection = conn;
                        cmd.Parameters.Add("@id_comentario", SqlDbType.Int).Value = obj.Id;

                        conn.Open();
                        cmd.ExecuteNonQuery();
                        conn.Close();
                    }
                }
            }
        }

        public Comentario BuscarPorId(int id)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Db"].ConnectionString))
            {
                string strSQL = @"SELECT c.*, u.nome as nome_usuario 
                                  FROM comentario c
                                  INNER JOIN usuario u ON  (c.id_usuario = u.id_usuario)
                                  WHERE c.id_comentario = @id_comentario;";
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
                            Usuario = new Usuario()
                            {
                                Id = Convert.ToInt32(row["id_usuario"]),
                                Nome = row["nome_usuario"].ToString()
                            },
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
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Db"].ConnectionString))
                {
                    string strSQL = @"SELECT c.*, u.nome as nome_usuario 
                                      FROM comentario c
                                      INNER JOIN usuario u ON  (c.id_usuario = u.id_usuario);";

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
                                Usuario = new Usuario()
                                {
                                    Id = Convert.ToInt32(row["id_usuario"]),
                                    Nome = row["nome_usuario"].ToString()
                                },
                                ProjetoDeLei = new ProjetoDeLei() { Id = Convert.ToInt32(row["id_projeto"]) },
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

        public List<Comentario> BuscarPorProjeto(int projetoDeLei)
        {
            var lst = new List<Comentario>();
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Db"].ConnectionString))
                {
                    string strSQL = @"SELECT c.*, u.nome as nome_usuario 
                                      FROM comentario c
                                      INNER JOIN usuario u ON  (c.id_usuario = u.id_usuario)
                                      WHERE c.id_projeto = @id_projeto;";

                    using (SqlCommand cmd = new SqlCommand(strSQL))
                    {
                        conn.Open();
                        cmd.Connection = conn;
                        cmd.Parameters.Add("@id_projeto", SqlDbType.Int).Value = projetoDeLei;
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
                                Usuario = new Usuario()
                                {
                                    Id = Convert.ToInt32(row["id_usuario"]),
                                    Nome = row["nome_usuario"].ToString()
                                },
                                ProjetoDeLei = new ProjetoDeLei() { Id = Convert.ToInt32(row["id_projeto"]) },
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
