using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace ApnCore_CrudExemplo.Models
{
    public class FuncionarioDAL : IFuncionarioDAL
    {
        string connectionString = @"Password=abc123;Persist Security Info=True;User ID=sa;Initial Catalog=CadastroDB;Data Source=JULIOW\SQLEXPRESS";
        //Password=millennium_qas3;Persist Security Info=True;User ID=millennium_qas3;Initial Catalog=millennium_qas3;Data Source=172.26.225.252
        //Password=abc123;Persist Security Info=True;User ID=sa;Initial Catalog=master;Data Source=JULIOW\SQLEXPRESS
        public IEnumerable<Funcionario> GetAllFuncionarios()
        {
            List<Funcionario> lstfuncionario = new List<Funcionario>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT FuncionarioId, Nome,Cidade, Departamento, Sexo from Funcionarios", con);
                cmd.CommandType = CommandType.Text;

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    Funcionario funcionario = new Funcionario();
                    funcionario.FuncionarioId = Convert.ToInt32(rdr["FuncionarioId"]);
                    funcionario.Nome = rdr["Nome"].ToString();
                    funcionario.Cidade = rdr["Cidade"].ToString();
                    funcionario.Departamento = rdr["Departamento"].ToString();
                    funcionario.Sexo = rdr["Sexo"].ToString();
                    lstfuncionario.Add(funcionario);
                }
                con.Close();
            }
            return lstfuncionario;
        }
        public void AddFuncionario(Funcionario funcionario)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string comandoSQL = "Insert into dbo.Funcionarios (Nome,Cidade,Departamento,Sexo) Values(@Nome, @Cidade, @Departamento, @Sexo)";
                SqlCommand cmd = new SqlCommand(comandoSQL, con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@Nome", funcionario.Nome);
                cmd.Parameters.AddWithValue("@Cidade", funcionario.Cidade);
                cmd.Parameters.AddWithValue("@Departamento", funcionario.Departamento);
                cmd.Parameters.AddWithValue("@Sexo", funcionario.Sexo);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }
        public void UpdateFuncionario(Funcionario funcionario)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string comandoSQL = "Update Funcionarios set Nome = @Nome, Cidade = @Cidade, Departamento = @Departamento, Sexo = @Sexo where FuncionarioId = @FuncionarioId";
                SqlCommand cmd = new SqlCommand(comandoSQL, con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@FuncionarioId", funcionario.FuncionarioId);
                cmd.Parameters.AddWithValue("@Nome", funcionario.Nome);
                cmd.Parameters.AddWithValue("@Cidade", funcionario.Cidade);
                cmd.Parameters.AddWithValue("@Departamento", funcionario.Departamento);
                cmd.Parameters.AddWithValue("@Sexo", funcionario.Sexo);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }
        public Funcionario GetFuncionario(long id)
        {
            Funcionario funcionario = new Funcionario();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string sqlQuery = "SELECT * FROM Funcionarios WHERE FuncionarioId= " + id;
                SqlCommand cmd = new SqlCommand(sqlQuery, con);
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    funcionario.FuncionarioId = Convert.ToInt32(rdr["FuncionarioId"]);
                    funcionario.Nome = rdr["Nome"].ToString();
                    funcionario.Cidade = rdr["Cidade"].ToString();
                    funcionario.Departamento = rdr["Departamento"].ToString();
                    funcionario.Sexo = rdr["Sexo"].ToString();
                }
            }
            return funcionario;
        }
        public void DeleteFuncionario(long id)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string comandoSQL = "Delete from Funcionarios where FuncionarioId = @FuncionarioId";

                SqlCommand cmd = new SqlCommand(comandoSQL, con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@FuncionarioId", id);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        //public void ResetarId()
        //{
        //    using (SqlConnection con = new SqlConnection(connectionString))
        //    {
        //        string comandoSQL = "DBCC CHECKIDENT ('dbo.crud_Funcionarios', RESEED, 0)";
        //        SqlCommand cmd = new SqlCommand(comandoSQL, con);
        //        cmd.CommandType = CommandType.Text;
        //        con.Open();
        //        cmd.ExecuteNonQuery();
        //        con.Close();
        //    }

        //}

    }
}

