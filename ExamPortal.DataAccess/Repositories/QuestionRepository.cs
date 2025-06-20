using ExamPortal.API.Models.Common;
using ExamPortal.API.Models.Entities;
using ExamPortal.Business.Interfaces;
using ExamPortal.DataAccess.Contexts;
using Microsoft.EntityFrameworkCore;

namespace ExamPortal.DataAccess.Repositories;

public class QuestionRepository : IQuestionRepository
{
    private readonly ExamPortalContext _context;

    public QuestionRepository(ExamPortalContext context)
    {
        _context = context;
    }
    public async Task<bool> CreateAsync(Question question)
    {
        await _context.Questions.AddAsync(question);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> DeleteQuestionAsync(int id)
    {
        var question = await _context.Questions.FindAsync(id);
        if (question == null)
            return false;
        _context.Questions.Remove(question);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<PagedResult<Question>> GetPagedQuestionsAsync(long pageIndex, long pageSize)
    {
        var query = _context.Questions.AsNoTracking();
        var totalCount = query.Count();
        var items = await query.Skip((int)((pageIndex - 1) * pageSize))
            .Take((int)pageSize)
            .ToListAsync();
        return new PagedResult<Question>
        {
            TotalCount = totalCount,
            Items = items
        };
    }


    public async Task<Question?> GetQuestionByIdAsync(int id)
    {
        return await _context.Questions.FirstOrDefaultAsync(q => q.Id == id);
    }

    public async Task<bool> UpdateQuestionAsync(Question question)
    {
        _context.Questions.Update(question);
        return await _context.SaveChangesAsync() > 0;

    }
}
