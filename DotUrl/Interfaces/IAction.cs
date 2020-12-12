using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotUrl.Interfaces
{
    public interface IAction<T>
    {
        string Execute(string input);
        bool VerifyInput(string input);
        T InputParser(string input);
        T ParseOtherPageInput(Uri uri);
        T ParseSearchPageInput(Uri uri);
        T ParseProductPageInput(Uri uri);
    }
}
