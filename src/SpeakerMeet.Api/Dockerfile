#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0-alpine AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

RUN apk add icu-libs
# https://www.abhith.net/blog/docker-sql-error-on-aspnet-core-alpine/
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false

ADD https://github.com/ufoscout/docker-compose-wait/releases/download/2.5.0/wait /wait
RUN /bin/ash -c 'ls -la /wait; chmod +x /wait; ls -la /wait'

FROM mcr.microsoft.com/dotnet/sdk:6.0-alpine AS build
WORKDIR /src
COPY ["src/SpeakerMeet.Api/SpeakerMeet.Api.csproj", "src/SpeakerMeet.Api/"]
COPY ["src/SpeakerMeet.Core/SpeakerMeet.Core.csproj", "src/SpeakerMeet.Core/"]
COPY ["src/SpeakerMeet.Infrastructure/SpeakerMeet.Infrastructure.csproj", "src/SpeakerMeet.Infrastructure/"]
RUN dotnet restore "src/SpeakerMeet.Api/SpeakerMeet.Api.csproj"
COPY . .
WORKDIR "/src/src/SpeakerMeet.Api"
RUN dotnet build "SpeakerMeet.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SpeakerMeet.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

CMD /wait && dotnet SpeakerMeet.Api.dll