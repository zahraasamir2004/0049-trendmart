using FurniStyle.Core.DTOs.Basket;
using FurniStyle.Core.Entities;
using FurniStyle.Core.Entities.OrderEntities;
using FurniStyle.Core.IServices;
using FurniStyle.Core.IServices.Payments;
using FurniStyle.Core.IUnitOfWork;
using FurniStyle.Core.Specifications.DelivaryMethods;
using FurniStyle.Repository.Specifications.Furnies;
using Microsoft.Extensions.Configuration;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurniStyle.Service.Services.Payments
{
    public class PaymentService : IPaymentService
    {
        private readonly IBasketService _basketService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _configuration;

        public PaymentService(IBasketService basketService,IUnitOfWork unitOfWork,IConfiguration configuration)
        {
            _basketService = basketService;
            _unitOfWork = unitOfWork;
            _configuration = configuration;
        }
        public async Task<CustometBasketDTO> CreateUpdatePaymentIntentAsync(string basketId)
        {
            StripeConfiguration.ApiKey = _configuration["Strip:SecretKey"];

            var basket = await _basketService.GetBasketByIdAsync(basketId);
            if (basket is null) return null;

            var shippingPrice = 0m;
            if (basket.DelivaryMethodId.HasValue)
            {
                var specification = new DelivaryMethodSpecifications(basket.DelivaryMethodId.Value);
                var delivaryMethod= await _unitOfWork.Repository<DelivaryMethod, int>().GetAsync(specification);
                shippingPrice=delivaryMethod.Cost;
            }
            if (basket.BasketItems.Count() > 0)
            {
                foreach (var item in basket.BasketItems) 
                {
                    var specifications = new FurniSpecifications(item.Id);
                    var funi = await _unitOfWork.Repository<Furni, int>().GetAsync(specifications);
                    if (item.Price != funi.Price)
                    { 
                        item.Price = funi.Price;
                    }
                }
            }
            var subTotal = basket.BasketItems.Sum(p=>p.Price * p.Quantatiy);
            var service = new PaymentIntentService();
            PaymentIntent paymentIntent;
            if (string.IsNullOrEmpty(basket.PaymentIntentId))
            {
                var option = new PaymentIntentCreateOptions()
                {
                    Amount = (long)(subTotal * 100 + shippingPrice * 100),
                    Currency = "usd"
                };
                paymentIntent= await service.CreateAsync(option);
                basket.PaymentIntentId=paymentIntent.Id;
                basket.ClientSecret=paymentIntent.ClientSecret;


            }
            else
            {
                var option = new PaymentIntentUpdateOptions()
                {
                    Amount = (long)(subTotal * 100 + shippingPrice * 100)
                };
                paymentIntent = await service.UpdateAsync(basket.PaymentIntentId,option);
                basket.PaymentIntentId = paymentIntent.Id;
                basket.ClientSecret = paymentIntent.ClientSecret;

            }

            await _basketService.UpdateOrCreateBasketAsync(basket);
            return basket;
        }
    }
}
