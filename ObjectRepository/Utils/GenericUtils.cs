using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectRepository
{
    public static class GenericUtils
    {
        public static bool IsValueOdd  {  get { return (new Random()).Next() % 2 != 0; }  }

        public static int GetRandomNumber(int startNumber, int endNumber)
        {            
            return (new Random()).Next(startNumber, endNumber);
        }
    }
}
