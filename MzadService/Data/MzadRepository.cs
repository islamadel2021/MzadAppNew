using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using MzadService.Data;
using MzadService.DTOs;
using MzadService.Entities;

namespace MzadService;

public class MzadRepository(MzadDbContext context, IMapper mapper) : IMzadRepository
{
    private readonly MzadDbContext _context = context;
    private readonly IMapper _mapper = mapper;

    public void AddMzad(Mzad mzad)
    {
        _context.Mzadat.Add(mzad);
    }

    public async Task<List<MzadDTO>> GetMzadatAsync(string lastUpdateDate)
    {
        var query = _context.Mzadat.OrderBy(m => m.MzadEnd).AsQueryable();
        if (!string.IsNullOrEmpty(lastUpdateDate))
        {
            query = query.Where(m => m.UpdatedAt > DateTime.Parse(lastUpdateDate).ToUniversalTime());
        }
        return await query.ProjectTo<MzadDTO>(_mapper.ConfigurationProvider).ToListAsync();
    }

    public async Task<MzadDTO> GetMzadByIdAsync(Guid id)
    {
        var mzad = await _context.Mzadat
             .Include(m => m.Horse)
             .FirstOrDefaultAsync(m => m.Id == id);
        return _mapper.Map<MzadDTO>(mzad);
    }

    public async Task<Mzad> GetMzadEntityById(Guid id)
    {
        return await _context.Mzadat.Include(m => m.Horse).FirstOrDefaultAsync(m => m.Id == id);
    }

    public void RemoveMzad(Mzad mzad)
    {
        _context.Mzadat.Remove(mzad);
    }

    public async Task<bool> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }
}
