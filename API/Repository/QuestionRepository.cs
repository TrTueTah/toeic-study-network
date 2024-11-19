using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using API.Data;
using API.Interfaces;
using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Repository
{
    public class QuestionRepository : IQuestionRepository
    {
        private readonly ApplicationDbContext _context;
        public QuestionRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Question> AddQuestion(Question question)
        {
            await _context.Questions.AddAsync(question);
            await _context.SaveChangesAsync();
            return question;
        }

        public List<Question> ExtractQuestionsFromLines(List<string> lines)
        {
            var questions = new List<Question>();
            int i = 0;

            while (i < lines.Count)
            {
                // Kiểm tra nếu dòng bắt đầu bằng questionNumber (từ 101 đến 200)
                if (lines[i].Length >= 4 && int.TryParse(lines[i][..3], out int questionNumber) && questionNumber >= 101 && questionNumber <= 200)
                {
                    // Lưu questionNumber và tiêu đề câu hỏi
                    var question = new Question
                    {
                        QuestionNumber = questionNumber,
                        Title = lines[i][4..].Trim() // Lấy phần còn lại sau "101. " làm tiêu đề
                    };

                    // Kiểm tra 4 dòng tiếp theo cho các lựa chọn (A), (B), (C), (D)
                    if (i + 4 < lines.Count)
                    {
                        if (lines[i + 1].StartsWith("(A)"))
                            question.AnswerA = lines[i + 1][3..].Trim();
                        if (lines[i + 2].StartsWith("(B)"))
                            question.AnswerB = lines[i + 2][3..].Trim();
                        if (lines[i + 3].StartsWith("(C)"))
                            question.AnswerC = lines[i + 3][3..].Trim();
                        if (lines[i + 4].StartsWith("(D)"))
                            question.AnswerD = lines[i + 4][3..].Trim();

                        // Thêm câu hỏi vào danh sách
                        // Console.WriteLine($"Câu hỏi: {questionNumber}: {question.Title}");
                        // Console.WriteLine($"(A) {question.AnswerA}");
                        // Console.WriteLine($"(B) {question.AnswerB}");
                        // Console.WriteLine($"(C) {question.AnswerC}");
                        // Console.WriteLine($"(D) {question.AnswerD}");
                        questions.Add(question);
                        i += 5; // Bỏ qua 5 dòng (câu hỏi + 4 lựa chọn)
                    }
                    else
                    {
                        // Nếu không đủ dòng, ghi log và bỏ qua câu hỏi này
                        Console.WriteLine($"Không đủ dữ liệu cho câu hỏi: {questionNumber}");
                        i++;
                    }
                }
                else
                {
                    // Nếu không tìm thấy questionNumber, bỏ qua dòng này
                    i++;
                }
            }

            return questions;
        }

        public async Task<List<Question>> GetAllQuestions()
        {
            return await _context.Questions.ToListAsync();
        }

        public async Task<Question> GetQuestionById(string id)
        {
            return await _context.Questions.FirstOrDefaultAsync(x => x.Id == id);
        }

        public Task<List<Question>> GetQuestionsByPartId(string partId)
        {
            return _context.Questions.Where(x => x.PartId == partId).ToListAsync();
        }
    }
}