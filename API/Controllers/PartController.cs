using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Dtos.PartDto;
using API.Interfaces;
using API.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/v1/part")]
    public class PartController : ControllerBase
    {
        private readonly IPartRepository _partRepository;
        public PartController(IPartRepository partRepository)
        {
            _partRepository = partRepository;
        }

        [HttpGet("getAllParts")]
        public async Task<IActionResult> GetAllParts()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var parts = await _partRepository.GetAllParts();

            return Ok(parts);
        }

        [HttpGet("getPartById/{id}")]
        public async Task<IActionResult> GetPartById(string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var part = await _partRepository.GetPartById(id);
            if (part == null)
            {
                return NotFound();
            }
            return Ok(part);
        }

        [HttpGet("getPartsByExamId/{examId}")]
        public async Task<IActionResult> GetPartsByExamId(string examId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var parts = await _partRepository.GetPartsByExamId(examId);
            if (parts == null)
            {
                return NotFound();
            }
            return Ok(parts);
        }

        [HttpPost("createPart")]
        public async Task<IActionResult> AddPart([FromForm] CreatePartDto createPartDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var part = new Part
            {
                PartNumber = createPartDto.PartNumber,
                ImageFile = createPartDto.ImageFile,
                AudioFile = createPartDto.AudioFile,
                ExamId = createPartDto.ExamId
            };
            var newPart = await _partRepository.AddPart(part);

            return Ok(newPart);
        }
    }
}