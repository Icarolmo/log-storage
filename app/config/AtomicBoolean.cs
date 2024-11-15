using System;
using System.Collections.Generic;
namespace log_storage.app.config
{
    public class AtomicBoolean
    {
        private int flag = 1;

        public bool SetTrue()
        {
            return Interlocked.Exchange(ref flag, 1) == 0;
        }

        public bool SetFalse()
        {
            return Interlocked.Exchange(ref flag, 0) == 1;
        }

        public bool IsTrue => flag == 1;
    }
}
