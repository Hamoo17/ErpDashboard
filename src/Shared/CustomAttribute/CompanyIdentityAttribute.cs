using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpDashboard.Shared.CustomAttribute
{
	[AttributeUsage(AttributeTargets.Property)]
	public class CompanyIdentityAttribute : Attribute
	{
	}

	[AttributeUsage(AttributeTargets.Property)]
	public class CompanyIdAttribute : Attribute
	{
	}
}
