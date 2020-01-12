using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASG.Interfaces
{
    interface ISong
    {
        string Name { get; set; }
        string Description { get; set; }
        string Singer { get; set; }
        string Author { get; set; }
        string Thumbnail { get; set; }
        string Link { get; set; }
    }
}
