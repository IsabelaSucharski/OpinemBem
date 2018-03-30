﻿using OpinemBem.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace OpinemBem.DataAccess
{
    public class ProjetoDeLeiDAO
    {
        public void Inserir(ProjetoDeLei obj)
        {
            using (SqlConnection conn = new SqlConnection(@"Initial Catalog=OpinemBem; Data Source=localhost; Integrated Security=SSPI"))
            {
                string strSQL = "INSERT INTO projeto_de_lei (nome, id_categoria, id_usuario, descricao, vantagens, desvantagens) VALUES (@nome, @id_categoria, @id_usuario, @descricao, @vantagens, @desvantagens);";
                {
                    using (SqlCommand cmd = new SqlCommand(strSQL))
                    {
                        cmd.Connection = conn;

                        cmd.Parameters.Add("@nome", SqlDbType.VarChar).Value = obj.Nome;
                        cmd.Parameters.Add("@id_categoria", SqlDbType.VarChar).Value = obj.Categoria.Id;
                        cmd.Parameters.Add("@id_usuario", SqlDbType.VarChar).Value = obj.Usuario.Id;
                        cmd.Parameters.Add("@descricao", SqlDbType.VarChar).Value = obj.Descricao;
                        cmd.Parameters.Add("@vantagens", SqlDbType.VarChar).Value = obj.Vantagens;
                        cmd.Parameters.Add("@desvantagens", SqlDbType.VarChar).Value = obj.Desvantagens;

                        conn.Open();
                        cmd.ExecuteNonQuery();
                        conn.Close();
                    }
                }
            }
        }

        public void Atualizar(ProjetoDeLei obj)
        {
            using (SqlConnection conn = new SqlConnection(@"Initial Catalog=OpinemBem; Data Source=localhost; Integrated Security=SSPI"))
            {
                string strSQL = "UPDATE projeto_de_lei set projeto_de_lei = @projeto_de_lei where id_projeto = @id_projeto;";
                {
                    using (SqlCommand cmd = new SqlCommand(strSQL))
                    {
                        cmd.Connection = conn;

                        cmd.Parameters.Add("@nome", SqlDbType.VarChar).Value = obj.Nome;
                        cmd.Parameters.Add("@id_categoria", SqlDbType.VarChar).Value = obj.Categoria.Id;
                        cmd.Parameters.Add("@id_usuario", SqlDbType.VarChar).Value = obj.Usuario.Id;
                        cmd.Parameters.Add("@descricao", SqlDbType.VarChar).Value = obj.Descricao;
                        cmd.Parameters.Add("@vantagens", SqlDbType.VarChar).Value = obj.Vantagens;
                        cmd.Parameters.Add("@desvantagens", SqlDbType.VarChar).Value = obj.Desvantagens;

                        conn.Open();
                        cmd.ExecuteNonQuery();
                        conn.Close();

                    }
                }
            }
        }

        public void Deletar(ProjetoDeLei obj)
        {
            using (SqlConnection conn = new SqlConnection(@"Initial catalog=OpinemBem; Data Source=localhost; Integrated Security=SSPI;"))
            {
                string strSQL = @"DELETE FROM  projeto_de_lei where id_projeto = @id_projeto;";
                {
                    using (SqlCommand cmd = new SqlCommand(strSQL))
                    {
                        cmd.Connection = conn;
                        cmd.Parameters.Add("@id_projeto", SqlDbType.VarChar).Value = obj.Id;

                        conn.Open();
                        cmd.ExecuteNonQuery();
                        conn.Close();
                    }
                }
            }
        }

        public ProjetoDeLei BuscarPorId(int id)
        {
            using (SqlConnection conn = new SqlConnection(@"Initial Catalog=OpinemBem; Data Source=localhost; Integrated Security=SSPI"))
            {
                string strSQL = "SELECT * FROM projetos_de_lei where id_projeto = @id_projeto;";
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
                        var projeto = new ProjetoDeLei()
                        {
                            Id = Convert.ToInt32(row["id_projeto"]),
                            Nome = row["nome"].ToString(),
                            Categoria = new Categoria() { Id = Convert.ToInt32(row["id_categoria"]) },
                            Usuario = new Usuario() { Id = Convert.ToInt32(row["id_usuario"]) },
                            Descricao = row["desvantagens"].ToString(),
                            Vantagens = row["vantagens"].ToString(),
                            Desvantagens = row["desvantagens"].ToString()
                        };
                        return projeto;
                    }
                }
            }
        }

        public List<ProjetoDeLei> BuscarTodos()
        {
            var lst = new List<ProjetoDeLei>();
            {
                using (SqlConnection conn = new SqlConnection(@"Initial Catalog=OpinemBem; Data Source=localhost; Integrated Security=SSPI;"))
                {
                    string strSQL = @"SELECT * FROM projeto_de_lei;";

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
                            var projetoDeLei = new ProjetoDeLei()
                            {
                                Id = Convert.ToInt32(row["id_projeto"]),
                                Nome = row["nome"].ToString(),
                                Categoria = new Categoria() { Id = Convert.ToInt32(row["id_categoria"]) },
                                Usuario = new Usuario() { Id = Convert.ToInt32(row["id_usuario"]) },
                                Descricao = row["desvantagens"].ToString(),
                                Vantagens = row["vantagens"].ToString(),
                                Desvantagens = row["desvantagens"].ToString()
                            };

                            lst.Add(projetoDeLei);
                        }
                    }
                }

                return lst;
            }
        }
    }
}