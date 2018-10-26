using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotNetCore_WebAPI.entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WS_DotNetCore_WebAPI.business.Interface;

namespace WS_DotNetCore_WebAPI.webApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {

        /*
          * créer les dll business et repo
          * ajouter les dépendances
          * ajouter dapper ou entity (dapper = bcp mieux :p ) sur repo et web
          * ajouter les autres nugets comme bootstrap, jquery, blablabla... 
          * créer la classe BaseRepository qui permet de récup les donnée de connexion et exécuter les requête 
          * le tout de façon async pour avoir des meilleurs perf (ce qu'on ne fait pas ici ... )
          * ajouter la connexion à sql dans le appsetting (spécific à core 2.0)
          * créer la class TaskResult comme entitie qui stock les différent retour d'action
          * 
          * remplire les classes business repo (sans oublier les interfaces)
          * cheminement : vue=>model=>controller=>manager=>repository=>manager=>controller=>model=>vue (le tout en passant par les interfaces
          * 
          * Penser a faire le maping des interface (uniquement pour core 2.0 )  dans le startup.cs sinon marche po
          * 
          * Voilà tu peux fauire un test de charge à 5 000 000 requête seconde, tu pete même pas de timeout (enfin peut être que la bdd crash mais le site lui tiendra :) )
          * 
          * pour l'asp.net framework c'est pareil juste moins performant, et tu risque de faire péter iis à 5 000 000 req / seconde
          * 
          * */

        private readonly IRechercheTextManager _rechercheTextManager;
        public ValuesController(IRechercheTextManager rechercheTextManager)
        {
            _rechercheTextManager = rechercheTextManager;
        }


        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }


        [HttpPost]
        [Authorize]
        [Route("Rechercher")]
        public async Task<ActionResult<TaskResult<PresenceTexte>>> Rechercher([FromBody] PresenceTexte presenceTexte)
        {
            // action de recherche

            TaskResult<PresenceTexte> result = new TaskResult<PresenceTexte>();


            //vérification des données d'entrée 
            if (!ModelState.IsValid)
            {
                result.Authorize = false;
                result.IsSuccess = false;
                result.ReturnMessage = "erreur sur le modèle de donnée en POST";
                return result;
            }

            //appel du manager pour réaliser notre action de recherche
            result = await _rechercheTextManager.GetText(presenceTexte.Bdd, presenceTexte.Text);

            return result;
        }
    }
}
