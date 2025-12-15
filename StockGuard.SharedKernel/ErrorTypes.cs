using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockGuard.SharedKernel;

public enum ErrorType
{
	Failure = 0,
	Validation = 1,
	Problem = 2,
	Notfound = 3,
	Conflict = 4,
	None = 5,
	NullValue = 6,
	BadRequest = 7,
	Empty
}