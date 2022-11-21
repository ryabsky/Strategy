using Microsoft.AspNetCore.Mvc;
using StrategyService.Models;
using Strategy;
using Strategy.Data;

namespace StrategyService.Controllers;

[ApiController]
[Route("strategy")]
public class StrategyController : ControllerBase
{
    private readonly ILogger<StrategyController> logger;
    private readonly IPortfolioService portfolioService;

    public StrategyController(ILogger<StrategyController> logger, IPortfolioService portfolioService)
    {
        this.logger = logger;
        this.portfolioService = portfolioService;
    }

    [Route("status/{id}")]
    [HttpGet]
    public IActionResult GetStatus(ulong id)
    {
        var status = portfolioService.GetStatus(id);
        if (status == null)
        {
            return NotFound();
        }
        else
        {
            return Ok(status);
        }
    }

    [Route("{id}")]
    [HttpGet]
    public IActionResult Get(ulong id)
    {
        var status = portfolioService.GetStatus(id);
        if (status == null)
        {
            return NotFound();
        }
        else if (status == ExperimentStatus.InProgress.ToString() || status == ExperimentStatus.NotStarted.ToString())
        {
            return BadRequest();
        }
        else
        {
            var result = portfolioService.GetResult(id)!;
            var response = result.Select(x => new EstimationModel
            {
                Name = x.Name,
                StartAmount = x.StartAmount,
                Percentile1 = x.Percentile1,
                Percentile5 = x.Percentile5
            }).ToList();
            return Ok(response);
        }
    }

    [HttpPost]
    public ulong Post(PortfolioModel portfolioModel)
    {
        var id = portfolioService.Submit(
            portfolioModel.assets.Select(x => new Asset(x.name, x.p0, x.m, x.s)), portfolioModel.S, portfolioModel.T);
        return id;
    }
}
