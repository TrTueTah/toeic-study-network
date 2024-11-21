using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Dtos.PartDto;
using API.Interfaces;
using API.Models;
using API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/v1/part")]
    public class PartController : ControllerBase
    {
        private readonly IPartRepository _partRepository;
        private readonly FirebaseService _firebaseService;
        public PartController(IPartRepository partRepository, FirebaseService firebaseService)
        {
            _partRepository = partRepository;
            _firebaseService = firebaseService;
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
            List<PartResponseDto> partResponseDtos = new List<PartResponseDto>();
            foreach (var part in parts)
            {
                partResponseDtos.Add(new PartResponseDto
                {
                    Id = part.Id,
                    PartNumber = part.PartNumber,
                    CreatedAt = part.CreatedAt,
                    ExamId = part.ExamId
                });
            }
            return Ok(partResponseDtos);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("createPart")]
        public async Task<IActionResult> AddPart([FromBody] CreatePartDto createPartDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var part = new Part
            {
                PartNumber = createPartDto.PartNumber,
                ExamId = createPartDto.ExamId
            };
            var newPart = await _partRepository.AddPart(part);

            return Ok(newPart);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("uploadMediaFile/{id}")]
        public async Task<IActionResult> UploadMediaFile([FromForm] UploadMediaFileDto uploadMediaFileDto, string id)
        {
            var part = await _partRepository.GetPartById(id);
            if (part == null)
            {
                return NotFound();
            }
            var imageFilesUrl = new List<string>();
            var audioFilesUrl = new List<string>();
            if (uploadMediaFileDto.ImageFiles != null && uploadMediaFileDto.ImageFiles.Count > 0)
            {
                foreach (var imageFile in uploadMediaFileDto.ImageFiles)
                {
                    var url = await _firebaseService.UploadFileAsync(imageFile, $"parts/{part.Id}", imageFile.FileName);
                    imageFilesUrl.Add(url);
                }
            }
            if (uploadMediaFileDto.AudioFiles != null && uploadMediaFileDto.AudioFiles.Count > 0)
            {
                foreach (var audioFile in uploadMediaFileDto.AudioFiles)
                {
                    var fileName = audioFile.FileName + "_" + part.Id;
                    var url = await _firebaseService.UploadFileAsync(audioFile, $"parts/{part.Id}", audioFile.FileName);
                    audioFilesUrl.Add(url);
                }
            }
            part.ImageFilesUrl = imageFilesUrl;
            part.AudioFilesUrl = audioFilesUrl;
            var updatedPart = await _partRepository.UpdatePart(part);

            return Ok(updatedPart);
        }
    }
}