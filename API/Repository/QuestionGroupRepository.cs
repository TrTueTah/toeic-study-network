using API.Data;
using API.Interfaces;
using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Repository
{
    public class QuestionGroupRepository : IQuestionGroupRepository
    {
        private readonly ApplicationDbContext _context;

        public QuestionGroupRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        
        public QuestionGroup CreateQuestionGroup(QuestionGroup questionGroup)
        {
            _context.QuestionGroups.Add(questionGroup);
            _context.SaveChanges();
            return questionGroup;
        }
        
        public QuestionGroup GetQuestionGroupById(string id)
        {
            return _context.QuestionGroups
                .Include(qg => qg.Questions)
                .FirstOrDefault(qg => qg.Id == id);
        }
        
        public List<QuestionGroup> GetAllQuestionGroupsForExam(string examId)
        {
            return _context.QuestionGroups
                .Include(qg => qg.Questions)
                .Where(qg => qg.ExamId == examId)
                .ToList();
        }
        
        public bool UpdateQuestionGroup(QuestionGroup questionGroup)
        {
            _context.QuestionGroups.Update(questionGroup);
            return Save();
        }
        
        public bool DeleteQuestionGroup(string id)
        {
            var group = _context.QuestionGroups
                .FirstOrDefault(qg => qg.Id == id);

            if (group == null)
            {
                return false;  // Return false if the group does not exist
            }

            using (var transaction = _context.Database.BeginTransaction())  // Start a transaction
            {
                try
                {
                    _context.QuestionGroups.Remove(group);
                    _context.SaveChanges();  // Save the deletion

                    // Commit transaction
                    transaction.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    // Rollback transaction if any error occurs
                    transaction.Rollback();
                    throw new Exception($"Error deleting question group: {ex.Message}");
                }
            }
        }

        // Check if a QuestionGroup exists by its Id
        public bool QuestionGroupExists(string id)
        {
            return _context.QuestionGroups.Any(q => q.Id == id);
        }
        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
