using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TrainingAPI.Models
{
    public class Result
    {
        public string message { get; set; }
        public string diferrence { get; set; }
    }

    public class Trainings
    {
        public string trainingName { get; set; }
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
    }


}