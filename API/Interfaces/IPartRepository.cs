using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Models;

namespace API.Interfaces
{
    public interface IPartRepository
    {
        Task<List<Part>> GetAllParts();
        Task<Part> GetPartById(string id);
        Task<Part> AddPart(Part part);
        Task<List<Part>> GetPartsByExamId(string examId);
        Task<Part> UpdatePart(Part part);
    }
}