# Generate db migration
~~~bash
# Update tool
dotnet tool update --global dotnet-ef

# Create migration
dotnet ef migrations add Initial --project DAL.EF.App --startup-project WebApp --context ApplicationDbContext
dotnet ef migrations add Token --project DAL.EF.App --startup-project WebApp --context ApplicationDbContext
dotnet ef migrations add ScheduleId --project DAL.EF.App --startup-project WebApp --context ApplicationDbContext

# Update database
dotnet ef database update --project DAL.EF.App --startup-project WebApp --context ApplicationDbContext

# Drop database
dotnet ef database drop --project DAL.EF.App --startup-project WebApp --context ApplicationDbContext
~~~

# Generate rest controllers
~~~bash
# Install tool
dotnet tool update --global dotnet-aspnet-codegenerator

# Install packages to WebApp
- microsoft.visualstudio.web.CodeGeneration.design
- microsoft.EntityFrameworkCore.sqlserver

# MVC
dotnet aspnet-codegenerator controller -m Domain.App.Appointment        -name AppointmentsController        -outDir Controllers -dc ApplicationDbContext -f -udl --referenceScriptLibraries
dotnet aspnet-codegenerator controller -m AppointmentService -name AppointmentServicesController -outDir Controllers -dc ApplicationDbContext -f -udl --referenceScriptLibraries
dotnet aspnet-codegenerator controller -m AppointmentStatus  -name AppointmentStatusesController -outDir Controllers -dc ApplicationDbContext -f -udl --referenceScriptLibraries
dotnet aspnet-codegenerator controller -m PaymentMethod      -name PaymentMethodsController      -outDir Controllers -dc ApplicationDbContext -f -udl --referenceScriptLibraries
dotnet aspnet-codegenerator controller -m Review             -name ReviewsController             -outDir Controllers -dc ApplicationDbContext -f -udl --referenceScriptLibraries
dotnet aspnet-codegenerator controller -m Salon              -name SalonsController              -outDir Controllers -dc ApplicationDbContext -f -udl --referenceScriptLibraries
dotnet aspnet-codegenerator controller -m Domain.App.SalonService       -name SalonServicesController       -outDir Controllers -dc ApplicationDbContext -f -udl --referenceScriptLibraries
dotnet aspnet-codegenerator controller -m Domain.App.Schedule           -name SchedulesController           -outDir Controllers -dc ApplicationDbContext -f -udl --referenceScriptLibraries
dotnet aspnet-codegenerator controller -m Service            -name ServicesController            -outDir Controllers -dc ApplicationDbContext -f -udl --referenceScriptLibraries
dotnet aspnet-codegenerator controller -m ServiceType        -name ServiceTypesController        -outDir Controllers -dc ApplicationDbContext -f -udl --referenceScriptLibraries
dotnet aspnet-codegenerator controller -m Stylist            -name StylistsController            -outDir Controllers -dc ApplicationDbContext -f -udl --referenceScriptLibraries

# Rest API
dotnet aspnet-codegenerator controller -m Appointment        -name AppointmentsController        -outDir Api -api -dc ApplicationDbContext -f -udl --referenceScriptLibraries
dotnet aspnet-codegenerator controller -m Domain.App.AppointmentService -name AppointmentServicesController -outDir Api -api -dc ApplicationDbContext -f -udl --referenceScriptLibraries
dotnet aspnet-codegenerator controller -m Domain.App.AppointmentStatus  -name AppointmentStatusesController -outDir Api -api -dc ApplicationDbContext -f -udl --referenceScriptLibraries
dotnet aspnet-codegenerator controller -m Domain.App.PaymentMethod      -name PaymentMethodsController      -outDir Api -api -dc ApplicationDbContext -f -udl --referenceScriptLibraries
dotnet aspnet-codegenerator controller -m Domain.App.Review             -name ReviewsController             -outDir Api -api -dc ApplicationDbContext -f -udl --referenceScriptLibraries
dotnet aspnet-codegenerator controller -m Salon              -name SalonsController              -outDir Api -api -dc ApplicationDbContext -f -udl --referenceScriptLibraries
dotnet aspnet-codegenerator controller -m Domain.App.SalonService       -name SalonServicesController       -outDir Api -api -dc ApplicationDbContext -f -udl --referenceScriptLibraries
dotnet aspnet-codegenerator controller -m Domain.App.Schedule           -name SchedulesController           -outDir Api -api -dc ApplicationDbContext -f -udl --referenceScriptLibraries
dotnet aspnet-codegenerator controller -m Service            -name ServicesController            -outDir Api -api -dc ApplicationDbContext -f -udl --referenceScriptLibraries
dotnet aspnet-codegenerator controller -m ServiceType        -name ServiceTypesController        -outDir Api -api -dc ApplicationDbContext -f -udl --referenceScriptLibraries
dotnet aspnet-codegenerator controller -m Stylist            -name StylistsController            -outDir Api -api -dc ApplicationDbContext -f -udl --referenceScriptLibraries
~~~

Generate Identity UI

~~~bash
cd WebApp
dotnet aspnet-codegenerator identity -dc DAL.EF.App.ApplicationDbContext --userClass AppUser -f
~~~