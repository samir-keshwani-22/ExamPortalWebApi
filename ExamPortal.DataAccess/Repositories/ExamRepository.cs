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

    public async Task<bool> UpdateExamAsync(Exam exam)
    {
        _context.Update(exam);
        return await _context.SaveChangesAsync() > 0;
    }
}
