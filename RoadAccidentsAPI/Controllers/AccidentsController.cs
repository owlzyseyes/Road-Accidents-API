using System;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace RoadAccidentsAPI.Controllers;

public class AccidentsController : BaseController
{
    private readonly AppDbContext _dbContext;
    private readonly ILogger<AccidentsController> _logger;

    public AccidentsController(AppDbContext dbContext, ILogger<AccidentsController> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

}
