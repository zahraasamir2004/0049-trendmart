using AutoMapper;
using FurniStyle.Core.DTOs.Basket;
using FurniStyle.Core.Entities;
using FurniStyle.Core.IServices;
using FurniStyle.Core.IUnitOfWork;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurniStyle.Service.Services.Basket
{
    public class BasketService : IBasketService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public BasketService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task DeletingBasketAsync(string id)
        {
           await _unitOfWork.Basket.DeleteBasketAsync(id);
        }

        public async Task<CustometBasketDTO> GetBasketByIdAsync(string id)
        {
            var basket = await _unitOfWork.Basket.GetBasketAsync(id);
            var mappedBasked = _mapper.Map<CustometBasketDTO>(basket);
            return mappedBasked;
        }

        public async Task<CustomerBasket> UpdateOrCreateBasketAsync(CustometBasketDTO custometBasketDTO)
        {
            var mappedBasked = _mapper.Map<CustomerBasket>(custometBasketDTO);
            var basket = await _unitOfWork.Basket.UpdateBasketAsync(mappedBasked);
            return basket;
        }
    }
}
