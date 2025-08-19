using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using TaskManagementApi.Features.Tasks.API.Contracts;
using TaskManagementApi.Features.Tasks.Application.Commands;
using TaskManagementApi.Features.Tasks.Application.Queries;

namespace TaskManagementApi.Features.Tasks.Api
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class TasksController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TasksController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Get all tasks with optional filters
        /// </summary>
        /// <param name="isCompleted">Filter by completion status</param>
        /// <param name="dueDate">Filter by due date</param>
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] bool? isCompleted = null, [FromQuery] DateTime? dueDate = null)
        {
            var query = new GetAllTasksQuery
            {
                IsCompleted = isCompleted,
                DueDate = dueDate
            };

            var tasks = await _mediator.Send(query);
            return Ok(tasks);
        }

        /// <summary>
        /// Get a task by ID
        /// </summary>
        /// <param name="id">Task ID</param>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var query = new GetTaskByIdQuery { Id = id };
            var task = await _mediator.Send(query);

            if (task == null)
                return NotFound(new { message = $"Task with ID {id} not found" });

            return Ok(task);
        }

        /// <summary>
        /// Create a new task
        /// </summary>
        /// <param name="request">Task creation data</param>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateTaskRequest request)
        {
            var command = new CreateTaskCommand
            {
                Title = request.Title,
                Description = request.Description,
                DueDate = request.DueDate
            };

            var task = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id = task.Id }, task);
        }

        /// <summary>
        /// Update a task (full update)
        /// </summary>
        /// <param name="id">Task ID</param>
        /// <param name="request">Updated task data</param>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateTaskRequest request)
        {
            var command = new UpdateTaskCommand
            {
                Id = id,
                Title = request.Title,
                Description = request.Description,
                IsCompleted = request.IsCompleted,
                DueDate = request.DueDate
            };

            try
            {
                var task = await _mediator.Send(command);
                return Ok(task);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Partially update a task
        /// </summary>
        /// <param name="id">Task ID</param>
        /// <param name="request">Partial update data</param>
        [HttpPatch("{id}")]
        public async Task<IActionResult> PartialUpdate(Guid id, [FromBody] UpdateTaskRequest request)
        {
            // For PATCH, we use the same command but only send non-null values
            var command = new UpdateTaskCommand
            {
                Id = id,
                Title = request.Title,
                Description = request.Description,
                IsCompleted = request.IsCompleted,
                DueDate = request.DueDate
            };

            try
            {
                var task = await _mediator.Send(command);
                return Ok(task);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Delete a task
        /// </summary>
        /// <param name="id">Task ID</param>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var command = new DeleteTaskCommand { Id = id };

            try
            {
                await _mediator.Send(command);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }
    }
}