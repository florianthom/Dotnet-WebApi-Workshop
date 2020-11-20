FROM mcr.microsoft.com/dotnet/sdk:5.0 as build

ARG BUILDCONFIG=RELEASE
ARG VERSION=1.0.0

COPY homepageBackend.csproj /build/

RUN dotnet restore ./build/homepageBackend.csproj

COPY . ./build/
WORKDIR /build/
RUN dotnet publish ./homepageBackend.csproj -c $BUILDCONFIG -o out /p:Version=$VERSION

WORKDIR /app
RUN mv /build/out .
RUN rm -r /build
WORKDIR /app/out
ENTRYPOINT ["dotnet", "homepageBackend.dll", "--urls", "https://0.0.0.0:5000"]
