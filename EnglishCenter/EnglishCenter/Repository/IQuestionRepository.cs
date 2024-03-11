﻿using EnglishCenter.DTO;

namespace EnglishCenter.Repository
{
    public interface IQuestionRepository
    {
        List<ShowQuestionDTO> getAllQuestion();

        void deleteQuestion(int id);
    }
}
