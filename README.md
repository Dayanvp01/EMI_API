# EMI_API

## Configuración de la Base de Datos

1. Configurar la conexión de la base de datos en el archivo `appsettings.json`:

    ```json
    "ConnectionStrings": {
        "DefaultConnection": "Server=localhost,1433;Database=EMI_API_DB;User Id=sa;Password=demoEmi@Passw0rd;TrustServerCertificate=True"
    }
    ```

2. Si no tiene SQL Server instalado, puede instalar una imagen Docker mediante el siguiente comando:

    ```sh
    docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=demoEmi@Passw0rd" -p 1433:1433 --name sqlserver -d mcr.microsoft.com/mssql/server
    ```

## Migraciones y Actualización de la Base de Datos

1. Establecer el proyecto `EMI_API.Data` como el principal.
2. Abrir la Consola del Administrador de Paquetes y seleccionar `EMI_API.Data` como proyecto predeterminado.
3. Ejecutar los siguientes comandos en la consola:

    ```sh
    Add-Migration crearBaseDatos
    Update-Database
    ```

   - `Add-Migration crearBaseDatos` crea las migraciones de las entidades a tablas de la base de datos.
   - `Update-Database` crea las tablas en la base de datos o aplica la migración.

## Seguridad

Pasar el valor de la llave `"Authentication:Schemes:Bearer:SigningKeys"` a los `secrets.json` del proyecto por motivos de seguridad.

## Sección 1: Programación en C#

- La solución de esta sección está en la clase `EmployeeExtension`.

## Sección 2: ASP.NET Core

### Autenticación y Autorización

En ASP.Net Core, se puede implementar un sistema de usuarios con Identity. Identity crea las tablas de SQL Server necesarias y proporciona métodos auxiliares para manejar diferentes escenarios. Una vez autenticado, el Web API devolverá un JSON Web Token (JWT), un string seguro firmado con una llave secreta, asegurando la veracidad de la información contenida en el JWT. El JWT contiene claims con información del usuario, como nombre, email, id, entre otros. Para utilizar endpoints protegidos, el usuario debe enviar el JWT a través de la cabecera de la petición, validándose como usuario autenticado. Luego, se configura la autorización basada en claims y se crea una política para proteger los endpoints deseados. Esta política configura el claim requerido para que un usuario específico acceda a todos los endpoints. Por ejemplo, un usuario registrado puede acceder solo a los endpoints tipo GET, mientras que un usuario con el claim "admin" puede acceder a todos los endpoints.

### Middleware en ASP.NET Core (Explain the concept of middleware in ASP.NET Core)

Los middleware definen acciones que se ejecutan cada vez que una petición HTTP es recibida por la aplicación. En la clase CustomMiddleware se ve un ejemplo de la implementación de un middleware. 

## Sección 3: Autenticación y Autorización (Provide an example of how to protect the endpoints in the employees controller using these roles.)

### Protección de Endpoints en el Controlador de Empleados

Para aplicar reglas de autorización, se debe agregar el atributo `[Authorize(Roles = Roles.ADMIN)]` al servicio deseado o utilizar el método `.RequireAuthorization(Roles.ADMIN)` en los endpoints.

## Sección 4: Diseño de Base de Datos y EF Core

### Esquema de Base de Datos para un Sistema de Gestión de Empleados

- **Employees**: tabla para almacenar información de los empleados.
- **Departments**: tabla para almacenar información de los departamentos.
- **Project**: tabla para almacenar información de los proyectos.
- **PositionsHistories**: tabla para rastrear las diferentes posiciones que ha tenido un empleado.
- **EmployeesProjects**: tabla intermedia para relacion de empleados y proyectos, realacion muchos a muchos

### Código para Crear el Contexto de la Base de Datos y Configuración de Entidades con Entity Framework Core

El código se encuentra en la librería `EMI_API.Data`.

### Consulta LINQ con EF Core para Obtener Empleados de un Departamento Específico que Trabajan en al Menos un Proyecto

El servicio expone esta consulta en el endpoint `/api/employees/ByDepartmentIdInAnyProject/departmentId`.

## Sección 5: Rendimiento y Optimización

### Problemas Comunes de Rendimiento en Aplicaciones .NET y Soluciones

- **Malas practicas de programación, como es el uso excesivo de recursos en operaciones de bucles, o falta de gestión de excepciones que pueden ralentizar la aplicación. Se puede solucionar realizando revisiones de código, utilizar herramientas de perfilado y optimización y seguir las practicas de codificación eficientes  ayuda a mejorar el rendimiento de la aplicación.
- **Consultas ineficientes a base de datos, consultas mal diseñadas  o sin indices pueden ralentizar bastante la aplicación. Se puede solucionar  mediante la optimización de consultas con el uso de índices adecuados, revisión del modelo de datos y el uso de técnicas como la carga diferida ayudan a mejorar el rendimiento..
- **Falta de almacenamiento de cache, acceder repetidamente a datos que raramente cambia sin almacenar en cache  puede afectar el rendimiento. Se puede Solucionar implementando estrategias de almacenamiento en cache como el uso de Redis para datos que se acceden con frecuencia puede reducir la carga en la base de datos y mejorar los tiempos de respuesta.
- **Versiones desactualizadas de .NET o bibliotecas**: no utilizar las últimas versiones puede afectar el rendimiento y la seguridad. Solución: mantenerse al día con las actualizaciones de .NET y bibliotecas.

### Perfilado y Optimización de Consultas Lentas en una Aplicación ASP.NET Core

1. Identificar la consulta específica que causa problemas de rendimiento mediante herramientas como SQL Server Profiler, SQL Management Studio o diagnóstico de Entity Framework Core.
2. Identificar cuellos de botella en la consulta, como falta de índices o operaciones costosas.
3. Optimizar la consulta asegurando que haya índices apropiados en las columnas utilizadas en las cláusulas WHERE y JOIN.
4. Refactorizar el código de la aplicación utilizando técnicas como carga diferida.
5. Realizar pruebas para corroborar que la optimización ha mejorado el rendimiento.
6. Monitoreo continuo de consultas críticas en la aplicación.
