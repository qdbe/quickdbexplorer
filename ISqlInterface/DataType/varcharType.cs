﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Data;



namespace quickDBExplorer.DataType
{
    internal class varcharType : charBaseType
    {
        public varcharType()
        {
            this.TypeHasSize = true;
        }
    }
}
