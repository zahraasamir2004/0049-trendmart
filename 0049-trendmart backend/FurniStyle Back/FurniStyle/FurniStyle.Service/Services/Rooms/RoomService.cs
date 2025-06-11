using AutoMapper;
using FurniStyle.Core.DTOs.Categories;
using FurniStyle.Core.DTOs.Furnis;
using FurniStyle.Core.DTOs.Rooms;
using FurniStyle.Core.Entities;
using FurniStyle.Core.IServices.Rooms;
using FurniStyle.Core.IUnitOfWork;
using FurniStyle.Repository.Specifications.Categories;
using FurniStyle.Repository.Specifications.Furnies;
using FurniStyle.Repository.Specifications.Rooms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurniStyle.Service.Services.Rooms
{
    public class RoomService : IRoomService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public RoomService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<RoomDTO>> GetAllRoomsAsync()
        {
            var specifications = new RoomSpecifications();
            return _mapper.Map<IEnumerable<RoomDTO>>(await _unitOfWork.Repository<Room, int>().GetAllAsync(specifications));
        }

        public async Task<RoomDTO> GetRoomByIdAsync(int id)
        {
            var specifications = new RoomSpecifications(id);
            return _mapper.Map<RoomDTO>(await _unitOfWork.Repository<Room, int>().GetAsync(specifications));
        }

        public async Task AddRoomAsync(string name)
        {
            var room = new Room { Name = name };
            await _unitOfWork.Repository<Room, int>().AddAsync(room);
            await _unitOfWork.CompleteAsync();

        }

        public async Task UpdateRoom(string oldName, string newName)
        {
            var specifications = new RoomSpecifications(oldName, "");
            var room = await _unitOfWork.Repository<Room, int>().GetAsync(specifications);

            if (room == null)
                throw new KeyNotFoundException($"Category with name '{oldName}' not found.");

            room.Name = newName; 

            _unitOfWork.Repository<Room, int>().Update(room); 

            await _unitOfWork.CompleteAsync();
        }

        public async Task DeleteRoom(string name)
        {
            var specifications = new RoomSpecifications(name, "");
            var existingRoom = await _unitOfWork.Repository<Room, int>().GetAsync(specifications);
            _unitOfWork.Repository<Room, int>().Delete(existingRoom);
            await _unitOfWork.CompleteAsync();
        }

        public async Task<IEnumerable<RoomDTO>> SortingRoomByNameAscendingAsync(string? sort)
        {
            var sortingResult = Sorting(sort);
            return await sortingResult;
        }

        public async Task<IEnumerable<RoomDTO>> SortingRoomByNameDescendingAsync(string? sort)
        {
            var sortingResult = Sorting(sort);
            return await sortingResult;
        }
       
        public async Task<RoomDTO> SearchRoomByNameAsyc(string? search)
        {
            var specifications = new RoomSpecifications(search, "");
            return _mapper.Map<RoomDTO>(await _unitOfWork.Repository<Room, int>().GetAsync(specifications));
        }

        public async Task<IEnumerable<RoomDTO>> ApplyingPaginationOnRoomAsyc(int pageIndex, int pageSize)
        {
            var specifications = new RoomSpecifications(pageIndex, pageSize);
            return _mapper.Map<IEnumerable<RoomDTO>>(await _unitOfWork.Repository<Room, int>().GetAllAsync(specifications));
        }
        
        private async Task<IEnumerable<RoomDTO>> Sorting(string? sort)
        {
            var specifications = new RoomSpecifications(sort);
            return _mapper.Map<IEnumerable<RoomDTO>>(await _unitOfWork.Repository<Room, int>().GetAllAsync(specifications));
        }

       
    }
}
