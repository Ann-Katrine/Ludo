using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ludo
{
    class Felt
    {
        internal Colors OptagetFarve { get; set; } = Colors.ingen;
        internal List<Spillebrik> Optagetbrik = new List<Spillebrik>();
    }
}
