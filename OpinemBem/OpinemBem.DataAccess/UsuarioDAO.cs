using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using OpinemBem.Models;

namespace OpinemBem.DataAccess
{
    public class UsuarioDAO
    {
        public void Inserir(Usuario obj)
        {
            using (SqlConnection conn = new SqlConnection(@"Initial Catalog=OpinemBem; Data Source=localhost; Integrated Security=SSPI;"))
            {
                string strSQL = "INSERT INTO usuario (nome, cpf, email, senha, data_nasc, admministrador, foto, caminho_foto, sexo) VALUES ( @nome, @cpf, @email, @senha, @data_nasc, @admministrador, @foto, @caminho_foto, @sexo);";
                {
                    using (SqlCommand cmd = new SqlCommand(strSQL))
                    {
                        cmd.Connection = conn;

                        cmd.Parameters.Add("@nome", SqlDbType.VarChar).Value = obj.Nome;
                        cmd.Parameters.Add("@cpf", SqlDbType.VarChar).Value = obj.CPF;
                        cmd.Parameters.Add("@email", SqlDbType.VarChar).Value = obj.Email;
                        cmd.Parameters.Add("@senha", SqlDbType.VarChar).Value = obj.Senha;
                        cmd.Parameters.Add("@data_nasc", SqlDbType.DateTime).Value = obj.DataNasc;
                        cmd.Parameters.Add("@administrador", SqlDbType.Bit).Value = obj.Administrador;
                        cmd.Parameters.Add("@foto", SqlDbType.VarChar).Value = obj.Foto;
                        cmd.Parameters.Add("@caminho_foto", SqlDbType.VarChar).Value = obj.CaminhoFoto;
                        cmd.Parameters.Add("@sexo", SqlDbType.Int).Value = obj.Sexo;

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
                string strSQL = @"UPDATE usuario set usuario = @usuario where id_usuario = @id_usuario;";
                {
                    using (SqlCommand cmd = new SqlCommand(strSQL))
                    {
                        cmd.Connection = conn;

                        cmd.Parameters.Add("@nome", SqlDbType.VarChar).Value = obj.Nome;
                        cmd.Parameters.Add("@cpf", SqlDbType.VarChar).Value = obj.CPF;
                        cmd.Parameters.Add("@email", SqlDbType.VarChar).Value = obj.Email;
                        cmd.Parameters.Add("@senha", SqlDbType.VarChar).Value = obj.Senha;
                        cmd.Parameters.Add("@data_nasc", SqlDbType.DateTime).Value = obj.DataNasc;
                        cmd.Parameters.Add("@administrador", SqlDbType.Bit).Value = obj.Administrador;
                        cmd.Parameters.Add("@foto", SqlDbType.VarChar).Value = obj.Foto;
                        cmd.Parameters.Add("@caminho_foto", SqlDbType.VarChar).Value = obj.CaminhoFoto;
                        cmd.Parameters.Add("@sexo", SqlDbType.Int).Value = obj.Sexo;

                        conn.Open();
                        cmd.ExecuteNonQuery();
                        conn.Close();
                    }
                }
            }
        }

        public void Deletar(Usuario obj)
        {
            using (SqlConnection conn = new SqlConnection("@Initial Catalog=OpinemBem; Data Source=localhost; Integrated Security=SSPI;"))
            {
                string strSQL = @"DELETE FROM usuario where id_usuario = @id_usuario";
                {
                    using (SqlCommand cmd = new SqlCommand(strSQL))
                    {
                        cmd.Connection = conn;

                        cmd.Parameters.Add("@id_usuario", SqlDbType.VarChar).Value = obj.Id;

                        conn.Open();
                        cmd.ExecuteNonQuery();
                        conn.Close();
                    }
                }
            }
        }

        public Usuario BuscarPorId(int id)
        {
            using (SqlConnection conn = new SqlConnection(@"Initial Catalog=OpinemBem; Data Source=localhost; Integrated Security=SSPI"))
            {
                string strSQL = "SELECT * FROM usuario where id_usuario = @id_usuario;";
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
                        var usuario = new Usuario()
                        {
                            Id = Convert.ToInt32(row["id_usuario"]),
                            Nome = row["nome"].ToString(),
                            CPF = row["cpf"].ToString(),
                            Email = row["email"].ToString(),
                            Senha = row["senha"].ToString(),
                            DataNasc = Convert.ToDateTime(row["data_nasc"]),
                            Administrador = Convert.ToBoolean(row["administrador"]),
                            Foto = row["foto"].ToString(),
                            CaminhoFoto = row["caminho_foto"].ToString(),
                            Sexo = (Sexo)Convert.ToInt32(row["sexo"])
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
                using (SqlConnection conn = new SqlConnection(@"Initial Catalog=OpinemBem; Data Source=localhost; Integrated Security=SSPI;"))
                {
                    string strSQL = @"SELECT * FROM usuario;";

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
                                DataNasc = Convert.ToDateTime(row["data_nasc"]),
                                Administrador = Convert.ToBoolean(row["administrador"]),
                                Foto = row["foto"].ToString(),
                                CaminhoFoto = row["caminho_foto"].ToString(),
                                Sexo = (Sexo)Convert.ToInt32(row["sexo"])
                            };

                            lst.Add(usuario);
                        }
                    }
                }
                return lst;
            }
        }
    }
}
