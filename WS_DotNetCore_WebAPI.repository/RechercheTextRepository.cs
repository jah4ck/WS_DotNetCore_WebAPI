using Dapper;
using DotNetCore_WebAPI.entities;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using WS_DotNetCore_WebAPI.repository.Interface;

namespace WS_DotNetCore_WebAPI.repository
{
    public class RechercheTextRepository : BaseRepository, IRechercheTextRepository
    {
        //permet de spécifier la connexion de cette classe 
        // on va donc allé chercher les info dans le fichier appsetting.json
        public RechercheTextRepository(IConfiguration config) : base(config)
        {
            config.GetConnectionString("DefaultConnection");
        }

        public async Task<TaskResult<PresenceTexte>> GetText(string bdd, string texte)
        {
            TaskResult<PresenceTexte> taskResult = new TaskResult<PresenceTexte>();
            try
            {
                await WithConnection(async c =>
                {
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("db", bdd); //le nom des paramètres de la proc
                    parameters.Add("text", texte);
                    taskResult.LstResult = await c.QueryAsync<PresenceTexte>("SRP.dbo.ps_help_text", parameters, null, 6000, commandType: CommandType.StoredProcedure);
                    taskResult.IsSuccess = taskResult.LstResult != null ? true : false;
                    return taskResult;
                });
                return taskResult;
            }
            catch (Exception ex)
            {
                taskResult.Exception = ex;
                taskResult.ReturnMessage = "Erreur lors de la récupération des données";
                taskResult.IsSuccess = false;
                return taskResult;
            }
        }
    }
}
