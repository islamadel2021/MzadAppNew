using AutoMapper;
using AutoMapper.QueryableExtensions;
using Contracts;
using MassTransit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MzadService.Data;
using MzadService.DTOs;
using MzadService.Entities;

namespace MzadService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MzadController(IMzadRepository repo, IMapper mapper, IPublishEndpoint publishEndpoint) : ControllerBase
    {
        private readonly IMzadRepository _repo = repo;
        private readonly IMapper _mapper = mapper;
        private readonly IPublishEndpoint _publishEndpoint = publishEndpoint;

        [HttpGet]
        public async Task<IEnumerable<MzadDTO>> GetMzadat(string lastUpdateDate)
        {
            return await _repo.GetMzadatAsync(lastUpdateDate);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MzadDTO>> GetMzadById(Guid id)
        {
            var mzad = await _repo.GetMzadByIdAsync(id);
            if (mzad == null) return NotFound();
            return mzad;
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<MzadDTO>> CreateMzad(MzadForCreateDTO mzadForCreateDTO)
        {
            var mzad = _mapper.Map<Mzad>(mzadForCreateDTO);
            mzad.Seller = User.Identity.Name.CapitalizeFirstLetter();
            _repo.AddMzad(mzad);
            var newMzad = _mapper.Map<MzadDTO>(mzad);
            await _publishEndpoint.Publish(_mapper.Map<MzadCreated>(newMzad));
            var result = await _repo.SaveChangesAsync();
            if (!result) return BadRequest("Fail to create Mzad");
            return CreatedAtAction(nameof(GetMzadById), new { id = mzad.Id }, newMzad);
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<ActionResult<MzadDTO>> UpdateMzad(Guid id, MzadForUpdateDTO mzadForUpdateDTO)
        {
            var mzad = await _repo.GetMzadEntityById(id);
            if (mzad == null) return NotFound("Mzad not found to be updated");
            //TODO: Add validation check seller = username
            if (!mzad.Seller.Equals(User.Identity.Name, StringComparison.CurrentCultureIgnoreCase)) return Forbid();
            mzadForUpdateDTO.UpdatedAt = DateTime.UtcNow;
            mzad.Horse.Name = mzadForUpdateDTO.Name ?? mzad.Horse.Name;
            mzad.Horse.Father = mzadForUpdateDTO.Father ?? mzad.Horse.Father;
            mzad.Horse.Mother = mzadForUpdateDTO.Mother ?? mzad.Horse.Mother;
            mzad.Horse.ImageUrl = mzadForUpdateDTO.ImageUrl ?? mzad.Horse.ImageUrl;
            mzad.Horse.Color = mzadForUpdateDTO.Color ?? mzad.Horse.Color;
            mzad.Horse.Breed = mzadForUpdateDTO.Breed ?? mzad.Horse.Breed;
            mzad.Horse.YearOfBirth = mzadForUpdateDTO.YearOfBirth ?? mzad.Horse.YearOfBirth;
            mzad.ReservePrice = mzadForUpdateDTO.ReservePrice ?? mzad.ReservePrice;
            mzad.MzadEnd = mzadForUpdateDTO.MzadEnd ?? mzad.MzadEnd;
            await _publishEndpoint.Publish(_mapper.Map<MzadUpdated>(mzad));
            var result = await _repo.SaveChangesAsync();
            if (!result) return BadRequest("No Updating has been done");
            return Ok(_mapper.Map<MzadDTO>(mzad));
        }
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteMzad(Guid id)
        {
            var mzad = await _repo.GetMzadEntityById(id);
            if (mzad == null) return NotFound("Mzad not found to be deleted");
            //TODO: Add validation check seller = username
            if (!mzad.Seller.Equals(User.Identity.Name, StringComparison.CurrentCultureIgnoreCase)) return Forbid();
            _repo.RemoveMzad(mzad);
            await _publishEndpoint.Publish<MzadDeleted>(new { Id = mzad.Id });
            var result = await _repo.SaveChangesAsync();
            if (!result) return BadRequest("Failed to delete Mzad");
            return Ok("Mzad deleted");
        }
    }
}