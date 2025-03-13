# Etapa 1: Build da aplicação
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copia os arquivos de projeto (.csproj) primeiro para otimizar o cache de build
COPY GeekStore.Domain/*.csproj ./GeekStore.Domain/
COPY GeekStore.Application/*.csproj ./GeekStore.Application/
COPY GeekStore.Infrastructure/*.csproj ./GeekStore.Infrastructure/
COPY GeekStore.API/*.csproj ./GeekStore.API/

# Restaura as dependências
RUN dotnet restore GeekStore.API/GeekStore.API.csproj

# Copia todos os arquivos do projeto para dentro do container
COPY GeekStore.API/ ./GeekStore.API/
COPY GeekStore.Domain/ ./GeekStore.Domain/
COPY GeekStore.Application/ ./GeekStore.Application/
COPY GeekStore.Infrastructure/ ./GeekStore.Infrastructure/

# Compila a aplicação em modo Release
RUN dotnet publish GeekStore.API/GeekStore.API.csproj -c Release -o /publish

# Etapa 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

# Copia os arquivos compilados da etapa anterior
COPY --from=build /publish .

# Define a porta exposta no container
EXPOSE 5005

# Comando para iniciar a API
ENTRYPOINT ["dotnet", "GeekStore.API.dll"]