namespace Eelarveanaluutik.Api.Services;

public sealed class CategoryNameConflictException() : Exception("A category with this name already exists.");
