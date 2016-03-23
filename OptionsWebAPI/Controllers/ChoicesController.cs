using DiplomaDataModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;

namespace OptionsWebAPI.Controllers
{
    [EnableCors("*", "*", "*")]
    public class ChoicesController : ApiController
    {
        private DiplomaContext db = new DiplomaContext();

        public IEnumerable<Choice> GetAllChoices()
        {
            return db.Choices.ToList();
        }

        public IHttpActionResult GetChoiceById(int id)
        {
            var choice = db.Choices.FirstOrDefault(c=>c.ChoiceId == id);

            if(choice == null)
            {
                return NotFound();
            }

            return Ok(choice);
        }

        [ResponseType(typeof(Choice))]
        public IHttpActionResult PostChoice(Choice choice)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Choices.Add(choice);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = choice.ChoiceId }, choice);
        }
    }
}
