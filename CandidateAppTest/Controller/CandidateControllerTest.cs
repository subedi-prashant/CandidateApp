using API.Controllers;
using Application.DTOs.Base;
using Application.DTOs.Candidate;
using Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Net;

namespace CandidateAppTest.Controller
{
    public class CandidateControllerTest
    {
        [Fact]
        public async Task Run_AddUpdateCandidate_When_Success_Returns_Ok()
        {
            //Arrange
            CandidateInfoDTO model = new CandidateInfoDTO()
            {
                FirstName = "",
                LastName = "",
                Email = "",
                Comment = ""
            };

            var candidateServiceMock = new Mock<ICandidateService>();

            candidateServiceMock.Setup(m => m.AddUpdateCandidate(It.IsAny<CandidateInfoDTO>())).ReturnsAsync(true);

            var candidateController = new CandidateController(candidateServiceMock.Object);

            var expectedResult = new ResponseDTO<CandidateInfoDTO>
            {
                Status = "Success",
                Message = "Successfully Added/Updated",
                StatusCode = HttpStatusCode.OK,
                Data = model
            };

            //Act 
            var response = await candidateController.AddUpdateCandidate(model);

            //Assert
            var okResult = Assert.IsType<OkObjectResult>(response);
            var responsee = Assert.IsType<ResponseDTO<CandidateInfoDTO>>(okResult.Value);
            Assert.Equal("Success", responsee.Status);
            Assert.Equal("Successfully Added/Updated", responsee.Message);
            Assert.Equal(HttpStatusCode.OK, responsee.StatusCode);
            Assert.Equal(model, responsee.Data);

        }

        [Fact]
        public async Task Run_AddUpdateCandidate_When_Fail_Returns_BadRequest()
        {
            //Arrange
            CandidateInfoDTO model = new CandidateInfoDTO()
            {
                FirstName = "",
                LastName = "",
                Email = "",
                Comment = ""
            };

            var candidateServiceMock = new Mock<ICandidateService>();

            candidateServiceMock.Setup(m => m.AddUpdateCandidate(It.IsAny<CandidateInfoDTO>())).ReturnsAsync(false);

            var candidateController = new CandidateController(candidateServiceMock.Object);

            var expectedResult = new ResponseDTO<object>
            {
                Status = "Error",
                Message = "Failed to add/update candidate",
                StatusCode = HttpStatusCode.InternalServerError,
                Data = ""
            };

            //Act 
            var response = await candidateController.AddUpdateCandidate(model);

            //Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(response);
            var responsee = Assert.IsType<ResponseDTO<object>>(badRequestResult.Value);
            Assert.Equal("Error", responsee.Status);
            Assert.Equal("Failed to add/update candidate", responsee.Message);
            Assert.Equal(HttpStatusCode.InternalServerError, responsee.StatusCode);
            Assert.Equal("", responsee.Data);
        }

    }
}
