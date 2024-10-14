# Microservicio de Gestión de Pedidos

Este proyecto es un microservicio RESTful para la gestión de pedidos y productos, desarrollado con .NET Core, C# y SQL Server.

## Tabla de Contenidos

- [Clonar el repositorio](#clonar-el-repositorio)
- [Configurar el entorno de desarrollo](#configurar-el-entorno-de-desarrollo)
- [Ejecutar la aplicación](#ejecutar-la-aplicación)
- [Realizar peticiones a la API](#realizar-peticiones-a-la-api)

## Clonar el Repositorio

Para clonar el repositorio, utiliza el siguiente comando:

```bash
git clone git@github.com:luanarmo/gestor_pedido_api.git
cd gestor_pedido_api
```

## Configurar el Entorno de Desarrollo

### Prerrequisitos

- [.NET Core SDK](https://dotnet.microsoft.com/download) (versión 6.0 o superior)
- [SQL Server](https://www.microsoft.com/es-es/sql-server/sql-server-downloads)

### Pasos de Configuración

1. Restaura las dependencias del proyecto:
   ```
   dotnet restore
   ```

2. Configura la cadena de conexión a la base de datos:
   - Agrega la linea siguiente en el archivo `appsettings.json`:
     ```
       "ConnectionStrings": {
          "DefaultConnection": "Server=localhost;Database=gestor_pedidos;User Id=sa;Password=TuContraseña;TrustServerCertificate=True;"
       }
     ```

3. Aplica las migraciones a la base de datos:
   ```
   dotnet ef database update
   ```

## Ejecutar la Aplicación

Para ejecutar la aplicación en modo de desarrollo, usa el siguiente comando:

```
dotnet run
```

La API estará disponible en `https://localhost:5179` (o el puerto que hayas configurado).

## Realizar Peticiones al Microservicio

Aquí tienes algunos ejemplos de cómo realizar peticiones al microservicio:


### Crear un Nuevo Producto
![image](https://github.com/user-attachments/assets/e8add087-4e96-4512-aae5-b0f279f78f04)

### Listar Todos los Productos
![image](https://github.com/user-attachments/assets/abac105f-115a-45af-9ea8-d39be831b5cf)

### Obtener un Producto
![image](https://github.com/user-attachments/assets/5d4b1476-171b-4d23-8090-9c2ecff50b8a)

### Actualizar un Producto
![image](https://github.com/user-attachments/assets/ba2f5b15-cf1b-463d-b822-6a21de96c622)

### Eliminar un Producto
![image](https://github.com/user-attachments/assets/56ebd973-d764-4965-a7b8-7a5f8f4a76d0)

### Crear un Nuevo Pedido
![image](https://github.com/user-attachments/assets/7362a9eb-ff99-4312-a5ad-7fef7363cf95)

### Listar Todos los Pedidos
![image](https://github.com/user-attachments/assets/24964f25-535d-4127-adfd-f5819feec5f8)

### Obtener un Pedido
![image](https://github.com/user-attachments/assets/db953174-fbc3-40d8-ab7c-f21285f226ba)

### Agregar un Producto a un Pedido
![image](https://github.com/user-attachments/assets/e06300e3-d516-41a3-b144-8f1090ce3a4f)

### Listar los Productos de un Pedido
![image](https://github.com/user-attachments/assets/1cfc12ca-a798-41ea-a782-2b99acc9809f)

### Actualizar un Producto de un Pedido
![image](https://github.com/user-attachments/assets/456b9e32-ec38-4d19-b06b-84282c2fc7b4)

### Eliminar un Producto de un Pedido
![image](https://github.com/user-attachments/assets/94bcbb21-15d0-4958-8138-e4f609c23f09)

### Eliminar un Pedido
![image](https://github.com/user-attachments/assets/8bd4aab9-a2d5-473d-bd5a-8406b81fa4a2)


## Despliegue del Microservicio

Para desplegar el microservicio en Azure, sigue estos pasos:

1. Crea un recurso de Azure App Service:
   - Ve al [Portal de Azure](https://portal.azure.com)
   - Crea un nuevo recurso de App Service
   - Selecciona el plan de servicio adecuado

2. Configura la publicación desde Visual Studio o usando Azure CLI:
   - En Visual Studio: Haz clic derecho en el proyecto > Publicar > Selecciona tu App Service
   - Con Azure CLI:
     ```
     az webapp up --sku F1 --name tu-app-service
     ```

3. Configura las variables de entorno en Azure:
   - En el portal de Azure, ve a tu App Service
   - Navega a Configuración > Configuración de la aplicación
   - Agrega la variable `DB_CONNECTION_STRING` con el valor correcto

4. (Opcional) Configura un pipeline de CI/CD:
   - Usa Azure DevOps o GitHub Actions para automatizar el despliegue

5. Monitorea tu aplicación:
   - Usa Azure Application Insights para monitorear el rendimiento y los errores

Recuerda siempre seguir las mejores prácticas de seguridad, como usar Azure Key Vault para gestionar secretos y credenciales.

