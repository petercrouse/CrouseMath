using System.Threading.Tasks;
using CrouseMath.Application.Students.Commands.CreateStudent;
using CrouseMath.Application.Students.Commands.DeleteStudent;
using CrouseMath.Application.Students.Commands.UpdateStudent;
using CrouseMath.Application.Students.Queries.GetStudent;
using CrouseMath.Application.Students.Queries.GetStudentList;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CrouseMath.WebUI.Controllers
{
    public class StudentsController : ApiController
    {
        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<StudentListViewModel>> GetAll()
        {
            return Ok(await Mediator.Send(new GetStudentListQuery()));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<StudentViewModel>> Get(int id)
        {
            return Ok(await Mediator.Send(new GetStudentQuery { Id = id }));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Create([FromBody] CreateStudentCommand command)
        {
            await Mediator.Send(command);

            return NoContent();
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update([FromBody]UpdateStudentCommand command)
        {
            await Mediator.Send(command);

            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            await Mediator.Send(new DeleteStudentCommand { Id = id });

            return NoContent();
        }
    }
}