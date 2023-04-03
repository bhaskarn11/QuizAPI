using AutoMapper;
using Microsoft.AspNetCore.Mvc;



namespace QuizAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssessmentController : ControllerBase
    {
        private readonly IAssessmentService assessmentService;

        public AssessmentController(IAssessmentService assessmentService)
        {
            this.assessmentService = assessmentService;
        }


        [HttpGet("{id}")]
        public Response<Assessment?> GetAssesmentById(int id)
        {
            try
            {
                Assessment? assesment = assessmentService.GetAssesmentById(id);
                return new Response<Assessment?>(assesment, true);
            }

            catch (Exception e)
            {
                return new Response<Assessment?>(null, false, e.Message);
            }
        }


        [HttpPost("InitiateAssessment")]
        public Response<Assessment?> InitiateAssessment(int userId)
        {

            try
            {
                var assessment = assessmentService.InitiateAssessment(userId);
                return new Response<Assessment?>(assessment, true);
            }

            catch (Exception e)
            {
                return new Response<Assessment?>(null, false, e.Message);
            }
            
        }


        [HttpPost("Submit")]
        public Response<Assessment?> SubmitAssessment(int assessmentId)
        {

            try
            {
                var ass = assessmentService.SubmitAssessment(assessmentId);
                return new Response<Assessment?>(ass, true);
            }

            catch (Exception e)
            {
                return new Response<Assessment?>(null, false, e.Message);
            }
        }

        [HttpPost("SetAnswer")]
        public Response<AssessmentAnswer?> SetAnswer(int assessmentId, int questionId, int optionId)
        {
            try
            {
                var a = assessmentService.SetAnswer(assessmentId, questionId, optionId);
                return new Response<AssessmentAnswer?>(a, true);
            }
            catch (Exception e)
            {
                return new Response<AssessmentAnswer?>(null, false, e.Message);
            }
        }



    }
}
