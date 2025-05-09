﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VillianSearcher.DAL.Data
{
    public interface IDataProvider
    {
        void LoadData();

        void SaveData();
    }
}
