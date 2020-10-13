using System;

/// <summary>
/// Description:
///     Developing Tag
/// </summary>
[AttributeUsage(AttributeTargets.Class |
AttributeTargets.Constructor |
AttributeTargets.Field |
AttributeTargets.Method |
AttributeTargets.Property,
AllowMultiple = true)]
public class DevelopingTag : System.Attribute
{
    public DevelopingTag(string message)
    {
        Message = message;
    }

    public string Message { get; private set; }

}