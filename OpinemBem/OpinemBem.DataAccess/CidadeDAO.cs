using OpinemBem.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace OpinemBem.DataAccess
{
    public class CidadeDAO
    {
        public void Inserir(Cidade obj)
        {
            //Criando uma conexão com o banco de dados
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Db"].ConnectionString))
            {
                //Criando instrução sql para inserir na tabela de cidade
                string strSQL = @"INSERT INTO cidade (nome, id_estado) VALUES (@nome, @id_estado);";

                //Criando um comando sql que será executado na base de dados
                using (SqlCommand cmd = new SqlCommand(strSQL))
                {
                    cmd.Connection = conn;
                    //Preenchendo os parâmetros da instrução sql
                    cmd.Parameters.Add("@nome", SqlDbType.VarChar).Value = obj.Nome;
                    cmd.Parameters.Add("@id_estado", SqlDbType.Int).Value = obj.Estado.Id;

                    //Abrindo conexão com o banco de dados
                    conn.Open();
                    //Executando instrução sql
                    cmd.ExecuteNonQuery();
                    //Fechando conexão com o banco de dados
                    conn.Close();
                }
            }
        }

        public void Atualizar(Cidade obj)
        {
            //Criando uma conexão com o banco de dados
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Db"].ConnectionString))
            {
                //Criando instrução sql para inserir na tabela de cidade
                string strSQL = @"UPDATE cidade set nome = @nome where id_cidade = @id_cidade;";

                //Criando um comando sql que será executado na base de dados
                using (SqlCommand cmd = new SqlCommand(strSQL))
                {
                    cmd.Connection = conn;
                    //Preenchendo os parâmetros da instrução sql
                    cmd.Parameters.Add("@nome", SqlDbType.VarChar).Value = obj.Nome;
                    cmd.Parameters.Add("@id_cidade", SqlDbType.Int).Value = obj.Id;

                    //Abrindo conexão com o banco de dados
                    conn.Open();
                    //Executando instrução sql
                    cmd.ExecuteNonQuery();
                    //Fechando conexão com o banco de dados
                    conn.Close();
                }
            }
        }

        public void Deletar(Cidade obj)
        {
            //Criando uma conexão com o banco de dados
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Db"].ConnectionString))
            {
                //Criando instrução sql para inserir na tabela de cidade
                string strSQL = @"DELETE FROM cidade where id_cidade = @id_cidade;";

                //Criando um comando sql que será executado na base de dados
                using (SqlCommand cmd = new SqlCommand(strSQL))
                {
                    cmd.Connection = conn;
                    //Preenchendo os parâmetros da instrução sql
                    cmd.Parameters.Add("@id_cidade", SqlDbType.Int).Value = obj.Id;

                    //Abrindo conexão com o banco de dados
                    conn.Open();
                    //Executando instrução sql
                    cmd.ExecuteNonQuery();
                    //Fechando conexão com o banco de dados
                    conn.Close();
                }
            }
        }

        public Cidade BuscarPorId(int id)
        {
            //Criando uma conexão com o banco de dados
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Db"].ConnectionString))
            {
                //Criando instrução sql para selecionar todos os registros na tabela de cidade
                string strSQL = @"SELECT 
                                        C.*,
                                        E.NOME AS NOME_ESTADO
                                 FROM CIDADE C
                                 INNER JOIN ESTADO E ON (E.ID_ESTADO = C.ID_ESTADO)
                                 WHERE C.ID_CIDADE = @ID_CIDADE;";

                //Criando um comando sql que será executado na base de dados
                using (SqlCommand cmd = new SqlCommand(strSQL))
                {
                    //Abrindo conexão com o banco de dados
                    conn.Open();
                    cmd.Connection = conn;
                    cmd.Parameters.Add("@id_cidade", SqlDbType.Int).Value = id;
                    cmd.CommandText = strSQL;

                    //Executando instrução sql
                    var dataReader = cmd.ExecuteReader();
                    var dt = new DataTable();
                    dt.Load(dataReader);
                    //Fechando conexão com o banco de dados
                    conn.Close();

                    if (!(dt != null && dt.Rows.Count > 0))
                        return null;

                    var row = dt.Rows[0];
                    var cidade = new Cidade()
                    {
                        Id = Convert.ToInt32(row["id_cidade"]),
                        Nome = row["nome"].ToString(),
                        Estado = new Estado() { Id = Convert.ToInt32(row["id_estado"]) }
                    };

                    return cidade;
                }
            }
        }

        public List<Cidade> BuscarTodos()
        {
            var lst = new List<Cidade>();

            //Criando uma conexão com o banco de dados
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Db"].ConnectionString))
            {
                //Criando instrução sql para selecionar todos os registros na tabela de cidade
                string strSQL = @"SELECT 
                                        C.*,
                                        E. NOME AS NOME_ESTADO
                                        FROM CIDADE C
                                        INNER JOIN ESTADO E ON (E.ID_ESTADO = C.ID_ESTADO);";

                //Criando um comando sql que será executado na base de dados
                using (SqlCommand cmd = new SqlCommand(strSQL))
                {
                    //Abrindo conexão com o banco de dados
                    conn.Open();
                    cmd.Connection = conn;
                    cmd.CommandText = strSQL;
                    //Executando instrução sql
                    var dataReader = cmd.ExecuteReader();
                    var dt = new DataTable();
                    dt.Load(dataReader);
                    //Fechando conexão com o banco de dados
                    conn.Close();

                    //Percorrendo todos os registros encontrados na base de dados e adicionando em uma lista
                    foreach (DataRow row in dt.Rows)
                    {
                        var cidade = new Cidade()
                        {
                            Id = Convert.ToInt32(row["id_cidade"]),
                            Nome = row["nome"].ToString(),
                            Estado = row["id_estado"] is DBNull ? null : new Estado()
                            {
                                Id = Convert.ToInt32(row["id_estado"]),
                                Nome = row["nome"].ToString()
                            }
                        };

                        lst.Add(cidade);
                    }
                }
            }
            return lst;
        }

        public List<Cidade> BuscarPorUF(int uf)
        {
            var lst = new List<Cidade>();

            //Criando uma conexão com o banco de dados
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Db"].ConnectionString))
            {
                //Criando instrução sql para selecionar todos os registros na tabela de cidade
                string strSQL = @"SELECT * FROM cidade where id_estado = @id_estado;";

                //Criando um comando sql que será executado na base de dados
                using (SqlCommand cmd = new SqlCommand(strSQL))
                {
                    //Abrindo conexão com o banco de dados
                    conn.Open();
                    cmd.Connection = conn;
                    cmd.Parameters.Add("@id_estado", SqlDbType.Int).Value = uf;
                    cmd.CommandText = strSQL;

                    //Executando instrução sql
                    var dataReader = cmd.ExecuteReader();
                    var dt = new DataTable();
                    dt.Load(dataReader);
                    //Fechando conexão com o banco de dados
                    conn.Close();

                    //Percorrendo todos os registros encontrados na base de dados e adicionando em uma lista
                    foreach (DataRow row in dt.Rows)
                    {
                        var Cidade = new Cidade()
                        {
                            Id = Convert.ToInt32(row["id_cidade"]),
                            Nome = row["nome"].ToString(),
                            Estado = new Estado() { Id = Convert.ToInt32(row["id_estado"]) }
                        };

                        lst.Add(Cidade);
                    }
                }
            }
            return lst;
        }
    }
}
