﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ludo
{
    class Braedt
    {
        internal List<Felt> BraedtFeltter = new List<Felt>();

        public Braedt()
        {
            for (int i = 0; i <= 51; i++)
            {
                BraedtFeltter.Add(new Felt());
            }
        }
    }
}
