using System;
using System.ComponentModel;
using BBDown.Core.Util;

namespace BBDown;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum)]
public class LocalizedDescriptionAttribute : DescriptionAttribute
{
    private readonly string _resourceKey;

    public LocalizedDescriptionAttribute(string resourceKey)
    {
        _resourceKey = resourceKey;
    }

    public override string Description => Localizer.GetString(_resourceKey);
}
