#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["src/Presentation/Sample.Api/Sample.Api.csproj", "src/Presentation/Sample.Api/"]
COPY ["src/Infrastructure/Sample.Infrastructure/Sample.Infrastructure.csproj", "src/Infrastructure/Sample.Infrastructure/"]
COPY ["src/Application/Sample.Application.Contract/Sample.Application.Contract.csproj", "src/Application/Sample.Application.Contract/"]
COPY ["src/Domain/Sample.Domain/Sample.Domain.csproj", "src/Domain/Sample.Domain/"]
COPY ["SharedKernel/Sample.SharedKernel/Sample.SharedKernel.csproj", "SharedKernel/Sample.SharedKernel/"]
COPY ["src/Application/Sample.Application/Sample.Application.csproj", "src/Application/Sample.Application/"]
RUN dotnet restore "src/Presentation/Sample.Api/Sample.Api.csproj"
COPY . .
WORKDIR "/src/src/Presentation/Sample.Api"
RUN dotnet build "Sample.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Sample.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Sample.Api.dll"]