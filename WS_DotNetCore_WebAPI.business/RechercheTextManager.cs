using DotNetCore_WebAPI.entities;
using System;
using System.Threading.Tasks;
using WS_DotNetCore_WebAPI.business.Interface;
using WS_DotNetCore_WebAPI.repository.Interface;

namespace WS_DotNetCore_WebAPI.business
{
    public class RechercheTextManager : IRechercheTextManager
    {
        //initialisation de l'interface du repo 
        readonly IRechercheTextRepository _rechercheTextRepository;
        public RechercheTextManager(IRechercheTextRepository rechercheTextRepository)
        {
            _rechercheTextRepository = rechercheTextRepository;
        }

        public async Task<TaskResult<PresenceTexte>> GetText(string bdd, string texte)
        {
            TaskResult<PresenceTexte> result = new TaskResult<PresenceTexte>();
            //test des règles de gestion 
            result = AuthorizeResearch(bdd, texte);

            if (result.Authorize)
            {
                //on effectue donc l'action en bdd (ou autre)
                return await _rechercheTextRepository.GetText(bdd, texte);
            }

            return result;

        }

        private TaskResult<PresenceTexte> AuthorizeResearch(string bdd, string texte)
        {
            TaskResult<PresenceTexte> result = new TaskResult<PresenceTexte>();
            //contrôle si on peu faire une recherche de texte
            // ici 1=1 pour l'exemple
            if (1 == 1)
            {
                result.Authorize = true;
            }
            else
            {
                result.ReturnMessage = "Vous n'ête pas authorisé a faire cette action !!";
                result.Authorize = false;
            }
            return result;
        }
    }
}
