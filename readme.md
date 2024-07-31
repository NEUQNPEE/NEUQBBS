# 项目简介

详见计算机网络实验报告完成模板

# 运行方式

您应当自行配置.net环境。本项目使用.NET 7.0。

## 前端

在克隆项目后，您应该在项目目录中打开控制台输入以下命令：

```
dotnet add package AntDesign
```

您还需要为项目引入以下 nuget 包：

```
TinyMCE (本项目采用版本7.2.1)
TinyMCE.Blazor (本项目采用版本1.0.4)
```

## 后端

本系统的前期版本跟随教程[C#/.NET BBS论坛项目开发全程实录（WebAPI、ASP .NET Core、EfCore、VUE、ElementUI、SqlServer、MVC）](https://www.bilibili.com/video/BV1ZT411V7u2)完成。后端运行需要的EFCore包如何引入可参见该视频的P98。

您还需要自行建立数据库。至于数据表您可以使用EFCore直接生成，项目中保留了数据表的迁移记录。