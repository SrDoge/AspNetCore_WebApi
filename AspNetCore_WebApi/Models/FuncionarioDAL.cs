using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using Microsoft.Extensions.Configuration;
using AspNetCore_WebApi.Models;


namespace ApnCore_CrudExemplo.Models
{
    public class FuncionarioDAL : IFuncionarioDAL
    {
        private readonly IConfiguration _configuration;

        public FuncionarioDAL(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GetConnection()
        {
            var connection = _configuration.GetSection("ConnectionsStrings").GetSection("FuncConnection").Value;
            return connection;
        }

        public IEnumerable<Funcionario> GetAllFuncionarios()
        {
            var connectionString = this.GetConnection();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                const string query = "SELECT FuncionarioId, Nome, Cidade, Departamento, Sexo, Salario from crud_Funcionarios";

                return con.Query<Funcionario>(query);
            }
        }

        public void AddFuncionario(Funcionario funcionario)
        {
            var connectionString = this.GetConnection();
            using (var con = new SqlConnection(connectionString))
            {
                const string comandoSQL = "Insert into crud_Funcionarios (Nome,Cidade,Departamento,Sexo,Salario) " +
                    "Values(@Nome, @Cidade, @Departamento, @Sexo, @Salario)";
                con.Execute(comandoSQL, funcionario);
            }
        }

        public void UpdateFuncionario(Funcionario funcionario)
        {
            var connectionString = this.GetConnection();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string comandoSQL = "Update crud_Funcionarios set Nome = @Nome, Cidade = @Cidade, Departamento = @Departamento, Sexo = @Sexo, Salario = @Salario " +
                    "where FuncionarioId = " + funcionario.FuncionarioId;
                con.Execute(comandoSQL, funcionario);
            }
        }

        public Funcionario GetFuncionario(long id)
        {
            var connectionString = this.GetConnection();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string sqlQuery = "SELECT * FROM crud_Funcionarios WHERE FuncionarioId= " + id;
                return con.Query<Funcionario>(sqlQuery).FirstOrDefault();
            }
        }

        public void DeleteFuncionario(long id)
        {
            var connectionString = this.GetConnection();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string comandoSQL = "Delete from crud_Funcionarios where FuncionarioId = " + id;
                con.Execute(comandoSQL);
            }
        }
    }
}
