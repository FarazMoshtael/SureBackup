
using System.ComponentModel.DataAnnotations;

namespace SureBackup.Domain.ExtensionMethods;

public static class ObjectExtensionMethods
{
    public static string ValidateRegex(this object instance, string memberName, string propertyValue)
    {
        ValidationContext context = new(instance) { MemberName = memberName };
        Validator.ValidateProperty(propertyValue, context);
        return propertyValue;
    }
}
