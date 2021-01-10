﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseApp.Models
{
    public class StudentAddress
    {
        public int Id { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Address { get; set; }
        //once student tablosuna veri eklenir daha sorna bu tabloya veri eklenir
        public int StudentId { get; set; }
        public Student Student { get; set; } //navigation properties
        
    }
}
