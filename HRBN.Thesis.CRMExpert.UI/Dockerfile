FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["HRBN.Thesis.CRMExpert.UI/HRBN.Thesis.CRMExpert.UI.csproj", "HRBN.Thesis.CRMExpert.UI/"]
COPY ["HRBN.Thesis.CRMExpert.Application.Core/HRBN.Thesis.CRMExpert.Application.Core.csproj", "HRBN.Thesis.CRMExpert.Application.Core/"]
COPY ["HRBN.Thesis.CRMExpert.Infrastructure/HRBN.Thesis.CRMExpert.Infrastructure.csproj", "HRBN.Thesis.CRMExpert.Infrastructure/"]
COPY ["HRBN.Thesis.CRMExpert.Domain/HRBN.Thesis.CRMExpert.Domain.csproj", "HRBN.Thesis.CRMExpert.Domain/"]
COPY ["HRBN.Thesis.CRMExpert.Domain.Core/HRBN.Thesis.CRMExpert.Domain.Core.csproj", "HRBN.Thesis.CRMExpert.Domain.Core/"]
COPY ["HRBN.Thesis.CRMExpert.Application/HRBN.Thesis.CRMExpert.Application.csproj", "HRBN.Thesis.CRMExpert.Application/"]
RUN dotnet restore "HRBN.Thesis.CRMExpert.UI/HRBN.Thesis.CRMExpert.UI.csproj"
COPY . .
WORKDIR "/src/HRBN.Thesis.CRMExpert.UI"
RUN dotnet build "HRBN.Thesis.CRMExpert.UI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "HRBN.Thesis.CRMExpert.UI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HRBN.Thesis.CRMExpert.UI.dll"]
