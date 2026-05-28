using Microsoft.Extensions.DependencyInjection;
using DormitoryManagement.Application.Interfaces;
using DormitoryManagement.Application.Interfaces.Services;
using DormitoryManagement.Application.Services;
using DormitoryManagement.Application.Services.Implements;
using DormitoryManagement.Application.Services.Interfaces;

namespace DormitoryManagement.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IRoomService, RoomService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IContractService, ContractService>();
            services.AddScoped<IBlockService, BlockService>();
            services.AddScoped<IBedService, BedService>();
            services.AddScoped<IViolationService, ViolationService>();
            services.AddScoped<IRoomTypeService, RoomTypeService>();
            services.AddScoped<IAssetService, AssetService>();
            services.AddScoped<IUtilityService, UtilityService>();
            services.AddScoped<IVehicleService, VehicleService>();
            services.AddScoped<IStatisticService, StatisticService>();
            services.AddScoped<IInvoiceService, InvoiceService>();
            services.AddScoped<IPaymentService, PaymentService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
            services.AddScoped<IMaintenanceRequestService, MaintenanceRequestService>();

            return services;
        }
    }
}
