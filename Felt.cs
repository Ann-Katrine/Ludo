using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ludo
{
    class Felt
    {
        internal colors OptagetFarve { get; set; } = colors.ingen;
        internal List<Spillebaerk> Optagetbrik = new List<Spillebaerk>();

        public Felt()
        {

        }
    }
}
