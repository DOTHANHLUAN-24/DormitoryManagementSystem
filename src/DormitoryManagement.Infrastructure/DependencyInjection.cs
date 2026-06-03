
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using DormitoryManagement.Domain.Interfaces.Repositories;
using DormitoryManagement.Domain.Interfaces.UnitOfWork;
using DormitoryManagement.Application.Services.Interfaces;
using DormitoryManagement.Infrastructure.Data;
using DormitoryManagement.Infrastructure.Repositories;
using DormitoryManagement.Infrastructure.ExternalServices;

namespace DormitoryManagement.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString)
            );

            // 2. Unit of Work
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            // 3. Repositories (Giữ nguyên logic SOLID)
            services.AddScoped<IRoomRepository, RoomRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IBedRepository, BedRepository>();
            services.AddScoped<IContractRepository, ContractRepository>();
            services.AddScoped<IBlockRepository, BlockRepository>();
            services.AddScoped<IRoomTypeRepository, RoomTypeRepository>();
            services.AddScoped<IInvoiceRepository, InvoiceRepository>();
            services.AddScoped<IAssetRepository, AssetRepository>();
            services.AddScoped<IUtilityRepository, UtilityRepository>();
            services.AddScoped<IVehicleRepository, VehicleRepository>();
            services.AddScoped<IViolationRepository, ViolationRepository>();
            services.AddScoped<IStatisticRepository, StatisticRepository>();
            services.AddScoped<IUtilityServiceRequestRepository, UtilityServiceRequestRepository>();
            services.AddScoped<IUtilityUsageRepository, UtilityUsageRepository>();
            services.AddScoped<IPaymentRepository, PaymentRepository>();
            services.AddScoped<IVisitorLogRepository, VisitorLogRepository>();
            services.AddScoped<IMaintenanceRequestRepository, MaintenanceRequestRepository>();

            // Surcharge
            services.AddScoped<ISurchargeRepository, SurchargeRepository>();

            // 4. External Services (Email, JWT, etc.)
            services.AddScoped<IEmailService, EmailService>();

            return services;
        }
    }
}