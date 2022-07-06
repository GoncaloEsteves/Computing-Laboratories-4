# IO.Swagger - ASP.NET Core 2.0 Server

Rest API of the mobile app PlugGoBeyond of the project course LI IV in the program Mestrado Integrado em Engenharia Informática

## Run

Linux/OS X:

```
sh build.sh
```

Windows:

```
build.bat
```

## Run in Docker

```
cd src/IO.Swagger
docker build -t io.swagger .
docker run -p 5000:5000 io.swagger
```
