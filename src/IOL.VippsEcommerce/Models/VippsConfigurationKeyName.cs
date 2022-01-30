using System;

namespace IOL.VippsEcommerce.Models;

[AttributeUsage(AttributeTargets.Property)]
internal sealed class VippsConfigurationKeyName : Attribute
{
	/// <summary>
	/// Specifies a name for this configuration value.
	/// </summary>
	/// <param name="name">Name of the configuration value.</param>
	public VippsConfigurationKeyName(string name) {
		Name = name;
	}

	public string Name { get; }
}