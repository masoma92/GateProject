using System;
using System.Collections.Generic;
using System.Text;

namespace GateProjectBackend.Common
{
    public class ListResult<T>
    {
        public IEnumerable<T> Records { get; }
        public int RecordCount { get; }

        public ListResult(IEnumerable<T> records, int recordCount)
        {
            Records = records;
            RecordCount = recordCount;
        }
    }
}
