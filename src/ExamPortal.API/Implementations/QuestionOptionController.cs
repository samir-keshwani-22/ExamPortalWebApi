using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using ExamPortal.API.Controllers;
using ExamPortal.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace ExamPortal.API.Implementations;

[ApiController]
public class QuestionOptionController : QuestionOptionApiController
{
    public override Task<IActionResult> AddQuestionOption([FromBody] QuestionOptionCreate questionOptionCreate)
    {
        throw new System.NotImplementedException();
    }

    public override Task<IActionResult> DeleteQuestionOption([FromRoute(Name = "id"), Required] int id)
    {
        throw new System.NotImplementedException();
    }

    public override Task<IActionResult> GetQuestionOptionById([FromRoute(Name = "id"), Required] int id)
    {
        throw new System.NotImplementedException();
    }

    public override Task<IActionResult> ListQuestionOptions([FromQuery(Name = "pageIndex")] long? pageIndex, [FromQuery(Name = "pageSize")] long? pageSize)
    {
        throw new System.NotImplementedException();
    }

    public override Task<IActionResult> UpdateQuestionOption([FromRoute(Name = "id"), Required] int id, [FromBody] QuestionOptionUpdate questionOptionUpdate)
    {
        throw new System.NotImplementedException();
    }

}
