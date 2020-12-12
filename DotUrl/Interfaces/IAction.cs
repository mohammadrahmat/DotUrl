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
        T ParseOtherUrl(Uri uri);
        T ParseSearchUrl(Uri uri);
        T ParseProductUrl(Uri uri);
    }
}
