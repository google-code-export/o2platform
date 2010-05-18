using System;
using CookComputing.XmlRpc;

namespace JoeBlogs.Structs
{
	/// <summary>
	/// Page.
	/// </summary>
	[XmlRpcMissingMapping(MappingAction.Ignore)]
	public struct PageTemplate
	{
		public string name;
		public string description;

		public override string ToString()
		{
			return name;
		}
	}
}
