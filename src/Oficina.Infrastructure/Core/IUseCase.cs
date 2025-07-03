using Oficina.Domain.SeedWork;
using System.Threading.Tasks; // Added for Task
using System.Threading; // Added for CancellationToken

namespace Oficina.Infrastructure.Core;

// public interface IUseCase<in TInputPort, TOutputPort>
// {
//     ValueTask<TOutputPort> Execute(TInputPort input, CancellationToken ct = default);
// }

// Using the definition provided by the user
public interface IUseCase<in TInputPort, TOutputPort>
{
    Task<Result<TOutputPort>> Execute(
        TInputPort input,
        CancellationToken ct = default);
}

