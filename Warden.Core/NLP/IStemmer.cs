﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warden.Core.NLP
{
    public interface IStemmer
    {
        string Stem(string s);
    }
}