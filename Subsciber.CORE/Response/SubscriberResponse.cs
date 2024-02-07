using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.model.Response
{
    public  class SubscriberResponse 
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public double BMI { get; set; } = 0;
        public double height { get; set; }
        public double weight { get; set; }
    }
}
