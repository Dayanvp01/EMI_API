namespace EMI_API.Utils
{
    public static class OpenApiConfigurationExtensions
    {

        
        public static RouteHandlerBuilder CreateDocumentation(this RouteHandlerBuilder builder, string entity)
        {
            return builder.WithOpenApi(opt =>
            {
                opt.Summary = $"Crear {entity}";
                opt.Description = $"Con este endpoint podemos crear una entidad {entity}";
                if (opt.RequestBody != null)
                {
                    opt.RequestBody.Description = $"Entidad {entity} que se desea crear";
                }
                return opt;
            });
        }


        public static RouteHandlerBuilder DeleteDocumentation(this RouteHandlerBuilder builder, string entity)
        {
            return builder.WithOpenApi(opt =>
            {
                opt.Summary = $"Eliminar {entity}";
                opt.Description = $"Con este endpoint podemos elimniar una entidad {entity}";
                if (opt.Parameters.Count > 0)
                {
                    opt.Parameters[0].Description = $"Id de la entidad {entity} a eliminar";
                }
                return opt;
            });
        }

        public static RouteHandlerBuilder UpdateDocumentation(this RouteHandlerBuilder builder, string entity)
        {
            return builder.WithOpenApi(opt =>
            {
                opt.Summary = $"Actualizar {entity}";
                opt.Description = $"Con este endpoint podemos actualizar una entidad {entity}";
                if (opt.Parameters.Count > 0)
                {
                    opt.Parameters[0].Description = $"Id de la entidad {entity} a actualizar";
                }
                if (opt.RequestBody != null)
                {
                    opt.RequestBody.Description = $"Entidad {entity} que se desea actualizar";
                }
                return opt;
            });
        }

        public static RouteHandlerBuilder GetDocumentation(this RouteHandlerBuilder builder, string entity)
        {
            return builder.WithOpenApi(opt =>
            {
                opt.Summary = $"Obtener todos los {entity}";
                opt.Description = $"Con este endpoint podemos obtner todas las entidades {entity}";
                return opt;
            });
        }

        public static RouteHandlerBuilder GetByIdDocumentation(this RouteHandlerBuilder builder, string entity)
        {
            return builder.WithOpenApi(opt =>
            {
                opt.Summary = $"Obtener {entity}";
                opt.Description = $"Con este endpoint podemos obtner una entidad {entity} por su id";
                if (opt.Parameters.Count > 0)
                {
                    opt.Parameters[0].Description = $"Id de la entidad {entity} a consultar";
                }
               
                return opt;
            });
        }
    }
}
