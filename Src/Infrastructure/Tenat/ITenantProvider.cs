using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Tenat;

public interface ITenantProvider
{
	public int GetTenantId();

	public string GetTenantDb();
}