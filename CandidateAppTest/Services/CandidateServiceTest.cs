using Application.DTOs.Candidate;
using Application.Interfaces.GenericRepository;
using Entities.Models;
using Infrastructure.Implementations.Services;
using Microsoft.Extensions.Caching.Memory;
using Moq;
using System.Linq.Expressions;

namespace CandidateAppTest.Services
{
    public class CandidateServiceTest
    {

        [Fact]
        public async Task Run_AddUpdateCandidate_CacheMiss_UpdateSuccess_Returns_true()
        {
            //Arrange
            CandidateInfoDTO model = new CandidateInfoDTO()
            {
                FirstName = "",
                LastName = "",
                Email = "",
                Comment = ""
            };

            var genericRepositoryMock = new Mock<IGenericRepository>();

            IMemoryCache cache = new MemoryCache(new MemoryCacheOptions());

            CandidateInfo candidateInfo = new CandidateInfo() { };

            object dummy = candidateInfo;

            genericRepositoryMock.Setup(m => m.GetFirstOrDefaultAsync(It.IsAny<Expression<Func<CandidateInfo, bool>>>())).ReturnsAsync(candidateInfo);

            var candidateService = new CandidateService(genericRepositoryMock.Object, cache);

            //Act
            var response = await candidateService.AddUpdateCandidate(model);

            //Assert
            Assert.True(response);


        }

        [Fact]
        public async Task Run_AddUpdateCandidate_CacheMiss_UpdateFailed_Returns_false()
        {
            //Arrange
            CandidateInfoDTO model = new CandidateInfoDTO()
            {
                FirstName = "",
                LastName = "",
                Email = "",
                Comment = ""
            };

            var genericRepositoryMock = new Mock<IGenericRepository>();

            IMemoryCache cache = new MemoryCache(new MemoryCacheOptions());

            CandidateInfo candidateInfo = new CandidateInfo() { };

            object dummy = candidateInfo;

            genericRepositoryMock.Setup(m => m.GetFirstOrDefaultAsync(It.IsAny<Expression<Func<CandidateInfo, bool>>>())).ReturnsAsync(candidateInfo);

            Exception ex = new Exception();
            genericRepositoryMock.Setup(m => m.UpdateAsync(It.IsAny<CandidateInfo>())).ThrowsAsync(ex);

            var candidateService = new CandidateService(genericRepositoryMock.Object, cache);

            //Act
            var response = await candidateService.AddUpdateCandidate(model);

            //Assert
            Assert.False(response);
        }

        [Fact]
        public async Task Run_AddUpdateCandidate_CacheMiss_InsertSuccess_Returns_true()
        {
            //Arrange
            CandidateInfoDTO model = new CandidateInfoDTO()
            {
                FirstName = "",
                LastName = "",
                Email = "",
                Comment = ""
            };

            var genericRepositoryMock = new Mock<IGenericRepository>();

            IMemoryCache cache = new MemoryCache(new MemoryCacheOptions());

            CandidateInfo candidateInfo = new CandidateInfo() { };

            object dummy = candidateInfo;

            genericRepositoryMock.Setup(m => m.GetFirstOrDefaultAsync(It.IsAny<Expression<Func<CandidateInfo, bool>>>())).ReturnsAsync((CandidateInfo)null);

            var candidateService = new CandidateService(genericRepositoryMock.Object, cache);

            //Act
            var response = await candidateService.AddUpdateCandidate(model);

            //Assert
            Assert.True(response);
        }


        [Fact]
        public async Task Run_AddUpdateCandidate_CacheMiss_InsertFailed_Returns_false()
        {
            //Arrange
            CandidateInfoDTO model = new CandidateInfoDTO()
            {
                FirstName = "",
                LastName = "",
                Email = "",
                Comment = ""
            };

            var genericRepositoryMock = new Mock<IGenericRepository>();

            IMemoryCache cache = new MemoryCache(new MemoryCacheOptions());

            CandidateInfo candidateInfo = new CandidateInfo() { };

            object dummy = candidateInfo;

            genericRepositoryMock.Setup(m => m.GetFirstOrDefaultAsync(It.IsAny<Expression<Func<CandidateInfo, bool>>>())).ReturnsAsync((CandidateInfo)null);

            Exception ex = new Exception();
            genericRepositoryMock.Setup(m => m.InsertAsync(It.IsAny<CandidateInfo>())).ThrowsAsync(ex);

            var candidateService = new CandidateService(genericRepositoryMock.Object, cache);

            //Act
            var response = await candidateService.AddUpdateCandidate(model);

            //Assert
            Assert.False(response);
        }

        [Fact]
        public async Task Run_AddUpdateCandidate_CacheHit_UpdateSuccess_Returns_true()
        {
            //Arrange
            CandidateInfoDTO model = new CandidateInfoDTO()
            {
                FirstName = "",
                LastName = "",
                Email = "",
                Comment = ""
            };

            var genericRepositoryMock = new Mock<IGenericRepository>();
            var cacheMock = new Mock<IMemoryCache>();

            CandidateInfo candidateInfo = new CandidateInfo() { };

            object dummy = candidateInfo;

            cacheMock.Setup(m => m.TryGetValue(It.IsAny<object>(), out dummy)).Returns(true);

            genericRepositoryMock.Setup(m => m.GetFirstOrDefaultAsync(It.IsAny<Expression<Func<CandidateInfo, bool>>>())).ReturnsAsync(candidateInfo);

            var candidateService = new CandidateService(genericRepositoryMock.Object, cacheMock.Object);

            //Act
            var response = await candidateService.AddUpdateCandidate(model);

            //Assert
            Assert.True(response);
        }


        [Fact]
        public async Task Run_AddUpdateCandidate_CacheHit_UpdateFailed_Returns_false()
        {
            //Arrange
            CandidateInfoDTO model = new CandidateInfoDTO()
            {
                FirstName = "",
                LastName = "",
                Email = "",
                Comment = ""
            };

            var genericRepositoryMock = new Mock<IGenericRepository>();
            var cacheMock = new Mock<IMemoryCache>();

            CandidateInfo candidateInfo = new CandidateInfo() { };

            object dummy = candidateInfo;

            cacheMock.Setup(m => m.TryGetValue(It.IsAny<object>(), out dummy)).Returns(true);

            genericRepositoryMock.Setup(m => m.GetFirstOrDefaultAsync(It.IsAny<Expression<Func<CandidateInfo, bool>>>())).ReturnsAsync(candidateInfo);

            Exception ex = new Exception();
            genericRepositoryMock.Setup(m => m.UpdateAsync(It.IsAny<CandidateInfo>())).ThrowsAsync(ex);

            var candidateService = new CandidateService(genericRepositoryMock.Object, cacheMock.Object);

            //Act
            var response = await candidateService.AddUpdateCandidate(model);

            //Assert
            Assert.False(response);
        }
    }
}
