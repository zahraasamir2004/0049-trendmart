using FurniStyle.API.ErrorHandling;
using FurniStyle.Core.IServices.Furnis;
using FurniStyle.Core.IServices.Rooms;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FurniStyle.API.Controllers.Room
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class RoomController : ControllerBase
    {
        private readonly IRoomService _roomService;

        public RoomController(IRoomService roomService)
        {
            _roomService = roomService;
        }
        [HttpGet("AllRooms")]
        public async Task<IActionResult> GetAllRooms()
        {
            var rooms = await _roomService.GetAllRoomsAsync();
            return Ok(rooms);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRoomById(int? id)
        {
            if (id == null) BadRequest("Invalid Id");
            var room = await _roomService.GetRoomByIdAsync(id.Value);
            if (room == null) return NotFound();
            return Ok(room);
        }

        [HttpGet("SortingRoomByNameAscending")]
        public async Task<IActionResult> SortingRoomByNameAscending(string? sort)
        {
            var result = await _roomService.SortingRoomByNameAscendingAsync(sort);
            return Ok(result);

        }

        [HttpGet("SortingRoomByNameDescending")]
        public async Task<IActionResult> SortingRoomByNameDescending(string? sort)
        {
            var result = await _roomService.SortingRoomByNameDescendingAsync(sort);
            return Ok(result);

        }

        [HttpGet("SearchRoomByName")]
        public async Task<IActionResult> SearchRoomByName(string? search)
        {
            var result = await _roomService.SearchRoomByNameAsyc(search);
            return Ok(result);

        }

        [HttpGet("ApplyingPaginationOnRooms")]
        public async Task<IActionResult> ApplyingPaginationOnRooms(int? pageIndex, int? pageSize)
        {
            var result = await _roomService.ApplyingPaginationOnRoomAsyc(pageIndex.Value, pageSize.Value);
            return Ok(result);

        }

        [Authorize("Admin")]
        [HttpPost("CreateRoom/{name}")]
        public async Task<IActionResult> CreateRoom(string name)
        {
            try
            {
                await _roomService.AddRoomAsync(name);
                return Ok("The Room Has Been Successfully Created and Added To The Database.");
            }
            catch (Exception)
            {
                return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest, "There Was an Error While Creating The Room. Please, Check Your Input Data"));

            }
        }

        [Authorize("Admin")]
        [HttpPut("UpdateRoom/{oldName}/{newName}")]
        public async Task<IActionResult> UpdateRoom(string oldName, string newName)
        {
            try
            {
                await _roomService.UpdateRoom(oldName, newName);
                return Ok("The Room Has Been Successfully Updated ");
            }
            catch (Exception)
            {
                return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest, "There Was an Error While Updating The Room. Please, Check Your Input Data"));

            }
        }

        [Authorize("Admin")]
        [HttpDelete("DeleteRoom/{name}")]
        public async Task<IActionResult> DeleteRoom(string name)
        {
            try
            {
                await _roomService.DeleteRoom(name);
                return Ok("The Room Has Been Successfully Deleted.");
            }
            catch (Exception)
            {
                return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest, "There Was an Error While Deleting The Room. Please, Check Your Input Data"));

            }
        }

    }
}
