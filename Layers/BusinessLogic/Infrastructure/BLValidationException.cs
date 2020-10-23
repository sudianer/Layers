using System;

namespace BusinessLogic.Infrastructure
{
	public class BLValidationException: Exception
	{
		public string Property { get; protected set; }
		public BLValidationException(string message, string property): base(message)
		{
			Property = property;
		}
	}
}
