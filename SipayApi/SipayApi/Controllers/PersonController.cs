using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace SipayApi.Controllers;

public class Person
{
    public string Name { get; set; }

    public string Lastname { get; set; }

    public string Phone { get; set; }

    public int AccessLevel { get; set; }

    public decimal Salary { get; set; }
}

public class PersonValidator : AbstractValidator<Person>
{
    public PersonValidator()
    {
        RuleFor(p => p.Name).NotNull().WithMessage("Staff person name").MinimumLength(5).MaximumLength(100);
        RuleFor(p => p.Lastname).NotNull().WithMessage("Staff person lastname").MinimumLength(5).MaximumLength(100);
        RuleFor(p => p.Phone).NotNull().WithMessage("Staff phone number").MinimumLength(10).MaximumLength(20);
        RuleFor(p => p.AccessLevel).NotNull().WithMessage("Staff person access level to system").LessThanOrEqualTo(5).GreaterThanOrEqualTo(1);
        RuleFor(p => p.Salary).NotNull().WithMessage("Staff person salary").LessThanOrEqualTo(50000).GreaterThanOrEqualTo(5000);
    }
}

[ApiController]
[Route("sipy/api/[controller]")]
public class PersonController : ControllerBase
{

    public PersonController() { }


    [HttpPost]
    public IActionResult Post([FromBody] Person person)
    {
        var validator = new PersonValidator();
        var result = validator.Validate(person);
        if(result.IsValid) return Ok( person);
        return BadRequest();
    }
}
