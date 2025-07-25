using manage_columns.src.models;
using manage_columns.src.repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace manage_columns.src.controller
{
    [Authorize]
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

        [HttpGet]
        [ProducesResponseType(typeof(ColumnList), StatusCodes.Status200OK)]
        public async Task<ActionResult<ColumnList>> GetColumns(int boardId, int userId)
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

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult CreateColumn(CreateColumn createColumnRequest)
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

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult UpdateColumn(UpdateColumn updateColumnRequest)
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

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult DeleteTask(int columnId, int userId)
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
    }
}