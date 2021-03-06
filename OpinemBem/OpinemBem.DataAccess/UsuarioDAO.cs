﻿using OpinemBem.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace OpinemBem.DataAccess
{
    public class UsuarioDAO
    {
        public void Inserir(Usuario obj)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Db"].ConnectionString))
            {
                string strSQL = @"INSERT INTO usuario (nome, cpf, email, senha, data_nasc, administrador, foto, sexo, id_estado, id_cidade) 
                                  VALUES ( @nome, @cpf, @email, @senha, @data_nasc, @administrador, @foto, @sexo, @id_estado, @id_cidade);";
                {
                    using (SqlCommand cmd = new SqlCommand(strSQL))
                    {
                        cmd.Connection = conn;
                        cmd.Parameters.Add("@nome", SqlDbType.VarChar).Value = obj.Nome;
                        cmd.Parameters.Add("@cpf", SqlDbType.VarChar).Value = obj.CPF;
                        cmd.Parameters.Add("@email", SqlDbType.VarChar).Value = obj.Email;
                        cmd.Parameters.Add("@senha", SqlDbType.VarChar).Value = obj.Senha;
                        cmd.Parameters.Add("@data_nasc", SqlDbType.DateTime).Value = obj.DataNasc.HasValue ? obj.DataNasc : DateTime.Now;
                        cmd.Parameters.Add("@administrador", SqlDbType.Bit).Value = obj.Administrador;
                        cmd.Parameters.Add("@foto", SqlDbType.VarChar).Value = obj.Foto;
                        cmd.Parameters.Add("@sexo", SqlDbType.Int).Value = obj.Sexo.HasValue ? (object)Convert.ToInt32(obj.Sexo) : DBNull.Value;
                        cmd.Parameters.Add("@id_cidade", SqlDbType.Int).Value = obj.Cidade.Id;
                        cmd.Parameters.Add("@id_estado", SqlDbType.Int).Value = obj.Estado.Id;

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

        public void Atualizar(Usuario obj)
        {
            using (SqlConnection conn = new SqlConnection("@Initial Catalog=OpinemBem; Data Source=localhost; Integrated Security=SSPI"))
            {
                string strSQL = @"UPDATE usuario SET 
                                    nome = @nome, 
                                    cpf = @cpf, 
                                    email = @email, 
                                    senha = @senha, 
                                    data_nasc = @data_nasc, 
                                    administrador = @administrador, 
                                    foto = @foto, 
                                    sexo = @sexo,
                                    id_estado = @id_estado,
                                    id_cidade = id_cidade
                                WHERE id_usuario = @id_usuario;";
                {
                    using (SqlCommand cmd = new SqlCommand(strSQL))
                    {
                        cmd.Connection = conn;
                        cmd.Parameters.Add("@nome", SqlDbType.VarChar).Value = obj.Nome;
                        cmd.Parameters.Add("@cpf", SqlDbType.VarChar).Value = obj.CPF;
                        cmd.Parameters.Add("@email", SqlDbType.VarChar).Value = obj.Email;
                        cmd.Parameters.Add("@senha", SqlDbType.VarChar).Value = obj.Senha;
                        cmd.Parameters.Add("@data_nasc", SqlDbType.DateTime).Value = obj.DataNasc.HasValue ? obj.DataNasc : DateTime.Now;
                        cmd.Parameters.Add("@administrador", SqlDbType.Bit).Value = obj.Administrador;
                        cmd.Parameters.Add("@foto", SqlDbType.VarChar).Value = obj.Foto;
                        cmd.Parameters.Add("@sexo", SqlDbType.Int).Value = obj.Sexo.HasValue ? (object)Convert.ToInt32(obj.Sexo) : DBNull.Value;
                        cmd.Parameters.Add("@id_cidade", SqlDbType.Int).Value = obj.Cidade.Id;
                        cmd.Parameters.Add("@id_estado", SqlDbType.Int).Value = obj.Estado.Id;

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

        public void Deletar(Usuario obj)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Db"].ConnectionString))
            {
                string strSQL = @"DELETE FROM usuario where id_usuario = @id_usuario";
                {
                    using (SqlCommand cmd = new SqlCommand(strSQL))
                    {
                        cmd.Connection = conn;
                        cmd.Parameters.Add("@id_usuario", SqlDbType.VarChar).Value = obj.Id;
                        cmd.CommandText = strSQL;

                        conn.Open();
                        cmd.ExecuteNonQuery();
                        conn.Close();
                    }
                }
            }
        }

        public Usuario BuscarPorId(int id)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Db"].ConnectionString))
            {
                string strSQL = @"SELECT U.*, E.NOME AS NOME_ESTADO, C.NOME AS NOME_CIDADE 
                                  FROM USUARIO U
                                  LEFT JOIN ESTADO E ON (E.ID_ESTADO = U.ID_ESTADO)
                                  LEFT JOIN CIDADE C ON(C.ID_CIDADE = U.ID_CIDADE) 
                                  WHERE ID_USUARIO = @ID_USUARIO;";
                {
                    using (SqlCommand cmd = new SqlCommand(strSQL))
                    {
                        conn.Open();
                        cmd.Connection = conn;
                        cmd.Parameters.Add("@id_usuario", SqlDbType.VarChar).Value = id;
                        cmd.CommandText = strSQL;

                        var dataReader = cmd.ExecuteReader();
                        var dt = new DataTable();
                        dt.Load(dataReader);
                        conn.Close();

                        if (!(dt != null && dt.Rows.Count > 0))
                            return null;

                        var row = dt.Rows[0];
                        var usuario = new Usuario()
                        {
                            Id = Convert.ToInt32(row["id_usuario"]),
                            Nome = row["nome"].ToString(),
                            CPF = row["cpf"].ToString(),
                            Email = row["email"].ToString(),
                            Senha = row["senha"].ToString(),
                            DataNasc = row["data_nasc"] is DBNull ? new Nullable<DateTime>() : Convert.ToDateTime(row["data_nasc"]),
                            Administrador = Convert.ToBoolean(row["administrador"]),
                            Foto = row["foto"].ToString(),
                            Sexo = row["sexo"] is DBNull ? new Nullable<Sexo>() : (Sexo)Convert.ToInt32(row["sexo"]),
                            Estado = row["id_estado"] is DBNull ? null : new Estado()
                            {
                                Id = Convert.ToInt32(row["id_estado"]),
                                Nome = row["nome_estado"].ToString()
                            },
                            Cidade = row["id_cidade"] is DBNull ? null : new Cidade()
                            {
                                Id = Convert.ToInt32(row["id_cidade"]),
                                Nome = row["nome_cidade"].ToString()
                            }
                        };

                        return usuario;
                    }
                }
            }
        }

        public List<Usuario> BuscarTodos()
        {
            var lst = new List<Usuario>();
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Db"].ConnectionString))
                {
                    string strSQL = @"SELECT U.*, E.NOME AS NOME_ESTADO, C.NOME AS NOME_CIDADE 
                                      FROM USUARIO U
                                      LEFT JOIN ESTADO E ON (E.ID_ESTADO = U.ID_ESTADO)
                                      LEFT JOIN CIDADE C ON (C.ID_CIDADE = U.ID_CIDADE);";

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
                            var usuario = new Usuario()
                            {
                                Id = Convert.ToInt32(row["id_usuario"]),
                                Nome = row["nome"].ToString(),
                                CPF = row["cpf"].ToString(),
                                Email = row["email"].ToString(),
                                Senha = row["senha"].ToString(),
                                DataNasc = row["data_nasc"] is DBNull ? new Nullable<DateTime>() : Convert.ToDateTime(row["data_nasc"]),
                                Administrador = Convert.ToBoolean(row["administrador"]),
                                Foto = row["foto"].ToString(),
                                Sexo = row["sexo"] is DBNull ? new Nullable<Sexo>() : (Sexo)Convert.ToInt32(row["sexo"]),
                                Estado = row["id_estado"] is DBNull ? null : new Estado()
                                {
                                    Id = Convert.ToInt32(row["id_estado"]),
                                    Nome = row["NOME_ESTADO"].ToString()
                                },
                                Cidade = row["id_cidade"] is DBNull ? null : new Cidade()
                                {
                                    Id = Convert.ToInt32(row["id_cidade"]),
                                    Nome = row["NOME_CIDADE"].ToString()
                                }
                            };

                            lst.Add(usuario);
                        }
                    }
                }
                return lst;
            }
        }

        public Usuario Logar(Usuario obj)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Db"].ConnectionString))
            {
                //admin igual a 0 pq nao é um admin
                string strSQL = @"SELECT U.*, E.NOME AS NOME_ESTADO, C.NOME AS NOME_CIDADE 
                                  FROM USUARIO U
                                  LEFT JOIN ESTADO E ON (E.ID_ESTADO = U.ID_ESTADO)
                                  LEFT JOIN CIDADE C ON (C.ID_CIDADE = C.ID_CIDADE)
                                  WHERE U.ADMINISTRADOR = 0 AND U.CPF = @CPF AND U.SENHA = @SENHA;";

                using (SqlCommand cmd = new SqlCommand(strSQL))
                {
                    conn.Open();
                    cmd.Connection = conn;
                    cmd.Parameters.Add("@CPF", SqlDbType.VarChar).Value = obj.CPF;
                    cmd.Parameters.Add("@SENHA", SqlDbType.VarChar).Value = obj.Senha;
                    cmd.CommandText = strSQL;

                    var dataReader = cmd.ExecuteReader();
                    var dt = new DataTable();
                    dt.Load(dataReader);
                    conn.Close();

                    if (!(dt != null && dt.Rows.Count > 0))
                        return null;

                    var row = dt.Rows[0];
                    var usuario = new Usuario()
                    {
                        Id = Convert.ToInt32(row["id_usuario"]),
                        Nome = row["nome"].ToString(),
                        CPF = row["cpf"].ToString(),
                        Email = row["email"].ToString(),
                        Senha = row["senha"].ToString(),
                        DataNasc = row["data_nasc"] is DBNull ? new Nullable<DateTime>() : Convert.ToDateTime(row["data_nasc"]),
                        Administrador = Convert.ToBoolean(row["administrador"]),
                        Foto = row["foto"].ToString(),
                        Sexo = row["sexo"] is DBNull ? new Nullable<Sexo>() : (Sexo)Convert.ToInt32(row["sexo"]),
                        Estado = row["id_estado"] is DBNull ? null : new Estado()
                        {
                            Id = Convert.ToInt32(row["id_estado"]),
                            Nome = row["nome_estado"].ToString()
                        },
                        Cidade = row["id_cidade"] is DBNull ? null : new Cidade()
                        {
                            Id = Convert.ToInt32(row["id_cidade"]),
                            Nome = row["nome_cidade"].ToString()
                        }
                    };

                    return usuario;
                }
            }
        }

        public Usuario LogarAdm(Usuario obj)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Db"].ConnectionString))
            {
                //admmin diferente de 0 = admin
                string strSQL = @"SELECT U.*, E.NOME AS NOME_ESTADO, C.NOME AS NOME_CIDADE 
                                  FROM USUARIO U
                                  LEFT JOIN ESTADO E ON (E.ID_ESTADO = U.ID_ESTADO)
                                  LEFT JOIN CIDADE C ON (C.ID_CIDADE = C.ID_CIDADE)
                                  WHERE U.ADMINISTRADOR <> 0 AND U.CPF = @CPF AND U.SENHA = @SENHA;";

                using (SqlCommand cmd = new SqlCommand(strSQL))
                {
                    conn.Open();
                    cmd.Connection = conn;
                    cmd.Parameters.Add("@CPF", SqlDbType.VarChar).Value = obj.CPF;
                    cmd.Parameters.Add("@SENHA", SqlDbType.VarChar).Value = obj.Senha;
                    cmd.CommandText = strSQL;

                    var dataReader = cmd.ExecuteReader();
                    var dt = new DataTable();
                    dt.Load(dataReader);
                    conn.Close();

                    if (!(dt != null && dt.Rows.Count > 0))
                        return null;

                    var row = dt.Rows[0];
                    var usuario = new Usuario()
                    {
                        Id = Convert.ToInt32(row["id_usuario"]),
                        Nome = row["nome"].ToString(),
                        CPF = row["cpf"].ToString(),
                        Email = row["email"].ToString(),
                        Senha = row["senha"].ToString(),
                        DataNasc = row["data_nasc"] is DBNull ? new Nullable<DateTime>() : Convert.ToDateTime(row["data_nasc"]),
                        Administrador = Convert.ToBoolean(row["administrador"]),
                        Foto = row["foto"].ToString(),
                        Sexo = row["sexo"] is DBNull ? new Nullable<Sexo>() : (Sexo)Convert.ToInt32(row["sexo"]),
                        Estado = row["id_estado"] is DBNull ? null : new Estado()
                        {
                            Id = Convert.ToInt32(row["id_estado"]),
                            Nome = row["nome_estado"].ToString()
                        },
                        Cidade = row["id_cidade"] is DBNull ? null : new Cidade()
                        {
                            Id = Convert.ToInt32(row["id_cidade"]),
                            Nome = row["nome_cidade"].ToString()
                        }
                    };

                    return usuario;
                }
            }
        }
    }
}
