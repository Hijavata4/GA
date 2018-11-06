using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoadSheddingGAOptimization
{
    class PriorityComparer: IComparer<Consumer>
    {

    public int Compare(Consumer x, Consumer y)
    {
        return ComparePriority(x, y);
    }

    public int ComparePriority(Consumer x, Consumer y)
    {
        if (x.Priority > y.Priority)
        {
            return 1;
        }
        else if (x.Priority < y.Priority)
        {
            return -1;
        }
        else
        {
            return 0;
        }
    }
}
}
