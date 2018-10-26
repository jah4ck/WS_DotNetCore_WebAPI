using DotNetCore_WebAPI.entities;
using System;
using System.Threading.Tasks;

namespace WS_DotNetCore_WebAPI.repository.Interface
{
    public interface IRechercheTextRepository
    {
        Task<TaskResult<PresenceTexte>> GetText(string bdd, string texte);
    }
}
