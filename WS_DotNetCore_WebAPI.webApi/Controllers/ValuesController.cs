using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotNetCore_WebAPI.entities;
using Microsoft.AspNetCore.Mvc;
using WS_DotNetCore_WebAPI.business.Interface;

namespace WS_DotNetCore_WebAPI.webApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
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
