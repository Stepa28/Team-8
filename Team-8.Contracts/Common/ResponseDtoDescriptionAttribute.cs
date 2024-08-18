using System.ComponentModel;

namespace Team_8.Contracts.Common;

public class ResponseDtoDescriptionAttribute(Type typeDto) : DescriptionAttribute(typeDto.Name)
{
    public Type TypeDto => typeDto;
}