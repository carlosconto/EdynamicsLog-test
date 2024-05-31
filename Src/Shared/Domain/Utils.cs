using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Domain;
public static class Utils
{
	public static string DateToString(DateTime date)
	{
		return date.ToString("s", CultureInfo.CurrentCulture);
	}

	public static DateTime StringToDate(string date, string format = "dd-MM-yyyy")
	{
		var splitDate = date.Split(" ")[0];
		return DateTime.ParseExact(splitDate, format, null);
		var dateF = Convert.ToDateTime(date, CultureInfo.InvariantCulture); //DateTime.ParseExact($"{date}", format, System.Globalization.CultureInfo.InvariantCulture);

		return dateF;
	}

	public static string FormatDate(this DateTime date)
	{
		return date.ToString("dd-MM-yyyy hh:mm tt", CultureInfo.CurrentCulture);
	}

	public static string ToSnake(this string text)
	{
		return string
			.Concat(
				text.Select((x, i) => i > 0 && char.IsUpper(x) ? "_" + x : x.ToString(CultureInfo.InvariantCulture)))
			.ToLowerInvariant();
	}

	public static string ToCamelFirstUpper(this string text)
	{
		var textInfo = new CultureInfo(CultureInfo.CurrentCulture.ToString(), false).TextInfo;

		return textInfo.ToTitleCase(text).Replace("_", string.Empty);
	}
}

