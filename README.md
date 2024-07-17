# EMI_API

#Section 2: ASP.NET Core
	* Describe how you would implement authentication and authorization in this API.
		En ASP.Net Core se puede implementar un sistema de usuarios con Identity, con identity se nos crean las tablas de SQL Server necesarias, ademas de un conjunto de métodos auxiliares para manejar en distintos escenarios. Luego de autenticado, nuestro WebApi devolvería un Json Web Token o JWT que es un string seguro firmado con una llave secreta, de manera que se puede estar seguro de la veracidad de la información que contiene el JWT. JWT contiene claims que es la información acerca del usuario como, nombre, email, id entre otros. Luego cuando el usuario quiera utilizar nuestros endpoints protegidos, debera enviar el JWT atravez de la cabecera de la petición y de esta manera podrá validarse como un usuario autenticado. Por ultimo se configuraría la autorizacion basada en claims,  se crearía una política para proteger los endpoints que deseemos. Con esta política se le configura el claim  que debe tener un tipo de usuario especifico para acceder a todos los endpoints, ejemplo un usuario registrado solo puede acceder a los endpoints tipo get, y un usuario con el claim configurado “admin” puede acceder a todos los endpoints. 
	
	* Explain the concept of middleware in ASP.NET Core.
		Los middleware definen acciones que queremos ejecutar cada vez que una petición http sea recibida por nuestra aplicación.

#Section 3: Authentication and Authorization

	* Provide an example of how to protect the endpoints in the employees controller using these roles.
		Se debe agregar el atributo  [Authorize(Roles = Roles.ADMIN)]  al servicio que se quiere aplicar reglas de autorizacion
		o tambien se puede en los endpoints mediante el metodo .RequireAuthorization(Roles.ADMIN)
	
#Section 4: Database Design and EF Core

	* Design a SQL database schema for an employee management system. Include tables for employees, departments, and projects. Also include a table for position history to track the different positions an employee has held.
	* Using Entity Framework Core, write the code to create the database context and entity configurations for the above schema.
	
		Se crea en la libreria EMI_API.Data
		
	* Write a LINQ query using EF Core to fetch all employees who are part of a specific department and are working on at least one project.
	
		Se expone en el servicio /api/employees/ByDepartmentIdInAnyProject/departmentId
		
#Section 5: Performance and Optimization 

	* What are some common performance issues in .NET applications and how can you address them? 

		Malas practicas de programación, como es el uso excesivo de recursos en operaciones de bucles, o falta de gestión de excepciones que pueden ralentizar la aplicación. Se puede solucionar realizando revisiones de código, utilizar herramientas de perfilado y optimización y seguir las practicas de codificación eficientes  ayuda a mejorar el rendimiento de la aplicación.

		Consultas ineficientes a base de datos, consultas mal diseñadas  o sin indices pueden ralentizar bastante la aplicación. Se puede solucionar  mediante la optimización de consultas con el uso de índices adecuados, revisión del modelo de datos y el uso de técnicas como la carga diferida ayudan a mejorar el rendimiento.

		Falta de almacenamiento de cache, acceder repetidamente a datos que raramente cambia sin almacenar en cache  puede afectar el rendimiento. Se puede Solucionar implementando estrategias de almacenamiento en cache como el uso de Redis para datos que se acceden con frecuencia puede reducir la carga en la base de datos y mejorar los tiempos de respuesta.

		Versiones desactualizadas de .NET o bibliotecas enn general, el no utilizar las ultimas versiones de las bibliotecas pueden afectar el rendimiento y seguridad de la aplicación.
		Para esto, lo mejor es mantenerse al día con las actualizaciones de .NET y bibliotecas utilizadas, y optimizar el uso de las nuevas características y mejoras de rendimiento, puede mejorar significativamente el rendimiento de la aplicación.


	* Describe how you would profile and optimize a slow-running query in an ASP.NET Core application. 

		Identificar la consulta especifica que esta causando los problemas de rendimiento, mediante las herramientas SQL Server Profiler , el SQL Management Studio o el  diagnóstico de Entity Framework Core. Luego Identificar los cuellos de botella de la consulta, como la revision de las tablas, la falta de indices adecuados  olas operaciones costosas. Luego se procedería a optimizar la consulta, asegurando que haya índices apropiados creados en las columnas utilizadas en las cláusulas WHERE y JOIN, si es necesario rescribir la consulta para minimizar el numero de operaciones y emplear funciones optimizadas.
		Otro factor a tener en cuenta es el código de la aplicación, realizando la refactorización del mismo empleando tecnicas como el uso eficiente de entity framework core, la carga diferida o diferida explicita según sea necesario para minimizar la sobrecarga y mejorar la eficiencia de acceso a datos.
		Despues de esto realizar las pruebas correspondientes para corroborar que la optimización de la consulta ha mejorado el rendimiento de la misma, con pruebas de cargas y estrés para simular el uso en ambiente de productivo por ultimo llevar el monitoreo  continuo de las consultas criticas de la aplicación