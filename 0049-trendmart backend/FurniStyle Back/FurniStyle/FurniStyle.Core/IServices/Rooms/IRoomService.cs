using FurniStyle.Core.DTOs.Categories;
using FurniStyle.Core.DTOs.Furnis;
using FurniStyle.Core.DTOs.Rooms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurniStyle.Core.IServices.Rooms
{
    public interface IRoomService
    {
        Task<IEnumerable<RoomDTO>> GetAllRoomsAsync();
        Task<RoomDTO> GetRoomByIdAsync(int id);
        Task<IEnumerable<RoomDTO>> SortingRoomByNameAscendingAsync(string? sort);
        Task<IEnumerable<RoomDTO>> SortingRoomByNameDescendingAsync(string? sort);
        Task<RoomDTO> SearchRoomByNameAsyc(string? search);
        Task<IEnumerable<RoomDTO>> ApplyingPaginationOnRoomAsyc(int pageIndex, int pageSize);

        Task AddRoomAsync(string name);
        Task UpdateRoom(string name, string name2);
        Task DeleteRoom(string name);
    }
}
