using ClassDemoDuatlonLib.model;
using ClassDemoDuatlonRest.managers;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ClassDemoDuatlonRest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DuathletesController : ControllerBase
    {
        /*
         * opret manager
         */
        private readonly DuathletesManager _mgr = new DuathletesManager();


        // GET: api/<DuathletesController>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult Get()
        {
            List<Duathlete> liste = _mgr.GetAll();
            return (liste.Count == 0) ? NoContent() : Ok(liste);
            
            /* samme som
             * if (liste.Count == 0)
             * {
             *    NoContent();
             * }
             * else
             * {
             *    Ok(liste);
             * }
             */
        }

        // GET api/<DuathletesController>/5
        [HttpGet("{bib}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Get(int bib)
        {
            try
            {
                return Ok(_mgr.GetByBib(bib));
            }
            catch(KeyNotFoundException knfe)
            {
                return NotFound(knfe.Message);
            }
            
        }

        // POST api/<DuathletesController>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Post([FromBody] Duathlete dua)
        {
            try
            {
                dua.Validate();
                Duathlete createdDuathlete = _mgr.Add(dua);
                return Created("api/Duathletes/" + createdDuathlete.Bib, createdDuathlete);
            }
            catch(ArgumentException ae)
            {
                return BadRequest(ae.Message);
            }

        }

        

        /* IKKE EN DEL AF Spørgmål
        
        // PUT api/<DuathletesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        */

        // DELETE api/<DuathletesController>/5
        [HttpDelete("{bib}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Delete(int bib)
        {
            try
            {
                Duathlete dua = _mgr.Delete(bib);
                return Ok(dua);
            }
            catch (KeyNotFoundException knfe)
            {
                return NotFound(knfe.Message);
            }
        }






        /*
         * Opgave 7
         */
        // POST api/<DuathletesController>
        [HttpPost]
        [Route("Age/{age}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Post(int age, [FromBody] Duathlete dua)
        {
            try
            {
                if (age < 1 || 125 < age)
                {
                    throw new ArgumentException("age skal være mellem 1-125");
                }

                Duathlete createdDuathlete = _mgr.Add(age, dua);
                return Created("api/Duathletes/" + createdDuathlete.Bib, createdDuathlete);
            }
            catch (ArgumentException ae)
            {
                return BadRequest(ae.Message);
            }

        }


        [HttpGet]
        [Route("AgeGroup/{ageGroup}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetByAge(int ageGroup)
        {
            try
            {
                Duathlete.CheckAgeGroup(ageGroup);
                List<Duathlete> liste = _mgr.GetAllByAge(ageGroup);
                return (liste.Count == 0) ? NoContent() : Ok(liste);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }

            
        }

    }
}
