 
using HotelManagement.Application.Interfaces;
using HotelManagement.Application.Services;
using HotelManagement.Infrastructure.Repositories;
using HotelManagement.Infrastructure.Services;
using Minio;

namespace HotelManagement.Api.Extensions;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddHotelManagementServices(
        this IServiceCollection services,
        string connectionString,
        IConfiguration configuration)
    {
        // ==========================================
        // Property
        // ==========================================

        services.AddScoped<IPropertyRepository>(
            _ => new PropertyRepository(connectionString));

        services.AddScoped<IPropertyService, PropertyService>();


        // ==========================================
        // Unit Type
        // ==========================================

        services.AddScoped<IUnitTypeRepository>(
            _ => new UnitTypeRepository(connectionString));

        services.AddScoped<IUnitTypeService, UnitTypeService>();


        // ==========================================
        // Unit
        // ==========================================

        services.AddScoped<IUnitRepository>(
            _ => new UnitRepository(connectionString));

        services.AddScoped<IUnitService, UnitService>();


        // ==========================================
        // Guest
        // ==========================================

        services.AddScoped<IGuestRepository>(
            _ => new GuestRepository(connectionString));

        services.AddScoped<IGuestService, GuestService>();


        // ==========================================
        // Reservation
        // ==========================================

        services.AddScoped<IReservationRepository>(
            _ => new ReservationRepository(connectionString));

        services.AddScoped<IReservationService, ReservationService>();


        // ==========================================
        // Company
        // ==========================================

        services.AddScoped<ICompanyRepository>(
            _ => new CompanyRepository(connectionString));

        services.AddScoped<ICompanyService, CompanyService>();


        // ==========================================
        // User
        // ==========================================

        services.AddScoped<IUserRepository>(
            _ => new UserRepository(connectionString));

        services.AddScoped<IUserService, UserService>();


        // ==========================================
        // Role
        // ==========================================

        services.AddScoped<IRoleRepository>(
            _ => new RoleRepository(connectionString));

        services.AddScoped<IRoleService, RoleService>();


        // ==========================================
        // Payment
        // ==========================================

        services.AddScoped<IPaymentRepository>(
            _ => new PaymentRepository(connectionString));

        services.AddScoped<IPaymentService, PaymentService>();


        // ==========================================
        // MinIO
        // ==========================================

        var minioSection = configuration.GetSection("MinIO");

        var rawEndpoint =
     minioSection["Endpoint"]?.Trim()
     ?? throw new InvalidOperationException(
         "MinIO:Endpoint no encontrado.");

        var accessKey =
     minioSection["AccessKey"]
     ?? throw new InvalidOperationException(
         "MinIO:AccessKey no encontrado.");

        var secretKey =
            minioSection["SecretKey"]
            ?? throw new InvalidOperationException(
                "MinIO:SecretKey no encontrado.");

        var useSsl =
            bool.TryParse(minioSection["UseSSL"], out var ssl) && ssl;

        var bucketName =
            minioSection["Bucket"]
            ?? "hotel-management";


        // Si el endpoint viene con esquema (https://host), lo normalizamos a host[:puerto]
        string endpoint;
        if (Uri.TryCreate(rawEndpoint, UriKind.Absolute, out var uri)
            && (uri.Scheme == Uri.UriSchemeHttp || uri.Scheme == Uri.UriSchemeHttps))
        {
            endpoint = uri.IsDefaultPort ? uri.Host : $"{uri.Host}:{uri.Port}";
            useSsl = uri.Scheme == Uri.UriSchemeHttps;
        }
        else
        {
            endpoint = rawEndpoint.TrimEnd('/');
        }

        services.AddSingleton<IMinioClient>(_ =>
            new MinioClient()
                .WithEndpoint(endpoint)
                .WithCredentials(accessKey, secretKey)
                .WithSSL(useSsl)
                .Build());
        // ==========================================
        // File Storage
        // ==========================================

        services.AddScoped<IFileStorageService, MinioFileStorageService>();


        // ==========================================
        // Property Images
        // ==========================================

        services.AddScoped<IPropertyImageRepository>(
            _ => new PropertyImageRepository(connectionString));

        services.AddScoped<IPropertyImageService>(provider =>
            new PropertyImageService(
                provider.GetRequiredService<IPropertyImageRepository>(),
                provider.GetRequiredService<IFileStorageService>(),
                bucketName));

        services.AddScoped<IFileStorageService, MinioFileStorageService>();


        services.AddScoped<IDocumentRepository>(
    _ => new DocumentRepository(connectionString));

        services.AddScoped<IDocumentService>(provider =>
    new DocumentService(
        provider.GetRequiredService<IDocumentRepository>(),
        provider.GetRequiredService<IFileStorageService>(),
        bucketName));


        services.AddScoped<IMonthlySummaryRepository>(
                _ => new MonthlySummaryRepository(connectionString));

        services.AddScoped<IMonthlySummaryService, MonthlySummaryService>(); 

        return services;
    }
} 