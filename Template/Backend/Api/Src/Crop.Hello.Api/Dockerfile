# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/runtime:9.0 AS base

# 필수 패키지 설치
#   - procps        : ps -ef
#   - net-tools     : ifconfig
#   - iputils-ping  : ping
#   - curl          : curl
#   - sudo          : sudo
USER root
RUN apt-get update -q \
  && apt-get --no-install-recommends --no-install-suggests --yes --quiet install \
          procps \
          net-tools \
          iputils-ping \
          curl \
          sudo \
  && apt-get clean \
  && apt-get --yes --quiet autoremove --purge \
  && rm -rf /var/lib/apt/lists/* /tmp/* /var/tmp/* \
            /usr/share/doc/* /usr/share/groff/* /usr/share/info/* /usr/share/linda/* \
            /usr/share/lintian/* /usr/share/locale/* /usr/share/man/*

USER $APP_UID
WORKDIR /app

# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY ["Directory.Packages.props", "."]
COPY ["Directory.Build.props", "."]
COPY ["nuget.config", "."]

COPY ["Backend/Api/Src/Crop.Hello.Api/Crop.Hello.Api.csproj", "Backend/Api/Src/Crop.Hello.Api/"]
COPY ["Backend/Api/Src/Crop.Hello.Api.Adapters.Infrastructure/Crop.Hello.Api.Adapters.Infrastructure.csproj", "Backend/Api/Src/Crop.Hello.Api.Adapters.Infrastructure/"]
COPY ["Assets/Frameworks/Src/Crop.Hello.Framework/Crop.Hello.Framework.csproj", "Assets/Frameworks/Src/Crop.Hello.Framework/"]
COPY ["Assets/Frameworks/Src/Crop.Hello.Framework.Contracts/Crop.Hello.Framework.Contracts.csproj", "Assets/Frameworks/Src/Crop.Hello.Framework.Contracts/"]
COPY ["Backend/Api/Src/Crop.Hello.Api.Application/Crop.Hello.Api.Application.csproj", "Backend/Api/Src/Crop.Hello.Api.Application/"]
COPY ["Backend/Api/Src/Crop.Hello.Api.Domain/Crop.Hello.Api.Domain.csproj", "Backend/Api/Src/Crop.Hello.Api.Domain/"]
COPY ["Assets/Domains/Src/Crop.Hello.Domain/Crop.Hello.Domain.csproj", "Assets/Domains/Src/Crop.Hello.Domain/"]
COPY ["Backend/Api/Src/Crop.Hello.Api.Adapters.Persistence/Crop.Hello.Api.Adapters.Persistence.csproj", "Backend/Api/Src/Crop.Hello.Api.Adapters.Persistence/"]

RUN dotnet restore "./Backend/Api/Src/Crop.Hello.Api/Crop.Hello.Api.csproj"
COPY . .
WORKDIR "/src/Backend/Api/Src/Crop.Hello.Api"
RUN dotnet build "./Crop.Hello.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Crop.Hello.Api.csproj" \
	-c $BUILD_CONFIGURATION \
	-o /app/publish \
	/p:UseAppHost=false \
    --os linux \
    --arch x64 \
    /t:PublishContainer

    # 컨테이너
    #   console, worker: /t:PublishContainer
    #   webapi: -p:PublishProfile=DefaultContainer

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Crop.Hello.Api.dll"]