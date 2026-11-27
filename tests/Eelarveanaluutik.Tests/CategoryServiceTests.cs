using Eelarveanaluutik.Api.Models;
using Eelarveanaluutik.Api.Repositories;
using Eelarveanaluutik.Api.Services;
using Moq;

namespace Eelarveanaluutik.Tests;

public class CategoryServiceTests
{
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public async Task EmptyNameIsRejectedWithoutWritingData(string? name)
    {
        var repository = new Mock<ICategoryRepository>(MockBehavior.Strict);
        var service = new CategoryService(repository.Object);
        await Assert.ThrowsAsync<ArgumentException>(() => service.CreateAsync(Guid.NewGuid(), name!));
        repository.VerifyNoOtherCalls();
    }

    [Fact]
    public async Task NameLongerThanEightyCharactersIsRejected()
    {
        var repository = new Mock<ICategoryRepository>(MockBehavior.Strict);
        var service = new CategoryService(repository.Object);
        await Assert.ThrowsAsync<ArgumentException>(() => service.CreateAsync(Guid.NewGuid(), new string('a', 81)));
        repository.VerifyNoOtherCalls();
    }

    [Fact]
    public async Task CreateTrimsNameAndSavesItForTheCorrectOwner()
    {
        var owner = Guid.NewGuid();
        Category? saved = null;
        var repository = new Mock<ICategoryRepository>(MockBehavior.Strict);
        repository.Setup(x => x.NameExistsAsync(owner, "TOIT", It.IsAny<CancellationToken>())).ReturnsAsync(false);
        repository.Setup(x => x.AddAsync(It.IsAny<Category>(), It.IsAny<CancellationToken>()))
            .Callback<Category, CancellationToken>((category, _) => saved = category).Returns(Task.CompletedTask);
        var result = await new CategoryService(repository.Object).CreateAsync(owner, "  Toit  ");
        Assert.NotNull(saved);
        Assert.Equal(owner, saved.UserId);
        Assert.Equal("TOIT", saved.NormalizedName);
        Assert.Equal("Toit", result.Name);
        Assert.Equal(saved.Id, result.Id);
    }

    [Fact]
    public async Task ExistingNormalizedNameIsRejectedWithoutAddingAnotherCategory()
    {
        var owner = Guid.NewGuid();
        var repository = new Mock<ICategoryRepository>(MockBehavior.Strict);
        repository.Setup(x => x.NameExistsAsync(owner, "TOIT", It.IsAny<CancellationToken>())).ReturnsAsync(true);
        var service = new CategoryService(repository.Object);
        await Assert.ThrowsAsync<CategoryNameConflictException>(() => service.CreateAsync(owner, "toit"));
        repository.Verify(x => x.AddAsync(It.IsAny<Category>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    public async Task ListReturnsOnlyCategoryIdAndNameFromTheOwnersRepositoryQuery()
    {
        var owner = Guid.NewGuid();
        var category = new Category { UserId = owner, Name = "Toit", NormalizedName = "TOIT" };
        var repository = new Mock<ICategoryRepository>(MockBehavior.Strict);
        repository.Setup(x => x.ListForUserAsync(owner, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new List<Category> { category });
        var result = await new CategoryService(repository.Object).ListAsync(owner);
        var item = Assert.Single(result);
        Assert.Equal(category.Id, item.Id);
        Assert.Equal("Toit", item.Name);
    }

    [Fact]
    public async Task EightyCharacterNameCanBeCreated()
    {
        var owner = Guid.NewGuid();
        var name = new string('a', 80);
        var repository = new Mock<ICategoryRepository>(MockBehavior.Strict);
        repository.Setup(x => x.NameExistsAsync(owner, name.ToUpperInvariant(), It.IsAny<CancellationToken>())).ReturnsAsync(false);
        repository.Setup(x => x.AddAsync(It.IsAny<Category>(), It.IsAny<CancellationToken>())).Returns(Task.CompletedTask);
        var result = await new CategoryService(repository.Object).CreateAsync(owner, name);
        Assert.Equal(name, result.Name);
    }
}
