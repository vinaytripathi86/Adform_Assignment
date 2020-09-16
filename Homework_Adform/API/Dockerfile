#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["API/Homework_Adform.TodoAPI.csproj", "API/"]
COPY ["Homework_Adform.CommonLibrary/Homework_Adform.CommonLibrary.csproj", "Homework_Adform.CommonLibrary/"]
COPY ["Homework_Adform.Services/Homework_Adform.Services.csproj", "Homework_Adform.Services/"]
COPY ["Homework_Adform.DAL/Homework_Adform.DAL.csproj", "Homework_Adform.DAL/"]
RUN dotnet restore "API/Homework_Adform.TodoAPI.csproj"
COPY . .
WORKDIR "/src/API"
RUN dotnet build "Homework_Adform.TodoAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Homework_Adform.TodoAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Homework_Adform.TodoAPI.dll"]