using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Domain.ValueObjects;

public abstract class IntValueObject(int value)
{
	public int Value { get; } = value;

	public override string ToString()
	{
		return Value.ToString();
	}

	public override bool Equals(object? obj)
	{
		if (this == obj) return true;


		if (obj is not IntValueObject item) return false;

		return Value == item.Value;
	}

	public override int GetHashCode()
	{
		return HashCode.Combine(Value);
	}
}
