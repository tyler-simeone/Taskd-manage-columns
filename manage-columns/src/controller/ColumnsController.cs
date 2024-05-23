using manage_columns.src.models;
using manage_columns.src.repository;
using Microsoft.AspNetCore.Mvc;

namespace manage_columns.src.controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class ColumnsController : Controller
    {
        IRequestValidator _validator;
        IColumnsRepository _columnsRepository;

        public ColumnsController(IColumnsRepository columnsRepository, IRequestValidator requestValidator)
        {
            _validator = requestValidator;
            _columnsRepository = columnsRepository;
        }

        [HttpGet("{columnId}")]
        [ProducesResponseType(typeof(Column), StatusCodes.Status200OK)]
        public async Task<ActionResult<Column>> GetColumn(int columnId, int userId)
        {
            if (_validator.ValidateGetColumns(userId, columnId))
            {
                try
                {
                    Column column = await _columnsRepository.GetColumn(columnId, userId);
                    return Ok(column);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                    throw;
                }
            }
            else
            {
                return BadRequest("boardId and userId are required.");
            }
        }

        [HttpGet]
        [ProducesResponseType(typeof(ColumnList), StatusCodes.Status200OK)]
        public async Task<ActionResult<ColumnList>> GetColumns(int boardId, int userId)
        {
            if (_validator.ValidateGetColumns(userId, boardId))
            {
                try
                {
                    ColumnList columnList = await _columnsRepository.GetColumnsWithTasks(boardId, userId);
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
                return BadRequest("boardId and userId are required.");
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult CreateColumn(CreateColumn createColumnRequest)
        {
            if (_validator.ValidateCreateColumn(createColumnRequest))
            {
                try
                {
                    _columnsRepository.CreateColumn(createColumnRequest);
                    return Ok("Column Created");
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
                    _columnsRepository.UpdateColumn(updateColumnRequest);
                    return Ok("Column Updated");
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
        public IActionResult DeleteTask(int columnId, int userId)
        {
            if (_validator.ValidateDeleteColumn(columnId, userId))
            {
                try
                {
                    _columnsRepository.DeleteColumn(columnId, userId);
                    return Ok("Column Deleted");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                    throw;
                }
            }
            else
            {
                return BadRequest("columnId and userId are required.");
            }
        }
    }
}