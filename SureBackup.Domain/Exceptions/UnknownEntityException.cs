

namespace SureBackup.Domain.Exceptions;

public class UnknownEntityException(string? entityName=null,string? message=null):Exception(message:!string.IsNullOrEmpty(entityName)?$"The {entityName} could not be found.":message)
{
}
