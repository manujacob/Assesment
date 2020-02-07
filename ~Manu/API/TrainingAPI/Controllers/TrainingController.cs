using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using TrainingAPI.Models;
using System.Web.Http;
using System.Web.Http.Description;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using System.Data.SqlClient;
using System.Data;

namespace TrainingAPI.Controllers
{
    public class TrainingController : ApiController
    {
        // POST api/values
        [HttpPost]
        [ResponseType(typeof(Result))]
        public async Task<IHttpActionResult> Post(HttpRequestMessage request)
        {
            Result result = new Result();
            try
            {
                Trainings trainings = JsonConvert.DeserializeObject<Trainings>(request.Content.ReadAsStringAsync().Result);
                if (trainings.startDate >trainings.endDate)
                {
                    result.message = "End date is before the start date";
                    result.diferrence = "";
                }
                else if (trainings.trainingName.Trim()=="")
                {
                    result.message = "Training name is Mandatory";
                    result.diferrence = "";
                }
                else
                {
                    result.message=AddNewTraining(trainings);
                    result.diferrence ="Duration: "+(trainings.endDate - trainings.startDate).Days.ToString()+" Days";
                }
            }
            catch(Exception)
            {
                result.message = "ERROR, Invalid Data";
                result.diferrence = "";
            }
            return Ok(result);

        }

        private string  AddNewTraining(Trainings trainings)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_New_Training", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("@TrainingName", SqlDbType.VarChar).Value = trainings.trainingName;
                        cmd.Parameters.Add("@startDate", SqlDbType.VarChar).Value = trainings.startDate;
                        cmd.Parameters.Add("@endDate", SqlDbType.VarChar).Value = trainings.endDate;
                        con.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch(Exception exp)
            {
                return (exp.Message);
            }
            return ("Training Added successfully.");
        }
    }
}
