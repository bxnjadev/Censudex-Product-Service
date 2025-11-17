# AuthService

Servicio de productos para la arquitectura de microservicios Censudex.

## Arquitectura y Patrón de Diseño

### Arquitectura: Microservicios con API Gateway

El AuthService implementa una arquitectura de capas, basada en modelo, repositorio y servicio.

### Patrones de Diseño Implementados:

1. **Data Transfer Object (DTO):** Transferencia de datos entre capas
2. **Dependency Injection:** Inyección de dependencias nativa de ASP.NET Core

## Tecnologías Utilizadas

- C# 
- .NET 9.0
- MongoDB
- Cloudinary

## Instalación
1.- Primero debemos abrir la consola de comandos apretando las siguientes teclas y escribir 'cmd':

- "Windows + R" y escribimos 'cmd'

2.- Ahora debemos crear una carpeta en donde guardar el proyecto, esta carpeta puede estar donde desee el usuario:
```bash
mkdir [NombreDeCarpeta]
```
3.- Accedemoss a la carpeta.
```bash
cd NombreDeCarpeta
```
4.- Se debe clonar el repositorio en el lugar deseado por el usuario con el siguiente comando:
```bash
git clone https://github.com/bxnjadev/Censudex-Product-Service
```
5.- Accedemos a la carpeta creada por el repositorio:
```bash
cd Censudex-Preoduct-Service
```
6.- Ahora debemos restaurar las dependencias del proyecto con el siguiente comando:
```bash
dotnet restore
```
7.- Con las dependencias restauradas, abrimos el editor:
```bash
code .
```
8.- Establecer las credenciales del archivo .appsettings.json

9.- Finalmente ya en la consola ejecutamos lo siguiente
```bash
dotnet run
```
10.- Es necesario tener corriendo en local el servicio de clientes


## Consultas disponibles
Se encuentran en el archivo Auth-Service.postman_collection.json