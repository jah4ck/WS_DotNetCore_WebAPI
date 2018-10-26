using DotNetCore_WebAPI.entities;
using System;
using System.Threading.Tasks;

namespace WS_DotNetCore_WebAPI.business.Interface
{
    public interface IRechercheTextManager
    {
        Task<TaskResult<PresenceTexte>> GetText(string bdd, string texte);
    }
}
