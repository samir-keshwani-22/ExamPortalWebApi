using ExamPortal.API.Models.Common;
using ExamPortal.API.Models.Entities;
using ExamPortal.Business.Interfaces;
using ExamPortal.DataAccess.Contexts;
using Microsoft.EntityFrameworkCore;

namespace ExamPortal.DataAccess.Repositories;

public class QuestionOptionRepository : IQuestionOptionRepository
{
    private readonly ExamPortalContext _context;

    public QuestionOptionRepository(ExamPortalContext context)
    {
        _context = context;
    }

    public async Task<bool> CreateAsync(QuestionOption questionOption)
    {
        await _context.QuestionOptions.AddAsync(questionOption);
        return await _context.SaveChangesAsync() > 0;
    }


    public async Task<bool> DeleteAsync(int id)
    {
        QuestionOption? questionOption = await _context.QuestionOptions.FindAsync(id);
        if (questionOption == null)
            return false;
        _context.QuestionOptions.Remove(questionOption);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<QuestionOption?> GetByIdAsync(int id)
    {
        return await _context.QuestionOptions.FirstOrDefaultAsync(qo => qo.Id == id);
    }

    public async Task<PagedResult<QuestionOption>> GetPagedAsync(long pageIndex, long pageSize)
    {
        IQueryable<QuestionOption> query = _context.QuestionOptions.AsNoTracking();
        int totalCount = query.Count();
        List<QuestionOption> items = await query.Skip((int)((pageIndex - 1) * pageSize))
          .Take((int)pageSize)
          .ToListAsync();
        return new PagedResult<QuestionOption>
        {
            TotalCount = totalCount,
            Items = items
        };
    }

    public async Task<bool> UpdateAsync(QuestionOption questionOption)
    {
        _context.QuestionOptions.Update(questionOption);
        return await _context.SaveChangesAsync() > 0;
    }
}
