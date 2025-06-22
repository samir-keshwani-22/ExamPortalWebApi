using ExamPortal.API.Models.Common;
using ExamPortal.API.Models.Entities;
using ExamPortal.Business.Interfaces;
using ExamPortal.DataAccess.Contexts;
using Microsoft.EntityFrameworkCore;

namespace ExamPortal.DataAccess.Repositories;

public class AnswerRepository : IAnswerRepository
{
    private readonly ExamPortalContext _context;

    public AnswerRepository(ExamPortalContext context)
    {
        _context = context;
    }
    public async Task<bool> CreateAsync(Answer answer)
    {
        await _context.Answers.AddAsync(answer);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> DeleteAnswerAsync(int id)
    {
        Answer? answer = await _context.Answers.FindAsync(id);
        if (answer == null)
            return false;
        _context.Answers.Remove(answer);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<Answer?> GetAnswerByIdAsnc(int id)
    {
        return await _context.Answers.FirstOrDefaultAsync(a => a.Id == id);
    }

    public async Task<PagedResult<Answer>> GetPagedAnswersAsync(long pageIndex, long pageSize)
    {
        IQueryable<Answer> query = _context.Answers.AsNoTracking();
        int totalCount = query.Count();
        List<Answer> items = await query.Skip((int)((pageIndex - 1) * pageSize))
          .Take((int)pageSize)
          .ToListAsync();
        return new PagedResult<Answer>
        {
            TotalCount = totalCount,
            Items = items
        };
    }

    public async Task<bool> UpdateAnswerAsync(Answer answer)
    {
        _context.Answers.Update(answer);
        return await _context.SaveChangesAsync() > 0;
    }

}
