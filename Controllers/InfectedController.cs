using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using ProjetoAPI.Data.Collections;
using ProjetoAPI.Models;

namespace ProjetoAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class InfectedController : ControllerBase
    {
        ProjetoAPI.Data.Collections.MongoDB _mongoDB;
        IMongoCollection<Infected> _infectedCollection;

        public InfectedController(ProjetoAPI.Data.Collections.MongoDB mongoDB)
        {
            _mongoDB = mongoDB;
            _infectedCollection = _mongoDB.DataBase.GetCollection<Infected>(typeof(Infected).Name.ToLower());
        }

        [HttpPost]
        public ActionResult SaveInfecteds([FromBody] InfectedDto dto)
        {
            var infected = new Infected(dto.BirthDate, dto.Sex, dto.Latitude, dto.Longitude);

            _infectedCollection.InsertOne(infected);
            
            return StatusCode(201, "Infectado adicionado com sucesso");
        }

        [HttpGet]
        public ActionResult GetInfecteds()
        {
            var infecteds = _infectedCollection.Find(Builders<Infected>.Filter.Empty).ToList();
            
            return Ok(infecteds);
        }
    }
}

