﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Center.Models.ModelView
{
    public class RoomView
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int EmpID { get; set; }
        public string EmpName { get; set; }
    }
}
