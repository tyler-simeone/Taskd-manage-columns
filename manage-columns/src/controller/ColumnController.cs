using manage_columns.src.models;
using manage_columns.src.repository;
using Microsoft.AspNetCore.Mvc;

namespace manage_columns.src.controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class ColumnController : Controller
    {
        IRequestValidator _validator;
        IColumnRepository _columnRepository;

        public ColumnController(IColumnRepository columnRepository, IRequestValidator requestValidator)
        {
            _validator = requestValidator;
            _columnRepository = columnRepository;
        }

        // [HttpGet("{id}")]
        // [ProducesResponseType(typeof(models.Task), StatusCodes.Status200OK)]
        // public async Task<ActionResult<models.Task>> GetTask(int id, int userId)
        // {
        //     if (_validator.ValidateGetTask(id, userId))
        //     {
        //         try
        //         {
        //             models.Task task = await _columnRepository.GetTask(id, userId);
        //             Console.WriteLine($"Task: {task.TaskDescription}");
        //             return Ok(task);
        //         }
        //         catch (Exception ex)
        //         {
        //             Console.WriteLine($"Error: {ex.Message}");
        //             throw;
        //         }
        //     }
        //     else
        //     {
        //         return BadRequest("Task ID is required.");
        //     }
        // }

        [HttpGet]
        [ProducesResponseType(typeof(ColumnList), StatusCodes.Status200OK)]
        public async Task<ActionResult<ColumnList>> GetColumns(int boardId, int userId)
        {
            if (_validator.ValidateGetColumns(userId, boardId))
            {
                try
                {
                    ColumnList columnList = await _columnRepository.GetColumnsWithTasks(boardId, userId);
                    return Ok(columnList);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                    throw;
                }
            }
            else
            {
                return BadRequest("User ID is required.");
            }
        }

        // [HttpGet("board/{boardId}")]
        // [ProducesResponseType(typeof(ColumnList), StatusCodes.Status200OK)]
        // public async Task<ActionResult<ColumnList>> GetColumnsWithTasks(int boardId, int userId)
        // {
        //     if (_validator.GetColumnsWithTasks(boardId, userId))
        //     {
        //         try
        //         {
        //             ColumnList columnList = await _columnRepository.GetColumnsWithTasks(boardId, userId);
        //             return Ok(columnList);
        //         }
        //         catch (Exception ex)
        //         {
        //             Console.WriteLine($"Error: {ex.Message}");
        //             throw;
        //         }
        //     }
        //     else
        //     {
        //         return BadRequest("boardId and userId are required.");
        //     }
        // }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult CreateColumn(CreateColumn createColumnRequest)
        {
            if (_validator.ValidateCreateColumn(createColumnRequest))
            {
                try
                {
                    _columnRepository.CreateColumn(createColumnRequest);
                    return Ok("Task Created");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                    throw;
                }
            }
            else
            {
                return BadRequest("createColumnRequest is required.");
            }
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult UpdateColumn(UpdateColumn updateColumnRequest)
        {
            if (_validator.ValidateUpdateColumn(updateColumnRequest))
            {
                try
                {
                    _columnRepository.UpdateColumn(updateColumnRequest);
                    return Ok("Task Updated");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                    throw;
                }
            }
            else
            {
                return BadRequest("updateColumnRequest is required.");
            }
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult DeleteTask(int boardId, int userId)
        {
            if (_validator.ValidateDeleteColumn(boardId, userId))
            {
                try
                {
                    _columnRepository.DeleteColumn(boardId, userId);
                    return Ok("Task Deleted");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                    throw;
                }
            }
            else
            {
                return BadRequest("DeleteTask is required.");
            }
        }
    }
}