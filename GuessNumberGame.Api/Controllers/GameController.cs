using GuessNumberGame.Application.Services;
using GuessNumberGame.Application.DTOModels;
using Microsoft.AspNetCore.Mvc;


namespace GuessNumberGame.Api.Controllers
{
    [ApiController]
    [Route("/[Action]")]
    public class GameController : Controller
    {
        private readonly IGameService gameService;

        public GameController(IGameService gameService)
        {
            this.gameService = gameService;
        }


        [HttpGet]
        [Route("/new")]
        public IActionResult New([FromBody] NewGameDTO newgame)
        {
            var id = gameService.AddGame(newgame);
            return Created("", id.ToString());
        }

        [Route("/guess")]
        [HttpGet]
        public IActionResult Guess([FromBody] GuessDTO guess)
        {
            var result = gameService.TryGuesst(guess);
            return Ok(result.ToString());
        }

        [Route("/scores")]
        public IActionResult Scores()
        {
            var highScore = gameService.GetHighScore();
            return Ok(highScore.ToString());
        }
    }
}