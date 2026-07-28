using MzadService.DTOs;
using MzadService.Entities;

namespace MzadService;

public interface IMzadRepository
{
    Task<List<MzadDTO>> GetMzadatAsync(string lastUpdateDate);
    Task<MzadDTO> GetMzadByIdAsync(Guid id);
    Task<Mzad> GetMzadEntityById(Guid id);
    void AddMzad(Mzad mzad);
    void RemoveMzad(Mzad mzad);
    Task<bool> SaveChangesAsync();
}
