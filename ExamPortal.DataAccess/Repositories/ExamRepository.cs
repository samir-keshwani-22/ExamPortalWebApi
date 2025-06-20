using ExamPortal.API.Models.Common;
using ExamPortal.API.Models.Entities;
using ExamPortal.Business.Interfaces;
using ExamPortal.DataAccess.Contexts;
using Microsoft.EntityFrameworkCore;

namespace ExamPortal.DataAccess.Repositories;

public class ExamRepository : IExamRepository
{
    private readonly ExamPortalContext _context;


    public ExamRepository(ExamPortalContext context)
    {
        _context = context;
    }

    public async Task<bool> CreateAsync(Exam exam)
    {
        await _context.Exams.AddAsync(exam);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> DeleteExamAsync(int id)
    {
        var exam = await _context.Exams.FindAsync(id);
        if (exam == null)
            return false;
        _context.Exams.Remove(exam);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<Exam?> GetExamByIdAsync(int id)
    {
        return await _context.Exams.FirstOrDefaultAsync(e => e.Id == id);
    }

    public async Task<PagedResult<Exam>> GetPagedExamsAsync(long pageIndex, long pageSize, string? title, DateOnly? startDateFrom, DateOnly? startDateTo, DateOnly? endDateFrom, DateOnly? endDateTo, long? createdBy)
    {
        var query = _context.Exams.AsNoTracking();
        if (!string.IsNullOrWhiteSpace(title))
            query = query.Where(e => e.Title.Contains(title));

        if (startDateFrom.HasValue)
            query = query.Where(e => e.StartDate >= startDateFrom.Value.ToDateTime(TimeOnly.MinValue));

        if (startDateTo.HasValue)
            query = query.Where(e => e.StartDate <= startDateTo.Value.ToDateTime(TimeOnly.MaxValue));

        if (endDateFrom.HasValue)
            query = query.Where(e => e.EndDate >= endDateFrom.Value.ToDateTime(TimeOnly.MinValue));

        if (endDateTo.HasValue)
            query = query.Where(e => e.EndDate <= endDateTo.Value.ToDateTime(TimeOnly.MaxValue));

        if (createdBy.HasValue)
            query = query.Where(e => e.CreatedBy == createdBy);

        var totalCount = await query.CountAsync();
        var items = await query
            .OrderByDescending(e => e.StartDate)
            .Skip((int)((pageIndex - 1) * pageSize))
            .Take((int)pageSize)
            .ToListAsync();

        return new PagedResult<Exam>
        {
            TotalCount = totalCount,
            Items = items
        };
    }



    public async Task<bool> UpdateExamAsync(Exam exam)
    {
        _context.Exams.Update(exam);
        return await _context.SaveChangesAsync() > 0;
    }
}
