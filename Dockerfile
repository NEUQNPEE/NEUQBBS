# 使用 ASP.NET Core 8.0 基础镜像作为基础镜像
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base

# 设置工作目录为 /app
WORKDIR /app

# 暴露端口 5286，使容器可以接收该端口的请求
EXPOSE 5286

# 设置环境变量 ASPNETCORE_URLS 为 http://+:5286，指定 ASP.NET Core 应用监听的 URL
ENV ASPNETCORE_URLS=http://+:5286

# 设置当前用户为 app，通常是为了提高安全性，避免使用 root 用户
USER app

# 使用 .NET SDK 8.0 镜像作为构建阶段的基础镜像
FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# 定义构建时使用的配置变量，默认为 Release
ARG configuration=Release

# 设置工作目录为 /src
WORKDIR /src

# 将项目文件复制到容器中的 /src/WebApiDemo 目录
COPY ["WebApiDemo/WebApiDemo.csproj", "WebApiDemo/"]

# 运行 `dotnet restore` 命令恢复项目的依赖
RUN dotnet restore "WebApiDemo/WebApiDemo.csproj"

# 将整个代码复制到容器中的当前目录
COPY . .

# 切换到 WebApiDemo 项目目录
WORKDIR "/src/WebApiDemo"

# 运行 `dotnet build` 命令构建项目，使用指定的配置（默认为 Release），输出到 /app/build 目录
RUN dotnet build "WebApiDemo.csproj" -c $configuration -o /app/build

# 使用 build 阶段构建的镜像作为发布阶段的基础镜像
FROM build AS publish

# 定义发布时使用的配置变量，默认为 Release
ARG configuration=Release

# 运行 `dotnet publish` 命令发布项目，使用指定的配置（默认为 Release），输出到 /app/publish 目录
# /p:UseAppHost=false 参数用于避免生成平台特定的可执行文件
RUN dotnet publish "WebApiDemo.csproj" -c $configuration -o /app/publish /p:UseAppHost=false

# 使用 base 阶段的镜像作为最终运行镜像
FROM base AS final

# 设置工作目录为 /app
WORKDIR /app

# 从 publish 阶段的镜像复制发布的文件到当前镜像的 /app 目录
COPY --from=publish /app/publish .

# 设置容器的入口点为运行 `dotnet WebApiDemo.dll` 命令，启动应用程序
ENTRYPOINT ["dotnet", "WebApiDemo.dll"]
