using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using AspNetCore_WebApi.Models;

namespace AspNetCore_WebApi.Models
{
    public interface IFuncionarioDAL
    {
        IEnumerable<Funcionario> GetAllFuncionarios();
        void AddFuncionario (Funcionario funcionario);
        void UpdateFuncionario(Funcionario funcionario);
        Funcionario GetFuncionario(long id);
        void DeleteFuncionario(long id);
    }
}
