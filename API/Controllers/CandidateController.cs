using Application.DTOs.Base;
using Application.DTOs.Candidate;
using Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace API.Controllers
{
    [Route("api/candidate")]
    [ApiController]
    public class CandidateController : ControllerBase
    {

        private readonly ICandidateService _candidateService;

        public CandidateController(ICandidateService candidateService)
        {
            _candidateService = candidateService;
        }

        [HttpPost("add-update-candidate")]
        public async Task<IActionResult> AddUpdateCandidate(CandidateInfoDTO candidate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ResponseDTO<object>
                {
                    Status = "Error",
                    Message = "Invalid candidate data",
                    StatusCode = HttpStatusCode.BadRequest,
                    Data = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage)
                });
            }

            try
            {
                var result = await _candidateService.AddUpdateCandidate(candidate);

                if (result)
                {
                    var response = new ResponseDTO<CandidateInfoDTO>
                    {
                        Status = "Success",
                        Message = "Successfully Added/Updated",
                        StatusCode = HttpStatusCode.OK,
                        Data = candidate
                    };

                    return Ok(response);
                }

                var errorResponse = new ResponseDTO<object>
                {
                    Status = "Error",
                    Message = "Failed to add/update candidate",
                    StatusCode = HttpStatusCode.InternalServerError,
                    Data = null
                };

                return StatusCode((int)HttpStatusCode.InternalServerError, errorResponse);

            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
