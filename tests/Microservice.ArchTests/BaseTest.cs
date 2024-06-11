using ArchUnitNET.Domain;
using ArchUnitNET.Fluent;
using ArchUnitNET.Loader;
using Microservice.API;
using Microservice.Core.Abstractions;
using Microservice.Infrastructure;
using Microservice.UseCases.Extensions;
using Assembly = System.Reflection.Assembly;

namespace Microservice.ArchTests;

public abstract class BaseTest
{
    protected static readonly Assembly DomainAssembly = typeof(Entity).Assembly;
    protected static readonly Assembly UseCasesAssembly = typeof(GenericTypeExtensions).Assembly;
    protected static readonly Assembly InfrastructureAssembly = typeof(AppDbContext).Assembly;
    protected static readonly Assembly ApiAssembly = typeof(Program).Assembly;
}

public abstract class ArchUnitBaseTest : BaseTest
{
    protected static readonly Architecture Architecture = new ArchLoader()
        .LoadAssemblies(
            DomainAssembly,
            UseCasesAssembly,
            InfrastructureAssembly,
            ApiAssembly)
        .Build();

    protected static readonly IObjectProvider<IType> DomainLayer =
        ArchRuleDefinition
            .Types()
            .That()
            .ResideInAssembly(DomainAssembly)
            .As("Domain layer");


    protected static readonly IObjectProvider<IType> InfrastructureLayer =
        ArchRuleDefinition
            .Types()
            .That()
            .ResideInAssembly(InfrastructureAssembly)
            .As("Infrastructure layer");

    protected static readonly IObjectProvider<IType> UseCasesLayer =
        ArchRuleDefinition
            .Types()
            .That()
            .ResideInAssembly(UseCasesAssembly)
            .As("Use-cases layer");

    protected static readonly IObjectProvider<IType> ApiLayer =
        ArchRuleDefinition
            .Types()
            .That()
            .ResideInAssembly(ApiAssembly)
            .As("Api layer");
}