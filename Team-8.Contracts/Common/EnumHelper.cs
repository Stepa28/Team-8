using System.ComponentModel;

namespace Team_8.Contracts.Common;

public static class EnumHelper
{
    public static string GetRequestDtoName<T>(this T enumValue) 
        where T : struct, IConvertible
    {
        if (!typeof(T).IsEnum)
            return "";
        
        var attribute = GetAttributeFromEnum<RequestDtoDescriptionAttribute, T>(enumValue);
        return attribute?.Description ?? enumValue.ToString()!;
    }
    
    public static Type? GetRequestDtoType<T>(this T enumValue) 
        where T : struct, IConvertible
    {
        if (!typeof(T).IsEnum)
            return null;
        
        var attribute = GetAttributeFromEnum<RequestDtoDescriptionAttribute, T>(enumValue);
        return attribute?.TypeDto;
    }
    
    public static string GetResponseDtoName<T>(this T enumValue) 
        where T : struct, IConvertible
    {
        if (!typeof(T).IsEnum)
            return "";

        var attribute = GetAttributeFromEnum<ResponseDtoDescriptionAttribute, T>(enumValue);
        return attribute?.Description ?? enumValue.ToString()!;
    }
    
    public static Type? GetResponseDtoType<T>(this T enumValue) 
        where T : struct, IConvertible
    {
        if (!typeof(T).IsEnum)
            return null;
        
        var attribute = GetAttributeFromEnum<ResponseDtoDescriptionAttribute, T>(enumValue);
        return attribute?.TypeDto;
    }

    private static V? GetAttributeFromEnum<V, T>(T enumValue) 
    where T : struct, IConvertible
    where V : DescriptionAttribute
    {
        var fieldInfo = enumValue.GetType().GetField(enumValue.ToString());

        if(fieldInfo != null)
        {
            var attrs = fieldInfo.GetCustomAttributes(typeof(V), true);
            if(attrs is { Length: > 0 })
                return attrs[0] as V;
        }

        return null;
    }
}