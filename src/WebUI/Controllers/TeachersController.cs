using System.Threading.Tasks;
using CrouseMath.Application.Teachers.Commands.CreateTeacher;
using CrouseMath.Application.Teachers.Commands.DeleteTeacher;
using CrouseMath.Application.Teachers.Commands.UpdateTeacher;
using CrouseMath.Application.Teachers.Queries.GetTeacher;
using CrouseMath.Application.Teachers.Queries.GetTeacherList;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CrouseMath.WebUI.Controllers
{
    public class TeachersController : ApiController
    {
        [HttpGet]
        public async Task<ActionResult<TeacherListViewModel>> GetAll()
        {
            return Ok(await Mediator.Send(new GetTeacherListQuery()));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TeacherViewModel>> Get(int id)
        {
            return Ok(await Mediator.Send(new GetTeacherQuery { Id = id }));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Create([FromBody] CreateTeacherCommand command)
        {
            await Mediator.Send(command);

            return NoContent();
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update([FromBody]UpdateTeacherCommand command)
        {
            await Mediator.Send(command);

            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            await Mediator.Send(new DeleteTeacherCommand { Id = id });

            return NoContent();
        }
    }
}