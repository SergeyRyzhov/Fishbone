using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fishbone.Drawing.Drawers;

namespace Fishbone.Drawing.Extended
{
    interface IExtendedDrawer<in T>
    {
        IDrawer<T> Internal { get; } 
    }
}
