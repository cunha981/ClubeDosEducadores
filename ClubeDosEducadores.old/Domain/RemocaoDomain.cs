using DataAccess;
using Helper;
using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Core.EntityClient;
using System.Data.SqlClient;
using System.Linq;
using ViewModel.RemocaoVMs;

namespace Domain
{
    public class RemocaoDomain
    {
        private Context _context;

        public RemocaoDomain(Context context)
        {
            _context = context;
        }

        public IQueryable<Remocao> Get(int funcionarioId, DateTime? date = null)
        {
            if (!date.HasValue)
            {
                return _context.Set<Remocao>().Where(a => a.FuncionarioId == funcionarioId && a.Data.Year == DateTime.Now.Year);
            }

            return _context.Set<Remocao>().Where(a => a.FuncionarioId == funcionarioId && a.Data.Year == date.Value.Year);
        }

        public IQueryable<Remocao> GetAll(int funcionarioId)
        {
            return _context.Set<Remocao>().Where(a => a.FuncionarioId == funcionarioId);
        }

        public void Save(IEnumerable<int> vagaIds, int funcionarioId)
        {
            _context.Set<Remocao>().RemoveRange(Get(funcionarioId));

            vagaIds.For((vagaId, total, position) =>
            {
                _context.Set<Remocao>().Add(new Remocao { Data = DateTime.Now, FuncionarioId = funcionarioId, Preferencia = position + 1, VagaRemocaoId = vagaId });
            });

            _context.SaveChanges();
        }

        public IndicacaoRemocaoVM[] GetIndicacaoRemocao(int funcionarioId)
        {
            DataSet ds = new DataSet();
            SqlConnection sqlConn = new SqlConnection(_context.Database.Connection.ConnectionString);
            SqlCommand cmdReport = new SqlCommand("gerarlista", sqlConn);
            SqlDataAdapter daReport = new SqlDataAdapter(cmdReport);
            using (cmdReport)
            {
                SqlParameter questionIdPrm = new SqlParameter("@FuncionarioId", funcionarioId);
                cmdReport.CommandType = CommandType.StoredProcedure;
                cmdReport.Parameters.Add(questionIdPrm);
                daReport.Fill(ds);
            }

            var list = new IndicacaoRemocaoVM[ds.Tables[0].Rows.Count];

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                var obj = new IndicacaoRemocaoVM();
                obj.Regiao = ds.Tables[0].Rows[i].ItemArray[0].ToString();
                obj.Unidade = ds.Tables[0].Rows[i].ItemArray[1].ToString();
                obj.Endereco = ds.Tables[0].Rows[i].ItemArray[2].ToString();
                obj.DificilAcesso = ds.Tables[0].Rows[i].ItemArray[3].ToString();
                obj.Telefone = ds.Tables[0].Rows[i].ItemArray[4].ToString();
                obj.Email = ds.Tables[0].Rows[i].ItemArray[5].ToString();
                obj.Vagas = int.Parse(ds.Tables[0].Rows[i].ItemArray[6].ToString());
                obj.Distancia = ds.Tables[0].Rows[i].ItemArray[7].ToString();
                obj.GoogleMaps = ds.Tables[0].Rows[i].ItemArray[8].ToString();
                list[i] = obj;
            }

            return list;
        }

        public IndicacaoRemocaoPotencialVM[] GetIndicacaoRemocaoCompleta(int funcionarioId)
        {
            DataSet ds = new DataSet();
            SqlConnection sqlConn = new SqlConnection(_context.Database.Connection.ConnectionString);
            SqlCommand cmdReport = new SqlCommand("gerarlistaP", sqlConn);
            SqlDataAdapter daReport = new SqlDataAdapter(cmdReport);
            using (cmdReport)
            {
                SqlParameter questionIdPrm = new SqlParameter("@FuncionarioId", funcionarioId);
                cmdReport.CommandType = CommandType.StoredProcedure;
                cmdReport.Parameters.Add(questionIdPrm);
                daReport.Fill(ds);
            }

            var list = new IndicacaoRemocaoPotencialVM[ds.Tables[0].Rows.Count];

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                var obj = new IndicacaoRemocaoPotencialVM();
                obj.Regiao = ds.Tables[0].Rows[i].ItemArray[0].ToString();
                obj.Unidade = ds.Tables[0].Rows[i].ItemArray[1].ToString();
                obj.Endereco = ds.Tables[0].Rows[i].ItemArray[2].ToString();
                obj.DificilAcesso = ds.Tables[0].Rows[i].ItemArray[3].ToString();
                obj.Telefone = ds.Tables[0].Rows[i].ItemArray[4].ToString();
                obj.Email = ds.Tables[0].Rows[i].ItemArray[5].ToString();

                if (ds.Tables[0].Rows[i].ItemArray[6] != null && ds.Tables[0].Rows[i].ItemArray[6].ToString() != "null")
                {
                    obj.VagasIniciais = int.Parse(ds.Tables[0].Rows[i].ItemArray[6].ToString());
                }

                if (ds.Tables[0].Rows[i].ItemArray[7] != null && ds.Tables[0].Rows[i].ItemArray[7].ToString() != "null")
                {
                    obj.VagasPotenciais = int.Parse(ds.Tables[0].Rows[i].ItemArray[7].ToString());
                }

                obj.Distancia = ds.Tables[0].Rows[i].ItemArray[8].ToString();
                obj.GoogleMaps = ds.Tables[0].Rows[i].ItemArray[9].ToString();
                list[i] = obj;
            }

            return list;
        }

        public IndicacaoRemocaoVM[] GetIndicacaoRemocaoPrecario(int funcionarioId)
        {
            DataSet ds = new DataSet();
            SqlConnection sqlConn = new SqlConnection(_context.Database.Connection.ConnectionString);
            SqlCommand cmdReport = new SqlCommand("gerarlistaPrecario", sqlConn);
            SqlDataAdapter daReport = new SqlDataAdapter(cmdReport);
            using (cmdReport)
            {
                SqlParameter questionIdPrm = new SqlParameter("@FuncionarioId", funcionarioId);
                cmdReport.CommandType = CommandType.StoredProcedure;
                cmdReport.Parameters.Add(questionIdPrm);
                daReport.Fill(ds);
            }

            var list = new IndicacaoRemocaoVM[ds.Tables[0].Rows.Count];

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                var obj = new IndicacaoRemocaoVM();
                obj.Regiao = ds.Tables[0].Rows[i].ItemArray[0].ToString();
                obj.Unidade = ds.Tables[0].Rows[i].ItemArray[1].ToString();
                obj.Endereco = ds.Tables[0].Rows[i].ItemArray[2].ToString();
                obj.DificilAcesso = ds.Tables[0].Rows[i].ItemArray[3].ToString();
                obj.Telefone = ds.Tables[0].Rows[i].ItemArray[4].ToString();
                obj.Email = ds.Tables[0].Rows[i].ItemArray[5].ToString();
                obj.Vagas = int.Parse(ds.Tables[0].Rows[i].ItemArray[6].ToString());
                obj.Distancia = ds.Tables[0].Rows[i].ItemArray[7].ToString();
                obj.GoogleMaps = ds.Tables[0].Rows[i].ItemArray[8].ToString();
                list[i] = obj;
            }

            return list;
        }
    }
}
