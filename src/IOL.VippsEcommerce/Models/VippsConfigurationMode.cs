namespace IOL.VippsEcommerce.Models
{
	public enum VippsConfigurationMode
	{
		/// <summary>
		/// Only check for values in the configuration object.
		/// </summary>
		ONLY_OBJECT = 0,

		/// <summary>
		/// Check for values in environment, then in the configuration object.
		/// </summary>
		ENVIRONMENT_THEN_OBJECT = 1,

		/// <summary>
		/// Check for values in the configuration object, then in environment.
		/// </summary>
		OBJECT_THEN_ENVIRONMENT = 2,

		/// <summary>
		/// Only check for values in environment.
		/// </summary>
		ONLY_ENVIRONMENT = 3,
	}
}