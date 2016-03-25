using DiplomaDataModel.Models;
using Newtonsoft.Json.Linq;
using OptionsWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;

namespace OptionsWebAPI.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ChoicesController : ApiController
    {
        private DiplomaContext db = new DiplomaContext();

        [Route("api/Choices/graph")]
        public GraphData GetGraphData()
        {
            GraphData data = new GraphData();

            var choices = db.Choices.Include(c => c.YearTermId).Include(c => c.FirstChoiceOptionId).Include(c => c.SecondChoiceOptionId).Include(c => c.ThirdChoiceOptionId).Include(c => c.FourthChoiceOptionId);
            var yearterms = db.YearTerms.ToList();
            var optionIds = db.Options.Select(c => c.OptionId).ToList();

            data.OptionsTitles = db.Options.Select(c => c.Title).ToList();
            data.ChoicesData = new List<ChoiceData>();

            //foreach yearterm as term
            foreach (var term in yearterms)
            {
                //grab choices where c=>yeartermid == term.id
                var TermChoices = db.Choices.Where(c => c.YearTermId == term.YearTermId);

                //4 Choices... 4 Charts...
                int count = optionIds.Count;
                int[] ChoiceC1 = new int[count];
                int[] ChoiceC2 = new int[count];
                int[] ChoiceC3 = new int[count];
                int[] ChoiceC4 = new int[count];

                //foreach choice
                foreach(var choice in TermChoices)
                {
                    int OptionId1 = choice.FirstChoiceOptionId ?? 0;
                    int OptionId2 = choice.SecondChoiceOptionId ?? 0;
                    int OptionId3 = choice.ThirdChoiceOptionId ?? 0;
                    int OptionId4 = choice.FourthChoiceOptionId ?? 0;

                    //foreach optionids as option
                    foreach(var option in optionIds)
                    {
                        if (option == OptionId1)
                            ChoiceC1[optionIds.IndexOf(option)]++;
                        if (option == OptionId2)
                            ChoiceC2[optionIds.IndexOf(option)]++;
                        if (option == OptionId3)
                            ChoiceC3[optionIds.IndexOf(option)]++;
                        if (option == OptionId4)
                            ChoiceC4[optionIds.IndexOf(option)]++;
                    }
                }

                ChoiceData ChoiceData = new ChoiceData();
                ChoiceData.YearTermId = term.YearTermId;
                ChoiceData.Term = term.Term;
                ChoiceData.Year = term.Year;
                ChoiceData.FirstChoice = ChoiceC1;
                ChoiceData.SecondChoice = ChoiceC2;
                ChoiceData.ThirdChoice = ChoiceC3;
                ChoiceData.FourthChoice = ChoiceC4;

                data.ChoicesData.Add(ChoiceData);
            }

            return data;
        }

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

        // POST: api/Choices
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

        // PUT: api/Choices/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutChoice(int id, Choice choice)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != choice.ChoiceId)
            {
                return BadRequest();
            }

            db.Entry(choice).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ChoiceExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        private bool ChoiceExists(int id)
        {
            return db.Choices.Count(e => e.ChoiceId == id) > 0;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
