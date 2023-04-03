using AutoMapper;

namespace QuizAPI.Schemas
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<CreateUser, User>();
            CreateMap<CreateQuiz, Quiz>();
            CreateMap<UpdateUser, User>();
            CreateMap<CreateQuestion, Question>();
            CreateMap<AddOption, Option>();
            CreateMap<Option, OptionResponse>();
        }
    }
}
