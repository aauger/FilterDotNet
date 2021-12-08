﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AAImageFilter.Interfaces
{
    public interface IGenerator
    {
        public Image Generate();
        public string GetName();
    }
}