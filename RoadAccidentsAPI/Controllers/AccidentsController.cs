using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RoadAccidentsAPI.Accidents;
using AutoMapper;

namespace RoadAccidentsAPI.Controllers;

public class AccidentsController : BaseController
{
    private readonly AppDbContext _dbContext;
    private readonly ILogger<AccidentsController> _logger;
    private readonly IMapper _mapper;

    public AccidentsController(AppDbContext dbContext, ILogger<AccidentsController> logger, IMapper mapper)
    {
        _dbContext = dbContext;
        _logger = logger;
        _mapper = mapper;
    }

    

    /// <summary>
    /// Get all accidents.
    /// </summary>
    /// <returns>An array of all accidents.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<Accident>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAllAccidents([FromQuery] GetAllAccidentsRequest request)
    {
        var accidents = await _dbContext.Accidents.ToArrayAsync();
        
        return Ok(accidents);

    }


    /// <summary>
    /// Gets an accident by ID.
    /// </summary>
    /// <param name="id">The ID of the employee.</param>
    /// <returns>The single employee record.</returns>
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(GetAccidentReport), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAccidentById([FromRoute] int id)
    {
        var accident = await _dbContext.Accidents.FindAsync(id);

        if(accident == null)
        {
            return NotFound();
        }

        var response = _mapper.Map<GetAccidentReport>(accident);
        
        return Ok(response);
    }

    
    
    /// <summary>
    /// Reports a new accident.
    /// </summary>
    /// <param name="request">The accident to be reported.</param>
    /// <returns>A link to the accident that was reported.</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> ReportAccident([FromBody] ReportAccidentRequest request)
    {
        var validationResult = await ValidateAsync(request);

        if(!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors);
        }

        _logger.LogInformation("Reporting an accident...");

        var newAccident = _mapper.Map<Accident>(request);
        _dbContext.Accidents.Add(newAccident);
        await _dbContext.SaveChangesAsync();

        _logger.LogInformation("Accident reported.");

        return CreatedAtAction(nameof(GetAllAccidents), new{id = newAccident.Id}, newAccident);


    }

    



}
