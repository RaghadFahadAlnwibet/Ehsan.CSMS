using AutoFixture;
using AutoMapper;
using AutoMapper.Internal.Mappers;
using Ehsan.CSMS.Dtos.CashierDto;
using Ehsan.CSMS.Entities;
using Ehsan.CSMS.IRepositories;
using Ehsan.CSMS.Services;
using FluentAssertions;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;
namespace Ehsan.CSMS.ServicesTests;


public class CashierServiceTests
{
    private readonly CashierService _cut;
    private readonly Mock<ICashierRepository> _cashierRepositoryMock;
    private readonly IFixture _fixture;
    private readonly ITestOutputHelper _testOutputHelper;

    public CashierServiceTests(
        ITestOutputHelper testOutputHelper)
    {
        _cashierRepositoryMock = new Mock<ICashierRepository>();
        _cut = new CashierService(_cashierRepositoryMock.Object);
        _testOutputHelper = testOutputHelper;
        _fixture = new Fixture();
    }
    #region AddAsync
    [Fact]
    public async Task Add_ShallThrowArgumentNullException_IfNullCashier()
    {
        // Arrange 
        CashierAddRequest? cashierRequest = null;
        // Act
        Func<Task> action = async () => await _cut.AddAsync(cashierRequest);
        // Assert
        await action.Should().ThrowAsync<ArgumentNullException>();
    }
    [Fact]
    public async Task Add_ShalThrowArgumentException_IfNullCahierName()
    {
        // Arrange 
        var cashierRequest = _fixture.Build<CashierAddRequest>()
                              .With(cashier => cashier.Name, null as string)
                              .Create();
        // Act 
        Func<Task> action = async () => await _cut.AddAsync(cashierRequest);
        // Assert 
        await action.Should().ThrowAsync<ArgumentException>();
    }

    //[Fact]
    //public async Task Add_ShallReturnNewlyGeneratedCashier_IfValidCashier()
    //{
    //    // Arrange
    //    var cashierAddRequest = _fixture.Create<CashierAddRequest>();
    //    var cashier = new Cashier()
    //    {Name = cashierAddRequest.Name};
    //    var cashierResponse_expected = new CashierResponse()
    //    { Name = cashier.Name };

    //    //_objectMapperMock.Setup(m => m.Map<CashierAddRequest, Cashier>(cashierAddRequest))
    //    //    .Returns(cashier);
    //    //_objectMapperMock.Setup(m => m.Map<Cashier, CashierResponse>(cashier))
    //    //                 .Returns(cashierResponse_expected);

    //    // Act 
    //    var cashierResponse_actual = await _cut.AddAsync(cashierAddRequest);
    //    cashierResponse_expected.Id = cashierResponse_actual.Id;
    //    // Assert 
    //    cashierResponse_actual.Should().Be(cashierResponse_expected);
    //}
    #endregion

}
