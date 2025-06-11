using AutoMapper;
using FurniStyle.Core.DTOs.Rooms;
using FurniStyle.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurniStyle.Core.Mapping.Rooms
{
    public class RoomProfile :Profile
    {
        public RoomProfile()
        {
            CreateMap<Room,RoomDTO>();
        }
    }
}
